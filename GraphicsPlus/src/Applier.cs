using System;
using System.Collections.Generic;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace GraphicsPlus
{
    // Turns the settings into changes in the running game. Every public entry point is exception-safe: a failure is
    // logged once and never reaches the game, and "Vanilla" values restore what the game had.
    internal static class Applier
    {
        private const int MaxSide = 8192;
        private const long BytesPerPixelBudget = 400; // rough HDRP cost per rendered pixel incl. history buffers

        // The size the game (or another mod, e.g. ButteryFixes) chose for the gameplay image; our multiplier is applied to it.
        private static int baseW, baseH;

        private static readonly HashSet<string> failed = new HashSet<string>();
        private static readonly HashSet<string> loggedTypes = new HashSet<string>();
        private static bool loggedVanillaResolution, loggedClamp;

        private static FilterMode? vanillaFilter;
        private static AnisotropicFiltering? vanillaAniso;
        private static float? vanillaLodBias, lastLodBiasWritten;
        private static int? vanillaMipLimit;
        private static int? mipEntryVanilla;

        private struct AaState { public HDAdditionalCameraData.AntialiasingMode Mode; public HDAdditionalCameraData.SMAAQualityLevel Smaa; public HDAdditionalCameraData.TAAQualityLevel Taa; public float Sharpen; }
        private struct ShadowDistState { public float Value; public bool Override; }
        private struct ToggleState { public bool Active; public bool Value; public bool Override; }
        private struct AoState { public bool Active; public float Intensity; public bool IntensityOverride; }
        private struct LightState { public bool UseOverride; public int Level; }

        // HDRP's VolumeComponent overrides GetHashCode/Equals using its *current parameter values*, so it must never be a
        // normal dictionary key: after we change a value the entry would be "lost" and the changed value remembered as vanilla.
        private sealed class ReferenceComparer<T> : IEqualityComparer<T> where T : class
        {
            public static readonly ReferenceComparer<T> Instance = new ReferenceComparer<T>();
            public bool Equals(T a, T b) { return ReferenceEquals(a, b); }
            public int GetHashCode(T x) { return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(x); }
        }

        private static readonly Dictionary<Camera, AaState> aaVanilla = new Dictionary<Camera, AaState>();
        private static readonly Dictionary<VolumeComponent, bool> activeVanilla = new Dictionary<VolumeComponent, bool>(ReferenceComparer<VolumeComponent>.Instance);
        private static readonly Dictionary<HDShadowSettings, ShadowDistState> shadowDistVanilla = new Dictionary<HDShadowSettings, ShadowDistState>(ReferenceComparer<HDShadowSettings>.Instance);
        private static readonly Dictionary<ContactShadows, ToggleState> contactVanilla = new Dictionary<ContactShadows, ToggleState>(ReferenceComparer<ContactShadows>.Instance);
        private static readonly Dictionary<ScreenSpaceAmbientOcclusion, AoState> aoVanilla = new Dictionary<ScreenSpaceAmbientOcclusion, AoState>(ReferenceComparer<ScreenSpaceAmbientOcclusion>.Instance);
        private static readonly Dictionary<CustomPassVolume, bool> passVanilla = new Dictionary<CustomPassVolume, bool>();
        private static readonly Dictionary<HDAdditionalLightData, LightState> lightVanilla = new Dictionary<HDAdditionalLightData, LightState>();

        public static void Guard(string what, Action action)
        {
            try { action(); }
            catch (Exception e) { if (failed.Add(what)) Plugin.Log.LogError("Applying '" + what + "' failed (logged once): " + e); }
        }

        public static void ApplyAll()
        {
            Guard("resolution", ApplyResolution);
            Guard("DLSS", ApplyDlss);
            Guard("anti-aliasing", ApplyAntiAliasing);
            Guard("volumes", ApplyVolumes);
            Guard("shadow resolution", ApplyShadowResolution);
            Guard("textures", ApplyTextureQuality);
        }

        // Cheap periodic check: re-assert only what the user actually changed, because scenes create new volumes/lights/cameras.
        public static void Reassert()
        {
            Guard("resolution base", EnsureBase);
            if (Settings.Dlss.Value != DlssChoice.Off || dlssOn) Guard("DLSS", ApplyDlss);
            if (Settings.AaMode.Value != AntiAliasingChoice.Vanilla) Guard("anti-aliasing", ApplyAntiAliasing);
            if (VolumesTouched()) Guard("volumes", ApplyVolumes);
            if (Settings.ShadowQuality.Value != ShadowQualityChoice.Vanilla) Guard("shadow resolution", ApplyShadowResolution);
        }

        private static bool VolumesTouched()
        {
            return !Settings.Vignette.Value || !Settings.Bloom.Value || !Settings.LensDistortion.Value || !Settings.FilmGrain.Value
                || !Settings.ChromaticAberration.Value || !Settings.DepthOfField.Value || !Settings.CustomPass.Value
                || Settings.ShadowDistance.Value != 1f || Settings.ContactShadows.Value != ToggleChoice.Vanilla || Settings.AmbientOcclusion.Value != ToggleChoice.Vanilla;
        }

        // ---- resolution ----------------------------------------------------------------------------------------

        // Called right after the game's SetPixelResolution (and after other mods' prefixes), when the render target
        // has exactly the size the game wants. Our own multiplier is applied on top of this size.
        public static void CaptureBase()
        {
            var settings = IngamePlayerSettings.Instance;
            var rt = settings != null ? settings.playerGameplayScreenTex : null;
            if (rt == null) return;
            baseW = rt.width; baseH = rt.height;
            if (!loggedVanillaResolution)
            {
                loggedVanillaResolution = true;
                Plugin.Log.LogInfo("Game's gameplay render target: " + baseW + "x" + baseH + ", filter " + rt.filterMode + ".");
            }
        }

        // Safety net if SetPixelResolution was never seen (hook failed): adopt the current size once.
        public static void EnsureBase()
        {
            if (baseW > 0 && baseH > 0) return;
            var settings = IngamePlayerSettings.Instance;
            if (settings == null || settings.playerGameplayScreenTex == null) return;
            Plugin.Log.LogWarning("SetPixelResolution was not observed; using the current render target size as the base.");
            CaptureBase();
        }

        public static void ApplyResolution()
        {
            var settings = IngamePlayerSettings.Instance;
            if (settings == null) return;
            var rt = settings.playerGameplayScreenTex;
            if (rt == null) return;

            if (!vanillaFilter.HasValue) vanillaFilter = rt.filterMode;
            if (baseW <= 0 || baseH <= 0) return; // the game has not chosen its size yet (SetPixelResolution not seen): touch nothing

            // Never ask for more pixels than the video memory can reasonably hold.
            var scale = Settings.ResolutionScale.Value;
            var vramMb = SystemInfo.graphicsMemorySize;
            if (vramMb > 0)
            {
                var maxPixels = (double)vramMb * 1024.0 * 1024.0 / BytesPerPixelBudget;
                var maxScale = (float)Math.Sqrt(maxPixels / (baseW * baseH));
                if (scale > maxScale)
                {
                    if (!loggedClamp) { loggedClamp = true; Plugin.Log.LogWarning("Resolution multiplier " + scale.ToString("0.##") + " is too high for " + vramMb + " MB of video memory; limited to " + maxScale.ToString("0.##") + "."); }
                    scale = Mathf.Max(1f, maxScale);
                }
            }

            var width = Mathf.Clamp(Mathf.RoundToInt(baseW * scale), 64, MaxSide);
            var height = Mathf.Clamp(Mathf.RoundToInt(baseH * scale), 64, MaxSide);
            if (rt.width != width || rt.height != height)
            {
                rt.Release();
                rt.width = width;
                rt.height = height;
                Plugin.Log.LogInfo("Gameplay render target set to " + width + "x" + height + " (x" + scale.ToString("0.##") + " of the game's " + baseW + "x" + baseH + ").");
            }

            switch (Settings.UpscaleFilter.Value)
            {
                case TextureFilterChoice.Vanilla: rt.filterMode = vanillaFilter.Value; break;
                case TextureFilterChoice.Point: rt.filterMode = FilterMode.Point; break;
                case TextureFilterChoice.Bilinear: rt.filterMode = FilterMode.Bilinear; break;
                case TextureFilterChoice.Trilinear: rt.filterMode = FilterMode.Trilinear; break;
            }
        }

        // ---- DLSS ----------------------------------------------------------------------------------------------
        //
        // HDRP already contains the whole DLSS pipeline and the game ships NVIDIA's DLL, but the game's render pipeline
        // asset has it switched off. It is switched on per frame by three things: the asset's dynamic-resolution
        // settings (enabled + DLSS), and on the gameplay camera "allow dynamic resolution" + "allow DLSS". We set those,
        // and put every one of them back when the option is Off, so an unused option changes nothing.

        private struct DlssCameraState { public bool AllowDrs, AllowDlss, CustomQuality, CustomAttributes, Optimal; public uint Quality; }
        private struct DrsAssetState { public bool Enabled, Dlss, Optimal; }

        private static readonly Dictionary<Camera, DlssCameraState> dlssVanilla = new Dictionary<Camera, DlssCameraState>();
        private static readonly HashSet<string> dlssReported = new HashSet<string>();
        private static readonly Dictionary<string, float> dlssFirstSeen = new Dictionary<string, float>();
        private static DrsAssetState? drsVanilla;
        private static System.Reflection.FieldInfo assetSettingsField;
        private static bool dlssOn, loggedDlssUnavailable;

        // HDRP sets this when its DLSS pass could be created: NVIDIA RTX card, new enough driver, plugin DLLs present.
        public static bool DlssAvailable
        {
            get { try { return HDDynamicResolutionPlatformCapabilities.DLSSDetected; } catch (Exception) { return false; } }
        }

        private static uint DlssQualityLevel(DlssChoice choice)
        {
            switch (choice)
            {
                case DlssChoice.UltraPerformance: return 3;   // NVIDIA's DLSSQuality values: 0 = max performance, 1 = balanced, 2 = max quality, 3 = ultra performance
                case DlssChoice.Performance: return 0;
                case DlssChoice.Balanced: return 1;
                default: return 2;
            }
        }

        // The asset's public setter also resets the render pipeline (a visible hitch and lost history); writing the field does not.
        private static bool WriteDrs(HDRenderPipelineAsset asset, DrsAssetState state)
        {
            if (assetSettingsField == null)
                assetSettingsField = typeof(HDRenderPipelineAsset).GetField("m_RenderPipelineSettings", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (assetSettingsField == null) throw new MissingFieldException("HDRenderPipelineAsset.m_RenderPipelineSettings");
            var settings = (RenderPipelineSettings)assetSettingsField.GetValue(asset);
            var drs = settings.dynamicResolutionSettings;
            if (drs.enabled == state.Enabled && drs.enableDLSS == state.Dlss && drs.DLSSUseOptimalSettings == state.Optimal) return false;
            drs.enabled = state.Enabled; drs.enableDLSS = state.Dlss; drs.DLSSUseOptimalSettings = state.Optimal;
            settings.dynamicResolutionSettings = drs;
            assetSettingsField.SetValue(asset, settings);
            return true;
        }

        public static void ApplyDlss()
        {
            var choice = Settings.Dlss.Value;
            var on = choice != DlssChoice.Off;
            if (!on && !dlssOn) return;                     // never touched: nothing to do, nothing to undo

            if (on && !DlssAvailable)
            {
                if (!loggedDlssUnavailable) { loggedDlssUnavailable = true; Plugin.Log.LogWarning("DLSS was chosen but HDRP reports it unavailable on this PC (it needs an NVIDIA RTX card and a current driver). The option is ignored."); }
                return;
            }

            var asset = GraphicsSettings.currentRenderPipeline as HDRenderPipelineAsset;
            if (asset == null) return;

            if (!drsVanilla.HasValue)
            {
                var s = asset.currentPlatformRenderPipelineSettings.dynamicResolutionSettings;
                drsVanilla = new DrsAssetState { Enabled = s.enabled, Dlss = s.enableDLSS, Optimal = s.DLSSUseOptimalSettings };
                Plugin.Log.LogInfo("Game's dynamic resolution settings: enabled=" + s.enabled + ", DLSS=" + s.enableDLSS + ", type=" + s.dynResType + ", filter=" + s.upsampleFilter + ".");
            }

            var wantAsset = on ? new DrsAssetState { Enabled = true, Dlss = true, Optimal = true } : drsVanilla.Value;
            if (WriteDrs(asset, wantAsset)) Plugin.Log.LogInfo(on ? "DLSS switched on in the render pipeline settings." : "DLSS switched off; the render pipeline's own settings were restored.");

            foreach (var cam in GameplayCameras())
            {
                var hd = cam.GetComponent<HDAdditionalCameraData>();
                if (hd == null) continue;
                ApplyDlssToCamera(cam, hd, choice);
            }
            Prune(dlssVanilla);
            dlssOn = on;
        }

        private static void ApplyDlssToCamera(Camera cam, HDAdditionalCameraData hd, DlssChoice choice)
        {
            if (!dlssVanilla.ContainsKey(cam))
                dlssVanilla[cam] = new DlssCameraState { AllowDrs = hd.allowDynamicResolution, AllowDlss = hd.allowDeepLearningSuperSampling, CustomQuality = hd.deepLearningSuperSamplingUseCustomQualitySettings,
                    CustomAttributes = hd.deepLearningSuperSamplingUseCustomAttributes, Optimal = hd.deepLearningSuperSamplingUseOptimalSettings, Quality = hd.deepLearningSuperSamplingQuality };

            if (choice == DlssChoice.Off)
            {
                var v = dlssVanilla[cam];
                hd.allowDynamicResolution = v.AllowDrs; hd.allowDeepLearningSuperSampling = v.AllowDlss; hd.deepLearningSuperSamplingUseCustomQualitySettings = v.CustomQuality;
                hd.deepLearningSuperSamplingUseCustomAttributes = v.CustomAttributes; hd.deepLearningSuperSamplingUseOptimalSettings = v.Optimal; hd.deepLearningSuperSamplingQuality = v.Quality;
                return;
            }

            hd.allowDynamicResolution = true;
            hd.allowDeepLearningSuperSampling = true;
            hd.deepLearningSuperSamplingUseCustomQualitySettings = true;
            hd.deepLearningSuperSamplingQuality = DlssQualityLevel(choice);
            hd.deepLearningSuperSamplingUseCustomAttributes = true;
            hd.deepLearningSuperSamplingUseOptimalSettings = true;
            ReportDlss(cam, hd, choice);
        }

        // One line per camera and setting, so a log shows whether HDRP really took DLSS up (render size < output size).
        private static void ReportDlss(Camera cam, HDAdditionalCameraData hd, DlssChoice choice)
        {
            var key = cam.GetInstanceID() + ":" + choice;
            if (dlssReported.Contains(key)) return;
            float first;
            if (!dlssFirstSeen.TryGetValue(key, out first)) { dlssFirstSeen[key] = Time.realtimeSinceStartup; return; }
            if (Time.realtimeSinceStartup - first < 3f) return;      // HDRP needs a few frames before the numbers mean anything
            dlssReported.Add(key);
            string size;
            try
            {
                var hdCamera = HDCamera.GetOrCreate(cam);
                var outW = cam.targetTexture != null ? cam.targetTexture.width : cam.pixelWidth;
                var outH = cam.targetTexture != null ? cam.targetTexture.height : cam.pixelHeight;
                size = "renders " + hdCamera.actualWidth + "x" + hdCamera.actualHeight + " for an output of " + outW + "x" + outH;
            }
            catch (Exception) { size = "render size not available yet"; }
            string flag;
            try { flag = Convert.ToString(typeof(HDAdditionalCameraData).GetField("cameraCanRenderDLSS", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public).GetValue(hd)); }
            catch (Exception) { flag = "?"; }
            Plugin.Log.LogInfo("DLSS " + choice + " requested on camera '" + cam.name + "' (HDRP flag cameraCanRenderDLSS=" + flag + "); " + size + ".");
        }

        // ---- anti-aliasing -------------------------------------------------------------------------------------

        private static IEnumerable<Camera> GameplayCameras()
        {
            var rt = IngamePlayerSettings.Instance != null ? IngamePlayerSettings.Instance.playerGameplayScreenTex : null;
            var seen = new HashSet<Camera>();
            var local = StartOfRound.Instance != null && StartOfRound.Instance.localPlayerController != null ? StartOfRound.Instance.localPlayerController.gameplayCamera : null;
            if (local != null && seen.Add(local)) yield return local;
            if (rt == null) yield break;
            foreach (var cam in Camera.allCameras)
                if (cam != null && cam.targetTexture == rt && seen.Add(cam)) yield return cam;
        }

        public static void ApplyAntiAliasing()
        {
            foreach (var cam in GameplayCameras())
            {
                var hd = cam.GetComponent<HDAdditionalCameraData>();
                if (hd == null) continue;

                if (!aaVanilla.ContainsKey(cam))
                {
                    aaVanilla[cam] = new AaState { Mode = hd.antialiasing, Smaa = hd.SMAAQuality, Taa = hd.TAAQuality, Sharpen = hd.taaSharpenStrength };
                    Plugin.Log.LogInfo("Vanilla anti-aliasing on camera '" + cam.name + "': " + hd.antialiasing + " (SMAA " + hd.SMAAQuality + ", TAA " + hd.TAAQuality + ").");
                }

                if (Settings.AaMode.Value == AntiAliasingChoice.Vanilla)
                {
                    var v = aaVanilla[cam];
                    hd.antialiasing = v.Mode; hd.SMAAQuality = v.Smaa; hd.TAAQuality = v.Taa; hd.taaSharpenStrength = v.Sharpen;
                    continue;
                }

                switch (Settings.AaMode.Value)
                {
                    case AntiAliasingChoice.Off: hd.antialiasing = HDAdditionalCameraData.AntialiasingMode.None; break;
                    case AntiAliasingChoice.FXAA: hd.antialiasing = HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing; break;
                    case AntiAliasingChoice.SMAA: hd.antialiasing = HDAdditionalCameraData.AntialiasingMode.SubpixelMorphologicalAntiAliasing; break;
                    case AntiAliasingChoice.TAA: hd.antialiasing = HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing; break;
                }
                hd.SMAAQuality = (HDAdditionalCameraData.SMAAQualityLevel)(int)Settings.AaQuality.Value;
                hd.TAAQuality = (HDAdditionalCameraData.TAAQualityLevel)(int)Settings.AaQuality.Value;
                hd.taaSharpenStrength = Settings.TaaSharpness.Value;
            }
            Prune(aaVanilla);
        }

        private static void Prune<TKey, TValue>(Dictionary<TKey, TValue> map) where TKey : UnityEngine.Object
        {
            List<TKey> dead = null;
            foreach (var key in map.Keys) if (key == null) (dead ?? (dead = new List<TKey>())).Add(key);
            if (dead != null) foreach (var key in dead) map.Remove(key);
        }

        // ---- volume overrides (post-processing, shadows distance, contact shadows, ambient occlusion) ----------

        public static void ApplyVolumes()
        {
            var wantedActive = new Dictionary<string, bool>
            {
                { "FilmGrain", Settings.FilmGrain.Value }, { "ChromaticAberration", Settings.ChromaticAberration.Value }, { "Vignette", Settings.Vignette.Value },
                { "Bloom", Settings.Bloom.Value }, { "DepthOfField", Settings.DepthOfField.Value }, { "LensDistortion", Settings.LensDistortion.Value },
            };

            foreach (var pass in UnityEngine.Object.FindObjectsOfType<CustomPassVolume>())
            {
                if (!passVanilla.ContainsKey(pass))
                {
                    passVanilla[pass] = pass.enabled;
                    Plugin.Log.LogInfo("Custom pass volume seen: '" + pass.name + "' (enabled=" + pass.enabled + ", passes=" + pass.customPasses.Count + ").");
                }
                var want = Settings.CustomPass.Value && passVanilla[pass];
                if (pass.enabled != want) { pass.enabled = want; Plugin.Log.LogInfo("Custom pass volume '" + pass.name + "' -> " + (want ? "on" : "off") + "."); }
            }
            Prune(passVanilla);

            foreach (var volume in UnityEngine.Object.FindObjectsOfType<Volume>())
            {
                var profile = volume.sharedProfile;
                if (profile == null) continue;
                foreach (var component in profile.components)
                {
                    if (component == null) continue;
                    var typeName = component.GetType().Name;
                    if (loggedTypes.Add(typeName))
                        Plugin.Log.LogInfo("Volume component seen: " + typeName + " on '" + volume.name + "' (active=" + component.active + ").");

                    bool on;
                    if (wantedActive.TryGetValue(typeName, out on))
                    {
                        if (!activeVanilla.ContainsKey(component)) activeVanilla[component] = component.active;
                        var target = on && activeVanilla[component];
                        if (component.active != target)
                        {
                            component.active = target;
                            Plugin.Log.LogInfo("Post-processing: " + typeName + " on '" + volume.name + "' -> " + (target ? "on" : "off") + " (vanilla " + (activeVanilla[component] ? "on" : "off") + ").");
                        }
                        continue;
                    }

                    var shadows = component as HDShadowSettings;
                    if (shadows != null) { ApplyShadowDistance(shadows); continue; }
                    var contact = component as ContactShadows;
                    if (contact != null) { ApplyContactShadows(contact); continue; }
                    var ao = component as ScreenSpaceAmbientOcclusion;
                    if (ao != null) ApplyAmbientOcclusion(ao);
                }
            }
        }

        private static void ApplyShadowDistance(HDShadowSettings s)
        {
            if (!shadowDistVanilla.ContainsKey(s))
            {
                shadowDistVanilla[s] = new ShadowDistState { Value = s.maxShadowDistance.value, Override = s.maxShadowDistance.overrideState };
                Plugin.Log.LogInfo("Vanilla shadow distance: " + s.maxShadowDistance.value + " (override " + s.maxShadowDistance.overrideState + ", active " + s.active + ").");
            }
            var v = shadowDistVanilla[s];
            var mult = Settings.ShadowDistance.Value;
            if (Math.Abs(mult - 1f) < 0.001f) { s.maxShadowDistance.value = v.Value; s.maxShadowDistance.overrideState = v.Override; }
            else s.maxShadowDistance.Override(Mathf.Clamp(v.Value * mult, 1f, 2000f));
        }

        private static void ApplyContactShadows(ContactShadows c)
        {
            if (!contactVanilla.ContainsKey(c))
                contactVanilla[c] = new ToggleState { Active = c.active, Value = c.enable.value, Override = c.enable.overrideState };
            var v = contactVanilla[c];
            switch (Settings.ContactShadows.Value)
            {
                case ToggleChoice.Vanilla: c.active = v.Active; c.enable.value = v.Value; c.enable.overrideState = v.Override; break;
                case ToggleChoice.Off: c.enable.Override(false); break;
                case ToggleChoice.On: c.active = true; c.enable.Override(true); break;
            }
        }

        private static void ApplyAmbientOcclusion(ScreenSpaceAmbientOcclusion ao)
        {
            if (!aoVanilla.ContainsKey(ao))
            {
                aoVanilla[ao] = new AoState { Active = ao.active, Intensity = ao.intensity.value, IntensityOverride = ao.intensity.overrideState };
                Plugin.Log.LogInfo("Vanilla ambient occlusion: active " + ao.active + ", intensity " + ao.intensity.value + ".");
            }
            var v = aoVanilla[ao];
            switch (Settings.AmbientOcclusion.Value)
            {
                case ToggleChoice.Vanilla: ao.active = v.Active; ao.intensity.value = v.Intensity; ao.intensity.overrideState = v.IntensityOverride; break;
                case ToggleChoice.Off: ao.active = false; break;
                case ToggleChoice.On: ao.active = true; ao.intensity.Override(Mathf.Max(v.Intensity, 1f)); break;
            }
        }

        // ---- shadow resolution (per light) ---------------------------------------------------------------------

        public static void ApplyShadowResolution()
        {
            var choice = Settings.ShadowQuality.Value;
            if (choice == ShadowQualityChoice.Vanilla && lightVanilla.Count == 0) return;

            foreach (var light in UnityEngine.Object.FindObjectsOfType<HDAdditionalLightData>())
            {
                var unityLight = light.GetComponent<Light>();
                if (unityLight == null || unityLight.shadows == LightShadows.None) continue;

                if (!lightVanilla.ContainsKey(light))
                    lightVanilla[light] = new LightState { UseOverride = light.shadowResolution.useOverride, Level = light.shadowResolution.level };

                if (choice == ShadowQualityChoice.Vanilla)
                {
                    var v = lightVanilla[light];
                    light.SetShadowResolutionOverride(v.UseOverride);
                    light.SetShadowResolutionLevel(v.Level);
                }
                else
                {
                    light.SetShadowResolutionOverride(false);
                    light.SetShadowResolutionLevel((int)choice - 1); // Low=0 .. Ultra=3
                }
            }
            if (choice == ShadowQualityChoice.Vanilla) lightVanilla.Clear(); else Prune(lightVanilla);
        }

        // ---- textures / detail ---------------------------------------------------------------------------------

        public static void ApplyTextureQuality()
        {
            if (!vanillaAniso.HasValue) vanillaAniso = QualitySettings.anisotropicFiltering;
            switch (Settings.Anisotropic.Value)
            {
                case AnisotropicChoice.Vanilla: QualitySettings.anisotropicFiltering = vanillaAniso.Value; break;
                case AnisotropicChoice.Off: QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable; break;
                case AnisotropicChoice.On: QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable; break;
                case AnisotropicChoice.Forced: QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable; break;
            }

            // Another mod may change lodBias too: only treat the current value as "vanilla" if it is not what we wrote.
            if (!vanillaLodBias.HasValue || (lastLodBiasWritten.HasValue && !Mathf.Approximately(QualitySettings.lodBias, lastLodBiasWritten.Value)))
                vanillaLodBias = QualitySettings.lodBias;
            var target = vanillaLodBias.Value * Settings.DetailDistance.Value;
            if (!Mathf.Approximately(QualitySettings.lodBias, target)) QualitySettings.lodBias = target;
            lastLodBiasWritten = QualitySettings.lodBias;

            ApplyMipLimit();
        }

        // MemoryJanitor re-applies its own 'MipmapLimit' on every scene load, so when it is installed we edit its setting.
        private static ConfigEntryBase FindMemoryJanitorMipEntry()
        {
            PluginInfoLike info;
            if (!TryGetPlugin("lcmemfix.MemoryJanitor", out info) || info.Config == null) return null;
            foreach (var key in info.Config.Keys)
                if (key.Section == "Textures" && key.Key == "MipmapLimit") return info.Config[key];
            return null;
        }

        private struct PluginInfoLike { public ConfigFile Config; }

        private static bool TryGetPlugin(string guid, out PluginInfoLike result)
        {
            result = default(PluginInfoLike);
            BepInEx.PluginInfo info;
            if (!Chainloader.PluginInfos.TryGetValue(guid, out info) || info == null || info.Instance == null) return false;
            result.Config = info.Instance.Config;
            return true;
        }

        private static void ApplyMipLimit()
        {
            if (!vanillaMipLimit.HasValue) vanillaMipLimit = QualitySettings.globalTextureMipmapLimit;
            var entry = FindMemoryJanitorMipEntry();
            if (entry != null && !mipEntryVanilla.HasValue) mipEntryVanilla = Convert.ToInt32(entry.BoxedValue);

            int? wanted = null;
            switch (Settings.TextureQuality.Value)
            {
                case TextureQualityChoice.Full: wanted = 0; break;
                case TextureQualityChoice.Half: wanted = 1; break;
                case TextureQualityChoice.Quarter: wanted = 2; break;
            }

            if (wanted.HasValue)
            {
                if (entry != null && Convert.ToInt32(entry.BoxedValue) != wanted.Value) entry.BoxedValue = wanted.Value;
                if (QualitySettings.globalTextureMipmapLimit != wanted.Value) QualitySettings.globalTextureMipmapLimit = wanted.Value;
            }
            else
            {
                // Vanilla: give the choice back to the game / MemoryJanitor.
                if (entry != null && mipEntryVanilla.HasValue && Convert.ToInt32(entry.BoxedValue) != mipEntryVanilla.Value) entry.BoxedValue = mipEntryVanilla.Value;
                var back = entry != null && mipEntryVanilla.HasValue ? mipEntryVanilla.Value : vanillaMipLimit.Value;
                if (QualitySettings.globalTextureMipmapLimit != back && lastMipWritten) QualitySettings.globalTextureMipmapLimit = back;
            }
            lastMipWritten = wanted.HasValue;
        }

        private static bool lastMipWritten;
    }
}

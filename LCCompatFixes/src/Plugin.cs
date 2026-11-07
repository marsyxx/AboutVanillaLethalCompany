using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace LCCompatFixes
{
    // Fixes for mods that broke after a game update, applied from the outside so the mods themselves stay untouched.
    // Every fix can be switched off in BepInEx/config/lc.compatfixes.cfg.
    [BepInPlugin(Guid, "LCCompatFixes", "1.0.0")]
    [BepInDependency("me.loaforc.soundapi", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "lc.compatfixes";

        private static ManualLogSource Log;
        private static Plugin instance;
        private static GameObject stubHolder;

        private void Awake()
        {
            Log = Logger;
            instance = this;
            var stubDarkenScreen = Config.Bind("Fixes", "StubLoadingDarkenScreen", true,
                "Gives HUDManager.loadingDarkenScreen (removed by a game update, still used by LethalCasino) a harmless stub Image. Needs the companion patcher.");
            var fixPackNames = Config.Bind("Fixes", "SoundApiPackNameDots", true,
                "loaforcsSoundAPI 2.x rejects sound packs whose name contains '.', which silently disables the RTLC Russian sound packs. Replaces '.' with '_' in pack names.");

            var fixStreamBundles = Config.Bind("Fixes", "StreamBundleResult", true,
                "EladsHUD+ loads its HUD from an embedded asset bundle with AssetBundle.LoadFromStream. With BepInEx_Faster_Load_AssetBundles_Patcher and XUnity both hooking that call, Unity loads the bundle but the caller is handed null, and the mod crashes in Awake. When that happens, hand the caller the bundle that was just loaded.");

            var sliceRtlc = Config.Bind("Fixes", "RtlcSceneLoadSlicing", true,
                "The RTLC Russian translation goes through every texture in memory on each level load, all in one frame: a 1-2 second freeze when the ship lands (a pause of its own on a modded game). When on, the same pass is done a few milliseconds per frame instead, so the game keeps running smoothly; the Russian textures appear a moment later.");

            var cacheTypeScans = Config.Bind("Fixes", "CacheTypeScans", true,
                "Mods that look types up by name (AccessTools.TypeByName / AllTypes) make HarmonyX call Assembly.GetTypes on every loaded assembly each time. For the few assemblies that cannot load fully (they reference mods that are not installed) that call throws and takes 1-2 seconds, and it was repeated over a hundred times per session. The result of each scan is remembered, so it runs once per assembly.");

            var rpgOrder = Config.Bind("Fixes", "LethalRpgStrengthBeforeBetterStamina", true,
                "LethalRPG's Strength skill finds the carry-weight reads in PlayerControllerB.Update/LateUpdate and scales them, but BetterStamina's patch (which runs first because of its priority) has already replaced those reads, so LethalRPG logs 'no reads of carryWeight found' and Strength does nothing. This puts LethalRPG's two carry-weight patches ahead of BetterStamina's, so both see the code they expect. Only the order changes; neither mod's code is touched.");

            var harmony = new Harmony(Guid);
            if (rpgOrder.Value) StartCoroutine(RpgOrderLoop());
            if (cacheTypeScans.Value) Safe("type scan cache", () => PatchTypeScans(harmony));
            if (sliceRtlc.Value) Safe("RTLC scene load slicing", () => PatchRtlc(harmony));
            if (stubDarkenScreen.Value) Safe("stub loadingDarkenScreen", () => PatchHud(harmony));
            if (fixPackNames.Value) Safe("SoundAPI pack names", () => PatchSoundApi(harmony));
            if (fixStreamBundles.Value) Safe("stream asset bundle result", () => PatchStreamBundles(harmony));
        }

        // ---- AssetBundle.LoadFromStream returning null although the bundle was loaded -----------------------------

        private static bool loggedStreamFix, loggedStreamMiss;

        private static void PatchStreamBundles(Harmony harmony)
        {
            var target = AccessTools.Method(typeof(AssetBundle), "LoadFromStreamInternal");
            if (target == null) { Log.LogWarning("AssetBundle.LoadFromStreamInternal not found; skipping the stream bundle fix."); return; }
            // first prefix / last postfix: the snapshot is taken before any other mod's hook runs, the check after all of them
            harmony.Patch(target,
                prefix: new HarmonyMethod(typeof(Plugin), nameof(StreamBundlePrefix)) { priority = Priority.First },
                postfix: new HarmonyMethod(typeof(Plugin), nameof(StreamBundlePostfix)) { priority = Priority.Last });
        }

        private static void StreamBundlePrefix(out HashSet<int> __state)
        {
            __state = null;
            try { __state = new HashSet<int>(AssetBundle.GetAllLoadedAssetBundles().Where(b => b != null).Select(b => b.GetInstanceID())); }
            catch (Exception e) { Log.LogError(e); }
        }

        private static void StreamBundlePostfix(ref AssetBundle __result, HashSet<int> __state)
        {
            if (__result != null || __state == null) return;
            try
            {
                var added = AssetBundle.GetAllLoadedAssetBundles().Where(b => b != null && !__state.Contains(b.GetInstanceID())).ToList();
                if (added.Count == 1)
                {
                    __result = added[0];
                    if (!loggedStreamFix) { loggedStreamFix = true; Log.LogInfo("AssetBundle.LoadFromStream returned null although it had loaded bundle '" + added[0].name + "'; returned that bundle to the caller."); }
                }
                else if (!loggedStreamMiss)
                {
                    loggedStreamMiss = true;
                    Log.LogWarning("AssetBundle.LoadFromStream returned null and " + added.Count + " new bundles appeared; left as it is.");
                }
            }
            catch (Exception e) { Log.LogError(e); }
        }

        private static void Safe(string what, Action action)
        {
            try { action(); }
            catch (Exception e) { Log.LogError("Fix '" + what + "' failed to install: " + e); }
        }

        // ---- LethalRPG Strength vs BetterStamina: patch order on PlayerControllerB --------------------------------------

        private static bool loggedRpgOrder;

        // Patches of other mods are registered at their own pace, so the check repeats for a while and is a no-op once the order is right.
        private static IEnumerator RpgOrderLoop()
        {
            for (var i = 0; i < 60; i++)
            {
                yield return new WaitForSeconds(2f);
                try { RpgOrderPass(); }
                catch (Exception e) { Log.LogError("LethalRPG patch order fix failed: " + e); yield break; }
            }
        }

        private static void RpgOrderPass()
        {
            var type = AccessTools.TypeByName("GameNetcodeStuff.PlayerControllerB");
            if (type == null) return;
            foreach (var name in new[] { "Update", "LateUpdate" })
            {
                var method = AccessTools.Method(type, name);
                var info = method != null ? Harmony.GetPatchInfo(method) : null;
                if (info == null) continue;
                var stamina = info.Transpilers.Where(p => p.owner == "BetterStamina").ToList();
                var rpg = info.Transpilers.Where(p => p.owner == "cucumis.lethalrpg" && p.PatchMethod.Name.IndexOf("CarryWeight", StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                if (stamina.Count == 0 || rpg.Count == 0) continue;
                var top = stamina.Max(p => p.priority);
                if (rpg.All(p => p.priority > top)) continue;          // already runs first
                var owner = new Harmony("cucumis.lethalrpg");           // same owner id, so LethalRPG's own unpatching still finds them
                foreach (var p in rpg)
                {
                    owner.Unpatch(method, p.PatchMethod);
                    owner.Patch(method, transpiler: new HarmonyMethod(p.PatchMethod) { priority = Math.Max(Priority.First, top + 1) });
                }
                if (!loggedRpgOrder) { loggedRpgOrder = true; Log.LogInfo("LethalRPG's carry-weight patch on PlayerControllerB." + name + " now runs before BetterStamina's (priority " + Math.Max(Priority.First, top + 1) + " instead of " + rpg[0].priority + ")."); }
            }
        }

        // ---- HarmonyX: Assembly.GetTypes repeated for assemblies that fail to load -------------------------------------

        private static readonly Dictionary<Assembly, Type[]> typeScans = new Dictionary<Assembly, Type[]>();
        private static readonly object typeScanLock = new object();
        private static int typeScanReuses;
        private static bool loggedTypeScanReuse;

        private static void PatchTypeScans(Harmony harmony)
        {
            var target = AccessTools.Method(typeof(AccessTools), "GetTypesFromAssembly", new[] { typeof(Assembly) });
            if (target == null) { Log.LogWarning("AccessTools.GetTypesFromAssembly not found; type scans are not cached."); return; }
            // first prefix / last postfix, so a cached answer is used before any other hook and a fresh one is stored after all of them
            harmony.Patch(target,
                prefix: new HarmonyMethod(typeof(Plugin), nameof(TypeScanPrefix)) { priority = Priority.First },
                postfix: new HarmonyMethod(typeof(Plugin), nameof(TypeScanPostfix)) { priority = Priority.Last });
        }

        private static bool TypeScanPrefix(Assembly assembly, ref Type[] __result)
        {
            if (assembly == null) return true;
            lock (typeScanLock)
            {
                Type[] cached;
                if (!typeScans.TryGetValue(assembly, out cached)) return true;
                __result = cached;
                typeScanReuses++;
            }
            if (!loggedTypeScanReuse && typeScanReuses >= 20) { loggedTypeScanReuse = true; Log.LogInfo("HarmonyX type scans: " + typeScanReuses + " repeated scans answered from the cache so far."); }
            return false;
        }

        private static void TypeScanPostfix(Assembly assembly, Type[] __result)
        {
            if (assembly == null || __result == null || assembly.IsDynamic) return;
            lock (typeScanLock) typeScans[assembly] = __result;
        }

        // ---- RTLC: texture pass on every level load, spread over frames ---------------------------------------------

        private sealed class SliceTarget { public MethodInfo TrySet; public MethodInfo EnabledGetter; public string Name; public bool Running; public bool Reported; public readonly HashSet<int> Done = new HashSet<int>(); }
        private static readonly Dictionary<MethodBase, SliceTarget> sliceTargets = new Dictionary<MethodBase, SliceTarget>();

        // RTLC and RTLC.Mods each hook the end of StartOfRound.SceneManager_OnLoadComplete1 with PatchOnSceneLoad.Postfix, which
        // runs BundleLoader.TrySetTexture on every Texture2D in memory in the same frame. We skip that call and do the same
        // loop in small time slices from a coroutine.
        private static void PatchRtlc(Harmony harmony)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var assemblyName = assembly.GetName().Name;
                if (assemblyName != "RTLC" && assemblyName != "RTLC.Mods") continue;
                Type[] types;
                try { types = assembly.GetTypes(); } catch (ReflectionTypeLoadException e) { types = e.Types.Where(t => t != null).ToArray(); }
                var patchType = types.FirstOrDefault(t => t.Name == "PatchOnSceneLoad");
                var loaderType = types.FirstOrDefault(t => t.Name == "BundleLoader");
                var configType = types.FirstOrDefault(t => t.Name == "Configuration");
                var postfix = patchType != null ? AccessTools.Method(patchType, "Postfix") : null;
                var trySet = loaderType != null ? AccessTools.Method(loaderType, "TrySetTexture") : null;
                if (postfix == null || trySet == null) { Log.LogInfo(assemblyName + ": PatchOnSceneLoad.Postfix / BundleLoader.TrySetTexture not found; leaving it alone."); continue; }
                sliceTargets[postfix] = new SliceTarget { Name = assemblyName, TrySet = trySet, EnabledGetter = configType != null ? AccessTools.PropertyGetter(configType, "IsPatchEnabled") : null };
                harmony.Patch(postfix, prefix: new HarmonyMethod(typeof(Plugin), nameof(RtlcPrefix)) { priority = Priority.First });
                Log.LogInfo(assemblyName + ": texture pass on level load will be spread over frames.");
            }
        }

        private static bool RtlcPrefix(MethodBase __originalMethod)
        {
            SliceTarget target;
            if (!sliceTargets.TryGetValue(__originalMethod, out target)) return true;
            try
            {
                if (!target.Running) instance.StartCoroutine(SliceTextures(target));
                return false;                       // the original (all in one frame) is skipped
            }
            catch (Exception e) { Log.LogError(e); return true; }   // could not start: let the original run
        }

        private static bool TextureStep(SliceTarget target, Texture2D texture, object[] args)
        {
            try { args[0] = texture; args[1] = texture.name; target.TrySet.Invoke(null, args); return true; }
            catch (Exception e) { Log.LogError("RTLC texture step failed, stopping the pass: " + (e.InnerException ?? e).Message); return false; }
        }

        private static IEnumerator SliceTextures(SliceTarget target)
        {
            target.Running = true;
            try
            {
                yield return null;                  // let the frame of the scene load finish first
                if (target.EnabledGetter != null)
                {
                    var entry = target.EnabledGetter.Invoke(null, null) as ConfigEntry<bool>;
                    if (entry != null && !entry.Value) yield break;     // RTLC's own switch: patching off
                }
                var started = Stopwatch.StartNew();
                var all = Resources.FindObjectsOfTypeAll<Texture2D>();
                var args = new object[2];
                var index = 0; var frames = 0; var ok = true;
                var slice = new Stopwatch();
                while (index < all.Length && ok)
                {
                    slice.Restart();
                    while (index < all.Length && slice.Elapsed.TotalMilliseconds < 3.0)
                    {
                        var texture = all[index++];
                        if (texture == null) continue;
                        var id = texture.GetInstanceID();
                        if (target.Done.Contains(id)) continue;     // seen on an earlier level load (RTLC already replaced it or has nothing for it)
                        ok = TextureStep(target, texture, args);
                        if (!ok) break;
                        target.Done.Add(id);
                    }
                    frames++;
                    yield return null;
                }
                if (!target.Reported) { target.Reported = true; Log.LogInfo(target.Name + ": texture pass over " + all.Length + " textures finished in " + frames + " frames (" + started.ElapsedMilliseconds + " ms wall time, about 3 ms per frame)."); }
            }
            finally { target.Running = false; }
        }

        // ---- HUDManager.loadingDarkenScreen ------------------------------------------------------------------

        private static void PatchHud(Harmony harmony)
        {
            if (AccessTools.Field(typeof(HUDManager), "loadingDarkenScreen") == null)
            {
                Log.LogWarning("HUDManager.loadingDarkenScreen is missing (patcher not loaded?); skipping the stub.");
                return;
            }
            harmony.Patch(AccessTools.Method(typeof(HUDManager), "Awake"),
                postfix: new HarmonyMethod(typeof(Plugin), nameof(HudAwakePostfix)));
        }

        private static void HudAwakePostfix(HUDManager __instance)
        {
            try
            {
                var field = AccessTools.Field(typeof(HUDManager), "loadingDarkenScreen");
                if (field == null || field.GetValue(__instance) as Image != null) return; // real one exists (game restored it)

                if (stubHolder == null)
                {
                    stubHolder = new GameObject("LCCompatFixes.DarkenScreenStub") { hideFlags = HideFlags.HideAndDontSave };
                    UnityEngine.Object.DontDestroyOnLoad(stubHolder);
                    stubHolder.AddComponent<Image>().enabled = false;
                }
                field.SetValue(__instance, stubHolder.GetComponent<Image>());
                Log.LogInfo("Assigned stub Image to HUDManager.loadingDarkenScreen.");
            }
            catch (Exception e) { Log.LogError(e); }
        }

        // ---- loaforcsSoundAPI sound pack names ---------------------------------------------------------------

        private static void PatchSoundApi(Harmony harmony)
        {
            var packType = AccessTools.TypeByName("loaforcsSoundAPI.SoundPacks.Data.SoundPack");
            if (packType == null) { Log.LogInfo("loaforcsSoundAPI not installed; nothing to fix."); return; }

            var validate = AccessTools.Method(packType, "Validate");
            if (validate == null) { Log.LogWarning("SoundPack.Validate not found; skipping."); return; }

            harmony.Patch(validate, prefix: new HarmonyMethod(typeof(Plugin), nameof(ValidatePrefix)));
        }

        private static void ValidatePrefix(object __instance)
        {
            try
            {
                var type = __instance.GetType();
                var prop = AccessTools.Property(type, "Name");
                var name = prop?.GetValue(__instance, null) as string;
                if (string.IsNullOrEmpty(name) || name.IndexOf('.') < 0) return;

                var fixedName = name.Replace('.', '_');
                foreach (var f in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (f.FieldType == typeof(string) && (string)f.GetValue(__instance) == name)
                    {
                        f.SetValue(__instance, fixedName);
                        Log.LogInfo("Sound pack name '" + name + "' -> '" + fixedName + "' (SoundAPI rejects '.').");
                        return;
                    }
                }
                Log.LogWarning("Could not locate the backing field of SoundPack.Name for '" + name + "'.");
            }
            catch (Exception e) { Log.LogError(e); }
        }
    }
}

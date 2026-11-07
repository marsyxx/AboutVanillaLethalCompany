using System;
using System.Collections.Generic;
using BepInEx.Configuration;

namespace GraphicsPlus
{
    public enum AntiAliasingChoice { Vanilla, Off, FXAA, SMAA, TAA }
    public enum QualityChoice { Low, Medium, High }
    public enum ShadowQualityChoice { Vanilla, Low, Medium, High, Ultra }
    public enum ToggleChoice { Vanilla, Off, On }
    public enum TextureQualityChoice { Vanilla, Full, Half, Quarter }
    public enum TextureFilterChoice { Vanilla, Point, Bilinear, Trilinear }
    public enum AnisotropicChoice { Vanilla, Off, On, Forced }
    public enum DlssChoice { Off, UltraPerformance, Performance, Balanced, Quality }

    internal static class Settings
    {
        // [1. Resolution]
        public static ConfigEntry<float> ResolutionScale;
        public static ConfigEntry<TextureFilterChoice> UpscaleFilter;
        public static ConfigEntry<DlssChoice> Dlss;
        // [2. Anti-aliasing]
        public static ConfigEntry<AntiAliasingChoice> AaMode;
        public static ConfigEntry<QualityChoice> AaQuality;
        public static ConfigEntry<float> TaaSharpness;
        // [3. Shadows]
        public static ConfigEntry<float> ShadowDistance;
        public static ConfigEntry<ShadowQualityChoice> ShadowQuality;
        public static ConfigEntry<ToggleChoice> ContactShadows;
        // [4. Textures and detail]
        public static ConfigEntry<TextureQualityChoice> TextureQuality;
        public static ConfigEntry<AnisotropicChoice> Anisotropic;
        public static ConfigEntry<float> DetailDistance;
        // [5. Lighting and effects]
        public static ConfigEntry<ToggleChoice> AmbientOcclusion;
        // [6. Interface]
        public static ConfigEntry<bool> LockVanilla;
        private static ConfigFile file;
        public static ConfigEntry<bool> Vignette, Bloom, LensDistortion, FilmGrain, ChromaticAberration, DepthOfField, CustomPass;

        public static readonly List<Option> All = new List<Option>();

        public static void Bind(ConfigFile c)
        {
            ResolutionScale = c.Bind("1. Resolution", "Resolution multiplier", 1f, new ConfigDescription(
                "Multiplies the size of the image the game renders (the vanilla 'Pixel resolution' presets are 860x520 / 620x364 / 400x260 / 186x104). 1.0 = vanilla. GPU cost grows with the square of this value. Automatically limited on cards with little video memory.",
                new AcceptableValueRange<float>(0.5f, 3f)));
            UpscaleFilter = c.Bind("1. Resolution", "Upscale filter", TextureFilterChoice.Vanilla,
                "How the rendered image is stretched to your screen. Point = crisp pixels, Bilinear/Trilinear = smoother.");
            Dlss = c.Bind("1. Resolution", "DLSS (NVIDIA RTX only)", DlssChoice.Off,
                "NVIDIA DLSS: the game is drawn at a lower internal size and upscaled by an AI model, which also smooths edges. Ultra performance draws the fewest pixels, Quality the most. Needs an NVIDIA RTX card; on any other card the option is ignored (and hidden in the in-game page). Best combined with a Resolution multiplier above 1.0.");

            AaMode = c.Bind("2. Anti-aliasing", "Mode", AntiAliasingChoice.Vanilla,
                "Vanilla leaves the game alone. SMAA is sharp, TAA is smoothest but slightly soft (best with a higher resolution multiplier).");
            AaQuality = c.Bind("2. Anti-aliasing", "Quality (SMAA/TAA)", QualityChoice.Medium, "Quality level used by SMAA and TAA.");
            TaaSharpness = c.Bind("2. Anti-aliasing", "TAA sharpness", 0.5f, new ConfigDescription("Sharpening applied by TAA.", new AcceptableValueRange<float>(0f, 1f)));

            ShadowDistance = c.Bind("3. Shadows", "Shadow distance", 1f, new ConfigDescription(
                "Multiplies how far from the camera shadows are drawn. 1.0 = vanilla.", new AcceptableValueRange<float>(0.25f, 3f)));
            ShadowQuality = c.Bind("3. Shadows", "Shadow resolution", ShadowQualityChoice.Vanilla,
                "Sharpness of shadows cast by lights. Higher costs more GPU time and video memory.");
            ContactShadows = c.Bind("3. Shadows", "Contact shadows", ToggleChoice.Vanilla, "Small, sharp shadows where objects touch surfaces.");

            TextureQuality = c.Bind("4. Textures and detail", "Texture quality", TextureQualityChoice.Vanilla,
                "Full = every texture at full size, Half/Quarter use less memory. If MemoryJanitor is installed this changes its 'MipmapLimit' setting instead of fighting it.");
            Anisotropic = c.Bind("4. Textures and detail", "Anisotropic filtering", AnisotropicChoice.Vanilla,
                "Sharper textures at grazing angles (floors, walls). Cheap on modern GPUs.");
            DetailDistance = c.Bind("4. Textures and detail", "Model detail distance", 1f, new ConfigDescription(
                "Multiplies the LOD bias: higher keeps detailed models visible further away. 1.0 = vanilla.", new AcceptableValueRange<float>(0.5f, 4f)));

            AmbientOcclusion = c.Bind("5. Lighting and effects", "Ambient occlusion", ToggleChoice.Vanilla, "Soft darkening in corners and crevices.");
            Vignette = c.Bind("5. Lighting and effects", "Vignette", true, "Untick to remove the darkened screen corners.");
            Bloom = c.Bind("5. Lighting and effects", "Bloom", true, "Untick to remove the glow around bright lights.");
            LensDistortion = c.Bind("5. Lighting and effects", "Lens distortion", true, "Untick to remove the fish-eye warping.");
            FilmGrain = c.Bind("5. Lighting and effects", "Film grain", true, "Untick to remove film grain (used in the game's levels).");
            ChromaticAberration = c.Bind("5. Lighting and effects", "Chromatic aberration", true, "Untick to remove colour fringing (only affects scenes that actually use it).");
            DepthOfField = c.Bind("5. Lighting and effects", "Depth of field", true, "Untick to remove background blur (only affects scenes that actually use it).");
            CustomPass = c.Bind("5. Lighting and effects", "Retro colour pass (experimental)", true,
                "Untick to switch off the game's custom HDRP render passes (the colour-grade / retro look). Tick again to restore.");

            LockVanilla = c.Bind("6. Interface", "Lock vanilla Graphics settings", true,
                "While any Graphics+ option is changed from its default, the game's own Graphics controls (terrain detail, motion blur, pixel resolution, indirect lighting) are greyed out and cannot be changed, so the two never fight. Set every Graphics+ option back to its default to unlock them.");
            file = c;

            All.Clear();
            All.Add(Slider(ResolutionScale, "Resolution", "Resolution multiplier", 0.5f, 3f, 0.05f, "x"));
            All.Add(Choice(UpscaleFilter, "Resolution", "Upscale filter"));
            dlssOption = Choice(Dlss, "Resolution", "DLSS (NVIDIA RTX)");
            dlssOption.Labels = new[] { "Off", "Ultra performance", "Performance", "Balanced", "Quality" };
            All.Add(Choice(AaMode, "Anti-aliasing", "Anti-aliasing"));
            All.Add(Choice(AaQuality, "Anti-aliasing", "Anti-aliasing quality"));
            All.Add(Slider(TaaSharpness, "Anti-aliasing", "TAA sharpness", 0f, 1f, 0.05f));
            All.Add(Slider(ShadowDistance, "Shadows", "Shadow distance", 0.25f, 3f, 0.05f, "x"));
            All.Add(Choice(ShadowQuality, "Shadows", "Shadow resolution"));
            All.Add(Choice(ContactShadows, "Shadows", "Contact shadows"));
            var textureQuality = Choice(TextureQuality, "Textures and detail", "Texture quality");
            // distinct wording so a generic word like "Half" cannot collide with another dictionary entry
            textureQuality.Labels = new[] { "Game default", "Full size", "Half size", "Quarter size" };
            All.Add(textureQuality);
            All.Add(Choice(Anisotropic, "Textures and detail", "Anisotropic filtering"));
            All.Add(Slider(DetailDistance, "Textures and detail", "Model detail distance", 0.5f, 4f, 0.1f, "x"));
            All.Add(Choice(AmbientOcclusion, "Lighting and effects", "Ambient occlusion"));
            All.Add(Toggle(Vignette, "Lighting and effects", "Vignette"));
            All.Add(Toggle(Bloom, "Lighting and effects", "Bloom"));
            All.Add(Toggle(LensDistortion, "Lighting and effects", "Lens distortion"));
            All.Add(Toggle(FilmGrain, "Lighting and effects", "Film grain"));
            All.Add(Toggle(ChromaticAberration, "Lighting and effects", "Chromatic aberration"));
            All.Add(Toggle(DepthOfField, "Lighting and effects", "Depth of field"));
            All.Add(Toggle(CustomPass, "Lighting and effects", "Retro colour pass (experimental)"));
        }

        // True when at least one Graphics+ option differs from its default (the "Interface" section does not count).
        public static bool InUse()
        {
            if (file == null) return false;
            foreach (var key in file.Keys)
            {
                if (key.Section.StartsWith("6.")) continue;
                var entry = file[key];
                if (!Equals(entry.BoxedValue, entry.DefaultValue)) return true;
            }
            return false;
        }

        private static Option dlssOption;

        // The rows for the in-game page. The DLSS row exists only on PCs where HDRP found DLSS usable (checked when the page is built).
        public static IList<Option> Options()
        {
            var list = new List<Option>(All);
            if (dlssOption != null && Applier.DlssAvailable) list.Insert(2, dlssOption);
            return list;
        }

        private static Option Slider(ConfigEntry<float> e, string group, string label, float min, float max, float step, string unit = "")
        {
            return new Option { Group = group, Label = label, Tooltip = e.Description.Description, Kind = OptionKind.Slider, Min = min, Max = max, Step = step, Unit = unit,
                Get = () => e.Value, Set = v => e.Value = (float)Math.Round(v / step) * step };
        }

        private static Option Toggle(ConfigEntry<bool> e, string group, string label)
        {
            return new Option { Group = group, Label = label, Tooltip = e.Description.Description, Kind = OptionKind.Toggle,
                Get = () => e.Value ? 1f : 0f, Set = v => e.Value = v >= 0.5f };
        }

        private static Option Choice<T>(ConfigEntry<T> e, string group, string label) where T : struct
        {
            var names = Enum.GetNames(typeof(T));
            var values = (T[])Enum.GetValues(typeof(T));
            return new Option { Group = group, Label = label, Tooltip = e.Description.Description, Kind = OptionKind.Choice, Choices = names,
                Get = () => Array.IndexOf(values, e.Value), Set = v => e.Value = values[Math.Max(0, Math.Min(values.Length - 1, (int)v))] };
        }
    }
}

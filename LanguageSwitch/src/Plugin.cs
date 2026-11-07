using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LanguageSwitch
{
    public enum Language { English, Russian, German }

    // Lets the player pick the game language (English / Russian / German) in the Settings screen. The text itself is
    // translated by XUnity.AutoTranslator (the same engine the RTLC Russian translation uses); this mod only drives it.
    [BepInPlugin(Guid, "LanguageSwitch", "1.0.1")]
    [BepInDependency(XuaGuid, BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "lc.languageswitch";
        private const string XuaGuid = "gravydevsupreme.xunity.autotranslator";

        internal static ManualLogSource Log;
        internal static ConfigEntry<string> Chosen;   // "" until the player picks something: then we leave XUnity as configured
        internal static Plugin Instance;

        private void Awake()
        {
            Instance = this;
            Log = Logger;
            Chosen = Config.Bind("General", "Language", "",
                "English, Russian or German. Empty = keep whatever XUnity.AutoTranslator is configured for. Change it in the game's Settings screen.");
            Translator.Capture();
            SettingsMenu.InstallInline(this, LanguageOption());
            SceneManager.sceneLoaded += (s, m) => { if (s.name == "MainMenu") StartCoroutine(MenuSpacing.ApplySoon()); };
            StartCoroutine(ApplyWhenReady());
        }

        private static Option LanguageOption()
        {
            return new Option
            {
                Group = "General", Label = "Language", Kind = OptionKind.Choice, Choices = Enum.GetNames(typeof(Language)),
                Get = () => (float)(int)Current(),
                Set = v => Translator.Apply((Language)(int)v),
            };
        }

        // XUnity finishes initialising a little after plugins are loaded.
        private IEnumerator ApplyWhenReady()
        {
            for (var i = 0; i < 40 && !Translator.Ready; i++) yield return new WaitForSeconds(0.5f);
            if (!Translator.Ready) { Log.LogWarning("XUnity.AutoTranslator was not found or never became ready; language switching is disabled."); yield break; }

            Language parsed;
            if (!string.IsNullOrEmpty(Chosen.Value) && TryParse(Chosen.Value, out parsed)) Translator.Apply(parsed);
            else Log.LogInfo("No language chosen yet; XUnity target language is '" + Translator.OriginalLanguage + "' and is left untouched.");
        }

        internal static bool TryParse(string value, out Language language) { return Enum.TryParse(value, true, out language); }

        internal static Language Current()
        {
            Language parsed;
            if (!string.IsNullOrEmpty(Chosen.Value) && TryParse(Chosen.Value, out parsed)) return parsed;
            switch ((Translator.OriginalLanguage ?? "").ToLowerInvariant())
            {
                case "ru": return Language.Russian;
                case "de": return Language.German;
                default: return Language.English;
            }
        }
    }

    // All access to XUnity goes through reflection so the mod loads (and does nothing) when XUnity is absent or changes.
    internal static class Translator
    {
        private static Type pluginType, settingsType;
        private static object xuaPlugin;
        private static string originalLanguage, originalTranslationsPath, originalAutoFile;

        public static string OriginalLanguage { get { return originalLanguage; } }

        public static bool Ready
        {
            get
            {
                if (xuaPlugin != null) return true;
                try
                {
                    if (pluginType == null) Capture();
                    if (pluginType == null) return false;
                    var current = pluginType.GetField("Current", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    xuaPlugin = current != null ? current.GetValue(null) : null;
                    if (xuaPlugin != null && originalLanguage == null) ReadOriginals();
                }
                catch (Exception e) { Plugin.Log.LogError(e); }
                return xuaPlugin != null;
            }
        }

        public static void Capture()
        {
            if (pluginType != null) return;
            var core = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "XUnity.AutoTranslator.Plugin.Core");
            if (core == null) return;
            pluginType = core.GetType("XUnity.AutoTranslator.Plugin.Core.AutoTranslationPlugin");
            settingsType = core.GetType("XUnity.AutoTranslator.Plugin.Core.Configuration.Settings");
        }

        private static void ReadOriginals()
        {
            originalLanguage = GetStatic("Language") as string ?? "";
            originalTranslationsPath = GetStatic("TranslationsPath") as string;
            originalAutoFile = GetStatic("AutoTranslationsFilePath") as string;
            Plugin.Log.LogInfo("XUnity found: language '" + originalLanguage + "', translations '" + originalTranslationsPath + "'.");
        }

        private static object GetStatic(string name)
        {
            var f = settingsType.GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            return f != null ? f.GetValue(null) : null;
        }

        private static void SetStatic(string name, object value)
        {
            var f = settingsType.GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (f == null) { Plugin.Log.LogWarning("XUnity setting '" + name + "' not found."); return; }
            f.SetValue(null, value);
        }

        private static object Call(string method, params object[] args)
        {
            var m = pluginType.GetMethod(method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (m == null) { Plugin.Log.LogWarning("XUnity method '" + method + "' not found."); return null; }
            return m.Invoke(xuaPlugin, args);
        }

        // Helps diagnosing "nothing gets translated": XUnity reports how many web translations succeeded or failed.
        private static IEnumerator LogEndpointStatus(Language language)
        {
            yield return new WaitForSeconds(12f);
            try
            {
                var status = Call("GetCurrentEndpointStatus") as string;
                Plugin.Log.LogInfo("XUnity translation endpoint status after switching to " + language + ": " + (status ?? "(unknown)"));
            }
            catch (Exception e) { Plugin.Log.LogWarning("Could not read XUnity endpoint status: " + e.Message); }
        }

        private static bool InTranslatedMode()
        {
            var f = pluginType.GetField("_isInTranslatedMode", BindingFlags.Instance | BindingFlags.NonPublic);
            return f == null || (bool)f.GetValue(xuaPlugin);
        }

        public static void Apply(Language language)
        {
            if (!Ready) return;
            try
            {
                Plugin.Log.LogInfo("Switching game language to " + language + ".");
                // Let XUnity put original textures back as well (it only does that when texture toggling is on), then
                // return every live text and texture to the game's original before applying the new language.
                SetStatic("EnableTextureToggling", true);
                if (InTranslatedMode()) Call("ToggleTranslation");

                if (language == Language.English)
                {
                    Call("DisableAutoTranslator");
                }
                else
                {
                    var code = language == Language.Russian ? "ru" : "de";
                    var dir = language == Language.Russian ? originalTranslationsPath : Path.Combine(Paths.ConfigPath, "Translation_de") + Path.DirectorySeparatorChar;
                    var autoFile = language == Language.Russian ? originalAutoFile : Path.Combine(dir, "_AutoGeneratedTranslations.txt");
                    if (language == Language.German && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    // XUnity prunes this file on reload and fails if it does not exist yet
                    if (autoFile != null && !File.Exists(autoFile)) File.WriteAllText(autoFile, string.Empty, new System.Text.UTF8Encoding(false));

                    // translated textures (e.g. RTLC's Russian logo) only belong to the Russian pack
                    SetStatic("EnableTextureTranslation", language == Language.Russian);
                    SetStatic("Language", code);
                    if (dir != null) SetStatic("TranslationsPath", dir);
                    if (autoFile != null) SetStatic("AutoTranslationsFilePath", autoFile);
                    Call("EnableAutoTranslator");
                    Call("ToggleTranslation");                         // back into 'translated' mode
                    Call("ReloadTranslations");                        // reloads the dictionaries and re-translates every live text
                }
                Plugin.Chosen.Value = language.ToString();
                SettingsMenu.Relocalize();                      // the Language dropdown itself follows the new language at once
                Plugin.Instance.StartCoroutine(MenuSpacing.ApplySoon());
                if (language != Language.English) Plugin.Instance.StartCoroutine(LogEndpointStatus(language));
            }
            catch (Exception e)
            {
                var inner = e is TargetInvocationException && e.InnerException != null ? e.InnerException : e;
                Plugin.Log.LogError("Language switch failed (the game keeps running with its previous language): " + inner);
            }
        }
    }

    // The Russian and German main-menu items are wider than the English ones and run into the logo. Tightening the gap
    // between letters (never the font size or the layout) keeps them clear of it; English is left exactly as the game has it.
    internal static class MenuSpacing
    {
        private const float Tighter = -12f;
        private static readonly Dictionary<int, float> original = new Dictionary<int, float>();

        public static IEnumerator ApplySoon()
        {
            // the translation reaches the buttons a moment after the scene or the language changes
            foreach (var wait in new[] { 0.5f, 2f, 4f }) { yield return new WaitForSeconds(wait); try { Apply(); } catch (Exception e) { Plugin.Log.LogWarning("Main menu spacing: " + e.Message); } }
        }

        private static void Apply()
        {
            var tighten = Plugin.Current() != Language.English;
            // scene objects only: going through every Transform in memory (assets of all loaded bundles included) costs seconds
            foreach (var t in UnityEngine.Object.FindObjectsOfType<Transform>(true))
            {
                if (t == null || t.name != "MainButtons") continue;
                foreach (var text in t.GetComponentsInChildren<TMP_Text>(true))
                {
                    float first;
                    if (!original.TryGetValue(text.GetInstanceID(), out first)) { first = text.characterSpacing; original[text.GetInstanceID()] = first; }
                    var want = tighten ? first + Tighter : first;
                    if (!Mathf.Approximately(text.characterSpacing, want)) text.characterSpacing = want;
                }
            }
        }
    }
}

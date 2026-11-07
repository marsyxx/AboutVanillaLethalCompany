using System.Collections;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GraphicsPlus
{
    // Extra graphics options in the game's own Settings screen (see SettingsMenu.cs); LethalConfig shows the same
    // entries as a fallback. Everything defaults to "vanilla": installing the mod changes nothing until a value is moved.
    [BepInPlugin(Guid, "GraphicsPlus", "1.0.1")]
    [BepInDependency("ainavt.lc.lethalconfig", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "lc.graphicsplus";

        internal static ManualLogSource Log;
        private static Plugin instance;

        private void Awake()
        {
            instance = this;
            Log = Logger;
            Settings.Bind(Config);
            Config.SettingChanged += (s, e) => Applier.ApplyAll();

            var harmony = new Harmony(Guid);
            Hook(harmony, typeof(IngamePlayerSettings), "SetPixelResolution", nameof(PixelResolutionPostfix));
            Hook(harmony, typeof(IngamePlayerSettings), "UpdateGameToMatchSettings", nameof(SettingsAppliedPostfix));
            Hook(harmony, typeof(StartOfRound), "Start", nameof(SettingsAppliedPostfix));
            SettingsMenu.Install(this, "Graphics+", "GRAPHICS+", () => Settings.Options(), () => Settings.LockVanilla.Value && Settings.InUse());

            SceneManager.sceneLoaded += (scene, mode) => instance.StartCoroutine(ApplyNextFrame());
            StartCoroutine(Reassert());
            Log.LogInfo("GraphicsPlus loaded. Settings are in the game's Settings screen (button 'Graphics+' in its Graphics box), and in the LethalConfig mod menu.");
        }

        private static void Hook(Harmony harmony, System.Type type, string method, string postfix)
        {
            var original = AccessTools.Method(type, method);
            if (original == null) { Log.LogWarning(type.Name + "." + method + " not found, that hook is skipped."); return; }
            harmony.Patch(original, postfix: new HarmonyMethod(typeof(Plugin), postfix));
        }

        private static void PixelResolutionPostfix()
        {
            Applier.Guard("resolution base", Applier.CaptureBase);
            Applier.Guard("resolution", Applier.ApplyResolution);
        }

        private static void SettingsAppliedPostfix() { Applier.ApplyAll(); }

        private static IEnumerator ApplyNextFrame() { yield return null; Applier.ApplyAll(); }

        private static IEnumerator Reassert()
        {
            var wait = new WaitForSeconds(5f);
            while (true) { yield return wait; Applier.Reassert(); }
        }
    }
}

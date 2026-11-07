using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Logging;
using Mono.Cecil;
using Mono.Cecil.Cil;

// Runs in the BepInEx preloader, before the game assembly is loaded. It only ADDS members that older mods still
// reference, so nothing on disk (game or mods) is modified and every step is skipped if the game already has them.
namespace LCCompatFixes.Patcher
{
    public static class Patcher
    {
        public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll" };

        private static readonly ManualLogSource Log = Logger.CreateLogSource("LCCompatFixes.Patcher");

        public static void Patch(AssemblyDefinition assembly)
        {
            var module = assembly.MainModule;
            Run("MenuManager.DisplayMenuNotification(string, string)", () => AddMenuNotificationOverload(module));
            Run("HUDManager.loadingDarkenScreen", () => AddLoadingDarkenScreenField(module));
        }

        private static void Run(string what, Action action)
        {
            try { action(); }
            catch (Exception e) { Log.LogError("Skipped '" + what + "': " + e); }
        }

        // Game update added a third parameter (bool setSelected, defaults to true) to MenuManager.DisplayMenuNotification.
        // Mods compiled against the old signature (e.g. LethalCasino) throw MissingMethodException on every call site.
        private static void AddMenuNotificationOverload(ModuleDefinition module)
        {
            var menu = module.GetType("MenuManager");
            if (menu == null) { Log.LogWarning("MenuManager not found, nothing to do."); return; }

            bool Is(MethodDefinition m, int count) => m.Name == "DisplayMenuNotification" && m.Parameters.Count == count;
            if (menu.Methods.Any(m => Is(m, 2))) { Log.LogInfo("MenuManager.DisplayMenuNotification(string, string) already exists."); return; }

            var three = menu.Methods.FirstOrDefault(m => Is(m, 3)
                && m.Parameters[0].ParameterType.FullName == "System.String"
                && m.Parameters[1].ParameterType.FullName == "System.String"
                && m.Parameters[2].ParameterType.FullName == "System.Boolean");
            if (three == null) { Log.LogWarning("Expected 3-parameter DisplayMenuNotification not found; the game changed again, skipping."); return; }

            var shim = new MethodDefinition("DisplayMenuNotification", MethodAttributes.Public | MethodAttributes.HideBySig, module.TypeSystem.Void);
            shim.Parameters.Add(new ParameterDefinition("notificationText", ParameterAttributes.None, module.TypeSystem.String));
            shim.Parameters.Add(new ParameterDefinition("buttonText", ParameterAttributes.None, module.TypeSystem.String));
            var il = shim.Body.GetILProcessor();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Ldc_I4_1); // setSelected = true, same behaviour as the old 2-argument method
            il.Emit(OpCodes.Callvirt, three);
            il.Emit(OpCodes.Ret);
            menu.Methods.Add(shim);
            Log.LogInfo("Added MenuManager.DisplayMenuNotification(string, string) -> (string, string, true).");
        }

        // The game update removed HUDManager.loadingDarkenScreen. Mods that still read it get a MissingFieldException.
        // The field is added back as non-serialized; the companion plugin fills it with a harmless stub Image.
        private static void AddLoadingDarkenScreenField(ModuleDefinition module)
        {
            var hud = module.GetType("HUDManager");
            if (hud == null) { Log.LogWarning("HUDManager not found, nothing to do."); return; }
            if (hud.Fields.Any(f => f.Name == "loadingDarkenScreen")) { Log.LogInfo("HUDManager.loadingDarkenScreen already exists."); return; }

            var image = module.GetTypeReferences().FirstOrDefault(r => r.FullName == "UnityEngine.UI.Image");
            if (image == null) { Log.LogWarning("UnityEngine.UI.Image is not referenced by the game assembly, skipping."); return; }

            hud.Fields.Add(new FieldDefinition("loadingDarkenScreen", FieldAttributes.Public | FieldAttributes.NotSerialized, image));
            Log.LogInfo("Added HUDManager.loadingDarkenScreen (UnityEngine.UI.Image, not serialized).");
        }
    }
}

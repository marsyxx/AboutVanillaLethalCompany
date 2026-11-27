using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LCCompatFixes;

internal static class MainMenuExit
{
    private static Plugin plugin;
    private static ManualLogSource log;
    private static bool saved;

    internal static void Install(Harmony harmony, Plugin owner, ManualLogSource logger)
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer)
            return;

        plugin = owner;
        log = logger;
        harmony.Patch(AccessTools.Method(typeof(GameNetworkManager), "OnApplicationQuit"),
            prefix: new HarmonyMethod(typeof(MainMenuExit), nameof(BeginQuit)),
            postfix: new HarmonyMethod(typeof(MainMenuExit), nameof(CompleteQuit)) { priority = Priority.Last },
            transpiler: new HarmonyMethod(typeof(MainMenuExit), nameof(MarkSuccessfulSave)));
    }

    private static void BeginQuit() => saved = false;

    private static IEnumerable<CodeInstruction> MarkSuccessfulSave(IEnumerable<CodeInstruction> instructions)
    {
        var disconnect = AccessTools.Method(typeof(GameNetworkManager), nameof(GameNetworkManager.Disconnect));
        var code = instructions.ToList();
        var index = code.FindIndex(instruction => instruction.Calls(disconnect));
        if (index >= 0 && code.FindLastIndex(instruction => instruction.Calls(disconnect)) == index)
        {
            // The selected-slot write and Disconnect run in the same try block.
            // A thrown save skips this call and lets vanilla shutdown continue.
            var mark = AccessTools.Method(typeof(MainMenuExit), nameof(SetSaved));
            code.Insert(index + 1, new CodeInstruction(OpCodes.Call, mark));
        }
        else
        {
            log.LogWarning("Could not find the vanilla quit save path. Using normal Unity shutdown.");
        }

        return code;
    }

    private static void SetSaved() => saved = true;

    private static void CompleteQuit()
    {
        if (!saved || SceneManager.GetActiveScene().name != "MainMenu" || StartOfRound.Instance != null)
            return;

        plugin.GetComponent<PerfReport>()?.Finish();
        log.LogMessage("Main menu save completed. Exiting before Unity native shutdown.");
        if (!TerminateProcess(GetCurrentProcess(), 0))
            log.LogWarning(new Win32Exception(Marshal.GetLastWin32Error()).Message);
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentProcess();

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TerminateProcess(IntPtr process, uint exitCode);
}

using System;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using HarmonyLib;
using TMPro;
using UnityEngine.SceneManagement;

namespace LCCompatFixes;

// Contracted finds the title of the end-of-day report by its text: "PERFORMANCE REPORT" or a contract title ending in an English
// result word. After it writes "<CONTRACT> · FAILURE" the pack's translation turns that into Russian or German, the title is never
// found again, and the report keeps old contract rows (with 90 retries every round). The translated endings are recognised too.
internal static class ContractReportTitle
{
    private const string PluginId = "com.y4ngz.company";
    private const string TypeName = "Y4NGZCompany.Contracts._Shared.ContractPerformanceReportUi";
    // the values of the "... · FAILURE" entries in Captured_ru.txt and Captured_de.txt
    private static readonly string[] TranslatedEndings = { " · ПРОВАЛ", " · GESCHEITERT" };

    private static ManualLogSource log;
    private static Harmony harmony;
    private static bool done;

    internal static void Install(Harmony patcher, ManualLogSource logger)
    {
        harmony = patcher;
        log = logger;
        // Contracted may load after this plugin: patch when the first scene loads, without forcing the load order
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (done)
            return;
        done = true;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        try
        {
            if (!Chainloader.PluginInfos.TryGetValue(PluginId, out var plugin) || plugin.Instance == null)
                return;
            var type = plugin.Instance.GetType().Assembly.GetType(TypeName);
            var method = type == null ? null : AccessTools.Method(type, "IsPerformanceReportTitleCandidate", new[] { typeof(TextMeshProUGUI) });
            if (method == null)
            {
                log.LogWarning("Contracted's report title check not found; translated report titles are not recognised.");
                return;
            }
            harmony.Patch(method, postfix: new HarmonyMethod(typeof(ContractReportTitle), nameof(AcceptTranslated)));
            log.LogInfo("Contracted: translated report titles are recognised.");
        }
        catch (Exception e)
        {
            log.LogWarning("Contracted report title fix failed: " + e.Message);
        }
    }

    private static void AcceptTranslated(TextMeshProUGUI __0, ref bool __result)
    {
        if (__result || __0 == null)
            return;
        var value = __0.text;
        if (string.IsNullOrWhiteSpace(value))
            return;
        value = value.Trim();
        foreach (var ending in TranslatedEndings)
        {
            if (value.EndsWith(ending, StringComparison.OrdinalIgnoreCase))
            {
                __result = true;
                return;
            }
        }
    }
}

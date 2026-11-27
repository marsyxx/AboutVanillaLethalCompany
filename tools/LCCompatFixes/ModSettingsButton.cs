using BepInEx.Bootstrap;
using HarmonyLib;
using UnityEngine;

namespace LCCompatFixes;

internal static class ModSettingsButton
{
    internal const string PluginId = "com.willis.lc.lethalsettings";

    internal static void Install(Harmony harmony)
    {
        if (!Chainloader.PluginInfos.ContainsKey(PluginId))
            return;

        var main = new HarmonyMethod(typeof(ModSettingsButton), nameof(MoveMainMenuButton))
        {
            after = new[] { PluginId },
        };
        var pause = new HarmonyMethod(typeof(ModSettingsButton), nameof(MovePauseButton))
        {
            after = new[] { PluginId },
        };
        harmony.Patch(AccessTools.Method(typeof(MenuManager), "Start"), postfix: main);
        harmony.Patch(AccessTools.Method(typeof(QuickMenuManager), "Start"), postfix: pause);
    }

    private static void MoveMainMenuButton(MenuManager __instance) =>
        Move(__instance.transform.parent.Find("MenuContainer/SettingsPanel"));

    private static void MovePauseButton(QuickMenuManager __instance) =>
        Move(__instance.menuContainer.transform.Find("SettingsPanel"));

    private static void Move(Transform settings)
    {
        var menu = settings?.Find("Mod Settings Container(Clone)");
        var launcher = menu?.Find("Open Mod Settings Button") as RectTransform;
        var header = menu?.Find("Mod Settings Header") as RectTransform;
        var back = settings?.Find("BackButton") as RectTransform;
        var reset = settings?.Find("SetToDefault") as RectTransform;
        if (launcher == null || back == null || reset == null)
            return;

        // Use the gap between the two buttons in the vanilla footer in both scenes.
        var backEdge = back.TransformPoint(new Vector3(back.rect.xMax, back.rect.center.y));
        var resetEdge = reset.TransformPoint(new Vector3(reset.rect.xMin, reset.rect.center.y));
        var center = menu.InverseTransformPoint((backEdge + resetEdge) * 0.5f);
        var old = launcher.localPosition;
        var offset = launcher.rect.center;
        launcher.localPosition = new Vector3(center.x - offset.x, center.y - offset.y, old.z);
        if (header != null)
            header.localPosition += launcher.localPosition - old;
    }
}

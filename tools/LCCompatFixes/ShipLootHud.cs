using BepInEx.Bootstrap;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace LCCompatFixes;

internal static class ShipLootHud
{
    internal const string PluginId = "com.github.tinyhoot.ShipLoot";
    private static CanvasGroup mount;
    private static readonly AccessTools.FieldRef<HUDManager, bool> HudHidden =
        AccessTools.FieldRefAccess<HUDManager, bool>("hudHidden");

    internal static void Install(Harmony harmony, ManualLogSource log)
    {
        if (!Chainloader.PluginInfos.TryGetValue(PluginId, out var plugin))
            return;

        var patchType = plugin.Instance.GetType().Assembly.GetType("ShipLoot.Patches.HudManagerPatcher");
        var build = patchType == null ? null : AccessTools.Method(patchType, "BuildCounter");
        if (build == null || AccessTools.Field(patchType, "_totalCounter") == null)
        {
            log.LogWarning("ShipLoot counter builder not found. Leaving ShipLoot's layout in place.");
            return;
        }

        harmony.Patch(build, postfix: new HarmonyMethod(typeof(ShipLootHud), nameof(DetachCounter)));
        harmony.Patch(AccessTools.Method(typeof(HUDManager), nameof(HUDManager.HideHUD)),
            postfix: new HarmonyMethod(typeof(ShipLootHud), nameof(UpdateVisibility)));
    }

    private static void DetachCounter(HUDManager hudManager, GameObject ____totalCounter)
    {
        if (____totalCounter == null || ____totalCounter.transform is not RectTransform counter ||
            counter.parent is not RectTransform vanillaPanel || vanillaPanel.name != "GlobalScanInfo" ||
            vanillaPanel.parent is not RectTransform scanner || scanner.name != "ObjectScanner")
            return;

        // Keep both text objects and the 1.2.2 renderer, while removing the vanilla panel's transform.
        var holder = new GameObject("ShipLootHud", typeof(RectTransform), typeof(CanvasGroup));
        var rect = (RectTransform)holder.transform;
        rect.SetParent(scanner, false);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = rect.anchorMin;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;

        var offset = counter.anchoredPosition;
        counter.SetParent(rect, false);
        counter.anchorMin = new Vector2(0.5f, 0.5f);
        counter.anchorMax = counter.anchorMin;
        counter.anchoredPosition3D = new Vector3(offset.x, offset.y, 0f);
        counter.localRotation = Quaternion.identity;
        counter.localScale = Vector3.one;

        mount = holder.GetComponent<CanvasGroup>();
        mount.interactable = false;
        mount.blocksRaycasts = false;
        UpdateVisibility(hudManager);
    }

    private static void UpdateVisibility(HUDManager __instance)
    {
        if (mount != null)
            mount.alpha = HudHidden(__instance) ? 0f : 1f;
    }
}

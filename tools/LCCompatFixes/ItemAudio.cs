using System.Collections.Generic;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace LCCompatFixes;

// The game plays an item's pocket, grab and drop sounds through gameObject.GetComponent<AudioSource>() without checking the result.
// An item without that component (Contracted's survey beacon until it first beeps) throws NullReferenceException in the middle of a
// slot switch: the item turns invisible and the player cannot switch, drop or interact until rejoining. Such items get a silent source.
internal static class ItemAudio
{
    private static ManualLogSource log;
    private static readonly HashSet<string> reported = new HashSet<string>();

    internal static void Install(Harmony harmony, ManualLogSource logger)
    {
        log = logger;
        var ensure = new HarmonyMethod(typeof(ItemAudio), nameof(Ensure));
        Patch(harmony, nameof(GrabbableObject.Start), postfix: ensure);
        // also right before the calls that need it: a subclass may not run the base Start
        Patch(harmony, nameof(GrabbableObject.PocketItem), prefix: ensure);
        Patch(harmony, nameof(GrabbableObject.EquipItem), prefix: ensure);
    }

    private static void Patch(Harmony harmony, string name, HarmonyMethod prefix = null, HarmonyMethod postfix = null)
    {
        var method = AccessTools.Method(typeof(GrabbableObject), name);
        if (method == null)
        {
            log.LogWarning("GrabbableObject." + name + " not found; item audio guard skips it.");
            return;
        }
        harmony.Patch(method, prefix, postfix);
    }

    private static void Ensure(GrabbableObject __instance)
    {
        if (__instance == null || __instance.GetComponent<AudioSource>() != null)
            return;
        var source = __instance.gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f;
        source.maxDistance = 25f;
        var item = __instance.itemProperties != null ? __instance.itemProperties.itemName : __instance.name;
        if (reported.Add(item))
            log.LogInfo("Item '" + item + "' had no AudioSource; one was added so switching slots cannot break.");
    }
}

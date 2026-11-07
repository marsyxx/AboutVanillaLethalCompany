using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Temporary harness (never shipped): pull the ship lever in an offline LAN lobby and measure where the freeze comes from.
[BepInPlugin("tmp.landingprobe", "LandingProbe", "0.0.1")]
public class Plugin : BaseUnityPlugin
{
    private static Plugin self;
    private static float pulledAt = -1f;

    private void Awake()
    {
        self = this;
        SceneManager.sceneLoaded += (s, m) =>
        {
            if (pulledAt >= 0f) Logger.LogInfo(string.Format("LandingProbe: t+{0:0.00}s scene loaded {1} ({2})", Time.realtimeSinceStartup - pulledAt, s.name, m));
            if (s.name == "InitSceneLaunchOptions") StartCoroutine(GoLan());
            if (s.name == "MainMenu") StartCoroutine(Host());
            if (s.name == "SampleSceneRelay") StartCoroutine(InGame());
        };
    }

    private IEnumerator GoLan()
    {
        yield return new WaitForSeconds(4f);
        var lan = Resources.FindObjectsOfTypeAll<Button>().FirstOrDefault(b => b != null && b.gameObject.scene.IsValid() && b.name.ToUpperInvariant().Contains("LAN"));
        if (lan != null) lan.onClick.Invoke(); else SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator Host()
    {
        yield return new WaitForSeconds(14f);
        var mm = UnityEngine.Object.FindObjectOfType<MenuManager>();
        mm.ClickHostButton(); yield return new WaitForSeconds(1f);
        mm.LAN_HostSetLocal(); yield return new WaitForSeconds(0.5f);
        mm.ConfirmHostButton();
    }

    // ---- method timing ----------------------------------------------------------------------------------------
    private static void Pre(out long __state) { __state = Stopwatch.GetTimestamp(); }
    private static void Post(MethodBase __originalMethod, long __state)
    {
        var ms = (Stopwatch.GetTimestamp() - __state) * 1000.0 / Stopwatch.Frequency;
        if (ms >= 15.0) self.Logger.LogInfo(string.Format("LandingProbe: t+{0:0.00}s {1}.{2} took {3:0} ms", pulledAt >= 0f ? Time.realtimeSinceStartup - pulledAt : -1f, __originalMethod.DeclaringType.Name, __originalMethod.Name, ms));
    }

    private void Timed(Harmony h, string typeName, string method)
    {
        var type = AccessTools.TypeByName(typeName);
        var m = type != null ? AccessTools.Method(type, method) : null;
        if (m == null) { Logger.LogInfo("LandingProbe: (not found) " + typeName + "." + method); return; }
        h.Patch(m, prefix: new HarmonyMethod(typeof(Plugin), nameof(Pre)) { priority = Priority.First }, postfix: new HarmonyMethod(typeof(Plugin), nameof(Post)) { priority = Priority.Last });
    }

    private IEnumerator InGame()
    {
        yield return new WaitForSeconds(25f);
        var h = new Harmony("tmp.landingprobe");
        foreach (var n in new[] { "LoadNewLevel", "GenerateNewFloor", "SpawnScrapInLevel", "SpawnMapObjects", "SetLevelObjectVariables", "SpawnOutsideHazards", "PredictAllOutsideEnemies", "RefreshEnemiesList", "FinishGeneratingNewLevelClientRpc", "UnloadSceneObjectsEarly", "InitializeRandomNumberGenerators", "SetExitIDs", "FlickerLights", "SyncScrapValuesClientRpc" })
            Timed(h, "RoundManager", n);
        foreach (var n in new[] { "StartGame", "OnShipLandedMiscEvents", "SceneManager_OnLoadComplete1", "SceneManager_OnLoad", "ArriveAtLevel", "StartGameServerRpc" })
            Timed(h, "StartOfRound", n);
        Timed(h, "DunGen.RuntimeDungeon", "Generate"); Timed(h, "DunGen.DungeonGenerator", "Generate");
        Timed(h, "DunGen.DungeonGenerator", "InnerGenerate");
        Timed(h, "Unity.AI.Navigation.NavMeshSurface", "BuildNavMesh");

        // which other mods hook these methods, and how long does each hook take?
        var targets = new[] { Tuple.Create("StartOfRound", "SceneManager_OnLoadComplete1"), Tuple.Create("StartOfRound", "SceneManager_OnLoad"), Tuple.Create("StartOfRound", "StartGame"), Tuple.Create("StartOfRound", "OnShipLandedMiscEvents"), Tuple.Create("StartOfRound", "StartGameServerRpc"),
            Tuple.Create("RoundManager", "LoadNewLevel"), Tuple.Create("RoundManager", "GenerateNewFloor"), Tuple.Create("RoundManager", "FinishGeneratingNewLevelClientRpc"), Tuple.Create("RoundManager", "SpawnScrapInLevel"), Tuple.Create("RoundManager", "SpawnMapObjects"), Tuple.Create("RoundManager", "SetLevelObjectVariables"), Tuple.Create("RoundManager", "RefreshEnemiesList") };
        foreach (var t in targets)
        {
            var ty = AccessTools.TypeByName(t.Item1); var m = ty != null ? AccessTools.Method(ty, t.Item2) : null; if (m == null) continue;
            var pi = Harmony.GetPatchInfo(m); if (pi == null) continue;
            Func<System.Collections.Generic.IEnumerable<Patch>, string> fmt = ps => string.Join("; ", ps.Where(x => x.owner != "tmp.landingprobe").Select(x => x.owner + ":" + x.PatchMethod.DeclaringType.Name + "." + x.PatchMethod.Name).ToArray());
            Logger.LogInfo("LandingProbe: hooks on " + t.Item1 + "." + t.Item2 + " | prefix [" + fmt(pi.Prefixes) + "] postfix [" + fmt(pi.Postfixes) + "] finalizer [" + fmt(pi.Finalizers) + "] transpilers [" + string.Join(", ", pi.Transpilers.Select(x => x.owner).Distinct().ToArray()) + "]");
            foreach (var x in pi.Prefixes.Concat(pi.Postfixes).Concat(pi.Finalizers))
            {
                if (x.owner == "tmp.landingprobe") continue;
                try { h.Patch(x.PatchMethod, prefix: new HarmonyMethod(typeof(Plugin), nameof(Pre)) { priority = Priority.First }, postfix: new HarmonyMethod(typeof(Plugin), nameof(Post)) { priority = Priority.Last }); } catch (Exception) { }
            }
        }

        StartCoroutine(FrameSpikes());
        var lever = UnityEngine.Object.FindObjectOfType<StartMatchLever>();
        var sor = StartOfRound.Instance;
        // fly to a real moon first (the ship starts at the company building, where the lever does not load a level)
        var terminal = UnityEngine.Object.FindObjectOfType<Terminal>();
        var change = typeof(StartOfRound).GetMethod("ChangeLevelServerRpc", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        Logger.LogInfo("LandingProbe: ChangeLevelServerRpc " + (change != null ? "found (" + change.GetParameters().Length + " args)" : "NOT FOUND") + ", credits " + (terminal != null ? terminal.groupCredits.ToString() : "?"));
        if (change != null) change.Invoke(sor, change.GetParameters().Length == 2 ? new object[] { 0, terminal != null ? terminal.groupCredits : 0 } : new object[] { 0 });
        for (var i = 0; i < 40 && (sor.currentLevelID != 0 || sor.travellingToNewLevel); i++) yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(3f);
        Logger.LogInfo("LandingProbe: level '" + sor.currentLevel.PlanetName + "', in ship phase=" + sor.inShipPhase + ", pulling the lever");
        pulledAt = Time.realtimeSinceStartup;
        lever.PullLever();                                   // animation and effects
        yield return new WaitForSeconds(0.2f);
        lever.StartGame();                                   // what the lever's animation event calls at the end
        yield return new WaitForSeconds(6f);
        if (sor.inShipPhase && !sor.shipHasLanded) { Logger.LogInfo("LandingProbe: lever.StartGame did not start the round; calling StartGameServerRpc"); sor.StartGameServerRpc(); }

        var deadline = Time.realtimeSinceStartup + 75f;
        var reported = false;
        while (Time.realtimeSinceStartup < deadline)
        {
            if (!reported && sor.shipHasLanded) { reported = true; Logger.LogInfo(string.Format("LandingProbe: ship landed at t+{0:0.0}s", Time.realtimeSinceStartup - pulledAt)); }
            yield return new WaitForSeconds(0.5f);
        }
        Logger.LogInfo("LandingProbe: done, quitting.");
        Application.Quit();
    }

    private IEnumerator FrameSpikes()
    {
        Logger.LogInfo("LandingProbe: GC mode " + GarbageCollector.GCMode + ", incremental=" + GarbageCollector.isIncremental + ", heap " + (GC.GetTotalMemory(false) / 1048576) + " MB, background loading priority " + Application.backgroundLoadingPriority);
        int g0 = GC.CollectionCount(0), g1 = GC.CollectionCount(1), g2 = GC.CollectionCount(2);
        long mem = GC.GetTotalMemory(false);
        while (true)
        {
            yield return null;
            var dt = Time.unscaledDeltaTime;
            var n0 = GC.CollectionCount(0); var n1 = GC.CollectionCount(1); var n2 = GC.CollectionCount(2); var nm = GC.GetTotalMemory(false);
            if (dt > 0.1f)
                Logger.LogInfo(string.Format("LandingProbe: t+{0:0.00}s FRAME {1:0} ms | GC collections this frame gen0 +{2} gen1 +{3} gen2 +{4} | heap {5} MB ({6:+0;-0} MB) | scenes {7}",
                    pulledAt >= 0f ? Time.realtimeSinceStartup - pulledAt : -1f, dt * 1000f, n0 - g0, n1 - g1, n2 - g2, nm / 1048576, (nm - mem) / 1048576, SceneManager.sceneCount));
            g0 = n0; g1 = n1; g2 = n2; mem = nm;
        }
    }
}

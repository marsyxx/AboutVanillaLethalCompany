using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using BepInEx;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Temporary test harness (never shipped): skips the launch-options screen, opens the Settings panel by code,
// dumps the UI hierarchy and writes screenshots next to the BepInEx folder, then quits the game.
[BepInPlugin("tmp.uiprobe", "UiProbe", "0.0.2")]
public class Plugin : BaseUnityPlugin
{
    private static string Dir { get { return Paths.BepInExRootPath; } }
    private int shots;

    private void Awake()
    {
        SceneManager.sceneLoaded += (s, m) =>
        {
            if (s.name == "InitSceneLaunchOptions") StartCoroutine(GoMain());
            if (s.name == "MainMenu") StartCoroutine(MenuRoutine());
        };
    }

    private IEnumerator GoMain()
    {
        yield return new WaitForSeconds(4f);
        Logger.LogInfo("UiProbe: skipping the launch-options screen (loading MainMenu directly, nothing is saved).");
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator MenuRoutine()
    {
        yield return new WaitForSeconds(14f);
        Dump("main menu closed");
        Shot("01_main_menu");
        yield return new WaitForSeconds(1f);

        // open the Settings panel the way the menu does: show the panel, hide the main buttons
        var panel = Resources.FindObjectsOfTypeAll<Transform>().FirstOrDefault(t => t != null && t.name == "SettingsPanel" && t.gameObject.scene.IsValid());
        if (panel == null) { Logger.LogWarning("UiProbe: no SettingsPanel found"); yield return Finish(); yield break; }
        var mainButtons = panel.parent.Find("MainButtons");
        if (mainButtons != null) mainButtons.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        { var es0 = UnityEngine.EventSystems.EventSystem.current; Logger.LogInfo("UiProbe: selected object right after opening Settings = " + (es0 != null && es0.currentSelectedGameObject != null ? es0.currentSelectedGameObject.name : "nothing")); }
        Dump("settings open");
        Shot("02_settings");
        yield return new WaitForSeconds(1f);

        // how the vanilla buttons do their hover/selection highlight
        foreach (var bn in new[] { "BackButton", "SetToDefault", "KeybindingsButton", "GraphicsPlusButton", "LanguageSwitchButton" })
        {
            var bt = panel.Find(bn);
            if (bt == null) { Logger.LogInfo("UiProbe: [" + bn + "] not found"); continue; }
            var info = new StringBuilder("UiProbe: [" + bn + "] ");
            var an = bt.GetComponent<Animator>();
            info.Append("animator=" + (an != null ? (an.runtimeAnimatorController != null ? an.runtimeAnimatorController.name : "no controller") + " params=" + string.Join(",", an.parameters.Select(p => p.name + ":" + p.type).ToArray()) + " updateMode=" + an.updateMode : "none"));
            var btn = bt.GetComponent<Button>();
            if (btn != null)
            {
                info.Append(" | Button.transition=" + btn.transition + " onClick persistent=");
                for (var i = 0; i < btn.onClick.GetPersistentEventCount(); i++) info.Append((btn.onClick.GetPersistentTarget(i) != null ? btn.onClick.GetPersistentTarget(i).GetType().Name : "null") + "." + btn.onClick.GetPersistentMethodName(i) + ";");
            }
            var et = bt.GetComponent<UnityEngine.EventSystems.EventTrigger>();
            if (et != null)
            {
                info.Append(" | EventTrigger:");
                foreach (var entry in et.triggers)
                {
                    info.Append(" " + entry.eventID + "->");
                    for (var i = 0; i < entry.callback.GetPersistentEventCount(); i++)
                        info.Append((entry.callback.GetPersistentTarget(i) != null ? entry.callback.GetPersistentTarget(i).GetType().Name : "null") + "." + entry.callback.GetPersistentMethodName(i) + ";");
                }
            }
            var sg = bt.GetComponent<SelectUIForGamepad>();
            info.Append(sg != null ? " | SelectUIForGamepad present" : "");
            Logger.LogInfo(info.ToString());
        }

        // hover animation + selection behaviour of our buttons, driven with synthetic pointer events
        {
            var es = UnityEngine.EventSystems.EventSystem.current;
            var gb = panel.Find("GraphicsPlusButton");
            if (es != null && gb != null)
            {
                var ped = new UnityEngine.EventSystems.PointerEventData(es);
                UnityEngine.EventSystems.ExecuteEvents.Execute(gb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerEnterHandler);
                yield return new WaitForSeconds(0.8f);
                Shot("06_hover_on_Graphics+");
                yield return new WaitForSeconds(0.4f);
                UnityEngine.EventSystems.ExecuteEvents.Execute(gb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
                yield return new WaitForSeconds(0.8f);
                Shot("06_hover_off_Graphics+");
                yield return new WaitForSeconds(0.4f);

                UnityEngine.EventSystems.ExecuteEvents.Execute(gb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerEnterHandler);
                UnityEngine.EventSystems.ExecuteEvents.Execute(gb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
                UnityEngine.EventSystems.ExecuteEvents.Execute(gb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
                UnityEngine.EventSystems.ExecuteEvents.Execute(gb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
                yield return new WaitForSeconds(0.6f);
                var pg = FindAny("GraphicsPlusPage");
                Logger.LogInfo("UiProbe: after clicking Graphics+: page active=" + (pg != null && pg.gameObject.activeSelf) + ", EventSystem selected=" + (es.currentSelectedGameObject != null ? es.currentSelectedGameObject.name : "nothing"));
                var pb = FindAny("GraphicsPlusBack");
                if (pb != null)
                {
                    UnityEngine.EventSystems.ExecuteEvents.Execute(pb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerEnterHandler);
                    UnityEngine.EventSystems.ExecuteEvents.Execute(pb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
                    UnityEngine.EventSystems.ExecuteEvents.Execute(pb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
                    UnityEngine.EventSystems.ExecuteEvents.Execute(pb.gameObject, ped, UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
                    yield return new WaitForSeconds(0.6f);
                    Logger.LogInfo("UiProbe: after Back: page active=" + (pg != null && pg.gameObject.activeSelf) + ", EventSystem selected=" + (es.currentSelectedGameObject != null ? es.currentSelectedGameObject.name : "nothing"));
                }
                yield return new WaitForSeconds(0.5f);
                Shot("06_after_back");
            }
        }

        // frame-time baseline of the Graphics+ page while scrolling it
        {
            QualitySettings.vSyncCount = 0; Application.targetFrameRate = -1;
            var gp = panel.Find("GraphicsPlusButton");
            var page0 = FindAny("GraphicsPlusPage");
            var sr0 = page0 != null ? page0.GetComponentInChildren<ScrollRect>(true) : null;
            yield return Measure("idle, settings panel open (no page)", 3f, null);
            if (gp != null && sr0 != null)
            {
                gp.GetComponent<Button>().onClick.Invoke();
                yield return new WaitForSeconds(1.5f);
                yield return Measure("Graphics+ page open, not scrolling", 3f, null);
                yield return Measure("Graphics+ page SCROLLING (1)", 5f, f => sr0.verticalNormalizedPosition = 0.5f + 0.5f * Mathf.Sin(f * 6.2832f * 3f));
                yield return Measure("Graphics+ page SCROLLING (2)", 5f, f => sr0.verticalNormalizedPosition = 0.5f + 0.5f * Mathf.Sin(f * 6.2832f * 3f));
                yield return Measure("Graphics+ page open, not scrolling (again)", 3f, null);
                page0.gameObject.SetActive(false);
            }
        }

        // press the buttons our mods injected
        foreach (var name in new[] { "GraphicsPlusButton", "LanguageSwitchButton" })
        {
            var btn = panel.Find(name);
            if (btn == null) { Logger.LogWarning("UiProbe: no " + name + " found"); continue; }
            Logger.LogInfo("UiProbe: pressing " + name);
            btn.GetComponent<Button>().onClick.Invoke();
            yield return new WaitForSeconds(2f);
            Dump(name + " pressed");
            Shot("03_" + name);
            yield return new WaitForSeconds(1f);
            var pageName = name.Replace("Button", "Page");
            var page = FindAny(pageName);
            if (page != null && name == "GraphicsPlusButton")
            {
                // drive the page like a player would and let the log prove the settings reach the game
                foreach (var cap in page.GetComponentsInChildren<TMP_Text>(true))
                {
                    var row = cap.transform.parent;
                    if (cap.text == "Resolution multiplier") { var s = row.GetComponentInChildren<Slider>(true); Logger.LogInfo("UiProbe: slider 'Resolution multiplier' -> 1.5"); s.value = 1.5f; }
                    if (cap.text == "Vignette") { var d = row.GetComponentInChildren<TMP_Dropdown>(true); Logger.LogInfo("UiProbe: dropdown 'Vignette' -> Off"); d.value = 0; }
                    if (cap.text == "Ambient occlusion") { var d = row.GetComponentInChildren<TMP_Dropdown>(true); Logger.LogInfo("UiProbe: dropdown 'Ambient occlusion' -> On"); d.value = 2; }
                    if (cap.text == "Texture quality") { var d = row.GetComponentInChildren<TMP_Dropdown>(true); Logger.LogInfo("UiProbe: dropdown 'Texture quality' -> Full"); d.value = 1; }
                    if (cap.text == "Shadow distance") { var s = row.GetComponentInChildren<Slider>(true); Logger.LogInfo("UiProbe: slider 'Shadow distance' -> 2"); s.value = 2f; }
                }
                yield return new WaitForSeconds(3f);
            }
            var sr = page != null ? page.GetComponentInChildren<ScrollRect>(true) : null;
            if (sr != null)
            {
                sr.verticalNormalizedPosition = 0.5f; yield return new WaitForSeconds(1f); Shot("03b_" + name);
                sr.verticalNormalizedPosition = 0f; yield return new WaitForSeconds(1f); Shot("03c_" + name);
            }
            if (page != null) page.gameObject.SetActive(false);
        }

        // language test: close the panel, then drive the Language dropdown by code and photograph the main menu
        var langPage = FindAny("LanguageSwitchPage");
        var dd = langPage != null ? langPage.GetComponentInChildren<TMP_Dropdown>(true) : null;
        panel.gameObject.SetActive(false);
        if (mainButtons != null) mainButtons.gameObject.SetActive(true);
        if (dd != null)
        {
            foreach (var idx in new[] { 2, 0, 1 })
            {
                Logger.LogInfo("UiProbe: choosing language index " + idx + " (" + dd.options[idx].text + ")");
                dd.value = idx;
                yield return new WaitForSeconds(8f);
                Shot("04_language_" + idx);
                yield return new WaitForSeconds(1f);

                // the mods' own pages must follow the chosen language
                if (mainButtons != null) mainButtons.gameObject.SetActive(false);
                panel.gameObject.SetActive(true);
                yield return new WaitForSeconds(1f);
                foreach (var bn in new[] { "GraphicsPlusButton", "LanguageSwitchButton" })
                {
                    var b = panel.Find(bn);
                    if (b == null) continue;
                    b.GetComponent<Button>().onClick.Invoke();
                    yield return new WaitForSeconds(2f);
                    Shot("05_" + bn + "_lang" + idx);
                    yield return new WaitForSeconds(1f);
                    var pg = FindAny(bn.Replace("Button", "Page"));
                    if (pg != null) pg.gameObject.SetActive(false);
                }
                panel.gameObject.SetActive(false);
                if (mainButtons != null) mainButtons.gameObject.SetActive(true);
                yield return new WaitForSeconds(1f);
            }
        }
        else Logger.LogWarning("UiProbe: no language dropdown found");
        yield return Finish();
    }

    private static Transform FindAny(string name)
    {
        foreach (var t in Resources.FindObjectsOfTypeAll<Transform>())
            if (t != null && t.name == name && t.gameObject.scene.IsValid()) return t;
        return null;
    }

    private IEnumerator Measure(string label, float seconds, Action<float> perFrame)
    {
        float t = 0f, max = 0f; var n = 0; double sum = 0;
        yield return null;
        while (t < seconds)
        {
            var dt = Time.unscaledDeltaTime;
            t += dt; n++; sum += dt; if (dt > max) max = dt;
            if (perFrame != null) perFrame(t / seconds);
            yield return null;
        }
        Logger.LogInfo(string.Format("UiProbe: PERF '{0}': {1} frames, avg {2:0.0} ms ({3:0} FPS), worst frame {4:0.0} ms", label, n, sum / n * 1000.0, n / sum, max * 1000.0));
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(3f);
        Logger.LogInfo("UiProbe: done, quitting.");
        Application.Quit();
    }

    private static Button FindButton(params string[] needles)
    {
        foreach (var b in Resources.FindObjectsOfTypeAll<Button>())
        {
            if (b == null || !b.gameObject.scene.IsValid() || !b.gameObject.activeInHierarchy) continue;
            var txt = string.Join(" ", b.GetComponentsInChildren<TMP_Text>(true).Select(t => t.text ?? "").ToArray()).ToLowerInvariant();
            var nm = b.name.ToLowerInvariant();
            if (needles.Any(n => txt.Contains(n) || nm.Contains(n))) return b;
        }
        return null;
    }

    private void Shot(string name)
    {
        var path = Path.Combine(Dir, "shot_" + name + ".png");
        if (File.Exists(path)) File.Delete(path);
        ScreenCapture.CaptureScreenshot(path);
        Logger.LogInfo("UiProbe: screenshot requested -> " + path);
    }

    private static string PathOf(Transform t)
    {
        var sb = new StringBuilder(t.name);
        for (var p = t.parent; p != null; p = p.parent) sb.Insert(0, p.name + "/");
        return sb.ToString();
    }

    private void Dump(string label)
    {
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== " + label + "  scene " + SceneManager.GetActiveScene().name + "  " + DateTime.Now);
            var options = Resources.FindObjectsOfTypeAll<SettingsOption>().Where(o => o != null && o.gameObject.scene.IsValid()).ToArray();
            sb.AppendLine("SettingsOption count: " + options.Length);
            foreach (var o in options) sb.AppendLine("  " + o.optionType + " active=" + o.gameObject.activeInHierarchy + " " + PathOf(o.transform));
            Transform root = null;
            if (options.Length > 0)
            {
                var common = options[0].transform;
                foreach (var o in options) while (common != null && !o.transform.IsChildOf(common)) common = common.parent;
                sb.AppendLine("COMMON ANCESTOR: " + (common != null ? PathOf(common) : "none"));
                root = common;
                for (var i = 0; i < 3 && root != null && root.parent != null; i++) root = root.parent;
            }
            if (root != null)
                try { DumpTree(sb, root, 0, 9); } catch (Exception e) { sb.AppendLine("TREE DUMP FAILED: " + e); }
            File.AppendAllText(Path.Combine(Dir, "UiDump.txt"), sb.ToString() + Environment.NewLine);
            Logger.LogInfo("UiProbe: dump '" + label + "' written.");
        }
        catch (Exception e) { Logger.LogError(e); }
    }

    private static void DumpTree(StringBuilder sb, Transform t, int depth, int maxDepth)
    {
        var comps = string.Join(",", t.GetComponents<Component>().Where(c => c != null && !(c is Transform) && !(c is RectTransform)).Select(c => c.GetType().Name).ToArray());
        var rt = t as RectTransform;
        var extra = rt != null ? " pos=" + rt.anchoredPosition + " size=" + rt.sizeDelta + " anc=" + rt.anchorMin + "-" + rt.anchorMax : "";
        var tmp = t.GetComponent<TMP_Text>();
        var text = tmp != null ? " text=\"" + (tmp.text ?? "").Replace("\n", "\\n") + "\"" : "";
        sb.AppendLine(new string(' ', depth * 2) + (t.gameObject.activeSelf ? "" : "[off] ") + t.name + " {" + comps + "}" + extra + text);
        if (depth >= maxDepth) return;
        foreach (Transform child in t) DumpTree(sb, child, depth + 1, maxDepth);
    }
}

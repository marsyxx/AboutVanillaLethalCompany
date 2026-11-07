using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Temporary test harness (never shipped): opens Settings -> Graphics+ and sends mouse-wheel events to several screen
// points through the real raycast path (EventSystem.RaycastAll + ExecuteEvents.ExecuteHierarchy), logging what handled them.
[BepInPlugin("tmp.scrollprobe", "ScrollProbe", "0.0.1")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        SceneManager.sceneLoaded += (s, m) =>
        {
            if (s.name == "InitSceneLaunchOptions") StartCoroutine(GoMain());
            if (s.name == "MainMenu") StartCoroutine(Run());
        };
    }

    private IEnumerator GoMain()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }

    private static Transform FindAny(string name)
    {
        foreach (var t in Resources.FindObjectsOfTypeAll<Transform>())
            if (t != null && t.name == name && t.gameObject.scene.IsValid()) return t;
        return null;
    }

    private static Vector2 ScreenOf(RectTransform rt)
    {
        var canvas = rt.GetComponentInParent<Canvas>();
        var root = canvas != null ? canvas.rootCanvas : null;
        Camera cam = root != null && root.renderMode != RenderMode.ScreenSpaceOverlay ? root.worldCamera : null;
        return RectTransformUtility.WorldToScreenPoint(cam, rt.position);
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(14f);
        var panel = FindAny("SettingsPanel");
        if (panel == null) { Logger.LogWarning("ScrollProbe: no SettingsPanel"); Application.Quit(); yield break; }
        var mainButtons = panel.parent.Find("MainButtons");
        if (mainButtons != null) mainButtons.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        foreach (var name in new[] { "GraphicsPlus", "LanguageSwitch" })
        {
            var open = panel.Find(name + "Button");
            if (open == null) { Logger.LogWarning("ScrollProbe: no " + name + "Button"); continue; }
            open.GetComponent<Button>().onClick.Invoke();
            yield return new WaitForSeconds(1.5f);
            var page = FindAny(name + "Page");
            var sr = page != null ? page.GetComponentInChildren<ScrollRect>(true) : null;
            if (sr == null) { Logger.LogWarning("ScrollProbe: no ScrollRect on " + name + "Page"); continue; }
            Canvas.ForceUpdateCanvases();
            Logger.LogInfo(string.Format("ScrollProbe: [{0}] screen {1}x{2}, content height {3:0}, viewport height {4:0}, forwarder={5}",
                name, Screen.width, Screen.height, sr.content.rect.height, sr.viewport.rect.height, page.GetComponent("ScrollForwarder") != null));

            var points = new List<KeyValuePair<string, Vector2>>();
            points.Add(new KeyValuePair<string, Vector2>("screen corner bottom-left", new Vector2(6f, 6f)));
            points.Add(new KeyValuePair<string, Vector2>("screen corner top-right", new Vector2(Screen.width - 6f, Screen.height - 6f)));
            points.Add(new KeyValuePair<string, Vector2>("left edge, middle", new Vector2(6f, Screen.height / 2f)));
            points.Add(new KeyValuePair<string, Vector2>("top edge, middle (title area)", new Vector2(Screen.width / 2f, Screen.height - 20f)));
            var scrollCentre = ScreenOf((RectTransform)sr.transform);
            points.Add(new KeyValuePair<string, Vector2>("centre of the list", scrollCentre));
            var vp = sr.viewport;
            var vpTopLeft = ScreenOf(vp);
            if (sr.verticalScrollbar != null) points.Add(new KeyValuePair<string, Vector2>("on the scrollbar", ScreenOf((RectTransform)sr.verticalScrollbar.transform)));
            var back = FindAny(name + "Back");
            if (back != null) points.Add(new KeyValuePair<string, Vector2>("on the Back button", ScreenOf((RectTransform)back)));

            var es = EventSystem.current;
            foreach (var p in points)
            {
                sr.verticalNormalizedPosition = 1f;
                yield return null;
                var ped = new PointerEventData(es) { position = p.Value, scrollDelta = new Vector2(0f, -1f) };
                var hits = new List<RaycastResult>();
                es.RaycastAll(ped, hits);
                var top = hits.Count > 0 ? hits[0].gameObject : null;
                GameObject handler = null;
                if (top != null) handler = ExecuteEvents.ExecuteHierarchy(top, ped, ExecuteEvents.scrollHandler);
                yield return null;
                Logger.LogInfo(string.Format("ScrollProbe: [{0}] wheel at {1,-32} ({2:0},{3:0}): hit={4,-22} handled by={5,-16} list moved {6:0.000} -> {7:0.000}",
                    name, p.Key, p.Value.x, p.Value.y, top != null ? top.name : "NOTHING", handler != null ? handler.name : "nobody", 1f, sr.verticalNormalizedPosition));
            }
            page.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2f);
        Logger.LogInfo("ScrollProbe: done, quitting.");
        Application.Quit();
    }
}

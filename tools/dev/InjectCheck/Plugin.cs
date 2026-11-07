using System;
using System.Collections;
using System.Linq;
using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Temporary harness (never shipped): are the Graphics+ button and the Language dropdown still added to both Settings panels
// (main menu and the in-game pause menu) after the lookup of the panel was changed?
[BepInPlugin(".0injectcheck", "InjectCheck", "0.0.1")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        SceneManager.sceneLoaded += (s, m) =>
        {
            if (s.name == "InitSceneLaunchOptions") StartCoroutine(GoLan());
            if (s.name == "MainMenu") StartCoroutine(MenuPhase());
            if (s.name == "SampleSceneRelay") StartCoroutine(GamePhase());
        };
    }

    private IEnumerator GoLan()
    {
        yield return new WaitForSeconds(4f);
        var lan = Resources.FindObjectsOfTypeAll<Button>().FirstOrDefault(b => b != null && b.gameObject.scene.IsValid() && b.name.ToUpperInvariant().Contains("LAN"));
        if (lan != null) lan.onClick.Invoke(); else SceneManager.LoadScene("MainMenu");
    }

    private void Report(string where)
    {
        var panels = UnityEngine.Object.FindObjectsOfType<Transform>(true).Where(t => t.name == "SettingsPanel").ToList();
        Logger.LogInfo("InjectCheck: [" + where + "] SettingsPanel objects in the scene: " + panels.Count);
        foreach (var p in panels)
        {
            var kids = Enumerable.Range(0, p.childCount).Select(i => p.GetChild(i).name).ToList();
            Logger.LogInfo("InjectCheck: [" + where + "] panel '" + p.GetHierarchyPath() + "' has GraphicsPlusButton=" + kids.Contains("GraphicsPlusButton") + " LanguageSwitchDropdown=" + kids.Contains("LanguageSwitchDropdown") + " children=" + kids.Count);
        }
    }

    private IEnumerator MenuPhase()
    {
        yield return new WaitForSecondsRealtime(35f);
        Report("main menu");
        var mm = UnityEngine.Object.FindObjectOfType<MenuManager>();
        mm.ClickHostButton(); yield return new WaitForSeconds(1f);
        mm.LAN_HostSetLocal(); yield return new WaitForSeconds(0.5f);
        mm.ConfirmHostButton();
    }

    private IEnumerator GamePhase()
    {
        yield return new WaitForSeconds(25f);
        var qm = UnityEngine.Object.FindObjectOfType<QuickMenuManager>();
        if (qm != null) { qm.OpenQuickMenu(); yield return new WaitForSeconds(1.5f); }
        Report("in game");
        yield return new WaitForSeconds(8f);
        Report("in game (later)");
        Application.Quit();
    }
}

internal static class Ext
{
    public static string GetHierarchyPath(this Transform t)
    {
        var path = t.name;
        while (t.parent != null) { t = t.parent; path = t.name + "/" + path; }
        return path;
    }
}

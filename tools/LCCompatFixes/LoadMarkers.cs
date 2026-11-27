using System;
using System.Collections;
using System.IO;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LCCompatFixes;

public sealed class LoadMarkers : MonoBehaviour
{
	private sealed class SettingsListener : ILogListener, IDisposable
	{
		public void LogEvent(object sender, LogEventArgs e)
		{
			if (e.Data is string text)
			{
				if (text.StartsWith("Async configuration loading completed", StringComparison.Ordinal))
				{
					settingsDone = true;
				}
				else if (text.StartsWith("Registering Bundle Content", StringComparison.Ordinal))
				{
					bundlesRegistered = true;
				}
			}
		}

		public void Dispose()
		{
		}
	}

	private const string LaunchOptionsScene = "InitSceneLaunchOptions";

	private const string MainMenuScene = "MainMenu";

	private const string InitScene = "InitScene";

	private static ManualLogSource log;

	private static volatile bool settingsDone;

	private static volatile bool bundlesRegistered;

	private static volatile bool menuSceneLoaded;

	private static bool windowReleased;

	private bool started;

	public static bool Ready { get; private set; }

	internal static void Start(BaseUnityPlugin plugin, ManualLogSource logger)
	{
		log = logger;
		plugin.gameObject.AddComponent<LoadMarkers>();
		Application.quitting += delegate
		{
			Release("LCPACK:quit");
		};
	}

	private void Awake()
	{
		SceneManager.sceneLoaded += OnLoaded;
		BepInEx.Logging.Logger.Listeners.Add(new SettingsListener());
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnLoaded;
	}

	private static void Release(string marker)
	{
		if (windowReleased)
		{
			return;
		}
		windowReleased = true;
		try
		{
			log.LogMessage(marker);
		}
		catch
		{
		}
	}

	private void OnLoaded(Scene scene, LoadSceneMode mode)
	{
		string text = scene.name;
		if (!(text == "InitSceneLaunchOptions"))
		{
			if (text == "MainMenu")
			{
				menuSceneLoaded = true;
				Release("LCPACK:menu-loaded");
				StartWatch(mainMenu: true);
			}
			else if (mode == LoadSceneMode.Single && scene.name != "InitScene")
			{
				Release("LCPACK:scene " + scene.name);
			}
		}
		else
		{
			Release("LCPACK:launch-options (waiting for the Online / LAN choice)");
			StartWatch(mainMenu: false);
		}
	}

	private void StartWatch(bool mainMenu)
	{
		if (!started)
		{
			started = true;
			StartCoroutine(Watch(mainMenu));
		}
	}

	private IEnumerator Watch(bool mainMenu)
	{
		try
		{
			File.WriteAllText(Path.Combine(Paths.BepInExRootPath, ".lcpack-plugincount"), Chainloader.PluginInfos.Count.ToString());
		}
		catch
		{
		}
		float begun = Time.realtimeSinceStartup;
		float windowStart = begun;
		int frames = 0;
		float worst = 0f;
		int calm = 0;
		bool hasSettingsMenu = Chainloader.PluginInfos.ContainsKey("dev.amrv.lethalCompany.config");
		bool waitForLll = Chainloader.PluginInfos.ContainsKey("imabatby.lethallevelloader");
		while (Time.realtimeSinceStartup - begun < ((mainMenu || menuSceneLoaded) ? 900f : 150f))
		{
			mainMenu |= menuSceneLoaded;
			bool waitForSettings = mainMenu && hasSettingsMenu;
			yield return null;
			frames++;
			worst = Mathf.Max(worst, Time.unscaledDeltaTime * 1000f);
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			if (!(realtimeSinceStartup - windowStart < 3f))
			{
				bool flag = (realtimeSinceStartup - windowStart) / (float)frames * 1000f < 40f && worst < 500f && (!waitForSettings || settingsDone) && (!waitForLll || bundlesRegistered);
				windowStart = realtimeSinceStartup;
				frames = 0;
				worst = 0f;
				calm = (flag ? (calm + 1) : 0);
				if (calm >= 2)
				{
					break;
				}
			}
		}
		Ready = true;
		log.LogMessage("LCPACK:ready after " + (Time.realtimeSinceStartup - begun).ToString("0") + " s on the " + (mainMenu ? "main menu" : "launch screen") + ".");
		windowReleased = true;
		UnityEngine.Object.Destroy(this);
	}
}

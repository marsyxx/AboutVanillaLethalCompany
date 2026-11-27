using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace LCCompatFixes;

[BepInPlugin(Guid, "LCCompatFixes", Version)]
[BepInDependency("me.loaforc.soundapi", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency(ShipLootHud.PluginId, BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency(ModSettingsButton.PluginId, BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BaseUnityPlugin
{
	private sealed class SliceTarget
	{
		public MethodInfo TrySet;

		public MethodInfo EnabledGetter;

		public string Name;

		public bool Running;

		public bool Reported;

		public readonly HashSet<int> Done = new HashSet<int>();
	}

	public const string Guid = "lc.compatfixes";

	public const string Version = "1.0.9";

	private static ManualLogSource Log;

	private static Plugin instance;

	private static GameObject stubHolder;

	private static bool loggedStubAssigned;

	private static bool loggedRpgOrder;

	private static readonly Dictionary<MethodBase, SliceTarget> sliceTargets = new Dictionary<MethodBase, SliceTarget>();

	private static bool loggedStreamFix;

	private static bool loggedStreamMiss;

	private static readonly Dictionary<Assembly, Type[]> typeScans = new Dictionary<Assembly, Type[]>();

	private static readonly object typeScanLock = new object();

	private static int typeScanReuses;

	private static bool loggedTypeScanReuse;

	private static readonly Dictionary<string, Type> typeFound = new Dictionary<string, Type>();

	private static readonly Dictionary<string, int> typeMissed = new Dictionary<string, int>();

	private static readonly Dictionary<Assembly, Dictionary<string, Type>> shortNames = new Dictionary<Assembly, Dictionary<string, Type>>();

	private static readonly object typeByNameLock = new object();

	private static int typeLookups;

	private static int typeMissAnswers;

	private static bool loggedTypeLookups;

	private static int typeByNameFallbacks;

	private void Awake()
	{
		Log = base.Logger;
		instance = this;
		ConfigEntry<bool> configEntry = base.Config.Bind("Fixes", "StubLoadingDarkenScreen", defaultValue: true, "Gives HUDManager.loadingDarkenScreen (removed by a game update, still used by LethalCasino) a harmless stub Image. Needs the companion patcher.");
		ConfigEntry<bool> configEntry2 = base.Config.Bind("Fixes", "SoundApiPackNameDots", defaultValue: true, "loaforcsSoundAPI 2.x rejects sound packs whose name contains '.', which silently disables the RTLC Russian sound packs. Replaces '.' with '_' in pack names.");
		ConfigEntry<bool> configEntry3 = base.Config.Bind("Fixes", "StreamBundleResult", defaultValue: true, "EladsHUD+ loads its HUD from an embedded asset bundle with AssetBundle.LoadFromStream. With BepInEx_Faster_Load_AssetBundles_Patcher and XUnity both hooking that call, Unity loads the bundle but the caller is handed null, and the mod crashes in Awake. When that happens, hand the caller the bundle that was just loaded.");
		ConfigEntry<bool> configEntry4 = base.Config.Bind("Fixes", "RtlcSceneLoadSlicing", defaultValue: true, "The RTLC Russian translation goes through every texture in memory on each level load, all in one frame: a 1-2 second freeze when the ship lands (a pause of its own on a modded game). When on, the same pass is done a few milliseconds per frame instead, so the game keeps running smoothly; the Russian textures appear a moment later.");
		ConfigEntry<bool> configEntry5 = base.Config.Bind("Fixes", "RtlcFastTextureCopy", defaultValue: true, "RTLC replaces each translated texture by encoding the replacement to PNG and decoding it again on the main thread: 40 ms for a 1024x1024 texture, 200-300 ms for 2048x2048, about 2 seconds of stalls when the ship lands on a moon. For plain uncompressed textures the pixels are copied directly instead (same result, about 1 ms); anything else is left to RTLC.");
		ConfigEntry<bool> rtlcSkipCompressed = base.Config.Bind("Fixes", "RtlcSkipCompressedTextures", defaultValue: false, "RTLC also replaces the game's own compressed textures (posters, signs, some HUD pictures) by recompressing them: 40-480 ms each, about 4 seconds in total on the first lobby of a session. When on, those textures are left as they are (in English) and the freeze is gone. Off = everything is translated, as before.");
		ConfigEntry<bool> configEntry6 = base.Config.Bind("Fixes", "QuietTmpUnderline", defaultValue: true, "TextMeshPro logs 'The character used for Underline is not available in font asset' for every underlined text in a font without the underscore glyph: about 900 lines per session, each a Unity log call with a stack trace. The text looks the same; when on, only that message is dropped.");
		ConfigEntry<bool> rtlcFastCopyValidate = base.Config.Bind("Tools", "RtlcFastCopyValidate", defaultValue: false, "Test mode for RtlcFastTextureCopy: for the first 40 textures, also does RTLC's PNG round trip on a scratch texture and logs whether the pixels are identical.");
		ConfigEntry<bool> configEntry7 = base.Config.Bind("Fixes", "CacheTypeScans", defaultValue: true, "Mods that look types up by name (AccessTools.TypeByName / AllTypes) make HarmonyX call Assembly.GetTypes on every loaded assembly each time. For the few assemblies that cannot load fully (they reference mods that are not installed) that call throws and takes 1-2 seconds, and it was repeated over a hundred times per session. The result of each scan is remembered, so it runs once per assembly.");
		ConfigEntry<bool> configEntry8 = base.Config.Bind("Fixes", "FastTypeByName", defaultValue: true, "HarmonyX's AccessTools.TypeByName (also behind AccessTools.Method(\"Type:Method\") and similar) compares the full name of every type in every loaded assembly, twice, on each call, and builds a new string for each one. A lookup of a mod that is not installed takes 2-6 seconds, and mods such as LazyLobby, LazyLanding and WeatherRegistry do dozens of them at startup and in the main menu; this was the biggest single cause of the long start and of the freezes of ten seconds and more in the menu. When on, names are looked up with the runtime's own per-assembly lookup and the answers are remembered; the result is the same type as before, a miss is answered at once, and the 'Could not find type named' warning for misses is no longer printed.");
		ConfigEntry<bool> configEntry9 = base.Config.Bind("Fixes", "LethalRpgStrengthBeforeBetterStamina", defaultValue: true, "LethalRPG's Strength skill finds the carry-weight reads in PlayerControllerB.Update/LateUpdate and scales them, but BetterStamina's patch (which runs first because of its priority) has already replaced those reads, so LethalRPG logs 'no reads of carryWeight found' and Strength does nothing. This puts LethalRPG's two carry-weight patches ahead of BetterStamina's, so both see the code they expect. Only the order changes; neither mod's code is touched.");
		ConfigEntry<bool> configEntry10 = base.Config.Bind("PerfReport", "Enabled", defaultValue: true, "Records frame times and memory into BepInEx/perf-report.csv (one row per second) and BepInEx/perf-summary.txt (per place: menu, orbit, moon, dungeon, ship), so one small file shows where the game stutters. Local only: nothing is sent, no player name, Steam id or folder path is written. Costs well under 0.1 ms per frame.");
		ConfigEntry<string> emoteKey = base.Config.Bind("Fixes", "EmoteEscapeKey", "Backspace", "Key that forces a stuck TooManyEmotes animation to end and gives the controls back (a name from Unity's Key list, for example Backspace, End, F8). Does nothing unless an emote is running, or while typing in the terminal or chat.");
		ConfigEntry<bool> configEntry11 = base.Config.Bind("Fixes", "TerminalIgnoreThe", defaultValue: true, "The terminal matches a typed word to any keyword that begins with it, so 'the' matched the monster file 'thewail' (from a monster mod) and 'the company' showed that file instead of the Company. When on, the word 'the' is ignored by the terminal.");
		ConfigEntry<bool> configEntry12 = base.Config.Bind("Fixes", "ModGuards", defaultValue: true, "Catches two errors that cancelled jumping for a client: BetterEXP's jump statistics throwing KeyNotFoundException (the jump is no longer cancelled) and TextMeshPro's font asset throwing NullReferenceException thousands of times (treated as a missing glyph).");
		ConfigEntry<string> configEntry13 = base.Config.Bind("Intro", "VideoFile", "intro.mp4", "Video played full screen with sound when the game starts (the main menu loads underneath). Space or Esc skips it. A file name next to LCCompatFixes.dll, or a full path. Leave empty to turn the intro off.");
		ConfigEntry<bool> loadingWindow = base.Config.Bind("Intro", "LoadingWindow", defaultValue: true, "A window that shows which mod is loading and how far the start has got (LCLoading.exe, started by the patcher before the game loads). It goes away by itself as soon as the Online / LAN screen or the main menu is reached, and never takes the keyboard from the game. Takes effect on the next start.");
		ConfigEntry<bool> configEntry14 = base.Config.Bind("Tools", "LogUntranslated", defaultValue: false, "Translation helper: writes the texts that are still English on screen to BepInEx/untranslated.txt (each once, with the object path). For the person who translates the pack; leave off while playing.");
		Harmony harmony = new Harmony("lc.compatfixes");
		if (Config.Bind("Fixes", "DetachShipLootCounter", true,
			"Shows ShipLoot independently of the vanilla scanner total.").Value)
			Safe("ShipLoot counter", () => ShipLootHud.Install(harmony, Log));
		if (Config.Bind("Fixes", "MoveModSettingsButton", true,
			"Moves the LethalSettings launcher below the settings list.").Value)
			Safe("LethalSettings launcher", () => ModSettingsButton.Install(harmony));
		if (Config.Bind("Fixes", "ItemAudioGuard", true,
			"Items without a sound source (Contracted's survey beacon) broke the inventory when switching away from them: invisible item, no switching or dropping until rejoin. Gives such items a silent source.").Value)
			Safe("item audio guard", () => ItemAudio.Install(harmony, Log));
		if (Config.Bind("Fixes", "ContractReportTitle", true,
			"Lets Contracted find its end-of-day report title after the translation changed it, so the report is filled every round.").Value)
			Safe("contract report title", () => ContractReportTitle.Install(harmony, Log));
		if (Config.Bind("Fixes", "ForceExitAfterMainMenuSave", false,
			"On Windows, bypasses Unity native shutdown after the main menu quit callback saves the selected slot.").Value)
			Safe("main menu exit", () => MainMenuExit.Install(harmony, this, Log));
		if (configEntry14.Value)
		{
			Safe("untranslated log", delegate
			{
				UntranslatedLog.Start(this, Log);
			});
		}
		if (loadingWindow.Value)
		{
			Safe("load markers", delegate
			{
				LoadMarkers.Start(this, Log);
			});
		}
		if (!string.IsNullOrWhiteSpace(configEntry13.Value) && Environment.GetEnvironmentVariable("LC_NOINTRO") == null)
		{
			string text = configEntry13.Value.Trim();
			string introPath = (Path.IsPathRooted(text) ? text : Path.Combine(Path.GetDirectoryName(base.Info.Location), text));
			Safe("intro video", delegate
			{
				IntroVideo.Start(this, Log, introPath, loadingWindow.Value);
			});
		}
		if (configEntry12.Value)
		{
			Safe("mod guards", delegate
			{
				ModGuards.Start(harmony, Log);
			});
		}
		if (configEntry6.Value)
		{
			Safe("quiet TMP underline", delegate
			{
				ModGuards.QuietUnderline(harmony, Log);
			});
		}
		if (configEntry11.Value)
		{
			Safe("terminal article", delegate
			{
				TerminalArticle.Start(harmony, Log);
			});
		}
		Safe("emote escape key", delegate
		{
			EmoteEscape.Start(this, Log, emoteKey.Value);
		});
		if (configEntry10.Value)
		{
			Safe("performance report", delegate
			{
				PerfReport.Start(this, Log);
			});
		}
		if (configEntry9.Value)
		{
			StartCoroutine(RpgOrderLoop());
		}
		if (configEntry7.Value)
		{
			Safe("type scan cache", delegate
			{
				PatchTypeScans(harmony);
			});
		}
		if (configEntry8.Value)
		{
			Safe("fast TypeByName", delegate
			{
				PatchTypeByName(harmony);
			});
		}
		if (configEntry4.Value)
		{
			Safe("RTLC scene load slicing", delegate
			{
				PatchRtlc(harmony);
			});
		}
		if (configEntry5.Value)
		{
			Safe("RTLC fast texture copy", delegate
			{
				RtlcFast.Start(harmony, Log, rtlcFastCopyValidate.Value, rtlcSkipCompressed.Value);
			});
		}
		if (configEntry.Value)
		{
			Safe("stub loadingDarkenScreen", delegate
			{
				PatchHud(harmony);
			});
		}
		if (configEntry2.Value)
		{
			Safe("SoundAPI pack names", delegate
			{
				PatchSoundApi(harmony);
			});
		}
		if (configEntry3.Value)
		{
			Safe("stream asset bundle result", delegate
			{
				PatchStreamBundles(harmony);
			});
		}
	}

	private static void Safe(string what, Action action)
	{
		try
		{
			action();
		}
		catch (Exception ex)
		{
			Log.LogError("Fix '" + what + "' failed to install: " + ex);
		}
	}

	private static void PatchHud(Harmony harmony)
	{
		if (AccessTools.Field(typeof(HUDManager), "loadingDarkenScreen") == null)
		{
			Log.LogWarning("HUDManager.loadingDarkenScreen is missing (patcher not loaded?); skipping the stub.");
		}
		else
		{
			harmony.Patch(AccessTools.Method(typeof(HUDManager), "Awake"), null, new HarmonyMethod(typeof(Plugin), "HudAwakePostfix"));
		}
	}

	private static void HudAwakePostfix(HUDManager __instance)
	{
		try
		{
			FieldInfo fieldInfo = AccessTools.Field(typeof(HUDManager), "loadingDarkenScreen");
			if (!(fieldInfo == null) && !(fieldInfo.GetValue(__instance) as Image != null))
			{
				if (stubHolder == null)
				{
					stubHolder = new GameObject("LCCompatFixes.DarkenScreenStub")
					{
						hideFlags = HideFlags.HideAndDontSave
					};
					UnityEngine.Object.DontDestroyOnLoad(stubHolder);
					stubHolder.AddComponent<Image>().enabled = false;
				}
				fieldInfo.SetValue(__instance, stubHolder.GetComponent<Image>());
				if (!loggedStubAssigned)
				{
					loggedStubAssigned = true;
					Log.LogInfo("Assigned stub Image to HUDManager.loadingDarkenScreen.");
				}
			}
		}
		catch (Exception data)
		{
			Log.LogError(data);
		}
	}

	private static void PatchSoundApi(Harmony harmony)
	{
		Type type = AccessTools.TypeByName("loaforcsSoundAPI.SoundPacks.Data.SoundPack");
		if (type == null)
		{
			Log.LogInfo("loaforcsSoundAPI not installed; nothing to fix.");
			return;
		}
		MethodInfo methodInfo = AccessTools.Method(type, "Validate");
		if (methodInfo == null)
		{
			Log.LogWarning("SoundPack.Validate not found; skipping.");
		}
		else
		{
			harmony.Patch(methodInfo, new HarmonyMethod(typeof(Plugin), "ValidatePrefix"));
		}
	}

	private static void ValidatePrefix(object __instance)
	{
		try
		{
			Type type = __instance.GetType();
			string text = AccessTools.Property(type, "Name")?.GetValue(__instance, null) as string;
			if (string.IsNullOrEmpty(text) || text.IndexOf('.') < 0)
			{
				return;
			}
			string text2 = text.Replace('.', '_');
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.FieldType == typeof(string) && (string)fieldInfo.GetValue(__instance) == text)
				{
					fieldInfo.SetValue(__instance, text2);
					Log.LogInfo("Sound pack name '" + text + "' -> '" + text2 + "' (SoundAPI rejects '.').");
					return;
				}
			}
			Log.LogWarning("Could not locate the backing field of SoundPack.Name for '" + text + "'.");
		}
		catch (Exception data)
		{
			Log.LogError(data);
		}
	}

	private static IEnumerator RpgOrderLoop()
	{
		for (int i = 0; i < 60; i++)
		{
			yield return new WaitForSeconds(2f);
			try
			{
				RpgOrderPass();
			}
			catch (Exception ex)
			{
				Log.LogError("LethalRPG patch order fix failed: " + ex);
				break;
			}
		}
	}

	private static void RpgOrderPass()
	{
		Type type = AccessTools.TypeByName("GameNetcodeStuff.PlayerControllerB");
		if (type == null)
		{
			return;
		}
		string[] array = new string[2] { "Update", "LateUpdate" };
		foreach (string text in array)
		{
			MethodInfo methodInfo = AccessTools.Method(type, text);
			Patches patches = ((methodInfo != null) ? Harmony.GetPatchInfo(methodInfo) : null);
			if (patches == null)
			{
				continue;
			}
			List<Patch> list = patches.Transpilers.Where((Patch p) => p.owner == "BetterStamina").ToList();
			List<Patch> list2 = patches.Transpilers.Where((Patch p) => p.owner == "cucumis.lethalrpg" && p.PatchMethod.Name.IndexOf("CarryWeight", StringComparison.OrdinalIgnoreCase) >= 0).ToList();
			if (list.Count == 0 || list2.Count == 0)
			{
				continue;
			}
			int top = list.Max((Patch p) => p.priority);
			if (list2.All((Patch p) => p.priority > top))
			{
				continue;
			}
			int priority = Math.Max(800, top + 1);
			Harmony harmony = new Harmony("cucumis.lethalrpg");
			foreach (Patch item in list2)
			{
				harmony.Unpatch(methodInfo, item.PatchMethod);
				harmony.Patch(methodInfo, null, null, new HarmonyMethod(item.PatchMethod)
				{
					priority = priority
				});
			}
			if (!loggedRpgOrder)
			{
				loggedRpgOrder = true;
				Log.LogInfo("LethalRPG's carry-weight patch on PlayerControllerB." + text + " now runs before BetterStamina's (priority " + priority + " instead of " + list2[0].priority + ").");
			}
		}
	}

	private static void PatchRtlc(Harmony harmony)
	{
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			string text = assembly.GetName().Name;
			if (text != "RTLC" && text != "RTLC.Mods")
			{
				continue;
			}
			Type[] source;
			try
			{
				source = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				source = ex.Types.Where((Type t) => t != null).ToArray();
			}
			Type type = source.FirstOrDefault((Type t) => t.Name == "PatchOnSceneLoad");
			Type type2 = source.FirstOrDefault((Type t) => t.Name == "BundleLoader");
			Type type3 = source.FirstOrDefault((Type t) => t.Name == "Configuration");
			MethodInfo methodInfo = ((type != null) ? AccessTools.Method(type, "Postfix") : null);
			MethodInfo methodInfo2 = ((type2 != null) ? AccessTools.Method(type2, "TrySetTexture") : null);
			if (methodInfo == null || methodInfo2 == null)
			{
				Log.LogInfo(text + ": PatchOnSceneLoad.Postfix / BundleLoader.TrySetTexture not found; leaving it alone.");
				continue;
			}
			sliceTargets[methodInfo] = new SliceTarget
			{
				Name = text,
				TrySet = methodInfo2,
				EnabledGetter = ((type3 != null) ? AccessTools.PropertyGetter(type3, "IsPatchEnabled") : null)
			};
			harmony.Patch(methodInfo, new HarmonyMethod(typeof(Plugin), "RtlcPrefix")
			{
				priority = 800
			});
			Log.LogInfo(text + ": texture pass on level load will be spread over frames.");
		}
	}

	private static bool RtlcPrefix(MethodBase __originalMethod)
	{
		if (!sliceTargets.TryGetValue(__originalMethod, out var value))
		{
			return true;
		}
		try
		{
			if (!value.Running)
			{
				instance.StartCoroutine(SliceTextures(value));
			}
			return false;
		}
		catch (Exception data)
		{
			Log.LogError(data);
			return true;
		}
	}

	private static bool TextureStep(SliceTarget target, Texture2D texture, object[] args)
	{
		try
		{
			args[0] = texture;
			args[1] = texture.name;
			target.TrySet.Invoke(null, args);
			return true;
		}
		catch (Exception ex)
		{
			Log.LogError("RTLC texture step failed, stopping the pass: " + (ex.InnerException ?? ex).Message);
			return false;
		}
	}

	private static IEnumerator SliceTextures(SliceTarget target)
	{
		target.Running = true;
		try
		{
			yield return null;
			if (target.EnabledGetter != null && target.EnabledGetter.Invoke(null, null) is ConfigEntry<bool> { Value: false })
			{
				yield break;
			}
			Stopwatch started = Stopwatch.StartNew();
			Texture2D[] all = Resources.FindObjectsOfTypeAll<Texture2D>();
			object[] args = new object[2];
			int index = 0;
			int frames = 0;
			bool ok = true;
			Stopwatch slice = new Stopwatch();
			while (index < all.Length && ok)
			{
				slice.Restart();
				while (index < all.Length && slice.Elapsed.TotalMilliseconds < 3.0)
				{
					Texture2D texture2D = all[index++];
					if (texture2D == null)
					{
						continue;
					}
					int instanceID = texture2D.GetInstanceID();
					if (!target.Done.Contains(instanceID))
					{
						ok = TextureStep(target, texture2D, args);
						if (!ok)
						{
							break;
						}
						target.Done.Add(instanceID);
					}
				}
				frames++;
				yield return null;
			}
			if (!target.Reported)
			{
				target.Reported = true;
				Log.LogInfo(target.Name + ": texture pass over " + all.Length + " textures finished in " + frames + " frames (" + started.ElapsedMilliseconds + " ms wall time, about 3 ms per frame).");
			}
		}
		finally
		{
			target.Running = false;
		}
	}

	private static void PatchStreamBundles(Harmony harmony)
	{
		MethodInfo methodInfo = AccessTools.Method(typeof(AssetBundle), "LoadFromStreamInternal");
		if (methodInfo == null)
		{
			Log.LogWarning("AssetBundle.LoadFromStreamInternal not found; skipping the stream bundle fix.");
			return;
		}
		harmony.Patch(methodInfo, new HarmonyMethod(typeof(Plugin), "StreamBundlePrefix")
		{
			priority = 800
		}, new HarmonyMethod(typeof(Plugin), "StreamBundlePostfix")
		{
			priority = 0
		});
	}

	private static void StreamBundlePrefix(out HashSet<int> __state)
	{
		__state = null;
		try
		{
			__state = new HashSet<int>(from b in AssetBundle.GetAllLoadedAssetBundles()
				where b != null
				select b.GetInstanceID());
		}
		catch (Exception data)
		{
			Log.LogError(data);
		}
	}

	private static void StreamBundlePostfix(ref AssetBundle __result, HashSet<int> __state)
	{
		if (__result != null || __state == null)
		{
			return;
		}
		try
		{
			List<AssetBundle> list = (from b in AssetBundle.GetAllLoadedAssetBundles()
				where b != null && !__state.Contains(b.GetInstanceID())
				select b).ToList();
			if (list.Count == 1)
			{
				__result = list[0];
				if (!loggedStreamFix)
				{
					loggedStreamFix = true;
					Log.LogInfo("AssetBundle.LoadFromStream returned null although it had loaded bundle '" + list[0].name + "'; returned that bundle to the caller.");
				}
			}
			else if (!loggedStreamMiss)
			{
				loggedStreamMiss = true;
				Log.LogWarning("AssetBundle.LoadFromStream returned null and " + list.Count + " new bundles appeared; left as it is.");
			}
		}
		catch (Exception data)
		{
			Log.LogError(data);
		}
	}

	private static void PatchTypeScans(Harmony harmony)
	{
		MethodInfo methodInfo = AccessTools.Method(typeof(AccessTools), "GetTypesFromAssembly", new Type[1] { typeof(Assembly) });
		if (methodInfo == null)
		{
			Log.LogWarning("AccessTools.GetTypesFromAssembly not found; type scans are not cached.");
			return;
		}
		harmony.Patch(methodInfo, new HarmonyMethod(typeof(Plugin), "TypeScanPrefix")
		{
			priority = 800
		}, new HarmonyMethod(typeof(Plugin), "TypeScanPostfix")
		{
			priority = 0
		});
	}

	private static bool TypeScanPrefix(Assembly assembly, ref Type[] __result)
	{
		if (assembly == null)
		{
			return true;
		}
		lock (typeScanLock)
		{
			if (!typeScans.TryGetValue(assembly, out var value))
			{
				return true;
			}
			__result = value;
			typeScanReuses++;
		}
		if (!loggedTypeScanReuse && typeScanReuses >= 20)
		{
			loggedTypeScanReuse = true;
			Log.LogInfo("HarmonyX type scans: " + typeScanReuses + " repeated scans answered from the cache so far.");
		}
		return false;
	}

	private static void TypeScanPostfix(Assembly assembly, Type[] __result)
	{
		if (assembly == null || __result == null || assembly.IsDynamic)
		{
			return;
		}
		lock (typeScanLock)
		{
			typeScans[assembly] = __result;
		}
	}

	private static void PatchTypeByName(Harmony harmony)
	{
		MethodInfo methodInfo = AccessTools.Method(typeof(AccessTools), "TypeByName", new Type[1] { typeof(string) });
		if (methodInfo == null)
		{
			Log.LogWarning("AccessTools.TypeByName not found; lookups stay as they are.");
			return;
		}
		harmony.Patch(methodInfo, new HarmonyMethod(typeof(Plugin), "TypeByNamePrefix")
		{
			priority = 800
		});
	}

	private static bool TypeByNamePrefix(string name, ref Type __result)
	{
		if (string.IsNullOrEmpty(name))
		{
			return true;
		}
		try
		{
			typeLookups++;
			if (!loggedTypeLookups && typeLookups >= 25)
			{
				loggedTypeLookups = true;
				Log.LogInfo("Fast TypeByName: 25 lookups so far, answered without scanning every type.");
			}
			lock (typeByNameLock)
			{
				if (typeFound.TryGetValue(name, out var value))
				{
					__result = value;
					return false;
				}
			}
			Type type = Type.GetType(name, throwOnError: false);
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			if (type == null)
			{
				lock (typeByNameLock)
				{
					if (typeMissed.TryGetValue(name, out var value2) && value2 == assemblies.Length)
					{
						typeMissAnswers++;
						__result = null;
						return false;
					}
				}
				Assembly[] array = assemblies;
				foreach (Assembly assembly in array)
				{
					if (!assembly.FullName.StartsWith("Microsoft.VisualStudio", StringComparison.Ordinal))
					{
						try
						{
							type = assembly.GetType(name, throwOnError: false);
						}
						catch
						{
							type = null;
						}
						if (type != null)
						{
							break;
						}
					}
				}
				if (type == null && name.IndexOf('.') < 0 && name.IndexOf('+') < 0)
				{
					type = FindByShortName(name, assemblies);
				}
			}
			lock (typeByNameLock)
			{
				if (type != null)
				{
					typeFound[name] = type;
				}
				else
				{
					typeMissed[name] = assemblies.Length;
				}
			}
			__result = type;
			return false;
		}
		catch (Exception ex)
		{
			if (++typeByNameFallbacks <= 3)
			{
				Log.LogWarning("Fast TypeByName fell back to HarmonyX for '" + name + "': " + ex.Message + ((typeByNameFallbacks == 3) ? " (further ones are not logged)" : ""));
			}
			return true;
		}
	}

	private static Type FindByShortName(string name, Assembly[] assemblies)
	{
		foreach (Assembly assembly in assemblies)
		{
			if (assembly.FullName.StartsWith("Microsoft.VisualStudio", StringComparison.Ordinal))
			{
				continue;
			}
			Dictionary<string, Type> value;
			lock (typeByNameLock)
			{
				shortNames.TryGetValue(assembly, out value);
			}
			if (value == null)
			{
				value = new Dictionary<string, Type>();
				Type[] array = null;
				try
				{
					array = AccessTools.GetTypesFromAssembly(assembly);
				}
				catch
				{
				}
				if (array != null)
				{
					Type[] array2 = array;
					foreach (Type type in array2)
					{
						if (type != null && !value.ContainsKey(type.Name))
						{
							value[type.Name] = type;
						}
					}
				}
				if (!assembly.IsDynamic)
				{
					lock (typeByNameLock)
					{
						shortNames[assembly] = value;
					}
				}
			}
			if (value.TryGetValue(name, out var value2))
			{
				return value2;
			}
		}
		return null;
	}
}

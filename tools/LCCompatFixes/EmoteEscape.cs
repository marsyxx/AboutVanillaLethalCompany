using System;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LCCompatFixes;

public sealed class EmoteEscape : MonoBehaviour
{
	private static ManualLogSource log;

	private static Key key;

	private static PropertyInfo localController;

	private static FieldInfo isPerforming;

	private static MethodInfo stop;

	private static MethodInfo reset;

	internal static void Start(BaseUnityPlugin plugin, ManualLogSource logger, string keyName)
	{
		log = logger;
		Type type = AccessTools.TypeByName("TooManyEmotes.EmoteControllerPlayer");
		Type type2 = AccessTools.TypeByName("TooManyEmotes.EmoteController");
		if (type == null || type2 == null)
		{
			log.LogInfo("TooManyEmotes not installed; the emote escape key is off.");
			return;
		}
		if (!Enum.TryParse<Key>(keyName, ignoreCase: true, out key) || key == Key.None)
		{
			log.LogWarning("Unknown emote escape key '" + keyName + "'; using Backspace.");
			key = Key.Backspace;
		}
		localController = AccessTools.Property(type, "emoteControllerLocal");
		isPerforming = AccessTools.Field(type2, "isPerformingEmote");
		stop = AccessTools.Method(type, "StopPerformingEmote", Type.EmptyTypes);
		reset = AccessTools.Method(type, "ResetPerformingEmote", Type.EmptyTypes);
		if (localController == null || isPerforming == null || stop == null || reset == null)
		{
			log.LogWarning("TooManyEmotes has changed; the emote escape key is off.");
		}
		else
		{
			plugin.gameObject.AddComponent<EmoteEscape>();
		}
	}

	private void Update()
	{
		Keyboard current = Keyboard.current;
		if (current != null && current[key].wasPressedThisFrame)
		{
			ForceStop();
		}
	}

	public static bool ForceStop()
	{
		try
		{
			GameNetworkManager instance = GameNetworkManager.Instance;
			PlayerControllerB playerControllerB = ((instance != null) ? instance.localPlayerController : null);
			object value = localController.GetValue(null, null);
			if ((UnityEngine.Object)(object)playerControllerB == null || value == null || playerControllerB.inTerminalMenu || playerControllerB.isTypingChat)
			{
				return false;
			}
			bool flag = (bool)isPerforming.GetValue(value);
			if (!flag && !playerControllerB.performingEmote)
			{
				return false;
			}
			if (flag)
			{
				stop.Invoke(value, null);
			}
			if ((bool)isPerforming.GetValue(value))
			{
				reset.Invoke(value, null);
			}
			playerControllerB.performingEmote = false;
			playerControllerB.playerBodyAnimator.SetInteger("emoteNumber", 0);
			log.LogInfo("Emote ended by the escape key.");
			return !(bool)isPerforming.GetValue(value);
		}
		catch (Exception ex)
		{
			log.LogWarning("Emote escape failed: " + (ex.InnerException ?? ex).Message);
			return false;
		}
	}
}

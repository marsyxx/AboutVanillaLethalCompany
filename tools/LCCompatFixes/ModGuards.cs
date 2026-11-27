using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace LCCompatFixes;

internal static class ModGuards
{
	private static ManualLogSource log;

	private static bool loggedJump;

	private static bool loggedFont;

	internal static void Start(Harmony harmony, ManualLogSource logger)
	{
		log = logger;
		MethodInfo methodInfo = AccessTools.Method(typeof(PlayerControllerB), "PlayJumpAudio");
		if (methodInfo != null)
		{
			harmony.Patch(methodInfo, null, null, null, new HarmonyMethod(typeof(ModGuards), "JumpFinalizer"), null);
		}
		else
		{
			log.LogWarning("PlayerControllerB.PlayJumpAudio not found; the BetterEXP jump guard is off.");
		}
		MethodInfo methodInfo2 = AccessTools.Method(typeof(TMP_FontAsset), "TryAddCharacterInternal");
		if (methodInfo2 != null && methodInfo2.ReturnType == typeof(bool))
		{
			harmony.Patch(methodInfo2, null, null, null, new HarmonyMethod(typeof(ModGuards), "FontFinalizer"), null);
		}
		else
		{
			log.LogWarning("TMP_FontAsset.TryAddCharacterInternal not found; the font guard is off.");
		}
	}

	internal static void QuietUnderline(Harmony harmony, ManualLogSource logger)
	{
		MethodInfo[] methods = typeof(Debug).GetMethods(BindingFlags.Static | BindingFlags.Public);
		foreach (MethodInfo methodInfo in methods)
		{
			if (!(methodInfo.Name != "LogWarning"))
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length != 0 && !(parameters[0].ParameterType != typeof(object)))
				{
					harmony.Patch(methodInfo, new HarmonyMethod(typeof(ModGuards), "UnderlinePrefix"));
				}
			}
		}
		logger.LogInfo("TextMeshPro 'character used for Underline' warnings are dropped (Fixes / QuietTmpUnderline).");
	}

	private static bool UnderlinePrefix(object message)
	{
		if (message is string text)
		{
			return !text.StartsWith("The character used for Underline is not available", StringComparison.Ordinal);
		}
		return true;
	}

	private static Exception JumpFinalizer(Exception __exception)
	{
		if (__exception is KeyNotFoundException && __exception.StackTrace != null && __exception.StackTrace.Contains("LCBetterEXP"))
		{
			if (!loggedJump)
			{
				loggedJump = true;
				log.LogInfo("BetterEXP failed while counting a jump (" + __exception.Message + "); the jump goes on. Further ones are not logged.");
			}
			return null;
		}
		return __exception;
	}

	private static Exception FontFinalizer(Exception __exception, ref bool __result)
	{
		if (__exception is NullReferenceException)
		{
			__result = false;
			if (!loggedFont)
			{
				loggedFont = true;
				log.LogInfo("TMP_FontAsset.TryAddCharacterInternal threw a NullReferenceException (a font asset without a source font); treated as 'glyph not available'. Further ones are not logged.");
			}
			return null;
		}
		return __exception;
	}
}

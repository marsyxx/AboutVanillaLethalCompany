using System;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using TMPro;

namespace LCCompatFixes;

internal static class TerminalArticle
{
	private static ManualLogSource log;

	private static bool logged;

	private static bool loggedFailure;

	private static FieldInfo textAdded;

	internal static void Start(Harmony harmony, ManualLogSource logger)
	{
		log = logger;
		textAdded = AccessTools.Field(typeof(Terminal), "textAdded");
		MethodInfo methodInfo = AccessTools.Method(typeof(Terminal), "ParsePlayerSentence");
		if (methodInfo == null || textAdded == null)
		{
			log.LogWarning("Terminal.ParsePlayerSentence / textAdded not found; the terminal article fix is off.");
			return;
		}
		harmony.Patch(methodInfo, new HarmonyMethod(typeof(TerminalArticle), "Prefix")
		{
			priority = 800
		});
	}

	private static void Prefix(Terminal __instance)
	{
		try
		{
			TMP_InputField screenText = __instance.screenText;
			if (screenText == null)
			{
				return;
			}
			string text = screenText.text;
			int num = (int)textAdded.GetValue(__instance);
			if (text == null || num <= 0 || num > text.Length)
			{
				return;
			}
			string text2 = text.Substring(text.Length - num);
			string[] words = text2.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (words.Length < 2)
			{
				return;
			}
			TerminalKeyword[] keywords = ((__instance.terminalNodes != null) ? __instance.terminalNodes.allKeywords : null);
			string[] array = words.Where((string w, int i) => !string.Equals(w, "the", StringComparison.OrdinalIgnoreCase) || i + 1 >= words.Length || IsKeyword(keywords, "the" + words[i + 1])).ToArray();
			if (array.Length != words.Length && array.Length != 0)
			{
				string text3 = string.Join(" ", array);
				screenText.text = text.Substring(0, text.Length - num) + text3;
				textAdded.SetValue(__instance, text3.Length);
				if (!logged)
				{
					logged = true;
					log.LogInfo("Terminal: the word 'the' is dropped before parsing ('" + text2.Trim() + "' -> '" + text3 + "').");
				}
			}
		}
		catch (Exception ex)
		{
			if (!loggedFailure)
			{
				loggedFailure = true;
				log.LogWarning("Terminal article fix failed: " + ex.Message + " (further failures are not logged)");
			}
		}
	}

	private static bool IsKeyword(TerminalKeyword[] keywords, string word)
	{
		if (keywords == null)
		{
			return false;
		}
		foreach (TerminalKeyword terminalKeyword in keywords)
		{
			if (terminalKeyword != null && string.Equals(terminalKeyword.word, word, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}
}

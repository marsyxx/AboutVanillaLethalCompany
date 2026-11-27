using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using BepInEx;
using BepInEx.Logging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LCCompatFixes;

public sealed class UntranslatedLog : MonoBehaviour
{
	private static ManualLogSource log;

	private static readonly Regex Tags = new Regex("<[^>]+>", RegexOptions.Compiled);

	private static readonly Regex Latin = new Regex("[A-Za-z]{3,}", RegexOptions.Compiled);

	private static readonly Regex Cyrillic = new Regex("[Ѐ-ӿ]", RegexOptions.Compiled);

	private readonly HashSet<string> seen = new HashSet<string>();

	private StreamWriter writer;

	private bool loggedFailure;

	internal static void Start(BaseUnityPlugin plugin, ManualLogSource logger)
	{
		log = logger;
		plugin.gameObject.AddComponent<UntranslatedLog>();
	}

	private void Awake()
	{
		string text = Path.Combine(Paths.BepInExRootPath, "untranslated.txt");
		writer = new StreamWriter(new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.ReadWrite), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false))
		{
			AutoFlush = true
		};
		log.LogInfo("Untranslated text log is ON: " + text);
		StartCoroutine(Loop());
	}

	private void OnDestroy()
	{
		writer?.Dispose();
	}

	private IEnumerator Loop()
	{
		while (true)
		{
			yield return new WaitForSecondsRealtime(1.5f);
			try
			{
				TMP_Text[] array = UnityEngine.Object.FindObjectsOfType<TMP_Text>();
				foreach (TMP_Text tMP_Text in array)
				{
					Check(tMP_Text.text, tMP_Text.gameObject);
				}
				Text[] array2 = UnityEngine.Object.FindObjectsOfType<Text>();
				foreach (Text text in array2)
				{
					Check(text.text, text.gameObject);
				}
			}
			catch (Exception ex)
			{
				if (!loggedFailure)
				{
					loggedFailure = true;
					log.LogWarning("Untranslated log: " + ex.Message + " (further failures are not logged)");
				}
			}
		}
	}

	private void Check(string text, GameObject go)
	{
		if (!string.IsNullOrWhiteSpace(text) && text.Length <= 3000)
		{
			string input = Tags.Replace(text, "").Trim();
			if (Latin.IsMatch(input) && !Cyrillic.IsMatch(input) && seen.Add(text))
			{
				writer.WriteLine(text.Replace("\r", "").Replace("\n", "\\n") + "\t" + ObjectPath(go));
			}
		}
	}

	private static string ObjectPath(GameObject go)
	{
		StringBuilder stringBuilder = new StringBuilder(go.name);
		Transform parent = go.transform.parent;
		while (parent != null && stringBuilder.Length < 120)
		{
			stringBuilder.Insert(0, parent.name + "/");
			parent = parent.parent;
		}
		return stringBuilder.ToString();
	}
}

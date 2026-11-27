using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

namespace LCCompatFixes;

internal sealed class PerfReport : MonoBehaviour
{
	private sealed class Agg
	{
		public int Frames;

		public double Seconds;

		public double MaxMs;

		public int Over33;

		public int Over100;

		public readonly int[] Hist = new int[201];
	}

	private struct Worst
	{
		public double T;

		public double Ms;

		public string Place;
	}

	private struct MemoryCounters
	{
		public uint Size;

		public uint PageFaults;

		public UIntPtr PeakWorkingSet;

		public UIntPtr WorkingSet;

		public UIntPtr QuotaPeakPagedPool;

		public UIntPtr QuotaPagedPool;

		public UIntPtr QuotaPeakNonPagedPool;

		public UIntPtr QuotaNonPagedPool;

		public UIntPtr Pagefile;

		public UIntPtr PeakPagefile;

		public UIntPtr Private;
	}

	private const int Bins = 201;

	private static ManualLogSource log;

	private readonly Dictionary<string, Agg> perPlace = new Dictionary<string, Agg>();

	private readonly List<Worst> worst = new List<Worst>();

	private readonly List<float> window = new List<float>(512);

	private StreamWriter csv;

	private string summaryPath;

	private string place = "start";

	private Agg current;

	private float windowStart;

	private float nextPlace;

	private float nextItems;

	private float nextSummary;

	private int shipItems;

	private double peakWorkingSet;

	private double peakPrivate;

	private double peakUnity;

	private double peakHeap;

	private bool loggedRowFailure;

	private bool loggedSummaryFailure;

	public static void Start(BaseUnityPlugin plugin, ManualLogSource logger)
	{
		log = logger;
		plugin.gameObject.AddComponent<PerfReport>();
	}

	private void Awake()
	{
		try
		{
			string bepInExRootPath = Paths.BepInExRootPath;
			string path = Path.Combine(bepInExRootPath, "perf-report.csv");
			summaryPath = Path.Combine(bepInExRootPath, "perf-summary.txt");
			Rotate(path, Path.Combine(bepInExRootPath, "perf-report.prev.csv"));
			Rotate(summaryPath, Path.Combine(bepInExRootPath, "perf-summary.prev.txt"));
			csv = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false))
			{
				AutoFlush = true
			};
			csv.WriteLine("t_s,place,fps,p99_ms,max_ms,working_set_mb,private_mb,mono_heap_mb,unity_mb,ship_items");
			windowStart = Time.unscaledTime;
			nextSummary = Time.unscaledTime + 30f;
			log.LogInfo("Performance report: writing perf-report.csv and perf-summary.txt next to the logs (local only, nothing personal).");
		}
		catch (Exception ex)
		{
			log.LogWarning("Performance report disabled: " + ex.Message);
			base.enabled = false;
		}
	}

	[DllImport("psapi.dll")]
	private static extern bool GetProcessMemoryInfo(IntPtr process, out MemoryCounters counters, uint size);

	[DllImport("kernel32.dll")]
	private static extern IntPtr GetCurrentProcess();

	private static void ProcessMemory(out double workingSetMb, out double privateMb)
	{
		workingSetMb = (privateMb = 0.0);
		try
		{
			if (GetProcessMemoryInfo(GetCurrentProcess(), out var counters, (uint)Marshal.SizeOf(typeof(MemoryCounters))))
			{
				workingSetMb = (double)counters.WorkingSet.ToUInt64() / 1048576.0;
				privateMb = (double)counters.Private.ToUInt64() / 1048576.0;
				return;
			}
		}
		catch
		{
		}
		try
		{
			using Process process = Process.GetCurrentProcess();
			workingSetMb = (double)process.WorkingSet64 / 1048576.0;
			privateMb = (double)process.PrivateMemorySize64 / 1048576.0;
		}
		catch
		{
		}
	}

	private static void Rotate(string path, string previous)
	{
		try
		{
			if (File.Exists(path))
			{
				if (File.Exists(previous))
				{
					File.Delete(previous);
				}
				File.Move(path, previous);
			}
		}
		catch
		{
		}
	}

	private static string DescribeHardware()
	{
		Resolution currentResolution = Screen.currentResolution;
		return string.Format(CultureInfo.InvariantCulture, "GPU: {0} ({1} MB), driver API {2}\nCPU: {3}, {4} threads\nRAM: {5} MB\nOS: {6}\nScreen: {7}x{8} @ {9} Hz, window {10}x{11}", SystemInfo.graphicsDeviceName, SystemInfo.graphicsMemorySize, SystemInfo.graphicsDeviceType, SystemInfo.processorType, SystemInfo.processorCount, SystemInfo.systemMemorySize, SystemInfo.operatingSystem, currentResolution.width, currentResolution.height, Math.Round(currentResolution.refreshRateRatio.value), Screen.width, Screen.height);
	}

	private static string ComputePlace()
	{
		StartOfRound instance = StartOfRound.Instance;
		if ((UnityEngine.Object)(object)instance == null)
		{
			if (!(SceneManager.GetActiveScene().name == "MainMenu"))
			{
				return "loading";
			}
			return "menu";
		}
		string text = (instance.inShipPhase ? "orbit" : ("moon:" + ((instance.currentLevel != null) ? instance.currentLevel.PlanetName : "?")));
		GameNetworkManager instance2 = GameNetworkManager.Instance;
		PlayerControllerB playerControllerB = ((instance2 != null) ? instance2.localPlayerController : null);
		if ((UnityEngine.Object)(object)playerControllerB != null)
		{
			text += (playerControllerB.isInsideFactory ? ":dungeon" : (playerControllerB.isInHangarShipRoom ? ":ship" : ":outside"));
		}
		return text.Replace(',', ' ');
	}

	private void Update()
	{
		float unscaledTime = Time.unscaledTime;
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		if (unscaledTime >= nextPlace || current == null)
		{
			nextPlace = unscaledTime + 0.25f;
			string text;
			try
			{
				text = ComputePlace();
			}
			catch
			{
				text = "unknown";
			}
			if (text != place || current == null)
			{
				place = text;
				if (!perPlace.TryGetValue(place, out current))
				{
					current = new Agg();
					perPlace[place] = current;
				}
			}
		}
		if (unscaledTime >= nextItems)
		{
			nextItems = unscaledTime + 5f;
			try
			{
				shipItems = (((UnityEngine.Object)(object)StartOfRound.Instance != null) ? UnityEngine.Object.FindObjectsOfType<GrabbableObject>().Count((GrabbableObject g) => (UnityEngine.Object)(object)g != null && g.isInShipRoom) : 0);
			}
			catch
			{
				shipItems = -1;
			}
		}
		double num = (double)unscaledDeltaTime * 1000.0;
		window.Add(unscaledDeltaTime);
		current.Frames++;
		current.Seconds += unscaledDeltaTime;
		current.Hist[Math.Min(200, (int)num)]++;
		if (num > current.MaxMs)
		{
			current.MaxMs = num;
		}
		if (num > 33.4)
		{
			current.Over33++;
		}
		if (num >= 100.0)
		{
			current.Over100++;
			worst.Add(new Worst
			{
				T = unscaledTime,
				Ms = num,
				Place = place
			});
			if (worst.Count > 64)
			{
				worst.Sort((Worst a, Worst b) => b.Ms.CompareTo(a.Ms));
				worst.RemoveRange(8, worst.Count - 8);
			}
		}
		if (unscaledTime - windowStart >= 1f)
		{
			WriteRow(unscaledTime);
		}
		if (unscaledTime >= nextSummary)
		{
			nextSummary = unscaledTime + 30f;
			WriteSummary();
		}
	}

	private void WriteRow(float now)
	{
		try
		{
			float num = 0f;
			float num2 = 0f;
			foreach (float item in window)
			{
				num += item;
				if (item > num2)
				{
					num2 = item;
				}
			}
			window.Sort();
			float num4 = ((window.Count > 0) ? window[Math.Min(window.Count - 1, (int)((double)window.Count * 0.99))] : 0f);
			float num5 = ((num > 0f) ? ((float)window.Count / num) : 0f);
			ProcessMemory(out var workingSetMb, out var privateMb);
			double num6 = (double)GC.GetTotalMemory(forceFullCollection: false) / 1048576.0;
			double num7 = (double)Profiler.GetTotalAllocatedMemoryLong() / 1048576.0;
			peakWorkingSet = Math.Max(peakWorkingSet, workingSetMb);
			peakPrivate = Math.Max(peakPrivate, privateMb);
			peakHeap = Math.Max(peakHeap, num6);
			peakUnity = Math.Max(peakUnity, num7);
			csv.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0:0.0},{1},{2:0.0},{3:0.0},{4:0.0},{5:0},{6:0},{7:0},{8:0},{9}", now, place, num5, num4 * 1000f, num2 * 1000f, workingSetMb, privateMb, num6, num7, shipItems));
		}
		catch (Exception ex)
		{
			if (!loggedRowFailure)
			{
				loggedRowFailure = true;
				log.LogWarning("Performance report row failed: " + ex.Message + " (further failures are not logged)");
			}
		}
		window.Clear();
		windowStart = now;
	}

	private static double Percentile(Agg a, double q)
	{
		if (a.Frames == 0)
		{
			return 0.0;
		}
		double num = (double)a.Frames * q;
		double num2 = 0.0;
		for (int i = 0; i < 201; i++)
		{
			num2 += (double)a.Hist[i];
			if (num2 >= num)
			{
				return i + 1;
			}
		}
		return 201.0;
	}

	private void WriteSummary()
	{
		try
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("LCCompatFixes performance summary (frame times in ms; percentiles are accurate to 1 ms, 200 means 200 or more)");
			stringBuilder.AppendLine(DescribeHardware());
			stringBuilder.AppendLine(string.Format(invariantCulture, "Peak memory: working set {0:0} MB, private {1:0} MB, Unity allocated {2:0} MB, Mono heap {3:0} MB", peakWorkingSet, peakPrivate, peakUnity, peakHeap));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("place                          seconds   frames   fps    p50   p95   p99    max  >33ms  >100ms");
			foreach (KeyValuePair<string, Agg> item in perPlace.OrderByDescending((KeyValuePair<string, Agg> p) => p.Value.Seconds))
			{
				Agg value = item.Value;
				if (value.Frames != 0)
				{
					stringBuilder.AppendLine(string.Format(invariantCulture, "{0,-30} {1,7:0} {2,8} {3,5:0.0} {4,6:0} {5,5:0} {6,5:0} {7,6:0} {8,5:0.0}% {9,7}", item.Key, value.Seconds, value.Frames, (double)value.Frames / Math.Max(0.001, value.Seconds), Percentile(value, 0.5), Percentile(value, 0.95), Percentile(value, 0.99), value.MaxMs, 100.0 * (double)value.Over33 / (double)value.Frames, value.Over100));
				}
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Longest frames (seconds since the game started, place, ms):");
			foreach (Worst item2 in this.worst.OrderByDescending((Worst w) => w.Ms).Take(8))
			{
				stringBuilder.AppendLine(string.Format(invariantCulture, "  {0,8:0.0} s  {1,-30} {2:0} ms", item2.T, item2.Place, item2.Ms));
			}
			File.WriteAllText(summaryPath, stringBuilder.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
		}
		catch (Exception ex)
		{
			if (!loggedSummaryFailure)
			{
				loggedSummaryFailure = true;
				log.LogWarning("Performance summary failed: " + ex.Message + " (further failures are not logged)");
			}
		}
	}

	private void OnApplicationQuit() => Finish();

	internal void Finish()
	{
		WriteSummary();
		try
		{
			csv.Dispose();
		}
		catch
		{
		}
	}
}

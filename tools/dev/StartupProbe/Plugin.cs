using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Temporary harness (never shipped): where does the time go between process start and a usable main menu?
// 1. A background thread samples the main thread every 200 ms. While the main thread is suspended it only reads its
//    registers and copies the top 256 KB of its stack (no allocation, no locks, every method pre-compiled on a dummy thread).
//    After the thread is resumed, the copy is scanned for return addresses into JIT-compiled code and named with mono_pmip.
// 2. A MonoBehaviour logs every frame longer than 200 ms.
// 3. Scene loads are timestamped. Output: BepInEx/p_startup.txt (age = seconds since the process started).
[BepInPlugin(".0startupprobe", "StartupProbe", "0.0.1")]
public class Plugin : BaseUnityPlugin
{
    [DllImport("kernel32")] private static extern uint GetCurrentThreadId();
    [DllImport("kernel32")] private static extern IntPtr OpenThread(uint access, bool inherit, uint tid);
    [DllImport("kernel32")] private static extern uint SuspendThread(IntPtr h);
    [DllImport("kernel32")] private static extern uint ResumeThread(IntPtr h);
    [DllImport("kernel32")] private static extern bool GetThreadContext(IntPtr h, IntPtr ctx);
    [DllImport("kernel32")] private static extern void GetCurrentThreadStackLimits(out ulong low, out ulong high);
    [DllImport("kernel32")] private static extern bool GetModuleHandleExA(uint flags, IntPtr addr, out IntPtr module);
    [DllImport("kernel32")] private static extern uint GetModuleFileNameA(IntPtr module, StringBuilder name, uint size);
    [DllImport("mono-2.0-bdwgc", EntryPoint = "mono_pmip")] private static extern IntPtr mono_pmip(IntPtr ip);

    private const int StackCopy = 256 * 1024;
    private static string outPath;
    private static uint mainTid;
    private static ulong mainLow, mainHigh;
    private static volatile bool stop;
    private static volatile string scene = "-";
    private static double ageAtAwake;
    private static readonly Stopwatch sw = Stopwatch.StartNew();
    private static int samples, errors;

    private static double Age() { return ageAtAwake + sw.Elapsed.TotalSeconds; }
    private static void Log(string s) { try { File.AppendAllText(outPath, s + "\n"); } catch { } }

    private void Awake()
    {
        outPath = Path.Combine(Paths.BepInExRootPath, "p_startup.txt");
        File.WriteAllText(outPath, "");
        ageAtAwake = (DateTime.Now - Process.GetCurrentProcess().StartTime).TotalSeconds;
        mainTid = GetCurrentThreadId();
        GetCurrentThreadStackLimits(out mainLow, out mainHigh);
        Log("INIT processAgeAtAwake=" + ageAtAwake.ToString("0.0") + "s mainTid=" + mainTid + " stack=" + ((mainHigh - mainLow) / 1024) + "KB");
        if (Environment.GetEnvironmentVariable("PROBE_NOSAMPLE") == null)
        {
            var t = new Thread(Sample) { IsBackground = true, Name = "StartupProbeSampler" };
            t.Start();
        }
        else Log("sampler off (PROBE_NOSAMPLE): timing only");
        gameObject.AddComponent<Hitch>();
        SceneManager.sceneLoaded += (s, m) =>
        {
            scene = s.name;
            Log("SCENE " + Age().ToString("0.0") + " " + s.name + " " + m);
            if (s.name == "InitSceneLaunchOptions") StartCoroutine(GoLan());
            if (s.name == "MainMenu") StartCoroutine(QuitLater());
        };
    }

    private IEnumerator GoLan()
    {
        yield return new WaitForSeconds(4f);
        var lan = Resources.FindObjectsOfTypeAll<Button>().FirstOrDefault(b => b != null && b.gameObject.scene.IsValid() && b.name.ToUpperInvariant().Contains("LAN"));
        if (lan != null) lan.onClick.Invoke(); else SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator QuitLater()
    {
        yield return new WaitForSecondsRealtime(95f);
        Log("QUIT " + Age().ToString("0.0") + " samples=" + samples);
        stop = true;
        Application.Quit();
    }

    // Runs with the target thread suspended: no allocation, no locks, only calls that were exercised before.
    private static int Capture(IntPtr h, IntPtr ctx, byte[] buf, ulong low, ulong high, out ulong rip)
    {
        rip = 0;
        Marshal.WriteInt32(ctx, 0x30, 0x100003);        // CONTEXT_AMD64 | CONTROL | INTEGER
        if (!GetThreadContext(h, ctx)) return -1;
        rip = (ulong)Marshal.ReadInt64(ctx, 0xF8);
        ulong rsp = (ulong)Marshal.ReadInt64(ctx, 0x98);
        if (rsp < low || rsp >= high) return -2;
        ulong avail = high - rsp;
        int n = avail < (ulong)buf.Length ? (int)avail : buf.Length;
        Marshal.Copy((IntPtr)(long)rsp, buf, 0, n);
        return n;
    }

    private static string PmipName(ulong pc)
    {
        if (pc < 0x10000UL || pc > 0x00007FFFFFFFFFFFUL) return null;
        var p = mono_pmip((IntPtr)(long)pc);
        if (p == IntPtr.Zero) return null;
        var s = Marshal.PtrToStringAnsi(p);
        if (string.IsNullOrEmpty(s)) return null;
        var plus = s.LastIndexOf(" + 0x", StringComparison.Ordinal);
        if (plus > 0) s = s.Substring(0, plus);
        return s.Replace(" < ", "_");
    }

    private static string Describe(ulong pc)
    {
        var n = PmipName(pc);
        if (n != null) return n;
        IntPtr mod;
        if (GetModuleHandleExA(6, (IntPtr)(long)pc, out mod) && mod != IntPtr.Zero)
        {
            var sb = new StringBuilder(260);
            GetModuleFileNameA(mod, sb, 260);
            return Path.GetFileName(sb.ToString()) + "+0x" + (pc - (ulong)(long)mod).ToString("x");
        }
        return "0x" + pc.ToString("x");
    }

    private static void Sample()
    {
        var buf = new byte[StackCopy];
        var raw = Marshal.AllocHGlobal(1232 + 32);
        var ctx = (IntPtr)(((long)raw + 15) & ~15L);

        // warm-up on a dummy thread, so every wrapper and method used while the real target is suspended is compiled already
        uint dummyTid = 0; ulong dLow = 0, dHigh = 0;
        var ready = new ManualResetEvent(false);
        var dummy = new Thread(() => { dummyTid = GetCurrentThreadId(); GetCurrentThreadStackLimits(out dLow, out dHigh); ready.Set(); while (!stop) Thread.Sleep(20); }) { IsBackground = true };
        dummy.Start();
        ready.WaitOne();
        Thread.Sleep(100);
        var dh = OpenThread(0x0002 | 0x0008 | 0x0040, false, dummyTid);
        for (int i = 0; i < 5; i++)
        {
            SuspendThread(dh); ulong r; int n = Capture(dh, ctx, buf, dLow, dHigh, out r); ResumeThread(dh);
            if (n > 0) { for (int k = 0; k + 8 <= 4096 && k + 8 <= n; k += 8) PmipName(BitConverter.ToUInt64(buf, k)); Describe(r); }
            Thread.Sleep(20);
        }

        var h = OpenThread(0x0002 | 0x0008 | 0x0040, false, mainTid);
        if (h == IntPtr.Zero) { Log("OpenThread failed"); return; }
        while (!stop)
        {
            Thread.Sleep(200);
            ulong rip; int len;
            SuspendThread(h);
            len = Capture(h, ctx, buf, mainLow, mainHigh, out rip);
            ResumeThread(h);
            if (len <= 0) { if (++errors >= 5) { Log("sampler disabled (" + len + ")"); return; } continue; }
            var frames = new List<string>(48) { Describe(rip) };
            string last = frames[0];
            for (int i = 0; i + 8 <= len && frames.Count < 48; i += 8)
            {
                var nm = PmipName(BitConverter.ToUInt64(buf, i));
                if (nm != null && nm != last) { frames.Add(nm); last = nm; }
            }
            samples++;
            Log("S " + Age().ToString("0.0") + " " + scene + " | " + string.Join(" < ", frames.ToArray()));
        }
    }

    private class Hitch : MonoBehaviour
    {
        private void Update()
        {
            var dt = Time.unscaledDeltaTime;
            if (dt > 0.2f) Log("H " + Age().ToString("0.0") + " dt=" + dt.ToString("0.00") + " scene=" + scene + " frame=" + Time.frameCount);
        }
    }
}

# Developer tools (not shipped)

All scripts are PowerShell + Mono.Cecil (the `Mono.Cecil.dll` that already ships in `BepInEx/core`), so they need nothing installed.
Edit the paths at the top of each script (`$Root` = your BepInEx folder, game path) before use.

| Script | Purpose |
|---|---|
| `check-compat.ps1` | After a **game update**: lists every reference from every mod DLL to game code (methods, fields, types) that no longer exists in `Assembly-CSharp.dll`. This is how LethalCasino's two broken references were found. |
| `listapi.ps1` / `listall.ps1` | Print the public (or all) members of types in any DLL: handy for reading Unity / HDRP / mod APIs. |
| `ildump.ps1` | Dump the IL of a method (e.g. `ildump.ps1 -Type SettingsOption -Methods OnEnable`). |
| `findstr.ps1` / `findcall.ps1` | Find which method uses a string / calls a member. |
| `extract_rtlc_keys.py` | Rebuild `LanguageSwitch/translations/rtlc_keys_en.json` (English game strings) from an RTLC dictionary folder. |
| `UiProbe/Plugin.cs` | Test harness plugin: skips the launch-options screen, opens the Settings panel and the mods' pages by code, dumps the UI hierarchy and writes screenshots (`ScreenCapture`) next to `BepInEx/`, drives the controls and quits. Build it like the other plugins (copy `build.ps1`'s `csc` call) and drop the DLL into `BepInEx/plugins/`. **Never ship it.** |
| `ScrollProbe/Plugin.cs` | Smaller harness of the same kind: opens Graphics+ / Language and sends mouse-wheel events to several screen points through the real raycast path, logging which object handled each one and how far the list moved. **Never ship it.** |
| `StartupProbe/Plugin.cs` + `stack-summary.ps1` + `startup-metrics.ps1` | Where does the start-up time go? The probe logs every frame over 200 ms and every scene load (seconds since the process started) and, unless `PROBE_NOSAMPLE` is set, samples the main thread's stack five times a second: it suspends the thread for a moment, copies the top of its stack, and names the return addresses into JIT-compiled code with `mono_pmip` (x64 Windows only; no allocation or locking while the thread is suspended). `stack-summary.ps1` groups the samples by call path, `startup-metrics.ps1` prints the numbers to compare before/after a change (time to the main menu, frozen seconds). Run the timing-only variant (`$env:PROBE_NOSAMPLE=1`) for comparisons: the sampler itself slows the game a little. **Never ship it.** |
| `InjectCheck/Plugin.cs` | Checks that the Graphics+ button and the Language dropdown were added to the Settings panel in the main menu and in the pause menu of an offline lobby. **Never ship it.** |
| `LandingProbe/Plugin.cs` + `run_probe.ps1` | Starts an offline LAN lobby, flies to Experimentation, pulls the lever and logs every frame over 100 ms, the slow level-loading methods and what other mods hook them. `run_probe.ps1 -Name LandingProbe -Pattern ...` builds a probe, runs the game once, restores the mod configs and prints the matching log lines. Paths are those of the author's machine. **Never ship a probe.** |

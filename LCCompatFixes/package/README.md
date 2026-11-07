# LCCompatFixes

Fixes for mods that broke after the Lethal Company update of 31 Oct 2026 (Unity 2022.3.62), made **without editing
those mods or any game file**. Every fix is a separate, switchable patch.

## What it fixes

| Fix | Problem | How |
|---|---|---|
| LethalCasino errors | `MissingMethodException: MenuManager.DisplayMenuNotification(string,string)` on every main-menu load and `MissingFieldException: HUDManager.loadingDarkenScreen` in the roulette HUD. The game update changed/removed both. | A small BepInEx *patcher* adds the old method overload and the removed field back **in memory** while the game starts (nothing on disk changes), and the plugin gives the field a harmless stub image. |
| RTLC Russian sound packs not loading | `loaforcsSoundAPI` 2.x rejects pack names containing a dot (`'name' can not contain special character '.'`), so the RTLC voice packs silently never loaded. | Replaces `.` with `_` in the pack name just before validation. The pack files are not modified. |

Each fix does nothing if the problem is not present (for example when LethalCasino is not installed, or when a game
update restores the member), and writes a line to the log saying so.

## Settings

`BepInEx/config/lc.compatfixes.cfg`

* `StubLoadingDarkenScreen` (default on)
* `SoundApiPackNameDots` (default on)

## Safety

* The patcher only **adds** members. If the game already has them, or has changed again, it logs and skips.
* Every step is wrapped so that a failure is logged and never stops the game from starting.
* Installed on its own it does nothing visible.

## Reporting problems

Please attach `BepInEx/LogOutput.log` (look for `[LCCompatFixes]` and `[LCCompatFixes.Patcher]`).

---

**По-русски:** исправляет моды, сломанные обновлением игры от 31.10.2026 (LethalCasino, русские звуковые паки RTLC),
не изменяя сами моды и файлы игры: недостающие элементы добавляются в память при запуске.

*Developed by devil1nme with AI assistance (Claude). MIT license.*

## More fixes

| Problem | What it does | Config switch |
|---|---|---|
| **EladsHUD+ crashes in `Awake`** (the HUD never appears). `AssetBundle.LoadFromStream` returns `null` although Unity did load the bundle (seen with BepInEx_Faster_Load_AssetBundles_Patcher and XUnity both installed). | When the call returns `null` but exactly one new bundle appeared during it, that bundle is handed to the caller. | `StreamBundleResult` (default on) |
| **A 1-2 second freeze when the ship lands** with the RTLC Russian translation. RTLC and RTLC.Mods each walk every texture in memory in one frame on every level load (measured 1.5 s + 0.6 s). | The same walk runs about 3 ms per frame from a coroutine; textures handled on an earlier level load are skipped. Russian textures appear a moment later. | `RtlcSceneLoadSlicing` (default on) |
| **Slow loading: the same failing type scan over and over.** Mods look types up by name; HarmonyX then calls `Assembly.GetTypes()` on every assembly, and for assemblies that cannot load fully (they reference mods you do not have) that throws, about 130 times per session. | Each assembly is scanned once and the answer is remembered. | `CacheTypeScans` (default on) |
| **Long start and freezes of 10-20 seconds in the main menu.** `AccessTools.TypeByName` (also behind `AccessTools.Method("Type:Method")`) builds the full name of every type in every loaded assembly, twice, on each call; looking up a mod that is not installed takes 2-6 seconds. LazyLobby, LazyLanding and WeatherRegistry do dozens of such lookups at startup and in the menu, about half of the dead time of a 100-mod profile. | Names are looked up with the runtime's own per-assembly lookup and the answers (including misses) are remembered. The result is the same type as before; the "Could not find type named" warning for a miss is no longer printed. | `FastTypeByName` (default on) |

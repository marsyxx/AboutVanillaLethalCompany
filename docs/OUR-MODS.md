# Our mods (devil1nme)

BepInEx mods for **Lethal Company** by **devil1nme** (Thunderstore team name).

| Mod | What it does | Package |
|---|---|---|
| [GraphicsPlus](../GraphicsPlus) | Graphics options in the game's own Settings screen (a button inside its Graphics box): render resolution, **DLSS** (NVIDIA RTX), anti-aliasing (FXAA / SMAA / TAA), shadows, textures, ambient occlusion, effects. Everything defaults to vanilla; while it is in use the game's own Graphics controls are locked. | `devil1nme-GraphicsPlus` |
| [LanguageSwitch](../LanguageSwitch) | English / Russian / German dropdown in the main Settings screen, applied live through XUnity.AutoTranslator. Ships a German dictionary for the game's own text; keeps Russian / German main-menu items clear of the logo. | `devil1nme-LanguageSwitch` |
| [LCCompatFixes](../LCCompatFixes) | Fixes for other mods **without editing them**: LethalCasino and RTLC sound packs after the 31 Oct 2026 update (preloader patcher + shims), EladsHUD+ crash, the 1-2 s landing freeze caused by the RTLC texture pass, repeated slow HarmonyX type scans and type-name lookups (half of the dead time at start-up), LethalRPG Strength vs BetterStamina patch order. Every fix has a config switch. | `devil1nme-LCCompatFixes` |

Part of the [AboutVanillaLethalCompany](../README.md) modpack. Target: Lethal Company build of 31 Oct 2026 (Unity 2022.3.62f2, Direct3D 11), BepInEx 5.4.2305.

## Build

Needs the **.NET SDK** (any recent one; only its Roslyn compiler is used) and a BepInEx 5 `core` folder. No NuGet, works offline.

```powershell
powershell -ExecutionPolicy Bypass -File tools\build.ps1            # all mods
powershell -ExecutionPolicy Bypass -File tools\build.ps1 -Only GraphicsPlus
# other locations:  -GameDir "D:\...\Lethal Company"  -BepInExCore "D:\...\BepInEx\core"
```

Output: `build\<Mod>\*.dll` and one Thunderstore-ready zip per mod in `dist\` (files at the zip root, forward slashes,
manifest / icon / README checks done the way Thunderstore validates them).

The German dictionary is generated: `python LanguageSwitch\translations\build_de.py` (reads `de_source.txt`, writes
`LanguageSwitch\package\config\Translation_de\Game\*.txt`).

## Layout

```
GraphicsPlus/   src/ (Plugin, Settings, Applier)   package/ (manifest.json, README.md, CHANGELOG.md, icon.png, LICENSE)
LanguageSwitch/ src/ (Plugin)  translations/ (German sources + generator)  package/ (… + config/Translation_de)
LCCompatFixes/  src/ (Patcher, Plugin)             package/
Shared/         SettingsMenu.cs   compiled into GraphicsPlus and LanguageSwitch under each mod's own namespace
tools/          build.ps1   dev/ (compat checker, Cecil helpers, UI test harness)
docs/           this file, PUBLISHING.md (the plan for releasing on Thunderstore)
modpack/        the modpack: mod list, profile file, configs (see ../README.md)
```

## How the Settings screen integration works

`Shared/SettingsMenu.cs` finds the game's `SettingsPanel` (main menu and pause menu), clones the game's own caption,
slider, dropdown and button, strips their link to the game's settings logic (`SettingsOption` component removed,
serialized listeners switched off), and builds a scrollable page opened by one extra button. If the panel layout is not
what is expected, nothing is added and the vanilla screen stays untouched.

## Testing without a mouse

`tools/dev/UiProbe` opens the panels by code, drives the controls and writes screenshots, so UI changes can be checked
from the log and the images. `tools/dev/LandingProbe` + `run_probe.ps1` start an offline LAN lobby, fly to a moon, pull the
lever and log frame spikes and slow hooks, which is how the landing freezes were measured.

## License

MIT (see `LICENSE`). Developed by devil1nme with AI assistance (Claude).

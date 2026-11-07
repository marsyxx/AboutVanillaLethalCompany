# GraphicsPlus

Adds the graphics options Lethal Company never had to the game's own **Settings** screen: render resolution,
anti-aliasing, shadows, textures, ambient occlusion and post-processing effects.

**Everything defaults to "Vanilla".** Installing the mod changes nothing until you move a setting, and every change
applies live: no restart, no reload.

## Where to find it

Main menu or pause menu -> **Settings** -> the **Graphics+** button at the bottom of the *Graphics* box. It opens a full-screen page with
a scroll bar; the mouse wheel scrolls the list wherever the cursor is on the page. The same options are also listed in the
[LethalConfig](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/) mod menu if you have it, and in
`BepInEx/config/lc.graphicsplus.cfg`.

The page follows the game language: English by default, Russian and German when
[LanguageSwitch](https://thunderstore.io/c/lethal-company/p/devil1nme/LanguageSwitch/) (or XUnity.AutoTranslator) is set to
that language. The texts come from `BepInEx/config/Translation/ru/Text/GraphicsPlus.txt` and
`BepInEx/config/Translation_de/Mods/GraphicsPlus.txt`; you can edit them.

## Options

| Group | Option | What it does |
|---|---|---|
| Resolution | Resolution multiplier (0.5 - 3.0) | Multiplies the size of the image the game renders. 1.0 = what the game / your other mods use. Cost grows with the square of the value. Limited automatically on cards with little video memory. |
| Resolution | Upscale filter | Point (crisp pixels), Bilinear or Trilinear when the image is stretched to your screen. |
| Anti-aliasing | Mode | Vanilla, Off, FXAA, SMAA or TAA. (Vanilla Lethal Company has **no** anti-aliasing.) |
| Anti-aliasing | Quality, TAA sharpness | Quality of SMAA / TAA and the sharpening applied by TAA. |
| Shadows | Shadow distance (x0.25 - x3) | How far from the camera shadows are drawn. |
| Shadows | Shadow resolution | Low / Medium / High / Ultra shadow sharpness for lights. |
| Shadows | Contact shadows | Small sharp shadows where objects touch surfaces. |
| Textures and detail | Texture quality | Full / Half / Quarter size textures. |
| Textures and detail | Anisotropic filtering | Sharper textures on floors and walls seen at a shallow angle. |
| Textures and detail | Model detail distance | Keeps detailed models visible further away. |
| Lighting and effects | Ambient occlusion | Soft darkening in corners and crevices. |
| Lighting and effects | Vignette, Bloom, Lens distortion, Film grain, Chromatic aberration, Depth of field | Switch individual effects off (film grain is used in the game's levels; the others only matter where a scene or mod uses them). |
| Lighting and effects | Retro colour pass (experimental) | Switches off the game's custom render pass (the colour-graded retro look). |

The game's own options (gamma, frame-rate cap, window mode, pixel resolution, motion blur, grass distance, advanced
lighting) stay exactly where they were.

## Compatibility

* **ButteryFixes**: its "high resolution" setting is respected; the multiplier is applied on top of whatever size the
  game or other mods choose.
* **MemoryJanitor**: it forces a texture mip limit on every scene load. When it is installed, *Texture quality* edits
  MemoryJanitor's own `MipmapLimit` setting instead of fighting it.
* **LethalPerformance, CullFactory, LobbyControl, MoreCompany**: not touched. GraphicsPlus only changes the camera image,
  volume overrides, light shadow settings and `QualitySettings`.
* Works in single player and in multiplayer: all options are client-side and nobody else needs the mod.

## Safety

* Nothing is applied until you change a setting; "Vanilla" always restores what the game had.
* If an option fails, the error is written once to the BepInEx log and the game carries on; the vanilla value stays.
* Resolution is clamped to what your video memory can hold, and never beyond 8192 pixels on a side.
* Removing the mod restores everything on the next launch (no game files are modified).

## Known limits

* Higher resolution, shadow resolution and ambient occlusion cost GPU time. If the frame rate drops, lower them.
* Options that a particular scene does not use (for example film grain on moons that have none) have no visible effect.
* The *Retro colour pass* option is experimental.

## Reporting problems

Please attach `BepInEx/LogOutput.log` (look for lines starting with `[GraphicsPlus]`).

---

**По-русски:** мод добавляет в настройки игры недостающие графические опции (разрешение рендера, сглаживание,
тени, текстуры, затенение углов, эффекты). По умолчанию всё «как в игре», изменения применяются сразу, без перезапуска.
Кнопка **GRAPHICS+** находится в меню **Настройки**.

*Developed by devil1nme with AI assistance (Claude). MIT license.*

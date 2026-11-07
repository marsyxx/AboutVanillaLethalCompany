## 1.0.0
- Fixed: in the pause menu in a game the Graphics+ page opened empty (the page was drawn on the wrong UI layer and the game's UI camera skipped it).
- The Graphics+ button sits inside the Graphics box of the Settings screen. While any Graphics+ option is changed from its default, the game's own Graphics controls are greyed out and locked (switch off with the "Lock vanilla Graphics settings" option).
- DLSS (NVIDIA RTX cards only; the row is hidden elsewhere): Ultra performance / Performance / Balanced / Quality. Uses the DLSS pass already inside the game; nothing from NVIDIA is shipped.
- Mouse wheel moves the list one row per notch; the list can also be dragged with the left mouse button from anywhere on the page.
- The page has no background of its own, so it shows whatever the game draws behind its Settings screen; headings use the game's own red. The game's controls are hidden while the page is open.
- First release.
- Resolution multiplier and upscale filter, anti-aliasing (FXAA / SMAA / TAA), shadow distance / resolution / contact shadows,
  texture quality, anisotropic filtering, model detail distance, ambient occlusion and individual effect toggles.
- The mouse wheel scrolls the page with the cursor anywhere on it (not only over the list or the scroll bar).
- Options appear on a full-screen, scrollable page opened by the Graphics+ button in the game's Settings screen (animated like the game's own buttons), in LethalConfig and in the config file.
- Page text follows the game language (English, Russian, German).

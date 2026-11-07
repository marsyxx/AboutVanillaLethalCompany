# XUnity.AutoTranslator

**Advanced translator plugin for Unity-based games.**

This Thunderstore package provides **XUnity.AutoTranslator** as a shared dependency for translation mods. It can translate Unity game text automatically and also provides the tools needed for manual, curated translations.

> **Original project:** [bbepis/XUnity.AutoTranslator](https://github.com/bbepis/XUnity.AutoTranslator)  
> **Original author:** [bbepis](https://github.com/bbepis)  
> **Thunderstore dependency ID:** `Hayrizan-XUnity_AutoTranslator`  
> **Latest upstream release checked:** `v5.6.1` — 19.04.2026

---

## What this package is for

Use this package when your mod, translation pack, or localization project needs **XUnity.AutoTranslator** but you do not want to bundle the same XUnity files directly inside your archive.

This is especially useful for Thunderstore packages because multiple DLLs and duplicated XUnity files can cause upload or dependency issues such as:

```text
Package rejected - Invalid submission
```

Instead of shipping XUnity.AutoTranslator inside every localization archive, add this package as a dependency and keep your own mod archive cleaner.

---

## Features

- Automatic translation for Unity-based games.
- Manual translation support through cached translation files.
- Works well as a base dependency for localization mods.
- Supports text replacement in many Unity UI systems.
- Useful for translating game UI, menus, dialogue, item names, mod text, and other in-game strings.
- Can be used together with curated translation files for better quality than raw machine translation.

---

## Installation

### Recommended: mod manager

Install this package through one of the supported Thunderstore-compatible mod managers:

- Thunderstore Mod Manager
- r2modman
- Gale Mod Manager

The manager will install this package automatically if another mod lists it as a dependency.

### Manual installation

1. Install the required mod loader for your game, usually **BepInEx**.
2. Download this package manually from Thunderstore.
3. Extract the archive into the game folder, where the game executable is located.
4. Start the game once so the plugin can generate its configuration and translation folders.

For most BepInEx-based games, configuration files are generated inside:

```text
BepInEx/config/
```

Translation files are usually stored inside:

```text
BepInEx/Translation/
```

Exact paths can differ depending on the game, loader, and XUnity.AutoTranslator build.

---

## Adding this as a dependency

If you are publishing a Thunderstore translation mod, add XUnity.AutoTranslator to your `manifest.json` dependencies.

Example:

```json
{
  "dependencies": [
    "Hayrizan-XUnity_AutoTranslator-5.6.1"
  ]
}
```

Use the version that matches the package version you are targeting.

---

## Important notes

- XUnity.AutoTranslator can use online translation services. If automatic translation is enabled, untranslated text may be sent to the selected translation endpoint.
- This package is a dependency/helper package, not an official replacement for the original GitHub repository.
- For full documentation, advanced configuration, manual translation rules, texture translation, and integration examples, use the original project documentation.
- If a game already includes another XUnity.AutoTranslator build, avoid installing duplicate versions at the same time.

---

## Upstream status

The latest upstream release checked while updating this README is **v5.6.1**.

Recent upstream highlights:

- `v5.6.1` — fixed IL2CPP font asset bundle loading issues and IL2CPP hotkey/GUI unstripping problems.
- `v5.6` — added UIElements support and fixed Papago translator compatibility with the current website structure.
- `v5.5.2` — fixed DeepL API authentication and an IL2CPP override-font issue.
- `v5.5.1` — improved input handling through UnityInput for legacy and new Unity input systems.
- `v5.5.0` — added initialization hooks for other plugins, `XUAIGNORETREE`, Yandex Translate API v2 support, and a new TMP font asset bundle.

See the included changelog for the full version history.

---

## Useful links

- [Original GitHub repository](https://github.com/bbepis/XUnity.AutoTranslator)
- [GitHub releases](https://github.com/bbepis/XUnity.AutoTranslator/releases)
- [Thunderstore package page](https://thunderstore.io/c/lethal-company/p/Hayrizan/XUnity_AutoTranslator/)

---

## Наши переводы

- [RTLC Russian Translation для Lethal Company](https://thunderstore.io/c/lethal-company/p/Hayrizan/RTLC_Russian_Translation/)
- [PEAK Russian Translation](https://thunderstore.io/c/peak/p/RTLC/PEAK_Russian_Translation/)
- [REPO Russian Translation](https://thunderstore.io/c/repo/p/RTLC/REPO_Russian_Translation/)

---

## Credits

- **XUnity.AutoTranslator:** [bbepis](https://github.com/bbepis)
- **Thunderstore package / redistribution helper:** Hayrizan / RTLC
- **Original license:** MIT License

This package exists to make localization mods easier to publish, install, and maintain.

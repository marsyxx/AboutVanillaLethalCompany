# Changelog

<details>
<summary><strong>v5.6.1</strong> — 2026-04-19</summary>

### Fixes

- Fixed font asset bundle loading failure in IL2CPP by @sorrowmoil in #831.
- Fixed hotkey unresponsiveness and GUI unstripping errors in IL2CPP by @sorrowmoil in #834.

</details>

<details>
<summary><strong>v5.6.0</strong> — 2026-04-10</summary>

### Added

- Added UIElements support by @axxag in #818.

### Fixes

- Fixed Papago translator JS chunk pattern to match the current website structure by @markruler in #820.

### Documentation

- Added AutoPollinationTranslator to the third-party translator list in the README by @kikyo2006 in #823.

</details>

<details>
<summary><strong>v5.5.2</strong> — 2026-03-06</summary>

### Fixes

- Fixed DeepL API authentication to use the `Authorization` header instead of legacy query/body parameters by @markruler in #815.
- Fixed override font stopping working after some time in IL2CPP.

</details>

<details>
<summary><strong>v5.5.1</strong> — 2026-01-28</summary>

### Fixes

- Switched to `UnityInput` to support both legacy and new Unity input systems by @ManlyMarco in #804.

</details>

<details>
<summary><strong>v5.5.0</strong> — 2025-10-27</summary>

### Added

- Added `AutoTranslatorState.PluginInitialized` property and `PluginInitializationCompleted` event, allowing other plugins to detect when AutoTranslator has finished loading by @ManlyMarco in #771.
- Added support for the `XUAIGNORETREE` tag in GameObject names to prevent translation of entire GameObject trees. Unlike `XUAIGNORE`, this applies to the full tree instead of only the named GameObject by @ManlyMarco in #772.
- Added support for Yandex Translate API v2 by @Noobik447 in #774.

### TMP_Font_AssetBundles

- Added `arialuni_sdf_u6000` by @Galiathus987 in #787.

</details>

<details>
<summary><strong>v5.4.6</strong> — 2025-09-28</summary>

### Fixes

- Replaced culture-sensitive string operations with invariant/ordinal variants by @ManlyMarco in #765.
- Fixed `ResizeUI` NullReferenceException when a component is destroyed by @ManlyMarco in #766.

</details>

<details>
<summary><strong>v5.4.5</strong> — 2025-03-28</summary>

### Added

- Added basic resizer support for NGUI by @ManlyMarco in #685.

### TMP_Font_AssetBundles

- Added `arialuni_sdf-u55to2017` by @djlaser.
- Added `arialuni_sdf_u2021` and `arialuni_sdf_u2022` by @ceshizhuanyong895.

</details>

<details>
<summary><strong>v5.4.4</strong> — 2025-01-31</summary>

### Fixes

- Added NGUI support to `TextGetterCompatibilityMode`.
- Fixed broken `Assembly-CSharp-firstpass` detection in `CallOrigin`.
- Fixed a potential assembly comparison issue in `TextGetterCompatModeHelper`.
- Fixed typo in `README.md` by @eltociear in #660.

</details>

<details>
<summary><strong>v5.4.3</strong> — 2024-12-18</summary>

### Fixes

- Added a hotfix for the minor regex performance improvement by @Lmeks in #638.

### CustomTranslateEndpoint

- Added `EnableShortDelay` and `DisableSpamChecks` settings by @ManlyMarco in #645.

</details>

<details>
<summary><strong>v5.4.2</strong> — 2024-11-24</summary>

### Changes

- Added OS-independent paths for the MelonMod version by @PranitModak in #606.
- Made Auto Translator plugin config accessible on Android by @PranitModak in #612.
- Improved translation scoping: if `GetScopeFromComponent` fails, `GetActiveSceneId` is used instead of `-1` by @Lmeks in #619.
- Added minor regex performance improvements by @Lmeks in #626.
- Moved `XZipper` from DotNetZip to SharpCompress by @ManlyMarco in #629.

</details>

<details>
<summary><strong>v5.4.1</strong></summary>

### Fixes

- Fixed crash when changing font if the previous font is `null`.

### Maintenance

- Moved all version constants into a global props file.

</details>

<details>
<summary><strong>v5.4.0</strong></summary>

### Changes

- Updated DeepL supported languages according to the API documentation.
- Updated to BepInEx 6.0 BE-704.

### Fixes

- Fixed silently failing to get some `Il2CppTypes` and crashing later.
- Fixed outline style being lost when overriding TextMeshPro font.

</details>

<details>
<summary><strong>v5.3.1</strong></summary>

### Changes

- Used reflection to access `Il2CppInteropManager.IL2CPPInteropAssemblyPath`.
- IL2CPP: Shimmed `UnityEngine.Input` to fix failing to find the type in some games.

### Fixes

- Fixed `HandleInputSafe` crashing in some games.
- DeepL: Fixed 404 error when calling `GetClientState`.

### Translation

- Allowed DeepLTranslator to support `auto`.
- Added Indonesian support for DeepL Translate.

</details>

<details>
<summary><strong>v5.3.0 FIX</strong></summary>

### Fixes

- Fixed console management error.

</details>

<details>
<summary><strong>v5.3.0</strong></summary>

### Features

- Added support for the latest MelonLoader and BepInEx bleeding edge builds for IL2CPP. Use the stable release for Mono. Support for earlier versions was dropped.
- Added `PersistRichTextMode` to control how text parsed as rich text during translation is persisted.

### Regressions

- Dropped support for pre-2017 Unity Engine versions for IL2CPP builds due to issues with the new IL2CPP interop. This may be re-added later.

### Fixes

- Fixed several potential `NullReferenceException` occurrences.
- Fixed a potential bug during substitution replacements. Thanks to TokcDK.

</details>

<details>
<summary><strong>v5.2.0</strong></summary>

### Features

- Added support for Lingo Cloud. Thanks to Kiles Duli.

### Fixes

- Fixed IL2CPP issue where the plugin did not always use correct unhollowed paths for BepInEx.

</details>

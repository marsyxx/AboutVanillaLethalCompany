# Publishing plan: Thunderstore (team **devil1nme**)

Based on the official wiki: [Creating a Package](https://wiki.thunderstore.io/mods/creating-a-package),
[Packaging Your Mods](https://wiki.thunderstore.io/mods/packaging-your-mods),
[Your First Upload](https://wiki.thunderstore.io/mods/your-first-upload),
[Updating a Package](https://wiki.thunderstore.io/mods/updating-a-package),
[Global Rules](https://wiki.thunderstore.io/moderation/global-rules).

## What the rules require, and where we stand

| Requirement (Thunderstore) | Status |
|---|---|
| `manifest.json`, `README.md`, `icon.png` at the **root** of the zip (exact, case-sensitive names) | done, checked by `tools/build.ps1` |
| `name`: letters/digits/underscore only, `version_number` `Major.Minor.Patch`, `description` <= 250 chars, `website_url` key present (may be `""`), `dependencies` list | done, validated by the build script |
| `icon.png` exactly 256x256 PNG | done (generated, checked) |
| README UTF-8 **without BOM** | done, checked |
| Zip must contain the files, not a wrapping folder | done (`dist/*.zip` built with files at the root) |
| BepInEx folders: `plugins/`, `patchers/`, `config/` are kept as folders by the mod manager | `LCCompatFixes` uses `patchers/` + `plugins/`; `LanguageSwitch` ships `config/Translation_de/` |
| Do not ship game files / other mods' files; no obfuscation; no self-updating or downloading code | nothing of that is included (the patcher only adds members in memory) |
| AI-generated packages should use the **"AI Generated"** category | **must be ticked at upload** (the code, German text, icons and READMEs were written with Claude) |
| "Test the zip in game with *Import local mod*, ideally on a clean profile" | **still to do** (see step 2) |
| README images must be hosted publicly (files inside the zip are not shown) | none used; add screenshots hosted elsewhere if wanted |

## Step by step

1. **Account and team.** Sign in at thunderstore.io (Discord, GitHub or Overwolf). Open *Teams* and create the team
   **`devil1nme`**. The team name cannot be changed later and becomes part of every dependency string
   (`devil1nme-GraphicsPlus-1.0.0`). Teams are not created automatically.
2. **Test the zips like a stranger would.** In Thunderstore Mod Manager create a **new, clean profile**
   (BepInExPack only), then *Settings -> Import local mod* for each zip in `dist/`:
   * `LCCompatFixes`: with LethalCasino installed, the main menu loads with no `MissingMethodException` in the log;
     without LethalCasino nothing happens.
   * `GraphicsPlus`: **Settings -> Graphics+** opens; move every option **in a real lobby** and confirm it applies and that
     "Game default" restores. Specifically check anti-aliasing, shadow resolution, contact shadows, ambient occlusion,
     the Retro colour pass, and the pause-menu copy of the page. Test as host and as client.
   * `LanguageSwitch`: with and without XUnity installed; English / Russian / German round trip; German in a lobby and at
     the terminal (long words may overflow some UI; note anything that does).
3. **Final checks.** Run `tools/dev/check-compat.ps1` against the current game build; bump nothing unless something
   changed; make sure `dist/` zips are rebuilt (`tools/build.ps1`) after the last edit.
4. **Validate metadata.** Paste each `manifest.json` into the *Manifest Validator* and each README into *Markdown
   Preview* (links are on the wiki page above).
5. **Upload** at thunderstore.io/package/create/ (one zip at a time):
   * *Upload file*: `dist/devil1nme-<Name>-<version>.zip`
   * *Team*: `devil1nme`
   * *Communities*: **Lethal Company**
   * *Categories*: **AI Generated** (required, see above) + the fitting ones, e.g. *Mods*, *Client-side*, and
     *Translations* for LanguageSwitch / *Tools* for LCCompatFixes (pick from the list the page offers)
   * *Contains NSFW content*: **No** (it cannot be changed without a new version)
   * Suggested order: **LCCompatFixes** -> **GraphicsPlus** -> **LanguageSwitch**.
6. **Check the package pages**: icon, README rendering, dependency list, dependency string, categories.
7. **Optional but recommended**: put the repository on GitHub and set `website_url` in each manifest (needs a new
   version, versions are immutable), add hosted screenshots to the READMEs.

## Decisions for you

* **Licence**: `LICENSE` is MIT with "devil1nme" as holder: change it if you prefer something else, then rebuild.
* **Version numbers**: all are `1.0.0`. If you want the first upload to read as a test release, change
  `version_number` to `0.9.0` in the three `package/manifest.json` files (suffixes like `-beta` are not allowed).
* **LanguageSwitch dependency**: the manifest depends on `RTLC-XUnity_AutoTranslator-5.6.11` (the maintained package;
  `Hayrizan-XUnity_AutoTranslator` is marked deprecated). Re-check that the version number is current right before
  upload (open the package page and copy its *Dependency string*).
* **German text** was written by AI from the English game strings and has not been reviewed by a native speaker; the README
  says so. A native review before release is worth it.

## After release

* Updating = bump `version_number`, rebuild, upload under the same team (the files of a published version cannot be edited).
  Keep `CHANGELOG.md` newest-first.
* After **every game update**: run `tools/dev/check-compat.ps1`; the patcher in LCCompatFixes is idempotent and logs when
  a game update makes a fix unnecessary.
* Listings of other people's mods that this pack relies on (RTLC, XUnity, LethalCasino, loaforcsSoundAPI) can change
  independently: re-test `LCCompatFixes` when they update.

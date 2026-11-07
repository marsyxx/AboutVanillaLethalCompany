# ShipLoot

A mod for Lethal Company which reliably displays the total scrap value on the ship.

> **Unofficial updated and edited build (v81).** ShipLoot was created by **tinyhoot**; this release is
> **Dysora's** update of it for current game versions, with some changes of its own. The counter
> now uses the game's default scan label instead of tinyhoot's custom one, among other things. See [ATTRIBUTION.md](ATTRIBUTION.md).

The vanilla scanner only totals up what is in your view, so getting a real number for the scrap you
already gathered means standing at the exact right angle — and even then it won't always be right.

This mod counts every piece of scrap within the ship bounds reliably, wherever you happen to be
looking. Scan from inside the ship and it shows the exact total you currently have aboard.

## Installation

- Install [BepInEx](https://thunderstore.io/c/lethal-company/p/BepInEx/BepInExPack/)
- Unzip this mod into your `Lethal Company/BepInEx` folder

Or use the thunderstore mod manager to handle the installing for you.

**Do not install this alongside tinyhoot's original ShipLoot.** Both use the same plugin GUID, so
BepInEx will refuse to load one of them. Uninstall the original first.

## Configuration

`com.github.tinyhoot.ShipLoot.cfg`

- `DisplayTime` (default `5`, range `1`–`30`) — how long the total stays on screen, in seconds.
- `VerticalOffset` (default `50`, range `-500`–`500`) — how far below the game's own scan total to
  place the ship total. Negative values put it above instead.

## Credits & license

- Original mod: **tinyhoot** — <https://github.com/tinyhoot/ShipLoot>
- Update and edits for this build: **Dysora**
- MIT licensed, © 2023 tinyhoot. See [LICENSE](LICENSE).

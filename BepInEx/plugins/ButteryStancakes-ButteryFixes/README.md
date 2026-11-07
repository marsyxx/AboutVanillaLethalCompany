# Buttery Fixes
A collection of miscellaneous vanilla-compatible bug fixes that were too small to release as individual mods.

## List of Changes
For a full list, please consult the [Wiki](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/ButteryFixes/wiki/) tab. Below is just a summary of the most important changes

<details>
<summary>General</summary>

**Host only**
- Fixed ordered items remaining inside the dropship after you are fired or the challenge moon resets, giving you a free "head start"
- The dropship's inventory will now be remembered when rehosting a save
  - Only applies if the file was both saved & loaded with this mod active
- Cruiser fixes
  - Fixed items being lost in orbit when dropped inside of an already magnetized Cruiser
  - Cruiser is lost when fired or between challenge moon attempts
- Fixed bee hives and sapsucker eggs not randomizing their price correctly

**Client-side**
- Fixed some v70 manor rooms not going dark when the breakers are turned off
- Ladder fixes
  - Stamina no longer drains if you try to sprint while climbing
  - You no longer climb slower/faster if you were moving up/down a slope
- The endgame stats screen now shows the correct amount of scrap in the level
- The casualties/bodies recovered screen now displays accurate fee percentages
- Data chips will now disappear for all players after being picked up
- Fixed TZP effects and mineshaft echo persisting after you are fired or change save files

</details>

<details>
<summary>Entities</summary>

**Host only**
- Fixed the broken "global clock" for nutcrackers (they are now more likely to "inspect" in sync with each other)
- Fixed bracken enraging on players immediately when shot by a nutcracker

**Client-side**
- Fixed Old Birds' stomps and dying Forest Keepers hurting you while you are inside the ship
- Fixed eyeless dogs getting stuck in aggro when making a lot of noise in one spot

</details>

<details>
<summary>Items</summary>

**Host only**
- Fixed several metallic items not being conductive in stormy weather (Configurable)

**Client-side**
- Fixed multiple flashlights in your inventory preventing pocketed lights from shining
- Fixed kitchen knives not showing "Value: $35" on scan until the save file is reloaded
- Fixed whoopie cushions showing a red cube on the "Scrap collected" screen
- Fixed boombox pitch being incorrect if you charge it after it reaches low battery

</details>

## Config

### Compatibility
- `PatchLadders` - Whether or not to patch ladders, disable this if you use any mod that adds sprinting on ladders as a feature
  - [Fast Climbing](https://thunderstore.io/c/lethal-company/p/Inoyu/Fast_Climbing/), [BetterLaddersFixed](https://thunderstore.io/c/lethal-company/p/Lunxara/BetterLaddersFixed/), [BetterStamina](https://thunderstore.io/c/lethal-company/p/FlipMods/BetterStamina/), and [GeneralImprovements](https://thunderstore.io/c/lethal-company/p/ShaosilGaming/GeneralImprovements/) all offer this (and probably others)
- `BodiesCollectSelf` - Bodies will automatically collect themselves when teleported to the ship, or when players die on the ship. You should probably disable this setting if you have another mod implementing similar behavior (such as [GeneralImprovements](https://thunderstore.io/c/lethal-company/p/ShaosilGaming/GeneralImprovements/)), or if you encounter strange behavior with body collection notifications.

<details>
<summary>Host only</summary>

- `MakeConductive` - Disables non-conductive vanilla items being made conductive by this mod, if you are used to vanilla properties
- `FixJumpCheese` - Jumping/landing while moving near dogs alerts them, preventing an exploit where you can move past them silently at sprinting speed.
- `FixSurfacePrices` - Bee hive prices finally randomize correctly (different hives cost different amounts), and sapsucker eggs have perfectly consistent prices based on the seed.

</details>

<details>
<summary>Client-side</summary>

- `RestoreFilmGrain` - Restores the film grain effects from earlier versions of the game (seen in v4-v30)
  - `MenusOnly` only applies this to the title screen and pause menu
  - `Full` restores it for everything (SYSTEMS ONLINE, flashbangs, fear, etc.)
- `ShowApparatusValue` - Replaces "???" text when scanning the apparatus with its actual value.
- `ScanImprovements` - Some miscellaneous improvements to the \"scan\" terminal command and radar.
  - When at The Company, the scan will display your ship loot just like it does in orbit.
  - Fixed incorrect prices being displayed for apparatus, bee hives, shotguns, kitchen knives, and sapsucker eggs.
  - Butlers' knives now display a scrap icon on the radar (and display their price in a terminal scan) before its owner is killed.
- `LockInTerminal` - When using the terminal, you will no longer be able to move your head, and typing should be available with less delay.
  - This also locks the camera when charging items, pulling the ship's lever, or sitting in the armchair.
- `AdjustCooldowns` - Increases cooldown on some items and interactable furniture for consistency and overlap prevention.
- `TheGoldenGoblet` - Renames "Golden cup" to "Golden goblet" :^)

</details>

## Recommendations
If you want a largely bug-free vanilla experience, I strongly suggest checking out all of the mods below (they all work great when used together):
- [Lobby Control](https://thunderstore.io/c/lethal-company/p/mattymatty/LobbyControl/) by [mattymatty](https://thunderstore.io/c/lethal-company/p/mattymatty/)
- [Additional Networking](https://thunderstore.io/c/lethal-company/p/mattymatty/AdditionalNetworking/) by [mattymatty](https://thunderstore.io/c/lethal-company/p/mattymatty/)
- [Matty's Fixes](https://thunderstore.io/c/lethal-company/p/mattymatty/Matty_Fixes/) by [mattymatty](https://thunderstore.io/c/lethal-company/p/mattymatty/)
- [LethalFixes](https://thunderstore.io/c/lethal-company/p/Dev1A3/LethalFixes/) by [1A3](https://thunderstore.io/c/lethal-company/p/Dev1A3/)
- [Pathfinding Lag Fix](https://thunderstore.io/c/lethal-company/p/Zaggy1024/PathfindingLagFix/) by [Zaggy1024](https://thunderstore.io/c/lethal-company/p/Zaggy1024/)
- [Spider position fix](https://thunderstore.io/c/lethal-company/p/Fandovec03/SpiderPositionFix/) by [Fandovec03](https://thunderstore.io/c/lethal-company/p/Fandovec03/)
- [Scandal's Fixes](https://thunderstore.io/c/lethal-company/p/Scandal/Scandals_Fixes/) by [Scandal](https://thunderstore.io/c/lethal-company/p/Scandal/)
- [CruiserImproved](https://thunderstore.io/c/lethal-company/p/DiggC/CruiserImproved/) by [DiggC](https://thunderstore.io/c/lethal-company/p/DiggC/) and [Scandal](https://thunderstore.io/c/lethal-company/p/Scandal/)
- [Melee Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/MeleeFixes/) by me
- [Jetpack Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/JetpackFixes/) by me
- [Enemy Sound Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/EnemySoundFixes/) by me
- [Weed Killer Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/WeedKillerFixes/) by me
- [Barber Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/BarberFixes/) by me
- [Mask Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/MaskFixes/) by me
- [Spawn Cycle Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/SpawnCycleFixes/) by me
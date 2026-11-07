# 2.7.4

### Fixed

- Compatibility with imperium mod
- v81 IL code not correctly added

# 2.7.3

### Added

- Updated mod to be compatible with v81
- Updated enemies, levels and items defaults

### Fixed

- Fixed enemy spawning issues

# 2.7.2

### Added

-   Default item, levels and enemies values from v73
-   Added new items from v73

### Fixed

-   Weather error when weather module is disabled
-   Fixed value rounding for enemy power

# 2.7.0

### Added

-   Randomizer now produces more coherent and balanced values

### Fixed

-   Synchronization issues regarding compatibility with other mods
-   Randomizer errors
-   Compatibility with v65
-   Beehives and apparatus x2 value
-   Floating vents (attempted, can't reproduce the issue anymore)

# 2.6.3

### Fixed

-   Apparatus now wont show its value
-   Scrap value wont get multiplied x2 by default
-   Removed useless debug logs

# 2.6.2

### Fixed

-   Updated scrap value and amount calculations and multiplications to prevent duplications
-   Scrap amount and value should be appended among multiplications made by other mods

# 2.6.1

### Fixed

-   Global scrap value multiplier applying more than once

# 2.6.0

### Added

-   New challenge seed `CHALLENGE`
-   Configuration for risk level text

### Modified

-   Tooltip for one handed and two handed items
-   Level, item and enemy configs updated for v64

### Fixed

-   Moon route price not working sometimes
-   Enemies not spawning when max quota days changes
-   Scrap value multiplier per level not working
-   Code optimizations

# 2.5.2

### Updated

-   Values for v61 for enemies
-   Values for v61 for items
-   Values for v61 for enemies

# 2.5.1

### Fixed

-   Removed unnecesary logs

# 2.5.0

### Added

-   Configuration for item's batteries
-   Configuration for item probability per level
-   Configuration for item conductivity
-   Configuration for vein shroud spread in levels
-   Configuration to force a seed for the game
-   Added some special seeds

### Fixed

-   Updated priority for moon route prices
-   Removed unnecesary production logs
-   Masked enemy incorrectly marked as outside enemy
-   Added enemy vanilla check
-   Fixed enemy spawn type label error
-   Fixed custom randomizers

### Modified

-   Item modifications now can be toggled and will be disabled for non-vanilla items
-   Updated configuration application for enemies and items per level

# 2.4.1

### Added

-   Configuration to globally change factory size [Thanks friskzips]

### Fixed

-   Fixed mod not working if any section breaks
-   Fixed enemy spawning breaking due to invalid external enemies

# 2.4.0

### Added

-   Added DISABLED mode for daytime speed to prevent LCV from altering the vanilla behavior
-   Added configuration for individual item weight
-   Added configuration for scrap value multiplication for individual levels
-   Added option to allow picking up items before game starts
-   Added configuration for moon route prices
-   Added internal framework for better IL modifications
-   Added configuration for quota overtime bonus
-   Added configuration for scrap worth

### Fixed

-   Fixed enemy power not reflecting configured value
-   Fixed trap multiplier not working correctly
-   Fixed quota not synchronizing with vanilla clients
-   Fixed incompatibility with LLL where enemies would not change configs
-   Fixed incompatibility with LLL where moons would not change configs
-   Fixed randomization on some configs resulting on errors
-   Fixed incompatibility with Starlanzed mod causing enemy defaults to being wrong
-   Fixed IL framework erros

### Modified

-   Externalized compatibility for enemies, levels and items
-   Optimized configuration application for moon enemies

# 2.3.2

### Fixed

-   Added compatibility for latest version of Lethal Level Loader
-   Synchronization issues with daytime speed multipliers

# 2.3.1

### Fixed

-   Issue that made players inmortal when a low defense setting was applied

# 2.3.0

### Fixed

-   Preloaders won't fail if the method already exists
-   Enemy spawn multiplier now will be clamped among other enemy spawning related configurations
-   Fixed Centipede enemy type not registering for vanilla planets
-   Fixed factory size not synchronizing

### Modified

-   Enemy spawns now won't be clamped by the amount of vents in the level. New vents will be created to spawn those enemies.

### Added

-   Configurations for player stats: `health`, `defense`, `sprint`, `jump force` and `movement speed`
-   Reintroduced configuration to hide all weathers
-   Added extra configuration to always display the in-game clock

# 2.2.1

### Fixed

-   Error making enemies not spawning if you changed the deadline

# 2.2.0

### Modified

-   Updated default configuration values for moon-related settings
-   Updated default configuration values for enemy-related settings
-   Added new moons to configurable list

### Fixed

-   Traps not loading correctly
-   Enemy power usage has been updated to work with decimal values

### Added

-   Made configuration for the newest trap (spike root trap)
-   Global multipliers for individual enemy spawning

# 2.1.0

### Fixed

-   v50 is now the supported version, older versions of lethal company are no longer supported.

# 2.0.5

### Fixed

-   Added safety measures to prevent configuration locking

# 2.0.4

### Fixed

-   [Regresion] fixed default scrap value multiplication using incorrect value
-   Fixed null pointer when registering non-compatible planets from LEC

# 2.0.3

### Fixed

-   Patch loading now uses a lazy load to prevent full crash when a component fails [Thanks rtfreal for the help testing in v50]

# 2.0.2

### Fixed

-   Error when the used didnt have LethalLevelLoader

# 2.0.1

### Fixed

-   Fixed patcher preloader not applying due to bad route
-   Fixed assembly versioning

# 2.0.0

### Modified

-   Now makes use of the new ConfigurableCompany 3.0 API
-   Individualized settings for each moon
-   Reworked `daytime speed`, `weather probability` and `enemy spawning` configurations.
-   Reworked project structure and internals
-   Configurations are now split in different pages for better organization

### Added

-   Added LethalLevelLoader compatibility for custom moons
-   Added `scrap amount range` so levels will have at least and at most the values in the provided range
-   Added `enemy spawn range` to clamp the spawns to a minimum and a maximum

### Fixed

-   Compatibility issues with players without the mod and weather disalignment
-   Daytime speed problems with players without the mod
-   Fixed compatibility with a lot of custom enemy mods

# 1.9.0

### Added

-   `Individual enemy cap` settings to configure how many enemies of any type can spawn
-   `Min enemies to spawn` configuration for a more reliable challenge
-   `Starting enemies` configuration to set a minimum amount of enemies to spawn in the level

### Fixed

-   Error when all weather chances were set to 0
-   No weather probability now works correctly without the `Allow every weather` option

### Modified

-   `Enemy spawn configurations` are now split between outside and inside enemies
-   Patches now will execute after other mods so they will override their settings (or amplificate them)

# 1.8.2

### Fixed

-   Weather chances not syncing when the users joins a host without opening the menu

# 1.8.1

### Fixed

-   Fixed soft crash when a planet did not have weathers
-   Fixed outside enemy spawn probabilities not applying

# 1.8.0

### Added

-   Configuration `Weather probability override` allows the user to set the probabilities more consistently.
-   Configuration `Allow every weather` allows any weather on any planet.
-   Configuration `Turret spawn multiplier` to allow to change the amount of turrets on each level.
-   Configuration `Landmine spawn multiplier` to allow to change the amount of landmines on each level.

### Fixed

-   Weather changes are now more reliable

# 1.7.4

### Added

-   New configurations for `Daytime speed` and `Daytime speed mode`.

# 1.7.3

### Fixed

-   Quota variables now follow the vanilla game scaling (a more consistant raise)

### Modified

-   Added an image of how the menu looks like in the readme

# 1.7.2

### Fixed

-   Fixed an oppsie when adding the failsafe that made the mod not patch methods correctly

# 1.7.1

### Fixed

-   Added a patch for compatibility with [Don't touch me](https://thunderstore.io/c/lethal-company/p/Kittenji/Dont_Touch_Me/)
-   Now those mods without compatibility won't crash (added a failsafe)

# 1.7.0

### Added

-   New configuration for starting quota, quota increase and quota exponential increase

### Modified

-   Changed the mod icon. Do you like it?
-   Experience multiplier is no longer considered experimental

# 1.6.0

### Added

-   New configuration to modify weather chances

### Fixed

-   Added missing information to the mod readme

### Modified

-   Updated mod descriptions
-   Updated ConfigurableCompany dependency

# 1.5.4

### Added

-   New configuration **Experience multiplier** to modify the amount of experience you earn/lose while playing. _After testing seems to work correctly, however is marked as experimental just in case I missed something during sync_

### Modified

-   Configurations now display on their correct page

# 1.5.3

_Nothing added, just and update to make people know the mod works correctly_

# 1.5.2

### Added

-   Compatibility with v47

### Fixed

-   Code cleanup

# 1.5.1

### Fixed

-   Fixed enemy spawn probability not syncing with configuration value between matches

# 1.5.0

### Added

-   Individual enemy spawn probabilities

### Modified

-   Changed ship door config description to clarity what it does [Thanks to @rayneontop]

### Fixed

-   Modifying deadline days on an already existing game could cause enemies to now spawn

# 1.4.2

### Fixed

-   Corrected scrap value multiplication

# 1.4.1

### Fixed

-   Beehives now correctly show their scrap value multiplied

# 1.4.0

### Fixed

-   Fixed scrap multiplier to correctly calculate scrap values
-   Now the scrap price multiplier affects items such as keys and the lung apparatus
-   Project cleanup

# 1.3.0

### Added

-   Configuration to disable signal translator limit (terminal input limit remains)

### Fixed

-   Made sure every configuration works correctly with the new Config API, this includes changes to
-   Enemy power and capacity wont overflow themselves anymore
-   Changing the quota deadline wont break enemy spawn
-   Weather is now hided correctly in the planet description
-   Correctly set death penalty display

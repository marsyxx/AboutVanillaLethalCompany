# v1.3.1
- Switched dependency from [Vent Spawn Fix](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/VentSpawnFix/) to [Spawn Cycle Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/SpawnCycleFixes/), as the former has been merged into the latter.
- Removed door open fix (fixed in vanilla)
# v1.3.0
- [Lobby Compatibility](https://thunderstore.io/c/lethal-company/p/BMX/LobbyCompatibility/) integration
- Increased the maximum value for `MaxCount`
- Replaced `SpawnInPairs` setting with new `SpawnInGroupsOf` setting
# v1.2.3
- Replaced `OnlyOneBarber` setting with new `MaxCount` setting
- Reduced lag spikes
  - When a new Barber spawns
  - When Barbers accelerate hourly
# v1.2.2
- Improved compatibility with custom moons that spawn Barbers outside
# v1.2.1
- Publicized some code in the assembly to allow compatibility with other Barber mods ([Clay Surgeon Overhaul](https://thunderstore.io/c/lethal-company/p/dopadream/ClaySurgeonOverhaul/))
# v1.2.0
- Fixed Barbers being unable to open doors
# v1.1.0
- Added an `ApplySpawningSettings` config option to improve compatibility with other content configuration mods (ex: [LethalQuantities](https://thunderstore.io/c/lethal-company/p/BananaPuncher714/LethalQuantities/))
- Added an `OnlyOneBarber` config option. Disabling this will let 8 Barbers spawn per day again.
- Fixed a bug where the drumroll overlaps the immediate "parade" sound when a Barber spawns.
- Switched dependency from [LethalFixes](https://thunderstore.io/c/lethal-company/p/Dev1A3/LethalFixes/) to [Vent Spawn Fix](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/VentSpawnFix/), as the latter's group spawn fix is no longer included in the former.
# v1.0.0
- Initial release.
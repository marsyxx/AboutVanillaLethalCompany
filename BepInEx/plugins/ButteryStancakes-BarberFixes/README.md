# Barber Fixes
**All players *need* to have this mod installed.**

Fixes bugs with the Barber:
- Fixed the Barber AI so it actually functions when multiple Barbers spawn.
  - In vanilla, only the first Barber spawned would work as expected. Any future Barbers would slide around with no sound or animations and then eventually get stuck in place.
- Fixed some unsafe code in the Barber AI, causing them to freeze in place and generate errors in the console.
  - This doesn't happen in vanilla because the code responsible doesn't run at all (due to the first bug)
- Fixed all Barbers (sometimes) freezing in place indefinitely when the first Barber kills someone that isn't the host.
- Fixed drumroll sometimes being desynced from the first jump that happens after a player is killed.
- Fixed the drumroll playing (and overlapping the immediate "parade" sound) when a Barber first spawns.

Also adds some config settings to re-enable certain behaviors:
- `MaxCount` - Lets you set the number of Barbers allowed to spawn during a single day. From v55-v61, this was set to 8. This was set to 1 in v62, likely to hide the synchronization issues mentioned above.
- `SpawnInGroupsOf` - Lets you set how many additional Barbers will try to spawn if a Barber is assigned to a vent. In v55, this was set to 2. This was set to 1 in v56, since vanilla never supported grouped indoor monsters.
  - [Spawn Cycle Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/SpawnCycleFixes/) is REQUIRED for values greater than 1 to function!
- `DrumrollFromAll` - When enabled, all Barbers will do a drumroll before they "jump." In vanilla, only the original Barber plays the drumroll. It's unclear whether this is intended or an oversight (the code is inconclusive)
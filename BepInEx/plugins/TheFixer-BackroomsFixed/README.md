# BackroomsFixed

Your coworkers may disappear into the Backrooms. Make sure to keep an eye on them.
Escape the Backrooms before the ship leaves.

This is Neekhaulas' Backrooms 0.1.3, rebuilt to work on current Lethal Company. **Everyone in the lobby needs it.**

## What changed from 0.1.3
- Carpet footsteps inside the Backrooms (sounds from YourFurnace's CarpetSound, built in). Unlike CarpetSound, wood floors elsewhere keep their normal sound. Remove CarpetSound if you have it.
- Rebuilt the networking for the v73 Netcode update. Previously every teleport request was silently dropped, so nobody reached the Backrooms and some hits did no damage at all.
- Fixed the Backrooms kill zone crashing on v81 (`KillPlayer` gained new parameters).
- The host now picks your spawn point. The old version reset the game's global random generator on every teleport.
- The exit only teleports the player who walked into it, once. Before, every client sent the request, and then sent it again 3 seconds later.
- The Smiler now wanders to real spots in the maze. Before, it walked toward the wrong coordinates.
- No damage roll happens in orbit or before the Backrooms are built for the moon.
- Dropped the unused LethalLib dependency.

## Config
`Neekhaulas.Backrooms.cfg` (only the host's values matter):
* `Teleportation odds`: % chance per hit or death-trigger to be sent to the Backrooms instead (default 3).
* `Number of smilers`: Smilers spawned in the Backrooms (default 1).

## Credits
Original mod and all assets: Neekhaulas (Backrooms/Backrooms on Thunderstore).
Carpet footstep sounds: YourFurnace (YourFurnace/CarpetSound on Thunderstore).

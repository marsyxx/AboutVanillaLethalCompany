# Changelog

## 1.0.2

- Fixes InsanityRemastered's sounds not loading when it's installed with a mod manager ("Failed to load audio assets!"). Its sound files are now loaded from its own folder.

## 1.0.1

- Fixes players being unable to join when another mod leaves a spawned NetworkObject's IsSceneObject unset. The host now treats such objects as dynamic spawns and logs their names.
- Fixes scan markers appearing in the wrong place with HDLethalCompany's resolution scaling on.
- Fixes weather showing inside interiors, or missing after teleporting outside, when an interior or moon lacks the game's weather switch at its entrances.
- Removes a ~0.4 s hitch when the ship loads: mod lookups from the main menu are reused instead of repeated.

## 1.0.0

- Initial release.
- Landing work is queued across frames, with the host's "finished generating" message held until the queue is empty.
- The outdoor navmesh rebuilds on worker threads.
- The host's spawns are split across frames.
- Chameleon, Diversity and CullFactory work gets its own frames.
- LazyLobby: More Suits textures are decoded at the menu, Diversity assets are preloaded, and Sponge's main menu unload is skipped.

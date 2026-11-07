# LazyLanding

Smooths out the freezes when a moon finishes loading, and shortens the freeze when the ship scene loads. Nothing about the level changes. The same work still runs, but it's spread over more frames, moved to background threads, or done earlier at the main menu.

## Landing

When a moon finishes generating, the game normally does everything in one frame. It spawns the outside trees and rocks, rebuilds the outdoor navmesh, runs every mod's level-finished code, then tells the host it's ready. In a big modpack that can be a one-second freeze.

LazyLanding:

- **Spreads the level-finished work over frames.** The outside hazards and every mod's `FinishGeneratingLevel` postfix go into a queue that runs a few jobs per frame, in their original order.
- **Keeps the host waiting.** Your "finished generating" message to the host is held until the queue is empty, so the host still waits for all of that work.
- **Builds the outdoor navmesh on worker threads.** Rebuilds use Unity's `UpdateNavMesh` instead of `BuildNavMesh` on the main thread. Back-to-back rebuilds (from vanilla, 2 Story Ship and Wider Ship Mod) are merged into one.
- **Spreads the host's spawns.** The host spawns synced props, scrap and map objects in separate frames, then tells everyone the level is ready.
- **Gives visual-only work its own frames:**
  - Chameleon's retexture, one override per frame
  - Diversity's bracken room, stairs, maze and round setup
  - CullFactory's culling refresh

Measured on a 140-mod pack, the worst landing frame went from 1014 ms to about 230 ms.

## Lobby (LazyLobby, included)

- **More Suits:** decodes its suit textures at the main menu instead of during the first lobby load of a session. That saves about 0.7 s.
- **Diversity:** loads its assets in the background at the main menu.
- **LethalSponge:** skips its `UnloadUnusedAssets` when the main menu opens. Unity has just done the same while loading the menu, so Sponge's second pass costs about 0.6 s and frees almost nothing. Sponge's daily unload still runs.

## Multiplayer

LazyLanding works whether or not the other players have it. Each player's own game gets smoother.

Each player's game builds the interior and outside hazards from a shared seed. LazyLanding doesn't change the order of that work, so the level is the same for players with and without it.

## Compatibility

- The parts for Chameleon, Diversity, CullFactory, More Suits and LethalSponge only apply when those mods are installed.
- If a game update changes the code LazyLanding hooks, it logs a warning and leaves that part as is.

## Tip

LethalSponge's `resizeTextures = true` resizes textures and runs `UnloadUnusedAssets` on every landing. That costs about 0.2 s plus a full garbage collection. Setting it to `false` removes that cost.

## Log

Your log will show lines like these:

```
[Info   :LazyLanding] Deferring DiversityRemastered.Patches.RoundManagerPatch.FinishGeneratingLevel
[Info   :LazyLanding] Level finished loading 0.5 s after generation, telling the server
[Info   : LazyLobby] Decoded 12 suit textures at the menu (0.6 s)
```

With debug logging on, each queued job also logs how long it took.

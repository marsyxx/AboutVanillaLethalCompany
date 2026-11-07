# MemoryJanitor

Lowers memory use in big Lethal Company modpacks.

- **Half-resolution textures.** Skips the largest mip level of every texture. Lethal Company renders the world at
  620×364, so textures above that resolution are mostly wasted. This saves about 75% of texture memory and looks
  the same in game.
- **Frees idle audio.** Clips set to *Decompress On Load* stay in RAM as raw PCM after they've played. Every
  30 s, MemoryJanitor frees the ones that haven't played for 2 minutes. When the ship reaches orbit, it frees
  every idle one. Unity reloads a clip the next time it plays.
- **Orbit cleanup.** When the ship reaches orbit, it runs `Resources.UnloadUnusedAssets` and a garbage
  collection to drop whatever the last moon left behind. This pass freezes the game briefly, so it only runs
  in orbit.

Pairs well with **BundleSlimmer**, which fixes LZMA bundles and audio settings on disk, and with LethalSponge's
dedupe options (`deDupeMeshes`, `deDupeTextures`, `deDupeAudio`).

## Config

`BepInEx/config/lcmemfix.MemoryJanitor.cfg`

| Section | Setting | Default | |
|---|---|---|---|
| Textures | `MipmapLimit` | 1 | Mip levels to skip. 1 = half resolution, 2 = quarter. 0 = off. |
| Audio | `UnloadIdleDecompressedAudio` | true | Free idle audio at all (sweep and orbit). |
| Audio | `SweepSeconds` | 30 | How often the idle-audio sweep runs. 0 = orbit only. |
| Audio | `IdleMinutes` | 2 | The sweep only frees clips that haven't played for this long. |
| Audio | `MinClipSeconds` | 3 | Ignore clips shorter than this. |
| Audio | `IncludePreloadedClips` | false | Also free clips marked *Preload Audio Data*. |
| General | `UnloadUnusedAssets` | true | Run the full unused-asset cleanup in orbit. |
| General | `DelaySeconds` | 4 | Wait this long after reaching orbit before cleaning. |
| Diagnostics | `MemoryReports` | false | Log a memory breakdown at the menu, after landing and in orbit. Causes short freezes. |

## Good to know

- **Client-side.** Players with and without it can play together.
- A freed clip may stutter for a moment the first time it plays again.
- Log: `BepInEx/LogOutput.log`. Search for `MemoryJanitor`.

## Credits

Built with Claude Code (Anthropic's Claude Opus 5.5). MIT licensed. See `LICENSE`.

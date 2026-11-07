# Attribution

**This is not an original mod.** It is an unofficial, updated and edited build of **ShipLoot**,
created by **tinyhoot** — brought back into working order on current Lethal Company versions, with
some changes of its own along the way.

|                                   |                                                  |
| --------------------------------- | ------------------------------------------------ |
| Original mod                      | [ShipLoot](https://github.com/tinyhoot/ShipLoot) |
| Original author                   | **tinyhoot**                                     |
| This build                        | **Dysora**                                       |
| Upstream version this is based on | 1.1.0                                            |
| License                           | MIT — see [LICENSE](LICENSE)                     |

The mod, its idea and its original implementation are tinyhoot's work. Dysora's contribution is
repairing it against the current game build and the edits listed below. **This is not a
behaviour-for-behaviour copy of 1.1.0** — do not assume it matches the original if you knew it
before.

## What is different from tinyhoot's 1.1.0

**The counter looks different.** The original drew its own single line of text, `SHIP: $123`, into a
copy of the game's old value counter. This build instead copies the game's own scan-total display and
fills in its label and value fields, so the counter uses Lethal Company's default label style and
sits with the vanilla total rather than having tinyhoot's custom look.

**What counts as "in the ship" changed.** The original summed everything parented to the ship object.
This build decides it by the item's physical position inside the ship's inner bounds, the same way
the game does — which is a fix, but it does mean the number moves at different moments than it used
to.

**The total updates live** while it is on screen instead of freezing at the value from the moment you
scanned.

**A `VerticalOffset` config option was added** to move the counter relative to the game's own scan
total.

## Why a separate release exists

The upstream mod has not been updated for some time and no longer functions on current game
versions. Rather than leave it broken, this build carries it forward. It keeps the original plugin
GUID (`com.github.tinyhoot.ShipLoot`), so it is a drop-in replacement for the original.

Because the GUID is shared, **do not install this alongside tinyhoot's ShipLoot** — BepInEx will
refuse to load the second plugin with a duplicate GUID.

If tinyhoot resumes updating the original, use theirs instead.

## How it was made

The original release was distributed as a compiled assembly only. It was decompiled, repaired
against the current game assemblies, edited as described above, and rebuilt.

## License

ShipLoot is released by tinyhoot under the MIT License, which permits redistribution of modified
copies provided the original copyright notice and license text are included. Both are preserved
verbatim in [LICENSE](LICENSE), and this build is distributed under those same terms.

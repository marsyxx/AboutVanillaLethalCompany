# Jetpack Fixes
Fun fact: The "overheat explosions" often attributed to the jetpack's design are actually caused by a bug. The jetpack has no heat meter or speed limit whatsoever.

<details>
<summary>More detailed explanation</summary>

- The reason the jetpack appears to explode on "overheat" is due to buggy crash damage behavior - the jetpack is programmed to explode ONLY if the player wearing it dies with jetpack controls active. Jetpack controls are enabled the moment you activate the thruster, and are only disabled after you land safely on the ground. (Hence, dying to fall damage still explodes the jetpack if you fall out of the sky with the jetpack turned off)
- Zeekerss programmed a special case where the player is instantly killed by Gravity if you are moving towards an obstacle that is less than 4 units away, while the jetpack is propelling you at more than 50 units per second.
- However, in v49, this condition would erroneously detect invisible "triggers" (which don't normally have collision) as obstacles. The player has several of these triggers attached to their body, causing you to "crash into yourself" and die almost immediately after reaching the speed threshold.
- v50 partially fixed this problem by disabling trigger detection, but there is still one non-trigger collider attached to the player that the jetpack can detect under specific circumstances, which causes you to die still, to the same bug.
- This bug is most easily replicated by using the thruster to travel straight upwards - you'll notice there's no limit to how high or fast you can travel, but once you let go of the thruster and start free-falling, you will explode randomly in open sky after only a brief moment.
- v64 makes this bug even worse, because belt bags attached to your waist (in your hotbar) also have collision and cause the jetpack to crash!
- Some other funny consequences of this behavior:
  - Since tulip snakes also enable jetpack controls, if they carry you into the air and kill you while you have a jetpack in your inventory, it will still explode.
  - Dying with jetpack controls explodes all jetpacks in your inventory - up to 5 at once!

</details>

It's possible Zeekerss intentionally left this bug in the game, as a pseudo-balancing feature for the raw power of the jetpack. However, since it's a side effect of incorrect behavior (that was even partially fixed in v50), this mod will get rid of it for you - allowing you to travel as fast as you like, as long as you don't crash (which will still kill you instantly)

You can also enable explosions when flying too fast at extreme altitudes, in accordance with the statement below...

> I wouldn't really want to drag down the victory of soaring straight up into the sky and exploding like a firework.

– Zeekerss, [The time is flying (and so are the jetpacks and tulip snakes)](https://www.patreon.com/posts/time-is-flying-102810602)

Though it's worth mentioning that this behavior is no longer exhibited in vanilla anyway... (See "More detailed explanation" above)

Although the main feature of this mod is the fixed crash code, there are several additional issues I've addressed:
- Fixed buggy behavior when "feathering" the thruster controls. (Sometimes, in vanilla, it would "drop" inputs and not activate the thruster when you click)
- Fixed buggy interactions between tulip snakes and jetpacks, in which each would cancel out the other. Now they co-exist, in (nearly) perfect harmony
- Fixed the jetpack "getting stuck" and storing momentum if your feet graze an object mid-flight
- Fixed the ship "dragging" you with it, if you fly off the deck in the middle of the landing sequence.
- Fixed the proximity warning (the beeping) logic being buggy, which caused it to continue playing in open sky, or not start playing when you are near terrain.
- Fixed jetpack audio being distorted when you're flying around with it. (The Doppler effect shouldn't apply to your own jetpack, just when others are flying it)
  - Also applies when the spectated player is flying a jetpack.
- The screen no longer shakes when turning the jetpack off
- Crashing the jetpack now deals "Inertia" damage instead of "Gravity" damage
  - When jetpack explodes in mid-air (based on `MidAirExplosions` config) it kills via "Blast"

## Config

- `MidAirExplosions` - Lets you control the circumstances that would explode the jetpack.
  - `Off` only destroys the jetpack if you crash into something solid, which is almost certainly the original intent behind the jetpack's code.
  - `OnlyTooHigh` allows the jetpack to explode on its own again, but only if you are exceeding vanilla's "safe speed" while you are far above the map. (This is possibly Zeekerss' new intended behavior?)
  - `Always` replicates v49's behavior, where the jetpack will always explode once it exceeds the safe speed.
  - `DontChange` does not change vanilla's broken behavior at all.
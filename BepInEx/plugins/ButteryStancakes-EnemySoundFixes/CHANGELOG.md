# v1.9.23
- Fixed easter eggs not playing their drop sound
  - This was a result of some underbaked code I accidentally shipped, sorry!
  - Easter eggs now play their drop sound *except* when they pop (which is what this code was intended to do all along)
# v1.9.22
- Removed doppler effect from the voice of the "ghost girl" (to reduce awkward distortion when she teleports)
- Fixed cavern "vents" only playing spawn SFX for their first assigned enemy
# v1.9.21
- Tweaked gift box fix to apply more consistently when opening over a tall ledge
# v1.9.20
- Fixed gift boxes playing drop sound when opened (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
# v1.9.19
- No longer patches enemy-on-enemy collision for baboon hawks (oops)
# v1.9.18
- Fixed fade logic for a couple miscellaneous audio effects (music, ears ringing)
- Fixed broken 200s cooldown between music tracks playing
- No longer patches enemy-on-enemy collision (for eyeless dogs or Kidnapper Fox)
# v1.9.17
- Fixed `WalkieHearsTalkies` being audible when there is only a player speaking into their own walkie
- Removed doppler effect from the squeaky lamp on the ship
- Fixed ambience behavior at the Company building (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
# v1.9.16
- Fixed missing hit sounds for *all* enemies which use a "hit voice" SFX but *not* a "hit body" SFX
  - The only enemy impacted by this in vanilla is the Feiopar (which now plays its intended hit audio, thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
  - This fix also addresses modded enemies, in case any exhibit this issue
# v1.9.15
- Slight refactoring to transpilers (users should notice no changes)
- Fixed a cruiser audio transpiler that has been broken since its addition (2 years!)
# v1.9.14
- Cruiser pushing no longer makes noise when it's magneted to the ship
# v1.9.13
- For any items which had their drop sound removed by this mod, it is now replaced with an empty audio clip for [loaforcsSoundAPI](https://thunderstore.io/c/lethal-company/p/loaforc/loaforcsSoundAPI/) compatibility
- Changed enemy hit patches to prevent double hit sounds playing for the fatal hit in multiplayer
# v1.9.12
- Fixed improperly packed assets from v1.9.11 which caused Cruiser audio to not work
# v1.9.11
- Cruiser engine audio now loops correctly (thanks [Scandal](https://thunderstore.io/c/lethal-company/p/Scandal/))
- Removed some unnecessary logging upon opening lobby (which I accidentally released with v1.9.10)
# v1.9.10
- Fixed disco ball volume getting too loud when turning it off (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
- Removed the wingflap sound from the tulip snake's "chuckle pool" (preventing a wingflap sound from randomly playing while it is resting on your head)
# v1.9.9
- Fixed Cadaver Blooms emitting audio after death when killed nearly instantly after spawning (such as when a player blooms from being crushed by a spike trap)
# v1.9.8
- RE-UPLOADED v1.9.7 WITH THE ASSET BUNDLE
# v1.9.7
- Flashlight toggle fix from v1.9.6 now applies to laser pointers (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
# v1.9.6
- Added `WalkieHearsTalkies` setting to restore the unused "WalkieTalkieTalkingNotHeld" SFX
- Fixed Cruiser manual not having the drop sound distance fix from v1.9.0
- Fixed pill bottles using the wrong drop sound since v1.9.3
- Fixed weed killer looping its "empty" audio if you hold the mouse button down (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
- Flashlights ($15 ones) now alternate between toggle audios, to match the behavior of light switches (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
# v1.9.5
- Re-uploaded v1.9.4 with the asset bundle...
# v1.9.4
- Fixed unlock sound playing twice when using a lockpicker/key on a door
- Cadavers now have a shorter "vent sound" before spawning, like the "ghost girl"
- Maybe(?) fixed Backwater Gunkfish making noises after death
  - I've only received inconclusive reports of this happening, but I've taken measures to fix it anyway
# v1.9.3
- Hoarding bug death is no longer randomized
  - I forgot the other "unused" clip was actually its stun sound
- Removed pickup sound from a couple items which either used the wrong sound or didn't have a sound in vanilla
  - Vanilla now has a generic grab sound for all items and I think these sound better with just that
# v1.9.2
- Re-uploaded v1.9.1 with the asset bundle
# v1.9.1
- Fixed Cadavers not playing audio when destroyed with weed killer
- Maybe fixed a new vanilla bug with snare flea audio in spectate mode
# v1.9.0
- v81 compatibility
  - Factory door sounds are no longer reversed
  - Removed Experimentation garage fix
- Fixed extension ladder retracting with no alarm if you pick it up after the alarm plays and re-deploy it
- Added `EclipsesBlockMusic` setting
- Fixed trash bin lid having misplaced interaction audio in manor garage
- Fixed certain items' drop sounds being audible from anywhere on the map
- Tweaked pickup/drop sounds for various items to improve consistency across the board
- Fixed microwave "hum" not playing automatically until you open and close its door back
- Hoarding bug now randomizes its death clip between another unused one
# v1.8.8
- Fixed landmines missing their far-away explosion SFX (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
# v1.8.7
- Fixed lockpicker playing drop sound when mounted to a door (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
- Simplified maneater death fixes
# v1.8.6
- Fixed cabin doors again (v70 changes broke my logic and I only just noticed)
- Fixed cupboard doors under greenhouse sink missing SFX (thanks [debbicar](thunderstore.io/c/lethal-company/p/debit_card_debit/))
# v1.8.5
- Added missing hit sound for Giant Sapsucker
# v1.8.4
- Fixed Cruiser horn getting stuck at wrong pitch when releasing and holding again
# v1.8.3
- Hotfix: Don't play dashboard button SFX when switching headlights
# v1.8.2
- Some more Cruiser fixes courtesy of [Scandal](https://thunderstore.io/c/lethal-company/p/Scandal/):
  - Fixed missing twist SFX when first inserting car key to ignition
  - Added missing dashboard button SFX when using wipers, cabin window, etc.
# v1.8.1
- Fixed Giant Sapsucker continuing to snore after being killed
- Added a config setting to make eyeless dogs play a death sound
# v1.8.0
- v70 compatibility
- Fixed Giant Sapsucker death audio being globally audible
- Removed `FixMasks` setting in config (fixed in vanilla)
- Added [LCMaxSoundsFix](https://thunderstore.io/c/lethal-company/p/Hardy/LCMaxSoundsFix/) as a dependency
  - You can technically still remove it if you insist, but it *really* should be used
# v1.7.0
- Fixed brackens using stun sound effects from eyeless dogs
# v1.6.7
- Cruiser push audio is now muted in orbit
# v1.6.6
- [Lobby Compatibility](https://thunderstore.io/c/lethal-company/p/BMX/LobbyCompatibility/) integration
# v1.6.5
- Fixed vents continuing to make noise after a monster spawns out of them
- Fixed nutcrackers using normal vent sounds (or vice versa - normal enemies using nutcracker vent sounds)
# v1.6.4
- Fixed mineshaft doors muffling their own audio as if they were behind a wall
# v1.6.3
- Fixed giants not stomping when ignited
  - It was me. I caused this bug 😢
# v1.6.2
- Actually fixed tulip snake wing flapping (the changes from v1.4.0 were a red herring all along)
- Fixed a potential issue with entrance doors in custom content
# v1.6.1
- Fixed maneater clicking its mandibles when freshly spawning
# v1.6.0
- Rolled back a few tulip snake changes from v1.4.0 since it didn't play nice with mods
- Several fixes to doors
  - The same random clip now plays on both sides of the entrance door
  - Entrance door sounds now play over walkie-talkies when players enter/exit the building (like mimics)
  - Mimics now play entrance door sounds on both sides when entering/exiting the building (like players)
  - Fixed factory doors, locker doors, and breaker box having backwards open/closed sounds.
  - Fixed cabin doors on Rend and Adamance still using the steel door sounds.
# v1.5.10
- Fixed mimics not playing hit sounds when the death animation occurs
# v1.5.9
- Fixed maneater not playing hit sounds when the death animation occurs
- Fixed snare fleas screeching after death if they are killed right as they drop from the ceiling
- Fixed "Hey" voice when the ship is struck by lightning
- Reduced most logs from "Info" to "Debug" level
# v1.5.8
- Cruiser gets muted a little earlier when heading into orbit (when the outdoor ambience ends, not after the results screen)
# v1.5.7
- Fixed the snare flea sometimes still playing sounds after death (after a change made in v1.5.3)
# v1.5.6
- Fixed maneater still making noises after being killed
# v1.5.5
- Cruiser tire audio no longer plays when the car is airborne (like from turbo boosts), not just when attached to the magnet
# v1.5.4
- Fixed Cruiser tires still making certain noises (skidding, rolling over gravel) while attached to the magnet
- Cruiser alarm no longer plays while attached to the magnet (since the ship can't take damage)
- Fixed baboon hawks still playing their attack sound when walking over a corpse
- Added a config setting to mute the Cruiser in orbit
  - By default, this will mute everything except for the radio
# v1.5.3
- Cruiser sound fixes
  - Fixed the "engine stalling" sound still playing when releasing and twisting the key again
  - Fixed the engine rev sound effect playing multiple times when twisting the key too quickly
  - Fixed the engine rev sound effect continuing after the engine starts up
  - Fixed collision sounds restarting instead of stacking
- Fixed volume fade of flaming giant effects
- Fixed several enemies not playing hit sounds when their death animation occurs
  - Brackens
  - Hoarding bugs
  - Eyeless dogs
# v1.5.2
- Tulip snakes now play hit sound when killed
- Fixed kidnapper fox drooling after being killed
# v1.5.1
- Fixed hardlock when attacking the kidnapper fox
# v1.5.0
- Some fixes for kidnapper fox
- `BetterMimicSteps` config setting to make mimics' footsteps better match players'
# v1.4.0
- Some more fixes for dogs
  - Retooled the pitch fix from v1.3.0
  - Voice pitch should now synchronize for all players who install this mod
  - Fixed overlapping breathing sounds coming from the same dog
  - Fixed breathing sound stopping sometimes after killing players
  - Added hit sound (like with forest giants)
- Fixes for tulip snakes
  - Fixed "scurrying" sounds continuing after latching to a player's head
  - Fixed randomized pitch for scurry sounds
  - Fixed some edge cases where dead tulip snakes would still make noise
  - Added hit sound
- "Fixed" thumpers playing thunder sounds from their voice when de-aggroing
  - Made this a config setting, because I'm still not 100% sure this is unintended
- Enemies no longer play hit sounds if another enemy hits them after they are dead
- Fixed old `DontFixMasks` option still showing up in the config under certain circumstances
# v1.3.0
- Dogs no longer continue breathing after they die
- Fixed dogs using the wrong voice pitch after they finish eating a player
# v1.2.4
- Butler corpses no longer buzz
# v1.2.3
- Inverted `DontFixMasks` option to `FixMasks` option, which I think is more intuitive
  - Your config should be updated automatically to use the proper setting
- Snare flea death scream is no longer random pitch
# v1.2.2
- Fixed a potential nullref exception with animated particles.
# v1.2.1
- Fixed a bug that caused certain animated map objects to not play audio (in vanilla, that would be garage door on Experimentation)
# v1.2.0
- Fixed mask items and enemies not playing laugh/cry noises as often as they are supposed to.
# v1.1.2
- Fixed fall damage for players using the wrong SFX (unless you crash a jetpack)
# v1.1.1
- Forest keeper "eating fixes" now apply when you teleport a player out of their hand
# v1.1.0
- Polish pass on forest keepers
- Reworked some snare flea code (to fix a broken patch that I replaced last minute before release)
# v1.0.0
- Initial release
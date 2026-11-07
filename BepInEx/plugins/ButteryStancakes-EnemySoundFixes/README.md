# Enemy Sound Fixes
Fixes numerous issues with missing sound effects, or SFX playing when they shouldn't.

## Enemies

<details>
<summary>Brackens</summary>

- Restored original stun sound
  - It was (most likely accidentally) replaced with the stun sound for dogs in v60
- Fixed hit sound being interrupted by death animation

</details>

<details>
<summary>Snare fleas</summary>

- Fixed shrieking when hitting its dead body
- Fixed chasing "footsteps" and suffocation noises not looping
- Fixed footsteps continuing while dead or clinging to a player's head
- Fixed improperly pitched scream when dropping from the ceiling after being damaged

</details>

<details>
<summary>Thumpers</summary>

- "Fixed"(?) thunder sounds playing from voice when losing track of the player
  - Can be re-enabled in config

</details>

<details>
<summary>Eyeless dogs</summary>

- Fixed breathing pitch being wrong after eating a player
- Fixed breathing sound sometimes overlapping (playing twice from one dog)
- Fixed breathing sometimes stopping completely after attacking
- Fixed missing hit sound effect
- "Added death sound"
  - Actually just the stun sound, reused
  - This can be disabled in the config, since it's a bit more of a subjective change

</details>

<details>
<summary>Hoarding bugs</summary>

- Fixed missing death sound effect
- Fixed hit sound being interrupted by death animation

</details>

<details>
<summary>Forest Keepers</summary>

- Fixed stun sound effect not playing when rescuing a player
- Fixed "chewing" sound and blood spray playing after eating animation is interrupted
- Fixed missing death sound effects
- Fixed fire volume not fading in when first ignited by an explosion
- Fixed roar sound effect (when grabbing players) being cut short by bite sound effect
- Fixed missing hit sound effect

</details>

<details>
<summary>Girl</summary>

- Removed doppler effect on voice (to reduce distortion immediately after she teleports)

</details>

<details>
<summary>Nutcrackers</summary>

- Fixed "marching music" playing endlessly after death if you kill it right as it fires its gun
- Fixed missing death sound effect

</details>

<details>
<summary>Masks</summary>

- Config setting to adjust footstep volume/distance to match player footsteps
- Fixed hit sound being interrupted by death animation
- Fixed entrance door sounds not playing on both sides of the door when entering/exiting the building

</details>

<details>
<summary>Butlers</summary>

- Fixed corpses still buzzing even once the mask hornets spawn out of it

</details>

<details>
<summary>Tulip snakes</summary>

- Scurry sounds now properly re-randomize their pitch when they play
- No longer randomly plays wingflap sounds instead of chuckle voices
- Failsafe to prevent tulip snakes from making noises after death
- Fixed missing hit sound effect

</details>

<details>
<summary>Kidnapper fox</summary>

- Fixed hurt sound not playing after tongue is broken
- Fixed hurt sound playing when attacking its dead body
- Fixed death sound sometimes not playing
- Fixed "growling" sound sometimes getting stuck when not dragging a player
- Fixed drooling after being killed

</details>

<details>
<summary>Maneaters</summary>

- Fixed hit sound being interrupted by death animation
- Fixed several sounds (footsteps, mandible clicking, etc.) still playing after death
- Fixed clicking mandibles when spawning as a baby

</details>

<details>
<summary>Giant Sapsuckers</summary>

- Fixed "snoring" occasionally continuing after death
- Fixed death sound being globally audible
- Fixed missing hit sound effect

</details>

<details>
<summary>Backwater Gunkfish</summary>

- Added some fallback code to mute audio on death (issue in vanilla? inconclusive)

</details>

<details>
<summary>Feiopars</summary>

- Fixed missing hit sound effect

</details>

<details>
<summary>Cadaver Growths</summary>

- Fixed missing destroy audio when using weed killer
- Reduced intensity of "vent sounds" before spawning (like "ghost girl")

</details>

<details>
<summary>Cadaver Blooms</summary>

- Fixed several sounds ("footsteps", breathing, etc.) still playing after death

</details>

## Other

Despite the (now outdated) name, this mod also fixes several other sounds unrelated to enemies:

<details>
<summary>Players</summary>

- Fixed fall damage not playing its special sound effect (except when crashing the jetpack)
- Fixed overlapping hit sound when attacked with shovel/knife

</details>

<details>
<summary>Cruiser</summary>

- Fixed the key twist sound effect not playing when initially inserting it into the ignition
- Fixed the "engine stalling" sound getting stuck if you turn the key for too long, even if you release it and turn it again
- Fixed the engine rev sound effect playing multiple times when turning the key in quick succession
- Fixed the engine rev sound effect continuing after the ignition finally starts
- Fixed tire contact audio still playing while the car is midair
- Fixed warning alarm playing while car is attached to the magnet (and immune to damage)
- Fixed horn getting stuck at wrong pitch when releasing and pressing again
- Fixed missing sounds for pressing dashboard buttons (windshield wipers, cabin window, etc.)
- Added config setting to mute Cruiser audio in orbit (engine, horn, radio, etc.)
- Fixed engine audio not looping properly
- Pushing the Cruiser is no longer noisy while it's magneted to the ship
- Fixed Cruiser collision audio sometimes not playing in certain circumstances

</details>

<details>
<summary>Doors</summary>

- Fixed backwards open/close SFX on storage lockers and the breaker box
- Fixed shed doors (Rend and Adamance) not utilizing wood door SFX
- Fixed mineshaft doors occluding their own audio (sounding muffled) from one side
- Fixed doors playing unlock sound twice in a row when unlocked
- Entrance doors now play the same random audio clip on both sides
- Entrance door audio now plays through walkie-talkies
- Fixed cupboard doors in greenhouse missing SFX
- Fixed trash bin lid in garage playing SFX in the wrong spot

</details>

<details>
<summary>Items</summary>

- Fixed lockpicker making dropped SFX when being placed on a door
- Fixed extension ladders not playing their alarm before retracting if you picked it up mid-alarm and re-deployed it
- Fixed certain items being audible at any distance when hitting the floor
- Adjusted the pickup/drop sounds for several items
- Fixed weed killer looping its empty audio when holding the spray button
- Flashlights and laser pointers now alternate between toggle audios (like light switches), rather than randomizing
- Walkie-talkies now emit "chatter" SFX when they are in use by another player (if you don't have one turned on in your inventory)
  - Can be disabled in config
- Fixed gift box playing drop sound when opened
- Fixed easter eggs playing dorp sound when exploding

</details>

<details>
<summary>Misc.</summary>

- Fixed "Hey" voice when ship gets struck by lightning
- Fixed vent noise getting stuck at the wrong volume after enemies spawn from them
- Fixed vents sometimes playing the wrong audio for the enemy that's about to spawn
- Fixed landmines not playing their faraway explosion SFX
- Added a slider to control how much Unity's simulated Doppler effect applies to music sources. (Dropship, boombox, Cruiser radio, etc.)
  - Any number between 1 (vanilla) and 0 (disabled) is valid.
  - Defaults to 0.333x which makes the effect more subtle.
- Fixed microwave "hum" not playing automatically until you opened and closed the door back
- Fixed disco ball volume getting too loud when turning it off
- Squeaky lamp on ship no longer has doppler effect
- Fixed "ambience library" from previous level being reused when landing on a moon which
  - Does not define its own library
  - Does not spawn enemies or scrap
- Level ambience no longer plays on moons that lack the normal gameplay loop (spawning enemies and scrap)
- Fixed broken 200s cooldown for ambience tracks
- Fixed cavern "vents" only playing spawn SFX for their first assigned enemy

</details>
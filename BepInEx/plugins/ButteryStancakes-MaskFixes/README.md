# Mask Fixes
Fixes a bunch of small issues with mask items and "masked" enemies.

I recommend pairing this with [Enemy Sound Fixes](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/EnemySoundFixes/) to (optionally) adjust their footstep volume to match real players.

## List of Changes

<details>
<summary><b>Reduced lag spike from Masked upon spawning in mineshafts</b></summary>

- Whenever a Masked spawns, its AI would call an expensive search function (to find the mineshaft's elevator script)
  - This search is unnecessary because there is a global variable already pointing to the elevator.
  - Now Masked AI references that global variable.

</details>

<details>
<summary><b>Fixed buggy roaming behavior from Masked AI</b></summary>

- Roaming Masked have a tendency to get stuck going in/out of the entrance on the surface every ~3 seconds.
- In mineshafts, roaming Masked also have trouble using the elevators, and will sometimes get stuck in a loop going up and down nonstop.
- Both of these behaviors have been fixed. Masked will now consistently wander outside the building, and wander inside mineshafts without issues.
- My patch can be disabled in the config, since it will likely conflict with any other mods that change Masked AI
  - The patch is auto-disabled when [SmartEnemyPathfinding](https://thunderstore.io/c/lethal-company/p/Zaggy1024/SmartEnemyPathfinding/) is installed, which has better support for modded interiors/moons

</details>

<details>
<summary><b>Fixed player conversion by Masked attacks</b></summary>

- In vanilla, mimics that are spawned by Masked vomiting onto players do not synchronize correctly in multiplayer.
  - The host will see the new mimic wearing that player's suit, while all other players will see the default orange suit.
  - Only the host will be able to watch the new mimic on the cameras. All other players will see the killed player disappear from radar.
  - If the player was killed outside, the new mimic will be unable to attack anybody except the host. This issue only disappears once the mimic enters the building.
- All of these issues have been fixed.
- As a side effect, this will also make players drop their items when they get converted - but this is more consistent with the behavior from wearing mask items

</details>

<details>
<summary><b>Fixed incomplete appearance of costumes</b></summary>

- When a player wearing the bunny or bee suit is converted into a Masked, its suit would be missing "attachments" like the bunny ears or bee antennae.
- Masked always wore the "Intern" badge on their suit, regardless of the rank of the player they were mimicking.
  - This also applies to the "V.I.P. Employee" badge, but as of v81 this is currently being displayed on all players.
- If the player they copied was covered in blood decals (from taking damage), those would not appear on the Masked.
- On challenge moons where the orange suit is unavailable, natural Masked still always spawn wearing it.
- There is also a bug that would sometimes cause Masked to turn invisible and spam errors in console.
  - I'm not sure if this is specifically a vanilla bug, or something related to a mod like [More Company](https://thunderstore.io/c/lethal-company/p/notnotnotswipez/MoreCompany/) (with the cosmetics)
- All of the above has been fixed.

</details>

<details>
<summary><b>Restored the "blood spillage" effect from v45-49</b></summary>

- When a Masked grabs a player, it "converts" them by vomiting blood into their face.
- Before v50, there was a visual effect during this animation that showed blood spilling into your helmet.
- This was removed in vanilla because (presumably) it had a tendency to get stuck on your screen forever, if you were rescued from the Masked before death.
  - This has been fixed.

</details>

<details>
<summary><b>Fixed all instances of masks using wrong assets</b></summary>

- When Comedy gets stuck to your face, it would transform into a Tragedy mask.
- Tragedy masks worn by players used Comedy's textures, which noticeably misalign with the mesh.
- Tragedy Masked were intended to cry like the item does, but they were silent in vanilla due to a networking bug.

</details>

<details>
<summary><b>Fixed some problems with Masked after death</b></summary>

- Masked no longer spin on the radar, becoming stationary like all other enemies.
- Masked no longer laugh or cry after being killed.
  - This is somewhat subjective, but masks are silent if attached to a player corpse (such as when putting a mask on at Gordion), and this makes them more consistent with other enemies.

</details>

<details>
<summary><b>Fixed "[Near activity detected!]" not working for Masked</b></summary>

- Masked are only added to the global list of enemies for the host.
- The proximity warning does not work (when only Masked are nearby) for any other clients, which makes it possible to immediately be grabbed upon entering the building, if you are unlucky.

</details>

<details>
<summary><b>Fixed Masked immediately grabbing players as they enter the building</b></summary>

- When a Masked uses the entrance doors, there is a 1.75s cooldown before it is allowed to attack players.
- This cooldown does not apply if the player is the one who uses the entrance doors, allowing them to be attacked instantly if the Masked was loitering next to the door.

</details>

<details>
<summary><b>Fixed Masked sprinting more often in multiplayer</b></summary>

- When a Masked takes damage, they have a random chance to begin sprinting.
- In multiplayer, this chance is rolled by each connected player, which causes it to become increasingly likely in larger lobbies.
- This fix will only fully apply if everyone in the lobby has the mod installed, but is partially alleviated by each player using it.

</details>

<details>
<summary><b>New logic for hiding aboard the ship</b></summary>

- When left outside for long enough without encountering a player, Masked will hide on the ship to ambush players when they return.
- They select a spot on the ship, then crouch and wait until a player is in line-of-sight.
- However, there is no code preventing multiple masked from choosing the same hiding spot, which will result in them getting stuck on each other and failing to crouch.
- I have adjusted this behavior so they will hide in different spots than each other, whenever possible.
  - This is a little more subjective than the other changes, so it can be disabled in the config, but it is enabled by default.

</details>

<details>
<summary><b>Fixed incorrect tagging of Masked colliders</b></summary>

- Some of the colliders on Masked still used residual tags from the player object, which Zeekerss likely cloned to create their prefab.
- These tags would lead to some esoteric behavior
  - Most notably, errors would be spammed in the console/log if Masked were walking through water, like during floods or in mineshaft tunnels

</details>

### Bonus

Starting in v1.2.0, I've included some additional customization options (all disabled by default):

<details>
<summary><b>Randomized suits for natural Masked</b></summary>

- By default, all vanilla suits are eligible
- You can add custom suits to the whitelist
  - Example: With [Classic Suit Restoration](https://thunderstore.io/c/lethal-company/p/ButteryStancakes/ClassicSuitRestoration/), you can add "brown" to the end of your list for compatibility
- Entering only "all" will automatically add every suit to the pool

</details>

<details>
<summary><b>Random chance for naturally spawning Tragedy Masked</b></summary>

- You can control the exact chance in your config

</details>

<details>
<summary><b>Scan nodes for Masked</b></summary>

- Similar to Cadaver Blooms, Masked will now display a "Body of `[playerName]`" scan node, with a cause of death
- Can be configured to show immediately, or only show after the Masked is "killed"
- Can also be configured to only show for converted players, or show for natural Masked as well

</details>

<details>
<summary><b>No spinning on the radar</b></summary>

- Lets you completely disable Masked spinning on the radar
- Regardless of this setting, Masked will always stop spinning once they are killed (as explained in the **List of Changes** section)

</details>
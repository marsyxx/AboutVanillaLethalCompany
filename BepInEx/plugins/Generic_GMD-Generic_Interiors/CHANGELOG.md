5.2.1



Overall

* Reduced weights for Wesley's moons to 0
* ???
* I genuinely don't remember what else I might have done



Bunker

* Ever so slightly lowered the size from 16 to 14
* Removed tooltip referencing the scrapped lighter mechanic
* Renamed Fuse to Big fuse to resolve an issue with Expanded facility



Library

* Removed



5.2.0



Overall

* More minor internal changes



Storehouse

* Fixed 3 puddles that ended abruptly



Drains

* Drastically improved lighting in the fan tile
* Added a chance for water in the fan tile
* Added a new staircase tile
* Lowered the chance of the existing staircase tile
* Changed depth weights for the stairs so they should dead end a lot less
* Changed the stairs to not repeat instantly
* More tile variants for the machinery tile in the facility section
* More tile variants for the small closet tile in the facility section
* Added chance for smaller pipes in the large junction pipe in the pipes section
* Reworked the way the water is handled internally, allowing me to easily add variants
* Reduced the pipe's branch size from 4 to 3
* Greatly reduced movement penalty in water



Bunker

* Remodeled lockers
* Added various random variations of the insides of the locker
* Changed armory door sound to be global
* Delayed the armory door to open after a few seconds
* Another gun has been added as a decoration in the armory
* A new special interaction with a certain item
* Did a bunch of things that I deleted after so just like know the thought was there. I just cant do anything for this fucking interior man.



Liminal Facility

* Readded weights



Bunker Scraps

* Added the Jammer which allows you to turn off the interior's power without pulling the apparatus
* Fixed incorrect icon for fuse
* JKConfig



5.1.2



Overall

* The rest of the VR support
* Minor internal changes



Liminal Facility

* Potentially fixed load times, I removed the flickering light and it's audio source so hopefully it's all good???





5.1.1



Overall

* VR support for like... Everything besides the single barrel shotgun and Crowbar.
* Added fade distances to the lights, should help performance



Liminal Facility

* Removed default weights due to it still being loading intensive. You can always just add the injection back using LLL's config if you do not care.



5.1.0



Overall

* Increased the filesize again
* Separated the interiors into their own assetbundles
* Renamed the existing assetbundles so they're more clear as to what they are
* Updated readme to be more helpful on how to report bugs
* Added assetbundle section to the readme where it lists what is in each assetbundle



Storehouse

* Fixed the issue with main not spawning sometimes (Paco fixed it with an update to LLL but I wanted to clarify for people who didn't know)
* Removed "Hi" door out of sheer panic when that issue was happening. Too lazy to readd.



5.0.0



Overall

* Increased the filesize again



Bunker

* Added to the game



Substation (Not a unique interior, Drains without the pipe section)

* Added to the game



Drains

* Added a few new tiles, some being transitional tiles
* To reflect this, transitional tiles can no longer repeat, so now it's more obvious where you came from and where you haven't been.
* Added some variance to the shelves and rolling cart, and allowed them to spawn scrap
* Made the machinery tile have a unique wallpaper
* This list feels short but trust me these are nice changes that took a while to do



Storehouse

* Replaced the lever for the main entrance with a new model from Bunker



Tower

* Removed all detail maps (I think)
* Changed elevator shaft inside texture



Liminal facility (done last update, only now documenting the changes because I'm evil and silently added it)

* Readded to the game
* Changed it's entrance gimmick
* It's no longer a door internally (yay)



4.2.0



Overall

* Increased the filesize because I'm a moron (Me from the future, this was a lie. I readded Liminal LOL.)



Storehouse

* Readded the Hi door. Off by default, enable it in configs to see it in game
* Fixed some minor z fighting



Bunker

* Rebalanced the single barrel shotgun's price



4.1.2 (Patch of shame but also look hey 2 new tiles)



Overall

* Fixed the Radmech being bundled for no reason



Drains

* Added two new tiles, the processing room and the narrow endcap storage tile.



Bunker

* Added in one of Bunker's scraps early for parity with Ignition releasing. The single barrel shotgun can be found there. (Download BeanieLib)



4.1.1 (Traditional patch of shame)



Storehouse

* Fixed the fire exits randomly killing you



Drains

* Bundled it's level matching properties



Tower

* Now has a floor if it doesn't generate downwards



4.1.0



Drains

* Added a new variant to the transitional tile
* Remade the catwalk junction tile
* Added 2 new tiles, the floor pipe storage room, and the narrow hallway



4.0.5 (You would not believe)



Storehouse

* Fixed the break room using the old blockers
* I know I did something else but I honestly don't remember



4.0.4 (Hah. 404) (patch of shame AGAIN AGAIN)



Storehouse

* Lowered the height of each shelf on the big shelves so its easier to see scrap
* Added a few more variants of the big shelves



Tower

* Re-enabled the basement tiles (Whoops)



4.0.3 (patch of shame AGAIN)



README

* Removed most of the known issues as they have been fixed in prior updates
* Added an overall known issues



Storehouse

* Added colliders to the ramp (whoops)



4.0.2 (patch of Batby)



Storehouse

* Fixed a couple of pipes clipping into the trim
* Made stairs not immediately be dead ends
* Changed the texture of the breakroom door to fit the walls better. Might end up just retexturing this whole room at a later point
* Changed the slope in the indented room to be curved instead of a hard angle
* Changed the texture of the edge in the indented room to be more visually appealing
* Lowered the water when flooded in specifically the indented room so that you can actually see items on the shelf
* Removed all mentions of the vanilla powered door
* Removed the second copy of water in the storage room with the elevator shaft on flooded/ocean
* Marked the main wall textures to be gpu instanced
* Something else probably I don't remember



Drains

* Added railings to the main entrance room
* Fixed two pipes in the catwalk tile that just... didn't connect to anything
* Hopefully fixed kill trigger not killing you if you dive into the water



Liminal facility

* Finally added a guaranteed spawn door that teleports you randomly outside when interacted with



Tower

* Pushed it over, called it mean names, and broke its legs with a metal pipe
* Slightly pulled out the stairs in the basement garage so there's a bit more of a landing to stand on where the door opens



4.0.1 (patch of same)



* Uploaded the correct version



4.0.0



Overall

* Probably fixed the doors pushing you when opened or closed



Storehouse

* Completely remade and uses different doorway sockets now, which allows me to actually use different wallpaper textures
* Added a large storage room tile
* Remade the interior stinger
* Removed the Wide hallway and Elevator Large Storage
* Removed the :3 door for now (or maybe forever iunno)
* Removed steam leaks



Drains

* Made it so the dead end drop tiles have a 16% chance for water and a 16% chance for the fall to be covered with a grate
* Changed the curved drop to straighten into a straight drop. I did not check it with a flashlight before and it looked very survivable with a flashlight
* Prefabbed the water so I can easily add new variants
* Slightly dimmed the lights in the circular pipe junction
* Something else I forget



Tower

* Reduced chance of the elevator doors being closed, even though it was a 30% chance of being closed, rng is rng
* Still have not made the Elevator key spawn. Whoops



Liminal facility

* Temporarily removed fake Storehouse due to not wanting to port the new one right now



3.0.1 (Patch of shame)

Overall

* Hopefully fixed an issue of old birds being bundled again.

Drains

* Actually bundled it's injection this time, so it should actually show up places automatically now.
* Added the breaker box.
* Slightly dimmed the lights in the circular junction
* Fixed potential z fighting issues in the 3 circular junction rooms.
* Changed the tag to not be Canyon and instead be Redlight for red lights.
* 3.0.0

  Overall

  * Changed the default of the silly poster remover to remove them by default
  * Added 4 posters made by the incredibly talented Beef!

  Drains

  * Added to the game. Apologies if it's a little rough, I'm tired. Please let me know if anything is wrong. Give me feedback.

  Tower

  * Remade the elevator door system to use animations and be synced. I have made a custom scrap item to allow you to open the doors, but the animations do not have sound and I have yet to implement the scrap item actually spawning. This will come later. It is 5 in the morning.

  Storehouse

  * Added one tile. I would have liked to make it more finished but I could never get myself to work on the elevator mechanics and doing the sound design, so I'm adding the tile as it is now, and hopefully I will get around to finishing it. I do not like working on Storehouse.

  Liminal facility

  * Added a config to turn off the beeping smoke detectors
* 2.5.0 (Rework update)

  Overall

  * Made it so you can add any interior to your own moon by adding the right tag. Tags are found on the readme
  * V70 radar support. Liminal facility stays blank.
  * Added a new config.
  * Fixed snare fleas on LF and Storehouse

  Liminal Facility

  * Added 6 New tiles
  * Added more variants of the actual rooms with different shapes
  * Added a trim to the walls to make them prettier (There is a config to turn this off)
  * Added a low chance for a light to be flashing
  * Added a new archway connector
  * Increased the chance of furniture to spawn
  * Allowed for more than one furniture spawn per tile
  * Slightly decreased max size
  * Removed the industrial section
  * Temporarily disabled the manor starting tile
  * Fixed the lights

  Tower

  * Added car to the garage basement tile
  * Ported 2 of the 4 LF industrial tiles
  * Fixed the straight stairs tile from not having walls if it doesn't connect on the lower floor.
  * Removed the tiny mystery wall staple

  Storehouse

  * Fixed one rare hallway that was slightly off grid, preventing it from reconnecting to itself.
* 2.1.0

  Tower

  * Added L Shaped multiroom (Room)
  * Added Ladder room (Room)
  * Added Alternative stair room (Hallway)
  * Added Storage closet (Hallway)
  * Added Weird hole room (Variant)
  * Added Security office (Variant)
  * Added Garage basement (Variant)
  * Slightly reduced branch size
* 2.0.2

  Overall

  * Removed physics API

  Tower

  * Added a chance for a poster on the left side of the elevator door
  * Changed the sound for the kitchen floor to tile
* 2.0.1

  Please tell me if I broke something i haven't tested or slept please god help.

  Tower

  * Added chance for mattresses to spawn on the beds
  * Replaced props that are similar to LC Office scrap
  * Hopefully fixed button sounds not being global
  * Hopefully fixed a niche issue where scrap spawns IN the shelf
  * Adjusted the way washing machines are affected by light
  * Slightly increased the chances of a room in the basement
  * Added missing credits
* 2.0.0

  I think I changed some configs around you might need to redo your GI configs.

  Tower

  * Added to the game

  Storehouse

  * Fixed the main entrance anomaly (no more doors blocked!)
  * Added four new posters (Thanks Monty from hit mod Biodiversity found on thunderstore at https://thunderstore.io/c/lethal-company/p/super\_fucking\_cool\_and\_badass\_team/Biodiversity/)
  * Added a config to specifically remove joke posters
  * Redid the way posters generated entirely
  * Added 4 more versions of the shutter door hallway, 3 of which being references to upturned
  * Fixed corner hallway by breakroom making carpet sounds (thank vodker)
  * Fixed apparatus room's hallway having incorrect roof (are you fucking kidding me)
  * Better branching from main

  The other one

  * Fixed the main entrance anomaly (no more doors blocked!)
  * Added config to turn off the fire exit wind sound
  * Added config to use an alternative fire exit design I made a while ago
  * Better branching from main
  * Should've fixed the rare issue of it not generating at all but I was never able to replicate it please lmk if it doesn't work
* 1.6.4

  Fixed astronomically bad and unfortunate readme misspelling

* 1.6.3

  UPLOADED THE CORRECT FILE THIS TIME OH MY GOD

  Removed Studio interior

* 1.6.2

  Added default configs to Wesley's moons provided by Wesley himself.

  Overall

  * This wasn't even from this update but last update I changed the readme. Cool!

  Storehouse

  * Patched a small hole in the slanty downward tile place with the door sometimes
  * Fixed a scrap spawn sometimes placing scrap in an unobtainable area
* 1.6.1

  THANK YOU SO MUCH FOR 100K

  Overall

  * Added Pedro
  * Drastically lowered silly poster chance
  * Doubled the range of smoke detectors

  Storehouse

  * REMOVED long shutter hallway tile
  * Added chance for elevator shaft room to be a dead end
  * Redid the way the elevator shaft rooms spawned
  * Added chance for pipes in the junction tile
  * Fixed incorrect stair and upper level audio for server room
  * ACTUALLY fixed the elevator falling
* 1.6.0

  Storehouse

  * Added thin storage room (Spawns rarely)
  * Added hallway junction tile (Spawns rarely)
  * Fixed elevator not falling
  * Slightly increased spawn rates of both individual shaft tiles
  * Increased size by roughly 10 megabytes (whoops)

  Other

  * Unleashed the manor
* 1.5.5

  Patch of shame

  Storehouse

  * Fixed seam on stairs
* 1.5.4

  Storehouse

  * Fixed really niche z fighting issue with stair tile generating next to an elevator shaft
  * Lowered the range of the elevator shaft red lights (they could reach into other tiles)
  * Deleted really nasty vent spawns left in the elevator shaft (spawned things out of bounds)
  * Allowed the elevator have a chance to drop in slightly varying spots
* 1.5.3

  Dude what the fuck am I doing

  Storehouse

  * Fixed missing storage room at the end of the elevator shaft
* 1.5.2

  Patch of shame 2

  Overall

  * Reduced size of the entire file by 20 megabytes (hooray!)
  * Hopefully unbricked lethal quantities
  * Got rid of the accidental second level 1 flow

  Storehouse

  * Added new shelf spawns in breakroom
* 1.5.1

  Patch of shame

  Overall

  * Fixed explosion bug
  * Added a config to remove any posters
  * Uh probably more
* 1.5.0

  Update so unimportant it skipped 4 versions

  Storehouse

  * Added Elevator storage room (Spawns rarely only once)
  * Added Server room (Spawns only once)
  * Added Break room (Spawns only once)
  * Added Taller stairs (spawn only once)
  * Added Elevator shaft (can spawn multiple times)
  * Added Steam leaks
  * Added more posters
  * Added custom stinger (toggleable)
  * Added config for randomized lighting (default on)
  * Added tag detection to have some fun stuff on ocean and flooded moons
  * Added props that make sound to add a bit more variety to the soundscape (pipes making sounds)
  * Made elevator falling before apparatus pull the default (configurable)
  * Stairs and storage rooms are less common
  * Easter egg door chance lowered by half
  * Increased size by roughly 20 megabytes (whoops)
* 1.1.1

  Storehouse

  * Made the pipe wall blocker randomly choose its pipes
  * Added a new connector that adds pipes along the wall
  * Hopefully fixed lag with enemies that can leave the interior
* 1.1.0

  Storehouse

  * Remade the storage cubby hallway
  * Storage rooms spawn in 4s instead of 6s
  * Storage rooms are now slightly more common
  * each Storage cubby is individually randomized instead of the whole set
  * More than one storage cubby can now spawn
  * Fixed a small issue with big stair tile having navmesh in the wall
* 1.0.0

  Storehouse

  * Initial release (yayyyy woohoo yippie)


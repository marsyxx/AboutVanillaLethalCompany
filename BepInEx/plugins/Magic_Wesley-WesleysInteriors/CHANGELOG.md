# CHANGELOG
## 5.0.19
- Grand Armory
  - Fixed the uncommon crash
- Art Gallery
  - Fixed the uncommon crash
  - Did.. something to prevent the painting cheese
  - Items should now spawn behind the paintings as non-host clients
- Extended mineshaft
  - Fixed some walls being on the wrong layer
- Atlantean Citadel
  - The secondary reward item should now be obtainable if the objective is completed before the chamber is raised
  - Fixed enemies being able to walk through the tomb door on the alternate variants
- Carnival
  - Fixed objects being able to spawn above the large pit
- Greenhouse
  - Fixed blatodara burning early
  - Fixed items being able to spawn below some props making them unreachable
- Lab
  - Sulfur and Peroxide have their values fixed
- Sewer
  - Made some changes to the elevator event
- Dilapidated manor
  - The third secondary should now be able to show up
- Fixed a bunch of other misc things
## 5.0.15
- Grand armory elevator should work in vr again
- Fixed some miscellaneous bugs and colliders
## 5.0.14
- 
## 5.0.13
- Existence darkens
## 5.0.12
- Reality decays
## 5.0.11
- Extended mineshaft
  - I hate you
  - Reworked the generation entirely
## 5.0.10
- Grand Armory
  - Fixed some Exterimator related issues
- Dilapidated manor
  - You can no longer grab the relic through the safe
## 5.0.9
- Strange day
## 5.0.8
- Rubber Rooms
  - Fixed the medallions not spawning or disappearing
## 5.0.7
- Extended mineshaft
  - Changed the generation again
  - Capped the max interior scale at 3
  - Fixed two dead end tiles never generating
  - Vents should no longer spawn in large crates
- Art gallery
  - Turrets should no longer be able to spawn in certain walls
- Grand Armory
  - Fire exits should no longer spawn inside the doors of the cold storage tile
  - The exterminator event will now always successfully complete when all thumpers are killed
- I am sure I fixed some other things but I have not had the time to note them down
## 5.0.6
- Atlantean citadel
  - Added some checks to the tribute puzzle
  - Added a hint to the stars puzzle
## 5.0.5
- Fixed an issue rendering Grand Armory's fire exits unusable
## 5.0.4
- Optimised the filesizes slightly
- Fixed some minor issues
## 5.0.2
- Asten Lab
  - Items can no longer spawn below the stairs in the freezer tile
- Grand armory
  - Some vents should no longer be below the catwalks
  - Grand armory fire exits have been moved and can no longer clip you into props when entering
- Art gallery
  - Scrap items can no longer spawn inside pedestals or displays
- Some other fixes I didn't list
## 5.0.1
- Carnival
  - Crystal balls now have the correct item properties
- Atlantean citadel
  - Added small lights to levers so they become more visible
  - Fixed one of the tiles having wrong textures on other variants
- Extended mineshaft
  - Changed up the generation a bit
  - Should now have an easier time generating on moons with a large interior (Such as Lecaro)
  - The hole in the oven tile is no longer there
- Rubber rooms
  - Loot can no longer get stuck below chairs
  - Decreased the value of the finger item
- Various other fixes I didn't note down
## 5.0.0
- All interiors
  - Lots of fixes
  - V81 support
  - VR support
  - Added secondary objectives
    - Every interior has a set of 3 random objectives it can pick from, these range from logic puzzles to tests of combat and endurance, all of them are unique
    - Completing this objective will open, unlock or reveal a special container with a reward
    - Each reward has three variants, high value, mid value and low value, the reward you get is randomised, but, the faster you complete the objective and obtain your reward, the higher the chance you have for a high value reward, take too long and you'll more likely get a low value reward (this is influenced by the vanilla luck system)
    - Can be turned off in the configs
  - Added events
    - Every interior now features the chance of generating a newly added event tile
    - Event tiles have a difficult and deadly challenge which can be overcome for a good reward
    - These events are completely new and unique from one another
    - The only interiors not featuring a new event are Toy store, which has the cake and Rubber Rooms, which has the void rooms
  - Added bestiary entries to most of the added enemy variants
- Asten Lab
  - Added
- Carnival
  - Added
- Toy store
  - Reworked a bunch of models (including all the tile models)
  - Updated a ton of textures
  - Revamped the look of about every tile
  - Updated the apparatus event :)
  - Added a carousel to the large tile
  - Added a bunch of reverb triggers for immersion
  - Nutcracker statues will once again spawn outside
  - Some other stuff
  - Switched out all the sketchfab models for newly made ones
  - Fixed the cutoff of the chest closing sound
  - Added 3 randomised optional objectives
    - Kill the key keeper
    - Turn the masks
    - Avoid the wires
- Grand armory
  - Complete visual rework like.. everything.. totally different from top to bottom
  - Completely changed the elevator
  - Updated the loot rooms
  - Overhauled the generation, this might be my favourite iteration
  - Added a new apparatus tile to the acid area
  - Updated the lurker's vfx and sfx
  - Removed the wake the birds config as old birds spawning systems have changed
  - Added a bunch of reverb triggers for immersion
  - Added openable shipping containers
  - Switched out all the sketchfab models for newly made ones
  - Added The Exterminator
  - Added 3 randomised optional objectives
    - Tune the frequencies
    - Find the code
    - Decipher the morse code
- Rubber rooms
  - Enemies can now follow you through the fog walls into the rubber rooms
  - Switched out all the sketchfab models for newly made ones
  - Updated some textures
  - Remodeled the isolation room doors
  - Remade every tile model (should be a big fps boost + nicer visuals)
  - Added 4 new tiles
  - Added some more ambience laughs
  - Added a bunch of reverb triggers for immersion
  - Updated the void rooms
  - Void rooms now spawns void items
  - Multiple people can now be sent to the void rooms at once
  - Added a few more scares
  - Reduced the scale at which it generates
  - Added 3 randomised optional objectives
    - Gut the corpse
    - Discover the body
    - Fill the test
- Atlantean citadel
  - Updated some prop models
  - Made some of the variant's lights brighter
  - Updated the chest
  - Updated the loot distribution
  - Made it generate slightly smaller
  - Added 4 new tiles
  - Added the Atlantean Tomb
  - Added a bunch of reverb triggers for immersion
  - The flood water should no longer slow you down as much
  - Changed the textures of the metal citadel variant
  - Updated the visuals for the Beholder's eye event
  - Remade the model for the Beholder's eye
  - Added 3 randomised optional objectives
    - Fill the waters of tribute
    - Turn on the fountains
    - Seek the hidden stars
- Fractured complex
  - Added 8 new tiles
  - Done some performance optimisation
  - Updated the loot distribution
  - Added the Compactor
  - Added a bunch of reverb triggers for immersion
  - Added 3 randomised optional objectives (one of which inspired by outlast trials)
    - Stabilise the pressure
    - Fix the power panels
    - Traverse the security room
- Greenhouse
  - Made the corn taller
  - There's now always an open path through the cornfield
  - Added a more maze-like tile to the cornfield
  - Scrap can no longer spawn in regular corn field tiles
  - Added a point of interest type tile with scrap spawns
  - Made some changes to the greenhouse tiles
  - Scrap spawns have been greatly reduced in regular greenhouse tiles
  - Added a water filtration tile to the greenhouse section
  - Added a glass hallway tile to the greenhouse section
  - Added a bunch of reverb triggers for immersion
  - Did some optimisation to corn
  - Added the Blatodara swarm
  - Added 3 randomised optional objectives
    - Power the towers
    - Stabilise the water pressure
    - Burn the Blatodara
- Deep sewer
  - Made a new main entrance tile
  - Main entrance now spawns in the middle of the interior
  - Largely changed the loot distribution
  - Added 13 new tiles
  - Made it easier and less jank to traverse through the small pipes
  - Made the interior generate smaller
  - Updated some textures and normals
  - Updated the Bloody and Cosmic variant
  - Added a bunch of reverb triggers for immersion
  - Added the deep maintenance elevator
  - Added 3 randomised optional objectives
    - Stabilise the flow
    - Restart the power
    - Traverse the sludge maze
- Art gallery
  - Mostly restored all the features lost to the corrupted project
  - Fixed the green lights issue
  - Remodeled all the tiles, giving them a bit more flair
  - Switched out all the sketchfab models for newly made ones
  - Updated the textures and normals
  - Reworked the main entrance layout
  - Reworked the mysterious paintings
    - Lasso will now move through the paintings and stalk you throughout the gallery
    - Lasso can now hear
  - Lighting changes
  - Added a bunch of tiny art pieces to pedestals and displays to fill up the emptiness
  - Updated the painting models
  - Added some more visual details
  - Added more decorative statues
  - Added two new tiles
  - Removed the loot safes
  - Updated the fog machine visuals
  - There can now only be one fog machine
  - Made the interior generate smaller overall
  - Completely overhauled the elevators to feel better and look more realistic
  - Enemies can now travel between floors
  - Updated the tile layout of the planetarium and the ogopogo tile
  - Added a bunch of reverb triggers for immersion
  - Improved performance
  - Added the lost corridors
  - The interior should now generate smaller
  - Added 3 randomised optional objectives
    - Decipher the code
    - Crack the vault
    - Discover the tower
- Dilapidated manor
  - Updated the web textures
  - Added the attic tile
  - Added the large library tile
  - Added the dining tile
  - Added the wine cellar tile
  - Added a bunch of reverb triggers for immersion
  - Moons with the tag “RegularManor“ will switch the textures to the vanilla manor textures
  - Added 3 randomised optional objectives
    - Reveal the key
    - Pull the books
    - Prepare for dinner
- Extended mineshaft
  - Updated the generation
  - Added an extra deadend tile (nothing special)
  - Added the gear up tile
  - Added the overhang cave tile
  - Added the spider cave
  - Added a bunch of reverb triggers for immersion
  - Shelves on which articles spawn during the trial will now only appear in the trial
  - Added 3 randomised optional objectives
   - Flick the switches
   - Fuel the generator
   - Dissolve the webs
- Store
  - Updated for v81
  - Removed from the pack
- Morvdonalds
  - Updated for v81
- Extended facility
  - Added the new v81 tiles
  - Removed the dark rim on the maze walls
  - Updated the maze wall materials
  - Added the large wire tile
  - Increased the amount of Mechanical parts that can spawn from 4->5/6
  - Inserted trial tickets will now save until quota is lost
  - Fixed a bunch of uv bugs
  - Made the interior generate much smaller to account for the larger tiles
  - Fixed the door sounds
  - Added more reverb triggers to small and large tiles
  - Added 3 randomised optional objectives
    - Restart the power
    - Fix the breakers
    - Control the pressure
  
## 4.1.15
- Fixed the new patterns of the Toy Store cake
- Added something to the cake when it's eclipsed
- Some minor sfx changes
- Fixed the door open/close collision
## 4.1.14
- Updated the toy store cake, updated the visuals for the spooky version
## 4.1.12
- Fixed the spooky cake room in toy store
## 4.1.11
- Extended mineshaft now has the original blue lights for cave entrances
## 4.1.10
- Deep Sewers
  - Fixed an issue with fire exits
## 4.1.9
- Deep Sewers
  - One of the tiles had an unpassable entrance
## 4.1.8
- Deep Sewers
  - Added a new tile to the arch tunnel section
- Updated the weights for Dilapidated manor, Expanded facility, Expanded mineshaft to match the weights of vanilla interiors on the vanilla moons
- Probably something else
## 4.1.7
- Deep Sewers
  - Added two more tiles
  - Some uv changes
  - Treasure room damage triggers no longer exceed the bounds of the tile
  - Probably something else
## 4.1.6
- Fixed an oopsie
## 4.1.4
- Deep Sewers
  - Some visual changes
  - Added faint dust particles
  - Added some more vent spots
  - Added a variant for the "Bloody" tag and the "Argon" tag
## 4.1.3
- Optimised some interiors, brought the interior pack down 120 mb
- Fixed some things in deep sewers
## 4.1.1
- Deep Sewers
  - Fixed some mapradar issues
  - Fixed out of bounds landmines
## 4.1.0
- Added the deep sewers
## 4.0.22
- Added the store
## 4.0.21
- I had just gotten up from my chair when I realised I accidentally invented an instant kill fire exit
## 4.0.20
- Expanded Facility
  - Added two more tiles (Large catwalks, Tall server room)
  - Added more cables and boxes to the cable tile
  - Updated the wall textures of the maze area
  - Made big changes to the generation
  - Added some uncommon vines as decoration
- Toy Store
  - Small texture fixes
  - Tunnel rooms now have wall blockers again in spooky mode
## 4.0.19
- "Wake the birds" config is now defaulted to false
- "Spooky mode" config is now defaulted to false
- Removed asteroid-13 from the Grand Armory weights
- Made the manor variant of Atlantean Citadel less shiny/smooth
## 4.0.18
- Fixed a silly vent spot on citadel
## 4.0.16
- Some visual/audio changes to the facility trial
- Also added some extra visual indicators
## 4.0.14
- Fixed some paper wall jank
- And some other shit
## 4.0.13
- Added two extra tiles to Expanded Facility
- Electric gates will now zap players carrying items tagged with "metal"
## 4.0.12
- Updated interior weights
- Added Expanded Facility
## 4.0.11
- Changed the LLL dependency
## 4.0.10
- Expanded mineshaft
  - Added back the water in the water cave tile
  - Made it easier to crawl through the underwater tile without getting stuck on edges
- Added WaterAssetRestorer as a dependency
## 4.0.9
- General
  - Added a config to allow the trial content to only spawn on moons tagged with "Trials" (enabled by default)
- Dilapidated manor
  - increased the min/max amount of hanging lights
  - made the lights slightly brighters
- Expanded mineshaft
  - Added an extra sign hinting towards the pressure room
## 4.0.8
- Dilapidated manor
  - Look.. It is not great to roll out so many patches in a single day.. but.. I just.. I just really wasn't happy with a past decision I made and I felt like I just really needed to do something about it you know. It's not a good feeling knowing that there's people out there experiencing what is to you.. a mistake, a mishap or maybe even just something that's just not right in a way.. So I just.. I just have to. It's really important to me. I need to deliver the best experience I can. So sometimes I have to do what is right and fix my grave mistakes. Thank you for understanding.
  - Halloween props now only show up on Hallowed weather
## 4.0.7
- okay so I added some scan notes for convenience
## 4.0.6
- OR THATS WHAT YOU THOUGHT HAHAHAHAHHAHAHAHAHHAA
- Don't worry guys, I'm stable
## 4.0.5
- Expanded mineshaft
  - Added blue lights for cave entrances
- Last patch of the day, prolly
## 4.0.4
- Expanded mineshaft
  - Fixed a hole in mineshaft
  - Slightly rebalanced the scaling and loot spawns
## 4.0.2
- hm
## 4.0.1
- Dilapidated manor
  - Removed reverb triggers to fix strange reverb trigger behaviour
- Expanded mineshaft
  - Removed reverb triggers
## 4.0.0
- Toy store
  - Changes to light dimming leading to big performance improvements
  - Fixed a connector missing in spooky mode
  - Updated the web textures
  - Fixed the pumpkin mesh
- Grand armory
  - Changes to light dimming leading to big performance improvements
- Atlantean Citadel
  - Changes to light dimming leading to big performance improvements
- Greenhouse
  - Changes to light dimming leading to big performance improvements
- Expanded mineshaft
  - Added
  - It's mineshaft but with small visual updates and 11 additional tunnel tiles
- Dilapidated manor
  - Added
  - Basically just a ruined skin for manor with additional Halloween shenanigans
- Fractured Complex
  - Fixed lights still being emissive when turned off
  - Changes to light dimming leading to big performance improvements
- Rubber rooms
  - You are now able to grab items on morgue trays
  - Increased the chance of the gutted man appearing
  - Changes to light dimming leading to big performance improvements
  - Moved the Void Head to Wesley's Enemy Variants
## 3.0.8
- Fractured Complex
  - Made some changes to ambience sfx
  - Made the lights a bit brighter
  - Updated the look of the factory rooms to make them more distinct
  - Made some changes to light spawns
  - Fixed items spawning in props
  - Fixed enemies, hazards and scrap being able to spawn in the floor
  - Fixed a slight gap between door connectors and the wall in the stairs tile
## 3.0.7
- Rubber rooms
  - Void area now only spawns 4 items
  - Some changes to morgue trays
## 3.0.6
- Rubber rooms
  - Fixed void head
## 3.0.5
- Greenhouse
  - Fixed the stinger cutting off
## 3.0.4
- Greenhouse
  - Reduced damage taken by the acid rains
  - Fireflies now dissappear after the apparatus is taken
- Rubber rooms
  - Fixed an oversight with the fake fire exit
## 3.0.3
- Fixed an issue with RR void zone
## 3.0.2
- Rubber rooms
  - Fixed the missing wall blockers in rubber rooms
- Atlantean Citadel
  - Fixed the issue with Cullfactory culling the main entrance tile while in an adjacent stairs tile
## 3.0.1
- Greenhouse
  - Walls be walls
## 3.0.0
- Fractured Complex
  - Added
- The Greenhouse
  - Added
- Toy Store
  - Fixed the map radar
- Art Gallery
  - Fixed the map radar
  - Fixed a bunch of navmesh issues
  - Added a little hint in the apparatus room
  - The gallery now generates smaller
  - The gallery now has a 50% chance to have an alternate look
  - Some visual changes
  - Added a bit of tile variation
  - Added secrets
  - Switched out the main entrance with the manor entrance
  - Decreased the amount of extra scrap spawned in glass displays
  - Two openable safes will now spawn in the gallery.. though.. one of these I wouldn't trust
  - PS. my art gallery project got corrupted meaning there won't be any fixes or updates to it for a while
- Rubber Rooms
  - Fixed the map radar
  - Spawned items on morgue trays will now be parented to it
  - Items are now more likely to spawn on morgue trays
  - Vents can now rarely spawn in the large rubber rooms
  - Changed lighting temperature in most rooms
  - Changed the lighting entirely for the eclipsed variant
  - Added a few new tiles
  - Added a few new weird things
  - Don't get taken by the void
  - Fixed some weird generation issues
  - Added a subtle tell to the spooky door
- Atlantean Citadel
  - Fixed the map radar
  - Made the small caves less common
  - Added two new hazards
  - Added a new cave tile
  - Made the small caves easier to navigate through
  - Added a bunch of tiles to the citadel section
  - Switched out the main entrance with a new one
  - Citadel and cave tiles can now connect directly to the entrance
  - Added 6 new tag-based variations
    - Olympean Citadel  -   Dungeon , Dark
    - Metal Citadel     -   Factory , Murica
    - Ruined Temple     -   Jungle , Temple
    - Molten Fortress   -   Volcanic , Lava
    - Royal Hive        -   Hive , Bees
    - Mansion           -   Mansion
  - Updated the spawn weights for atlantean citadel
- Grand Armory
  - Fixed the radar
  - Did some generation changes to allow more open areas to generate, it is a little inconsistent however
## 2.1.0
- Art Gallery
  - Fixed enemies getting stuck behind curtains
  - performance improvements
  - Made the laser patterns slightly easier to recognise
  - Possibly fixed the elevator
  - The floor numbers on the elevator tile actually match the floor now
- Rubber Rooms
  - Changed the tooltip for closing the horror door
  - The tall rubber room should no longer be able to trap you on the surface of a moon
  - Added mrov
  - Fixed the door eye scare still showing when no spooky mode is enabled
  - Made it more clear what to do when the apparatus is taken
## 2.0.11
- Hotfix for RR
## 2.0.10
- Toy Store
    - Fixed an issue with fire exits
## 2.0.9
- Rubber rooms
    - Added a config to disable the scary door
- Art Gallery
    - Made the interior generation smaller
    - Allowed more scrap to spawn in the glass containers
## 2.0.8
- GET UPDATED ON MONTY HAHAHAHAHAHA
## 2.0.7
- Rubber rooms
    - It comes from the halls
- Atlantean Citadel
    - Updated a bunch of textures
## 2.0.6
- Rubber rooms
    - Fixed some seems
    - Added laughs
    - Hehehe
    - Added more
    - Added some more decorations
    - Fixed shit
## 2.0.5
- Art Gallery
    - Fire exits are more likely to appear near the elevator
    - Added a painting
- Atlantean Citadel
    - Uh I changed something
## 2.0.4
- Rubber Rooms
    - Updated the apparatus effects for a more clear indication in Rubber Rooms
    - The confusing hallway should no longer be able to block you off entirely
    - Fixed some z-fighting
- Art Gallery
    - Possibly fixed elevator desyncs.. report back
## 2.0.3
- Fixed some blockers missing in RR
## 2.0.2
- Small changes to Rubber Rooms
- I honestly don't remember what else I did..
## 2.0.1
- Atlantean Citadel
    - Adjusted scaling
    - Adjusted some scrap spawn locations and balancing
    - Reduced the amount of main branches 3 -> 2
    - Updated default weights to be more common on Atlantica
- Art Gallery
    - Fixed some small things
    - Adusted the fog machine fog
- Rubber Rooms
    - Adjusted scaling
    - Fixed turrets being able to fire through the walls in the infirmary
- Toy Store
    - The apparatus room will can now only be found deep in the interior
## 2.0.0
- Added content <br>
- Updated default spawn weights <br>
- The cake will no longer spawn loot if every victim dies
<h2>1.6.2</h2>
- Default config changes
<h2>1.6.1</h2>
- Slight adjustment to the cake in spooky store <br>
- Wooden doors will no longer push you away
<h2>1.6.0</h2>
- Decreased the file size a bit <br>
- Explosive chests are easier to distinguish on spooky store <br>
- Added a new tile.. with a cake <br>
- Delightful hats have been added to nutcracker statues <br>
- Lowered the damage of explosive chests slightly <br>
- Y <br>
- Thanks for the 1M downloads :D, enjoy your cake
<h2>1.5.17</h2>
- Fixed some colliders
<h2>1.5.16</h2>
- Beans be back
<h2>1.5.15</h2>
- Spooky store is now disabled by default <br>
- Added a config for nutcracker statues to spawn on moons when toy store is present.. because.. funny <br>
- Added a config for old birds to spawn in Grand Armory <br>
- ..Old Birds can now wake up in Grand Armory
<h2>1.5.14</h2>
- Fixed the sinking issue
<h2>1.5.13</h2>
- Fixed some visual jank in Grand Armory that started to annoy me
<h2>1.5.11</h2>
- Fixed some errors <br>
- Entrances are now networked properly <br>
- Changed some stuff <br>
- Yeah idk just have fun
<h2>1.5.10</h2>
- Adjusted something
<h2>1.5.9</h2>
- You can no longer burn to death by being on a platform above lava (or beans (or any other burning liquid))
<h2>1.5.8</h2>
- You can now enter Grand Armory without fearing eternal damnation by acid pit
<h2>1.5.7</h2>
- I kinda completely broke toy store, should be fixed now
<h2>1.5.6</h2>
- The loot room switches *should* now be synced
<h2>1.5.5</h2>
- Fixed one of the parkour platforms being gone
<h2>1.5.4</h2>
- Hotfix 2: electric boogaloo
<h2>1.5.3</h2>
- Hotfix
<h2>1.5.2</h2>
- Included the actual config.. you can play spooky mode now
<h2>1.5.0</h2>
Grand Armory <br>
- Completely redid the way Grand Armory generates to (hopefully) prevent it from not generating sometimes <br>
> This also means grand armory will be wildly different path wise compared to before, you might have to cross an acid section to get to another part of the regular armory <br>
> And occasionally not every type of loot room will be present <br>
- Replaced the acid tiles with one more modular acid tile <br>
- Allowed a second elevator to spawn <br>
- Added stairs to acid pool transition tiles <br>
- Added some variation to the acid pool transition jumps <br>
- Added lava pools to "Canyon" and "Wasteland" tags <br>
- Added ball pits to the "Fun" tag <br>
- Added far audios to certain sounds <br>
- An alarm system will now go off on the loot rooms if the power is down, you can disable these <br>
> Could help with finding the loot rooms <br>
- Made fire exits more likely to spawn deeper in the armory <br>
- Added a loot spot in the cold storage server room <br>
- Added some more ambient sounds <br>
- Added a possible apparatus spot in the cold storage area <br>
Toy Store <br>
- Spooky mode (configurable) <br>
- There's nothing below the ballpit <br>
- Made shotgun doors a bit more dangerous <br>
- Some optimisation <br>
- Added the (I think) unused nutcracker crawling through vent sound as ambience in some tiles <br>
- Removed a wall turret <br>
- I was supposed to work on my moon update and I was only going to fix an issue with Grand Armory.. now toy store has a whole Halloween mode T-T <br>
<h2>1.4.5</h2>
- I am feeling kind today, removed the turret firing squad I placed in the Grand Armory entry tile <br>
- Added some spike trap spots in the entry tile
<h2>1.4.4</h2>
- Fixed empty room in toy store
<h2>1.4.3</h2>
- Hotfix 2: electric boogaloo
<h2>1.4.2</h2>
- Hotfix
<h2>1.4.1</h2>
Grand Armory <br>
- Changed the water color <br>
- Added baked beans to the acid types
<h2>1.4.0</h2>
Toy Store <br>
- Shotgun traps will now fling your body <br>
- Shotgun traps can now hurt enemies <br>
- Increased damage for chest traps <br>
- Chest traps now use burnt body type <br>
- Increased blast range for chest traps <br>
- Chest traps can now hurt enemies <br>
- Apparatus pull event now deals a 100 damage instead of 99 <br>
- Made the wire on the "welcome home trap" harder to spot <br>
- Min-max of shotgun traps is now 5-8 <br>
- Min-max of welcome home traps is now 2-4 <br>
- Fixed some inverse softlock areas <br>
Grand Armory <br>
- Ceiling crushers will now burst your head <br>
- The elevator can now crush you <br>
- Hooks now deal 25 damage <br>
- Lurkers now deal recurring damage (10dmg/0.2s) <br>
- Lurkers can now hurt enemies <br>
- Fixed an issue where recharge station particles would always play <br>
- You should no longer be able to teleport onto acid pools and instantly die <br>
- Enemies should no longer be able to teleport onto or underneath the acid pools <br>
- Falling in acid no longer has a body type <br>
- Added some off mesh links to areas enemies couldn't normally reach <br>
- Added different versions for the acid pools based on level tags <br>
- Removed LethalToolbox as a dependency <br>
- Added JLL as a dependency
<h2>1.3.17</h2>
Toy Store <br>
- Fixed an issue where enemies couldn't enter the entry tile in certain areas
<h2>1.3.16</h2>
Grand Armory <br>
- Forgot to disable something, should be slightly more optimised now
<h2>1.3.15</h2>
- I changed nothing
<h2>1.3.14</h2>
- Fixed the custom apparatusses turning into regular apparatusses after lobby reload 
<h2>1.3.13</h2>
Grand Armory <br>
- Removed the testing elevator
<h2>1.3.12</h2>
Grand Armory <br>
- Fire exit fix
<h2>1.3.10/11</h2>
Grand Armory <br>
- Items will now finally stay on the Elevator.. finally <br>
- Made the interior smaller for players with a mild case of skill issue :D <br>
- No rubber rooms update today, 1.3.9 added a couple tiles'n things
<h2>1.3.9</h2>
- Rat but more <br>
- Fix exit fire yes tired
<h2>1.3.8</h2>
Grand Armory <br>
- fix <br>
<h2>1.3.7</h2>
Grand Armory <br>
- Stopped fire exits from being weird (for real this time) <br>
Toy Store <br>
- I changed nothing here lol
<h2>1.3.6</h2>
Grand Armory <br>
- Replaced a sound <br>
- Added a new texture <br>
- T <br>
- Stopped fire exits from being weird hopefully
<h2>1.3.5</h2>
Grand Armory <br>
- changed generation, should be easier to get around the place and find loot spots
<h2>1.3.4</h2>
Toy Store <br>
- Slightly adjusted the new entry room
<h2>1.3.3</h2>
Grand Armory <br>
- Fixed spike traps not spawning <br>
- The entry stinger now randomly plays when you first enter due to custom interior stingers being broken <br>
^It has a 20% chance to play on first enter<br>
- Added breaker box spawn spots in cold storage <br>
Toy Store <br>
- I forgot something and it broke the entire interior.. oops <br>
- The entry stinger now randomly plays when you first enter due to custom interior stingers being broken <br>
^It also has a 20% chance to play on first enter<br>
- Also fixed the spike traps not spawning <br>
- Added more variation to the entry tile
<h2>1.3.2</h2>
Grand Armory <br>
- Ported the project to v61 <br>
- Elevator is now smoother <br>
- Elevator now has navmesh (enemies can walk on it) <br>
- Elevator is now faster <br>
- I DELETED ONE OF THE TILES ASDFAKJHSFGAJSHGFJHASG <br>
- Re-added a tile <br>
Toy Store <br>
- Ported the project to v61 <br>
- Fixed some things for v61 <br>
- Uh yeah probably something else as well
<h2>1.3.1</h2>
- Fixed the elevator desync <br>
- Forgot to mention I also added stingers to the interiors in the previous changelog.. whoops
<h2>1.3.0</h2>
Grand Armory <br>
- Couldn't fix the elevator :( have some other things instead: <br>
- Added 2 new ascending tiles <br>
- Added an uncommon caged tile <br>
- Added some alt tiles for a special alt path <br>
- Oh deer <br>
- Added more doors to tiles <br>
- Fixed enemies walking through blocked off doors <br>
- Added a cold storage room <br>
- Fixed fake door backgrounds <br>
- He lurks <br>
- Bunch of generation changes <br>
- Made the toxic waste less vibrant to match the rest of the interior better <br>
- Tagged/retagged a lot of props for proper footstep sounds <br>
- Bunch of optimisations <br>
- Added decorative hooks <br>
- Added not-so decorative hooks <br>
- Added more variation to 'normal' walking tiles <br>
- Some optimisations <br>
Toy Store <br>
- More variation in backrooms/store tile pathing, the backrooms *should* connect to regular store tiles more <br>
- Added a bit more texture variety <br>
- Backrooms hallways now have special blockers <br>
- Made the shotgun trap string slightly thinner <br>
- Removed the eyeballs from the nutcrackers <br>
- Made the nutcrackers a bit cleaner <br>
- I hope you have a good reaction speed (you all are going to hate me for this) <br>
- Made shotgun blasts have a bit more impact visually <br>
- Added a new tile <br>
- Some optimisations
<h2>1.2.17</h2>
- Spike traps are back <br>
- Landmines also exist now
<h2>1.2.16</h2>
- One of the tiles in toy store could put a bunch of loot in unreachable locations
<h2>1.2.15</h2>
- Made the main entrance background lights more yellow <br>
- Made it more likely for chests to have loot <br>
- Loot in chests should no longer be able to spawn underneath the chests
<h2>1.2.14</h2>
- Enemies can no longer walk through certain walls
<h2>1.2.11</h2>
- wa
<h2>1.2.10</h2>
- Explosion sound is no longer global
<h2>1.2.9</h2>
- Ceiling = pain again <br>
- Toy store interior visual details
<h2>1.2.8</h2>
- Imagine forgetting to update your changelog.. couldn't be me
<h2>1.2.7</h2>
- Made the toy store smaller so loot is easier to find
<h2>1.2.6</h2>
- Temp fix for the acid pools in Grand Armory <br>
- You will no longer vanish when standing on the Elevator
<h2>1.2.5</h2>
- Artifrice
<h2>1.2.4</h2>
- Small GA material fix
<h2>1.2.3</h2>
Toys Store <br>
- Added extra doors inside the store part of the interior <br>
- Added the disconnect sound to the apparatus <br>
- The actual project has been updated from v49 to v56 meaning: <br>
- The apparatus will now wake up old birds <br>
- The explosion effect has improved <br>
Grand Armory <br>
- Made the apparatus pull event slightly more dangerous :) <br>
- Probably some other visual things I forgot about
<h2>1.2.2</h2>
- There can now only be 2 to 8 shotgun traps in the Toy Store
<h2>1.2.1</h2>
- Solved some navmesh issues in toy store
<h2>1.2.0</h2>
Toys Store <br>
- The floors should no longer clip through each other <br>
- Added some new tiles for some variety <br>
- Updated some old tiles for some variety <br>
- Added a small tile to the storage section which should add a lot more connectivity and variety in paths <br>
- Red carpets are no longer shiny <br>
- Improved performance a bit <br>
- Slightly increased the default dungeon size to account for the new small tile <br>
Grand Armory <br>
- Added some extra props <br>
- Added CullFactory support <br>
- Added some extra variation to square catwalk tiles <br>
- Idk just have fun
<h2>1.1.19</h2>
- Changed Toy Store apparatus color
<h2>1.1.18</h2>
- Changed GA apparatus color <br>
- Increased GA apparatus value to 280 <br>
- Updated the GA generation to be more diverse at the entrance (hopefully)
<h2>1.1.17</h2>
- Enemies should no longer walk through containers or walls
<h2>1.1.16</h2>
- Now GA entrance turrets are gone too :( <br>
- Turrets in TS should no longer spawn in walls or objects
<h2>1.1.15</h2>
- I love Grand Armory container turrets! but I had to remove them :(
<h2>1.1.14</h2>
- It was desynced.. Whoopsies.. Hopefully fixed now
<h2>1.1.13</h2>
- Added a lovely new tile to the Toy Store which I haven't properly tested so do yell at me if it's broken in any way (it's probably fine though)
<h2>1.1.12</h2>
- Added a few more chests <br>
- You can now hide in chests (enemies will lose you if you close the lid)<br>
- Loot rooms now have more logical loot spawns <br>
- (:<
<h2>1.1.11</h2>
- Wa
<h2>1.1.10</h2>
- Added some extra decoration to the GA
<h2>1.1.9</h2>
- Loot should now properly spawn on cabinets
<h2>1.1.8</h2>
- Minor fixes
<h2>1.1.7</h2>
- Added the recharge station tile
<h2>1.1.6</h2>
- Alright, just throwing this new size at the wall for the GA, please let me know if it's too big
<h2>1.1.5</h2>
- Grand Armory got slightly bigger again <br>
- Added some variation to loot room server racks <br>
- The apparatus tile and elevator tile are less likely to be at the very start <br>
- Added another loot room <br>
- Loot rooms will no longer always have 4 storage shelves <br>
- Some balancing things
<h2>1.1.4</h2>
- Now using extendedmod, teh game should no longer get stuck on randomseed <br>
- Grand Armory got slightly bigger <br>
- Old configs are no longer used
<h2>1.1.3</h2>
- Turrets should no longer fire through objects <br>
- Enemies should no longer walk through objects <br>
- Fire exits will lock down once the apparatus has been taken <br>
- More loot rooms have been added <br>
- Scrap no longer spawns randomly around the place <br>
- More fire exit spots were added <br>
- Elevators now have buttons at the bottom <br>
- Turrets should no longer spawn inside containers
<h2>1.1.2</h2>
- Nukes should now have normal collision <br>
- That one shipping container now has collision <br>
- Elevator fix? <br>
- Made the interior slightly smaller <br>
- fixed acid trigger going through walls <br>
- Added more possible fire exit locations <br>
- Fixed some other things as well 
<h2>1.1.0</h2>
- Added the Grand Armory Interior <br>
- Toy Store Can now have spike traps <br>
- Toy Store loot spots have been Updated/Changed<br>
- Toy Store is now much smaller by default
<h2>1.0.5</h2>
- Bullets fired from a turret will now realise that bookshelves are bookshelves and decide not to phase through it (hopefully)
<h2>1.0.4</h2>
- Lowered the shotgun spawn rate
<h2>1.0.3</h2>
- Chest fix
<h2>1.0.2</h2>
- Fixed a few small details
<h2>1.0.1</h2>
- Chest interact is now on the lid <br>
- Fixed the weird wall chest in the big loot room <br>
- Added another possible chest spot <br>
- More connectivity <br>
- Cull Factory Support <br>
- Fixed items/turrets spawning at a weird spot in the table loot room <br>
- Decreased trap scanning range <br>
- Initial release
<h2>1.0.0</h2>
- Initial release
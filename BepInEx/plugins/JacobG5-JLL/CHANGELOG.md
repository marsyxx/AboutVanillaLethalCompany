## v1.10.1
- JLightningRod now uses a custom rpc for triggering lightning strikes fixing a bug when triggering on non stormy weather.
- JLightningRod has a ton more customization options for lightning's gameplay effects.
- JLineOfSight has a new event fired when the object was previously viewed by one or more players and is no longer in view of any.
- EnemySpawner now checks if an enemy requires a nest and will attempt to spawn a nest for an enemy if it is required and attempts to spawn.

## v1.10.0
- Fixed JThrowableItem explosions not triggering when on company/timeless moons.
- InventoryRemover & ItemConsumer now take the vanilla reserved utility slot into account.
- Added Dependency DestoryItemInSlotFix to fix some errors with destroying items in the v80 reserved slot.
- Added JStaminaController - Component that can modify the local clients stamina when they are within collider bounds.
- Added JLightningRod - Component that can detect and trigger lightning.
- Added JSlimePoint - Component that can slime players near it similar to slime puddles from one of the new enemies.
- Added JLineOfSight - Network Component for checking when players have line of sight to a position.

## v1.9.10
- Recompiled all modules for v80
- Reworked entrance teleport door splash patch for v80 entrance changes.
- Manually patch network behavior classes removing broken example code required by network patcher.

## v1.9.9
- I changed some of the settings in the csproj and have simplified code across the entire project accordingly.
- Updated Netcode for v73

### Core Module
- DelayScript now has methods to start waiting with a set delay.
- RandomizedEvent now has random number events. You trigger it using a Vector2 where X is the minimum value and Y is the maximum value number the host is allowed to generate before syncing to all clients. There is an event for a float and integer value.
- Created JTranspilerHelper as a class for me to create some common transpiler structures easier. (Partial port from other project)
- JLevelEventTriggers hourly events now trigger off a transpiler on TimeOfDay rather than trying to track daytime on their own.

### Item Module
- Made previousPlayer fields of all scripts publicly accessible.
- Throwableitem now cheks to make sure a sell desk exists on a moon before checking if the item is parented to it. (Fixes error spam when an item explodes on Oxyde)

## v1.9.8
- Fixed bug when LLL was present without any .lethalbundle files causing JLL network prefabs to never register.
- WesleyMoonScripts Progression Objects now have an option to delete only when progression is active instead of just when it is inactive.

## v1.9.7
- (SoftMask Fix) JLLEditorModule is no longer included in the thunderstore download.

The editor module contains refrences to the UnityEditor as it is meant to be used for modders within their projects. Under normal conditions it is not loaded in game by BepinEx. But with the combination of some other mods that force the editor module to get loaded in game it can cause reflection errors such as the one that caused SoftMasking to occur.

To fix this the only thing I can do is remove this dll from the thunderstore download. For modders who still want to use this package I will keep an updated build of the dll on JLL's github page.

I also added a README to the github page along with the built Editor Module dll.

- WesleyMoonScripts LevelCassetteLoader now relinks the audio source to audio track 0 when a tape is played. Unity's video player component removes this information when playing a video clip that has 0 audio tracks. Wesley made his clip to reset the projector have no audio tracks causing Galetry to break it's audio after the first tape played.

## v1.9.6
- Recompiled all modules for v70.
- There may be some unfinished WIP features. If you discover one of these please don't implement it in your mods. (I'm pretty sure I removed most of it temporarily tho.)

## v1.9.5
- AllSortedEnemies list now gets filled at slightly later point in time to ensure it captures enemies registered in lethal lib & other modded enemies.
- Fixed JLevelEventTriggers not getting registered correctly after the last update.

## v1.9.4
- LLLHelper level lock method no longer releases the LMU story unlock when locking a level.
- ExtendedLevelLocker `shouldUnlock` is no longer inversed on the LockLevel(sceneName) and HideLevel(sceneName) methods.
- Changed network object initialization from on the `Start` method to the `OnNetworkSpawn` method. This should fix some issues with some in-scene behaviors having problems on clients. (Thanks Diffoz for the help)
- Added Fans. I can now say I have fans. On a more serious note this was added for Beanie's new interior.
- JLevelEventTriggers has changed how instances of the component are stored / cleared to be on the enable / disable of an instance of this component instead of when a new round is loaded. This should allow it to work correctly when the object persists between rounds.
- JLevelEventTriggers `LevelLoaded` event now triggers once dungeon generation completes rather than when the component is initialized.

## v1.9.3
- Patched edge case in DamageTriggerNetworking where a corpse killed by a DamageTrigger gets deleted before it gets fully set up causes error spam. One way I suspect this error could occur is if a player disconnects after being killed by a damage trigger. Reguardless it should now be fixed.
- Defines LCCutscene as a soft dependency in the BepinPlugin.

## v1.9.2
### Core Module
- Moved Interfaces and Enums to a different file for better ogrganization.
- Random Seed Event should now cycle the result allowing for it to be reused multiple times and get different results in the same round.
- EventLimiter now has methods for adding and setting the amount of triggers left for the event.
- DayCycleAnimator now has a method for setting the end time through events.
- RandomSeedEvent replaced roll on start with roll post dungeon generation.
- RandomSeedEvent now reads the seed from the PlayersManager instead of StartOfRound.
- IDungeonLoadListener now gets triggered on PostFix instead of PreFix.
- PlayerFilter & EnemyFilter now check if the player/enemy is dead and will trigger the fail event and cancel the user defined checks if they are.
- JBillboard has settings for creating billboard objects. A billboard traditionally is something that is locked to the camera rotation to make something 2d always face towards the camera in 3d space. This script can be used for that or it can be used to simply make something face in the direction of the player. It has options to lock certain axis of rotation and to lerp position over time instead of being in a locked rotation.
- Added JBridgeTrigger which is a modified version of the vanilla BridgeTrigger (Used for Vow & Adamance). I originally planned to make this as a seperate mod patching and adding new mechanics to the vow bridge but testers told me they didn't want another dependency and I didn't want to make extreme modifications of vanilla behaviors within this mod. Downsides of doing it though a new component like this are if any other mods are detecting for bridge triggers they will not be able to find this but I've been told there aren't *currently* any mods that require this so it should be fine. I just wanted to add this blurb in here because I know this is far from the best way of doing this for niche compatibility but it will be fine for normal use. This script allows the bridge to detect cruisers on the bridge along with the items within the cruisers to deteriorate it's durability. I've also removed the clamps on durability deterioration to allow higher durability bridges to work correctly. You can also define your own fall type strings rather than the integers Zeekers added.

## Item Module
- JGrabbableObject now has equip and pocketed events that pass the player holding the item. These events only get fired if the item is held by specifically a player. The old events will still get fired in situations where the item is not held by a player.
- JGrabbableObject now has a method to destroy the item if it is in the player's hand.
- Fixed JInteractableItem Hold mode only triggering the toggle off.
- JMeleeWeapon now allows you to change the HitId of the weapon. The default is 1 but changing this number may allow different behaviors to happen when things get hit. Setting this ID to 5 would make it behave more like a knife doing things like poping Butlers.

### Wesley Moons
- Moons with integration no longer get locked even when WesleyMoons is not present.
- Progression locking and unlock protection can now be disabed in config.
- Added Script for randomly changing volumetric fog.
- Optimization for LoneTraps.
- Addition of item shops.
- Addition of ProggressionObjects which get disabled / deleted when progression is disabled.

## Editor Module
- Added warning messages on JBillboard & JBridgeTrigger.
- Made some copy paste code into methods.
- Warnings on layer incompatibilities now take collider layer overrides into consideration.

## v1.9.1
- JLLMods created through JSON files will now be correctly registered.
- JLLMods with invalid or empty names will no longer be registered. (This is to prevent future incidents of `..cfg`)
- JLLMods that contain no configs will no longer generate an empty config file.

## v1.9.0
### Core Module
- Fixed mostly harmless error on disconnecting mid game with JClientSync.
- JDestructableObject now has an event for whenever it takes damage.
- Removed Mod Loaded Chaching. Fixed some things in my helper class for determining if a mod is loaded.
- Network Prefabs created through JLL will now work correctly. (Should fix issue with SimpleCommands `/prefab`)
- Added respectEnemyCap and respectPowerCap options to EnemySpawners. Will not spawn an enemy if those conditions are met.

### Wesley Moon Scripts
- HERE BE DRAGONS!
This is a new DLL included with JLL. It contains a lot of very random components and features designed for the Wesley's Moons Journeys Update.
You can use features from this package if you want to experiment but I will not guarantee the same level of support that the rest of the mod has.
Some features were scrapped on and others may not be fully functional outside of their intended use.
If you have an issue while using a script from this DLL then either experiment on your own to figure it out or don't use it.

### Wesley Moon Integration
Included within WesleyMoonScripts is a JLLAddon that you can add to your JLLMod to allow your mod to integrate with the Unlock Progression system. 
You can put all of your levels into a list of their sceneNames and optionally enable/disable force locking (If your moon is set as unlocked by default in LLL and WesleyMoons is present it will change it to be locked by default) and Unlock Protection (Attempts to blacklist moons from being unlocked by most popular moon unlock/unhide mods)

### Editor Module
- Added help messages for invalid layers on JDestructableObject.

## v1.8.0
### Core Module
- JCompatabilityHelper now has a cached compatibility check for LCCutscene.
- JRandomPropPlacer now waits 1 frame after dungeon generation completes before spawning props. 
- A compatibility class has been created for LCCutscene, Diversity 3.0.0, and LittleCompany
- Added DiversityFocusPoint, a component that, when the Diversity mod is loaded, can be used to focus the player's gaze to a fixed point. This is the functionality for looking at posters or monitors in Diveristy.
- Added LCCutsceneTrigger, a component for triggering custom cutscene events with LCCutscene. You can set various parameters and even attach an animator to create more advanced cutscenes.
- Added LittleCompanyScaleModifier which is a component for modifying the scale of players when LittleCompany is installed.
- Fixed ItemSpawnRotation for things using EnemySpawner. This change also inherently fixes rotations for item spawners and several JLLItemModule components.
- EnemySpawner & ItemSpawner scripts have been given options to use name fields in replacement of including an empty enemy prefab. I've had reports of a bunch of mods doing brute-force scrubs of empty SO references and not handling them correctly. While none of these issues are my fault I've still included a way for creators to get around this.
- SeatController now has failsafes to try and force a passenger to exit when the seat is disabled or destroyed.
- JLevelPropertyRegistry now has an event for PostLevelSettup that gets fired after level overrides and other JLL start-of-round events finish running.
- JLevelPropertyRegistry now has a POI system that can be used to define points referenceable by string identifiers. POIs get reset after a level gets unloaded.
- Added JPOI which can be used to define a POI transform that gets added to the current LevelPOIs.
- JClientSync now disconnects its event listener to a hostFilter when deleted.
- Updated DamageTriggerNetworking corpse functionality to optimize and fix issues with corpse attachment.
- Removed LethalLib dependency and now manually registers its networking manager through the network prefab set system JLL previously added.
- Added LethalLib to JCompatabilityHelper loaded mods cache.
- DamageTriggerNetworking player killed event now gives the option to pass a player parameter. Note that the player will be dead at the time of this event triggering so be careful what you use it for!
- Telepoint now sets the player as inside or outside the facility on random teleport correctly.
- Simplified some code by adding IDungeonLoadListener interface. Some pre-existing components now use that interface to trigger something based on completed dungeon generation.
- Telepoint has changed internally and now has an option for setting the player as inside or outside facility. Defaults to `None` which leaves the player in whatever state they were before going through the teleport. This gets ignored when using RandomTeleport.
- Fixed ItemConsumer to work consistently with MoreCompany.
- Added RandomSeedEvent which can be used to trigger events or move GameObjects at the start of a round based on the round seed.
- Added a class for Extensions. Added extensions for interacting with weighted random enumerable and an extension for checking if a PlayerControllerB is a local player.

### Item Module
- Added JInteractableItem which can be used to create some more basic interactable items. Several people wanted an item script like this so now you can have it.
- JGrabbableObject now has some base methods to do more things with events. Added:
  - `DamageHolder(int damage)` - Damages the player holding the item if they are present. (Only works on players)
  - `ForceDrop(bool dropAll)` - When the bool is active will drop all items a player has in their inventory. When unchecked will just drop this item.
  - Fixed hideWhenPocketed not re-enabling target objects when re-equiped.

### Editor Module
- Updated EnemySpawner to function with new properties.
- WeightedEnemyPools now have custom inspector GUIs and have updated Itemspawner ones with the new name replacement stuff.
- Added some new descriptions and help boxes to JClientAttatchedObject.

## v1.7.7
### Core Module
- Fixed issue with my itemspawner code when network spawning is handled externally throwing an error on setting scrap value. (This broke JEventBoxItems with some settings)
- Fixed issue with JEventBoxItem not syncing the boxUses correctly on all clients causing it to not destroy on secondary clients.

## v1.7.6
### Core Module
- Fixed EventLimiter to show up in Unity.

### Item Module
- JEventBoxItem's SpawnItemsOnServer method is now synced correctly when triggered by external events.

## v1.7.5
### Core Module
- All log entries from JLL now display the logging level of the message at the beginning to indicate what level messages are related to.
- In the last update I synced the ItemConsumer's success event for all clients but I was told that's not how some people want it to work so I've made it a toggleable option.
- Items spawned by an ItemSpawner now get their scan nodes updated on all clients correctly. Previously when scanned items didn't show their scrap value but worked correctly when sold when spawned by an ItemSpawner.
- Added JDestructableObject which can be used to make an object damageable by shovels with health. There are options to trigger events based on the weapon a player uses to cause damage to the object.
- Added a null check for SeatController's SetPlayerInSeat function. 
- SeatController now just disables the exit trigger's colliders instead of the entire object when that setting is enabled.
- RandomClipPlayer now lets you set a volume and range multiplier for creatures being able to hear the sound.
- There's now a better API for grabbing JLLMods for other modders to code with. You can use JLLMod.GetMod(name, author) to attempt to get a JLLMod, you can also get all mods by a particular author with JLLMod.GetModsFromAuthor(author).
- JLLMods now have a field for JLLAddons. These can be implemented by other mods to load scriptable objects more easily through a JLLMod.
- Added a bunch more cached mod-loaded statuses to JCompatabilityHelper. These are used in my mods to check if mods are loaded in a faster way than searching all loaded mods every time.
- Added ModLoadedCheck that lets you check if a mod is loaded to trigger UnityEvents or to disable objects.
- Added DungeonFilter which can be used to filter for ExtendedDungeons based on Dungeon Name or the dungeon's content tags.
- Add DungeonFilter which can be used to filter based on the tags or name of a generated dungeon on a moon.
- Re-Added HostFilter to JClientSync which was removed for being broken before the 1.7.0 release that way I could ensure a more stable build for the Weather Registry update. This set of options allows you to run a filter only on the host and then trigger network-synced events based on the result of the filter.
- Added new options to PlayerFilter: Walking, Sprinting, Crouching, Exhausted, Underwater, Sinking, Climbing, Riding Vehicle, Bleeding, Alone, Emoting, and performing special animation.
- Optimized some code for all JFilter components.
- Added some error catches to some of the more important API code and patches in order to future-proof and make the mod more stable. For all the catches I added log-relevant information as errors.
- There is now an API event for when JLLBundles have finished loading. When LLL is present this event will fire when both JLL and LLL have completed bundle loading.
- JRandomPropPlacer now has a prefabWidth variable for spawnable props that are used to calculate for props being placed too close to edges of nav mesh causing them to intersect walls.
- Made some fixes related to JRandomPropPlacer's BackToWall option. Also fixed distanceBetweenSpawns. Both should work more consistently.
- Added DamageTriggerNetworking which is an addon for DamageTriggers to fix several desyncing and weird issues related to certain options on the triggers. KilledEvents have moved to be part of this new component.
- Lowered the default prefab width on JRandomPropPlacer.
- DamageTrigger's ability to attach a corpse to a point now lets you specify the bone that gets attached.

### Item Module
- JGrabbableObject now has unity events for OnEquip and OnPocketed.
- JGrabbableObject now also has a virtual method and event for placing an item on the company's sell deposit desk.
- JThrowableItem now has a virtual method and event for throwing an item.
- Renamed explodeOnTimer on the JThrowableItem to explosionStartsTimer to hopefully make it more clear what that setting does at a glance.
- Optimized some random code a little bit.
- JMeleeWeapon now changes the animation speed for reeling back a heavy weapon for the client reeling back the weapon. I can't change the animation speed for a specific animation so I decided the best option was to just change the animation speed on the client using the weapon that way when you look at your friends their other animations aren't at weird speeds.
- Fixed tooltips for JThrowableItem for throwables that require interaction.

### Editor Module
- Added custom inspectors for more components.
- Made new editor scripts and edited some existing editor scripts with more dynamic hiding.

## v1.7.4
- Fixed DayCycleAnimator to start at the start of the animation correctly. I also changed the name of the scale variable and gave it a tooltip to more accurately describe what it actually does.
- Added JItemDropshipModifier which can be used to reparent items spawned by the item dropship when placed on the object with the component in a scene. The methods are virtual so other modders could make their own scripts that extend off of this one to do even more to the items spawned by the dropship. This is another component nobody asked for but I made for one of my upcoming moons.
- Added JNetworkPrefabSet scriptable object along with JNetworkPrefabSpawner which can be used to register and spawn custom network prefabs. To set up add it to a JLLMod scriptable object and it will get registered in the game.
- Re-coded the Editor Module making an abstract class for each Editor script to inherit from to reduce duplicate code.
- Custom Editor Scripts no longer display the component's script at the top.
- Added MathHelper which can perform a list of basic operations with Unity Events. This can be combined with the config system to do more complex things.
- DamageTrigger now has the option to override player corpse meshes. You may have noticed the slime death corpse was missing from the list before. I looked into this and apparently, zeekers hard-coded that a few seconds after a slime kills a player to replace the corpse's mesh. I have now given you the ability to input an override mesh. You can use this to get the slime corpse or to create your own custom corpse meshes. Happy killing!
- DamageTrigger now has an additional option to make the corpse stick to a position for a limited or indefinite amount of time. Extra customization options involving this are included.
- TelePoint's rotate player now correctly sets the Euler Angles based on transform parents.
- Fixed ItemConsumer not syncing item removal on connected clients.
- The Editor Module now gives a warning when a JLL network behavior doesn't have a network object component on the same game object. A lot of the 'bugs' I've been getting have been people missing network objects on their scripts which obviously will cause desyncs and other issues.
- Added JRandomPropPlacer which can be used to spawn objects around a map at select nodes. You can use this to spawn props at custom-defined nodes or use the interior or exterior AI nodes. This component gets activated after dungeon generation is completed on all clients. You can also choose to rebake the navmesh after spawning props. You can also change the prop container, if left empty will use the normal map props container. If props have network objects they will be spawned on the network correctly. If you make custom network object props you will still have to register them in some way for them to work in the game.

## v1.7.3
- Added AttatchToObject script. To be honest I just added this one for one of the moons I am working on I don't know if anybody else is going to use this. You can use it to attach the object you place it on's transform to another transform. You can update position or rotation or both.
- Fixed bug where DamageTriggers always deleted corpses even when given a corpse type.

## v1.7.2
- Fixed ItemConsumer removing items from the wrong slot.
- EnemySpawner can now ignore sampling navmesh if you set navMeshRange to a negative number.
- The default navMeshRange has been increased to 10. There's also now a debug log to inform developers in the event of navmesh being out of range of the spawner.
- Fixed issue DamageTriggers that had continuous damage enabled damaging players even after they've exited the collider.
- DamageTriggers now check if a damaged target that has entered is dead. If it is already dead it won't add it to the colliders inside list. (Also fixed JPlayerInsideRegion)
- JFilterProperty now can have the filter and the check variables be separate types.
- Added ItemFilter JFilterProperty for checking the values of items. This is basically just a refactor of old code to make it reusable in other contexts outside of PlayerFilter's HeldItemFilter.
- Fix for a client disconnecting from the game mid DamageTrigger RPC throwing an error.
- ItemFilter can do all the filters HeldItemFilter previously could do along with filtering for scrap value, inside the factory, is scrap, and if the item is two-handed.
- Flipped order of NameFilter's filter properties in the inspector. (Looks neater imo)
- ItemConsumer and InventoryRemover now support using ItemFilter to check for more properties than just name matches.
- InventoryRemover's removeAllInstances mode should be fixed.
- Optimized ItemConsumer to only run the filtering on the server and not all clients. Destroying the item is then synced afterward.

### Editor Module
- Moved some duplicate code from a couple of editor scripts to a static method.

## v1.7.1
- Fixed missing PlayerEvents on ItemConsumer, JPlayerInsideRegion, and ObjectConverter.

## v1.7.0
- Weather Registry compatibility updated for 0.2.0
- A lot of components have default custom presets for lists that way when creating components for the first time you don't have to check a bunch of default options.

### JLL Core Module
- Fixed issue with random weighted index generation.
- Fixed bug with item spawners not updating scan nodes to show the value of spawned scrap items.
- TelePoint now has options for teleporting players to random positions. These can be anywhere in the level or specifically indoor or outdoor positions. There is also an option for a random position within a certain range of the player and one for the position to be around a random player in the lobby.
- TelePoint now has options to add a teleport sound and play teleport particles on players.
NameFilters now have a compareMethod field. The default compare method is Equal, which has the same functionality as before. You can also filter for whether a string contains, starts with, or ends with another string.
- Added JClientSync script which modders can use to run UnityEvents on the host or for all clients in the lobby.
- Changed RandomizedEvent's triggerOnEnable to trigger on start. Turns out Unity Netcode does some kinda screwy things I didn't know about with enabling/disabling netcode objects. Changing this to the start method removes the ability for this to get triggered by enabling/disabling the object but fixes some syncing issues. This doesn't affect any already existing moons just some in development ones.
- JFilters now have an interface for interacting with them on a more abstract level.
- Added JShipController which allows you to trigger some things to happen with the autopilot ship. You can trigger power surges, toggle monitors or lights, or even tell the ship to leave due to dangerous conditions.
- Added JTerminalController which allows you to manipulate the terminal. Right now you can award/take away money from the player and trigger the signal transmitter.
- Added JClientAttatchedObject which lets you target an object that you can set to enable/disable based on whether the player is inside the facility or not. It also has options for teleporting the object onto the local client.
- RandomizedEvent now has an option to disable sending the client RPC on certain weighted events. For example, if you want one of your events to just be a nothing happens event you shouldn't have to send a message to every client to do nothing. You can mark it to not send an RPC when that event gets rolled.
- Added JPlayerInsideRegion which can be used for a continuous chance-based event when a player is inside of a trigger collider. There's also an event for when a player enters the collider.

### JLL Items Module
- JThrowableItem:
  - Now has a check to toggle if it is throwable or not.
  - Now has an option for explodeOnTimer. When enabled an attempt to explode the projectile will instead start a timer and will explode on the timer's completion.
  - Now has a function for resetting the explosion timer. You can trigger this with events or custom scripts.
  - Now has support for triggering a custom animation on explode. Remember interactions set the "pullPin" trigger the same as vanilla. Explosions in this script set the "explode" trigger.
  - Publicized ExplodeProjectile function that way you can trigger it with events or other code. The boolean you input is whether to destroy the object or not.
  - wasThrown now gets reset on pickup.
- JGrabbableObject:
  - Added OnSetInsideShip virtual method for coders and OnSetInShip UnityEvent which both get triggered when the item gets brought inside the autopilot ship. Both the method and event are given a boolean representing if the item is entering or exiting the ship.
- JEventBoxItem
  - Updated RandomOpenEvent to support unchecking SendClientRPC from RandomizedEvent.

### JLL Editor Module
This is a new module that has been added to provide custom inspector windows in the unity editor. Right now I have set up a handful of scripts to hide certain properties based on other properties to hopefully make it easier to understand what is going on in some scripts.
In the future, I plan to add some extra help messages for some scripts to make it easier to set up some things.
- The editor module does not add any new components, in fact, it only will do anything unless placed inside the Unity Editor with the other modules of this mod.
- Added Help Boxes which can describe how to fix some common issues when they are detected. I'm not going to list all of them in the changelog but one example is having damage triggers set to damage vehicles but having the trigger on a layer that can't interact with the vehicle layer. The hope is that these messages can help people fix some issues before they happen and turn into a false bug report. *cough -Wesley- cough* 

## v1.6.5
### JLL Core Module
- Added JLLMod scriptable object that allows you to set up mod config properties the same way you could before with JSON. The JSON functionality will remain. You can use either one you'd like.
- Added JLevelProperties scriptable object that allows you to mess with the level property override system I added several updates ago. This functionality is experimental and hasn't gotten much testing so you may run into issues using it.
- Moved ObjectConverter to a new namespace for helper components.
- Added BooleanHelper which can split boolean events into separate events or invert the boolean value.
- Added TransformHelper which can allow you to Parent / Unparent GameObjects through Unity Events.
- Added EventLimiter which you can set a maximum number of times it is allowed to trigger an event. A player can optionally be passed too.
- Added DateFilter which can filter based on your computer's system time.
- JLevelEventTriggers now has a trigger for the breaker box that passes a boolean of whether it was turned on or off.
- TerrainObsticle now has events for OnDamaged and onDestroy.
- Renamed JsonHelper to JFileHelper. It also was relocated outside of the JSON namespace.
- JLL can now read scriptable objects out of asset bundles. JLL reads asset bundles using the .lethalbundle or .jll extensions.
- DelayScript now has a method to clear the event queue along with an option to clear the queue when the object is disabled.
- Renamed JsonConfigGrabber to JModConfigGrabber since json is no longer required. I kept the old version because Unity would be dumb and remove everybody's scripts on this change. JsonConfigGrabber now adds a JModConfigGrabber script to the object and removes the old script on Start().
- I also renamed a bunch of other scripts related to the JSON stuff added in the last update as it is no longer exclusively registered as JSON.
- ItemSpawner and EnemySpawner now have options to input position targets to spawn them somewhere other than the object's position similar to ExplosiveEmitter added in the last update.
- EnemySpawner now has the option to change the spawned enemy's rotation. The default is ObjectRotation which is the same behavior as before. But now you can make it have no rotation or a random rotation.
- PlayerFilter can now filter for if a player is the local player and filter for username.
- DamageTrigger's list of colliders inside now gets cleared on the object being disabled.
- DamageTrigger now does a calculation to change the hit direction based on the rotation of the object it is attached to. You can select a different option for hitRotation to change this behavior.
- Fixed an issue where DamageTrigger could modify the list of colliders inside during a traversal.
- EnemySpawner now samples the level's NavMesh to make sure it only spawns an enemy on a NavMesh. If a NavMesh is not found it won't spawn anything. The default search range is 1 but can be set on the component.
- TelePoint now has an option to rotate objects that now default to true.
- Did some code optimizations for EnemySpawner. Also added a check to skip empty EnemyTypes put inside the random pool.
- Added various new console logs to a bunch of scripts for help debugging.
- Fixed issue with DamageTriggers where MoreCompany would attempt to place cosmetics on the corpse that never spawned. Now I am destroying the corpse after it spawns instead of blocking it from spawning.
- ItemSpawner now has an option to apply rotations to spawned items.
- Added RandomClipPlayer which can be used to play a random unsynced audio clip. Handy for adding variation to sounds triggered by events.
- Made a bunch of weighted arrays on components that have defaults to hopefully make setting them up for the first time easier.
- DamageTrigger now has a killEvent that gets run when the thing hit by the trigger dies.
- Added option to disable network syncing on RandomizedEvent.
- Added NotEqualTo as an option on NumFilters.

## v1.6.0
### JLL Items Module
- Added a new Items Module DLL. This is a separate DLL that's part of the same mod. This script will contain custom / modified item scripts.

The rest of the change notes for this section will be the new item scripts.

- JMeleeWeapon can be used to create shovel and knife variants. Any form of melee weapon can be made with this.
- JEventBoxItem is similar to gift boxes and has both overridable methods for scripting custom events and a UnityEvent for linking with other JLL scripts. It also has the combined functionality of RandomizedEvent and ItemSpawner which should make them pretty versatile.
- JNoisemakerProp is similar to vanilla Noisemaker prop but has some extra options like making it able to be toggled off and waiting for a sound to finish before being able to use the prop again. It also inherits from JGrabbableObject so it has extra code inherited from that.
- JThrowableItem is a more customizable version of a throwable item with configurable events for when dropped, thrown, landed, and other things.
- JGrabbableObject is the base item that all my other item scripts extend from. It has some extra-base methods for coders and some configurable options for disabling things like lights and particles when pocketed.

### JLL Core Module
- JEventVariables Number Variables now have an event for sending a rounded integer number.
- EnemySpawner now has an event for when something is spawned that uses the newly spawned enemy.
- Code optimizations for DamageTrigger.
- Added tooltips to a LOT of components.
- Added ExplosiveEmitter. This can be used to generate explosions and screen-shake effects.
- Added ObjectConverter which is sort of like a filter that allows grabbing GameObjects from mono behaviors along with determining if GameObjects are Players or Enemies.
- Added InventoryRemover which can be used to remove items from a player's inventory.
- Added ItemConsumer which checks the player's held item for a match, destroys their item if it matches any of the items in the list, and runs an event on success.
- Added JsonHelper class for creating and reading JSON files easily with other mods.
- Added JsonMod which can allow people to create basic config options by placing a file ending with "JLLMod.json" (Example: "PinnacleJLLMod.json") anywhere inside the plugins folder with their moon, interior, or other mod. This implementation isn't super fancy but it is intended to allow basic configs for things like moons without having to create an entire .dll along with some scripts to disable something in a level.
Note: Currently LethalConfig isn't able to recognise config files created from JLL. You can edit these config files externally in a mod manager like R2modman, Thunderstore, Gale, or even open the config folder in File Explorer and open the .cfg files and edit them manually. They just won't appear in any in-game GUI's. I want to fix this in the future but don't currently have a way.
- Added JsonConfigGrabber which is a component you can use to check/get config values from your JsonMod.
- Fixed issue where DamageTrigger did not include code to prevent damaging already dead Players, Enemies, and Vehicles. DamageZoneTrigger had this functionality and it just got forgotten in the refactor.
- DamageTrigger now lets you toggle if custom sounds should play for each target individually.
- Changed how some random weighted events work to reduce duplicate code.
- Fixed issue with PlayerFilter's held item check always returning false when enabled.
- Added LethalConfig as a soft dependency.
- DelayScript has had its code for counting down completely rewritten. This won't affect any existing scripts that make use of this but is hopefully a slightly better implementation.
- I AM GOING TO KILL WESLEY
- Added ItemSpawner component which can be used to spawn items from either a custom pool, a random registered item, an item from the current moon's scrap pool, or the list of store items.
- JEventVariables now has an easy method to check if all Boolean Variables are true and runs an event if so.
- JEventVariabls Number Variables now has an option for adding a number filter check when running the normal trigger. If the check passes it will run like normal, if it fails it will run the failed event. There is also an option to run an event if all number variables pass their checks.
- The NumberFilter JFilterProperty now is made from an abstract class called NumFilter. 
- IntFilter has been added as another type of filter that takes inheritance from NumFilter.
- NumFilter now has filter options for GreaterThanOrEqual, LessThanOrEqual, and ModuloZero. The first two are pretty self-explanatory but the last one can be used to determine if the current value can evenly be divided by the value you're checking. For example, if you want to check if the number is an even number you can make the operand ModuloZero and input the number 2.
- Added ShipLeaving event to JLevelEventTriggers.
- Fixed hourly event hour slider being smaller than the number of hours in the game. (The TimeOfDay script sets the value to 7 by default so I figured that was what the cap should be but turns out Zeekers overrides that value in unity to 18)
- Added LevelFilter that lets you search through some vanilla-level properties along with LethalLevelLoader tags and properties. You can search the current level on awake or manually, or search a given level. One use case for this could be to allow interiors to change things based on the level they are loaded into.
- NameFilter JFilterProperty now has an option to enable case sensitivity. Before it was always not case sensitive but now you can enable it.
- PlayerFilter now has an option to filter for the local player client and an option to filter all clients currently in the game.
- PlayerFitler can now filter for if a player is inside the facility. HeldItemFilter can now check LLL conetent tags.
- All JFilters have had their performance improved by no longer checking all the filter properties after one that's already failed since the overall result would be a failure regardless.
- Added JActionEvents component can be used to get Unity Events out of being damaged by a weapon or hazard, being shocked by a stun gun, or listening to sounds similar to how Eyeless Dogs can hear things. 
- Fixed issue with DamageTriggers involving mods that allow a player to respawn after having died inside a continuous damage trigger causing the player to continue to take damage until they either died again or touched the trigger they died in.
- Rewrote ExtendedLevelLocker to be easier to use. It now only requires the scene name which should allow people to use it easier. If anyone was using this script before it may break in the new version.
- Fixed issue with EnemySpawner & ItemSpawner, TriggerEnterEvent, DelayScript, & RandomizedEvent's awake toggle not working under certain conditions.
- Most things that previously used awake have been replaced with OnEnable and have been renamed accordingly.
- JLevelProperties now uses the scene Name instead of the object name because that was a mistake. It should be usable now.
- JLevelProperties can now be included inside of a JLLMod.json file. 
- DelayScript now has an option to wait on enable. That way you don't have to have another script with an enabled event to trigger it.
- DamageTrigger now has a DamageRaycast option. You can attach a transform that it will use to calculate the direction of the ray and specify a length for the Raycast. The Raycast can be triggered either by enabling continuous Raycast damage or manually through UnityEvents. The Raycast will attempt to damage the first thing the ray hits.
- Added a JLogHelper class along with config options for logging level. You can now adjust how many logs you want to receive in the mod's brand-new config. Config gets registered with LethalConfig as well.
- JLevelPropertyRegistry now caches the terminal so my mods don't have to do a lengthy FindObjectOfType call anymore.
- Fixed bug where damage triggers would ignore if a target is alive or not.

## v1.5.2
- Fixed x2

## v1.5.0
- Replaced DamageZoneTrigger with DamageTrigger. The new version has been reoptimized following some suggestions from IAmBatby (thanks!)
I haven't removed DamageZoneTrigger just yet that way people have a chance to update without breaking their mods.
- DamageTrigger's methods for damaging individual types can now be called by UnityEvents again.
- DamageTrigger now has damage multipliers which can modify the damage applied to things.
- DamageTrigger's damage for each type can now be set through UnityEvent scripting.
- You can change the damage amount for each type through UnityEvent scripting.
- RandomizedEvent now optionally be given to a player to trigger a PlayerEvent. PlayerEvent only gets triggered if a player is given when the script is initially triggered. Regardless if a player is given or not the old event still triggers.
- Optimized TriggerEnterEvent by reusing some of the new code for DamageTriggers.
- DelayScript can now also optionally be given a player similar to RandomizedEvent.
- JLevelEventTriggers now have an option to only trigger ApparatusPulled on the first instance of an apparatus being pulled. (For interiors with multiple apparatus's)
- Added ClientSeperator which can be used to trigger events only for specific targets. Be careful when using this to avoid client desync. This can allow you to do interesting things in combination with other scripts such as displaying a hud tip when a client enters a room without displaying that tip to every client in the game.
- RandomizedEvent can now be used to trigger an event on a random player in the lobby. RandomPlayerEvent is the event triggered with a random player in the lobby after StartRandomPlayerEvent() is triggered.
- JLevelProperties now get properly removed when leaving a game mid-round.
- Packaged MagicWesley into the .dll. (Don't ask)
- Added PlayerFilter which can be used to check the properties of a player and if all checks succeed will trigger an event. Currently, PlayerFilter is capable of checking if items are in a player's inventory, some properties of the currently held item, the player's health, the player's stamina, and the player's carry weight. I know this is very similar to Lethal Toolbox but some people wanted it anyway so I added it. I may add more filters in the future if they're requested.
- Added JEventVariables which can be used to store variables for use in events. Say you want to make a damage trigger do 2 more damage each time it hurts somebody, or you want to save the first player to enter a trigger for use with a different event later. You can do that now! This script supports storing Numbers (floats), Booleans (true/false), GameObjects, Players, and Enemies. To modify the values of these variables you need to first set the target index with an event by calling TargetIndex(index). Some variable types have special operations you can perform such as AddNumber(number). When you want to trigger the Event related to a variable you must first set your target index to the correct index and then call Trigger(varType). The string identifier for varTypes is listed in parenthesis above each variable in the inspector.
- JLevelEventTriggers now updates on FixedUpdate instead of Update.
- Added EnemyFilter which can be used to check the properties of an enemy. It can currently check an enemy's type, if the enemy is invincible, and its health.
- EnemySpawner should no longer throw an error when given an empty copy of a modded enemy when that modded enemy is not present in the game.
- JWeatherObject now has the option to disable setting the active object to itself when no active object is provided.

## v1.4.0
- Added TelePoint which when triggered by an event can be used to teleport things to that object.
- Added TriggerEnterEvent which can be used to trigger events when something enters a trigger collider on the same object.
- Added a timeScale variable to DayCycleAnimator to change the length of time an in-game daylight cycle will animate through the animation.
- Added custom UnityEvents: EnemyEvent (EnemyAI) VehicleEvent (VehicleController) and DamageableEvent (IHittable) among others.
- Added RandomizedEvent which can be used to trigger events based on a randomized weight system that gets synced between clients.
- Publicized the damage methods in DamageZoneTrigger to allow invoking them through Unity Events.
- Added JMessageLogger which can be triggered by events to send log messages, chat messages, and HUD tips/warnings.
- JLevelEventTriggers now can run events on hour changes.
- JLevelPropertyRegistry now caches all entrance teleports for easy access to them.
- JLevelProperties can now contain LevelPrefabs. When a level gets loaded these prefabs will be spawned at the specified location. This can be used to add new structures of objects to other vanilla or modded levels.
- Added SeatController which can be used to make functional seats or benches. This acts similar to sitting in the passenger seat of a cruiser. I recommend looking at the cruiser prefab to figure out how to set it up with interact triggers.
- JLevelPropertyRegistry when registering new level properties if properties already exist for that level will merge the contents of both.

## v1.3.0
- DamageZoneTriggers no longer damage already dead players and creatures.
- DamageZoneTriggers now have a public method to damage everything inside, which can be used for events.
- Added EnemySpawner which can be used to spawn enemies without code. You can spawn from a random weighted pool or spawn a specific enemy. When spawning random if you don't include any enemies in your weighted pool then it will have an equal chance of spawning any enemy.
- Added JWaterFilter which can be used to replace the HDRP Volume. (Basically, Change the color or completely override the water shader when attached  to a water trigger)
- Added JLevelEventTrigger. This object will probably get some updates in the future with more stuff but the idea is you can use it to add events that run scripts based on certain level events occurring. 
Right now there's: OnLevelLoaded (When the level finishes loading) OnShipLanded (When the ship landing animation finishes) and ApparatusPulled (When the apparatus gets pulled inside the facility.)
- New additions to JWeatherObject: You can now specify the active object to enable/disable along with an inverse object that will be enabled/disabled the opposite of the normal object. When activeObject is left empty it reverts to the old behavior of enabling/disabling it's self. There are also now onActivate and onDeactivate events that you can use to script code execution.
- Added JLevelPropertyRegistry which can be used to specify override properties for levels. Unlike a lot of the other bits in this mod, this registry is only usable through code and not an editor script as of now.
- Added JMaterialReplacer. This was something Nikki originally requested. It can be used to replace the materials on objects. It also has the functionality to search through all of its children automatically. Only use this if you know what you are doing basically. This feature is experimental and may even be removed in the future.
- JCompatabilityHelper now has a check for SimpleCommands (One of my other mods) and LLL.
- Added LLL as a soft dependency and added a Helper Class for interacting with LLL.
- Cleaned up DamageZoneTrigger code a bit.
- Added damageOnCollision as an option for DamageZoneTriggers.
- DamageZoneTriggers can now optionally damage Vehicles.
- Added ExtendedLevelLocker which can either be used with triggers or in combination with UnityEvents to lock/unlock LLL extended levels.
- Added event for DamageZoneTriggers for OnPlayerDamaged.
- Replaced JFloodPath entirely with the new DayCycleAnimator. DayCycleAnimator lets you animate things synced with the time of day. To set this up add DayCycleAnimator to the same object that has your Animator script. Go into the animator pane and add a float called "time". Finally, in your animation clip check "parameter" for "Motion Time" and select the "time" variable you just created. After that, it should just work. 0 is the start of the day and 1 is the end of the day.
- Added DelayScript which can be used as a timer before triggering events. Invoke "StartWaiting()" and after the delay you input it will invoke the events you put in it.

## v1.2.0
Note: I talked with Mrov and & decided to delay JWeatherOverride compatibility with WeatherTweaksBeta because I *might* help implement a similar object override API to WeatherRegistry in the future. For now, I have worked on adding other compatibility features with WeatherRegistry.
- Added MIT license.
- Added DamageZoneTrigger which can be used by level creators to make triggers that can do customizable 1 time damage or continuous damage. You can also define if it can damage players, enemies, or objects individually.
- Added TerrainObstacle which makes an object destructable by the cruiser with custom sounds and particle fx.
- JWeatherObject now allows you to toggle between a whitelist (the only option before) and blacklist mode.
- JWeatherObject now has options for dropdown weather selection instead of being forced to use string IDs.
- JWeatherObject now supports custom weathers registered through WeatherRegistry. Check custom weather mods to see what name they were registered with.
- JWeatherOverride can now override weathers registered through WeatherRegistry. (I've not tested this very extensively so there may still be issues)
- Some minor tweaks to other code for performance & memory reduction.
- Made project Netcode Compatible.
- Added LethalLib as a dependency to make networking easier for me.
- Added a small helper class for WeatherRegistry compatibility.
- Changed how ChargeLimiter works.
- Removed the Behaviors API for editing weather objects as it is no longer used. I marked it deprecated in the last patch so removing it now just felt right since nothing else uses it.

## v1.1.5
- Fixed issues with JWeatherOverride improperly handling weather-effect objects.
- Fixed the issue causing JWeatherOverride to not work if the override was missing a permanent object.
- Added JWeatherObject which can be used to make objects on custom moons only appear under certain weather conditions at the start of the round.
- Added JFloodPath which can be used to make objects in custom moons lerp between different transform states throughout the day. This can be used to create custom flood planes in flooded weather or for basically whatever you want.
- Added Support for applying fog weather variables to HDRP Volume Fog.
- Fixed issue where multiple fog volumes in foggy weather would have different fog thicknesses.
- Support for other mods hasn't been properly tested as of yet so there may be issues.
- Deprecated the old weather override system from Pinnacle's release build. I may completely remove it in a later update, but for now, I'm keeping it for legacy reasons.

## v1.1.0
- Added weather override script that when placed on a custom moon allows you to replace weather objects.

## v1.0.2
- Fixed Missing Icon

## v1.0.0 Release 
- Limited Use Recharge Stations
- Level Weather Effect Modifications
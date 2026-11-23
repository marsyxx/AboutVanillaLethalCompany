# Changelog

## 0.8.0
### changes
## This update is mainly moving the drone away from using the terminal to control it. The "utilitydrone" command can still be used from the terminal just in case, but from now on the tablet will be the best way of using the drone. 
- The terminal command is now `utilitydrone`, so it stops fighting another unnamed drone mod for the same terminal command (why would you use both anyways lol).
- Removed `drone list`. It's a old command leftover from when the drone was an item.
- Removed `drone recall` too. The recall key and the station screen have it covered.
- The drone's boombox now plays CustomBoomboxMusic songs. Bring your own bangers.
- The drone only sees teammates through walls if they're on the same side of the facility doors. 
- The drone management screen got a makeover: bigger, brighter text, a glowing PILOT DRONE button, module icons everywhere, roomier presets with a scroll bar, a little TV for the drone preview, and a cleaner Workshop where you pick modules from a drop-down beside each mount. It's easier on your FPS too. Your eyes can thank me later.
- Pilot the drone from a tablet now. Grab it from the station by clicking "Pilot Drone" and left click to fly. You can drop the tablet to return it to the drone station through the power of teleportation. Or something. It rests on its own charging dock on the station while nobody's using it.
- New battery: a single AA battery. 10 battery, +100% flight speed. Godspeed.
- The drone can see the facility interior when it goes in first. No more flying into the void.
- New config: Pilot Tablet/UseOutsideShip lets you fly from anywhere. Off by default. 
- New config: Interface/ManagementWindowSize sets how much of the screen the station window takes. 75% by default.
- New config: Battery/RechargeSpeed lets the host speed up or slow down station charging, from 0.1x to 10x. Impatient crews, rejoice.
- Press T while flying to switch between hearing the drone and hearing yourself back on the ship. Now you have the option to hear eyeless dogs before they take a bite out of you.
- The drone sets down on its station 50% faster. It was taking its sweet time.
- Performance improvements to prevent needless fps drops. 

## 0.7.9
### Changes
- The magnet module can now pick up dead employees' bodies too, holding them by the chest like the winch does. A body slows the drone by only 25%.
- The grapple winch can now hook employees who are facing the drone. Whatever they were holding used to block the line.
- The grapple winch reels loads in 50% faster.
- New host setting Modules / MaxModules sets how many modules a drone can have installed, from 0 to 5 (default 4). Set it to 5 to fill every mount. Literally cannot go higher than 5, there's only 5 slots bro...
- The drone can now be flown from the charging station. It has a custom tablet model that the player holds while piloting. I'll likely leave the drone command for the terminal but slowly phase it out.
- The drone's camera now shows its pilot's whole body instead of a floating helmet and arms.
- Updated the drone management UI to the new appearance. 
- Updated the workshop to a new UI type.  This is still a bit WIP so I'll update it over the coming days till it's exactly how I want it!

## 0.7.8
### Changes
- The drone's health system is now on by default (Health / Enabled). If your config already has it set to false, turn it on there.

## 0.7.7
### Changes
- Eyeless dogs now throw the drone much farther, about 20 m. YEET.
- An eyeless dog's bite covers the drone camera in saliva now, because funny. It fades over 10 seconds.
- The zap gun's range is now 15 m, up from 10 m. I couldn't hit crap with it.
- The boombox and beacon now switch off when the drone is wrecked. A boombox playing as the drone returns is both funny and problematic...
- The station's welding arms now move up and down the drone as well as side to side while repairing it. Also added some sexy new spark particles to replace the crappy old ones. So epic. So wow.
- Eyeless dogs grab the drone more easily. They seriously couldn't grab it before for the life of them. 

## 0.7.6
### Changes
## Hopefully this addresses all the issues people mentioned in the feedback page. Thank you for anyone who submitted feedback, its been a huge help!
- Hosts can now set the drone's max cargo weight as high as 200 lb, up from 50 (Cargo / MaxCargoWeight). The default is still 50. Cargo still slows the drone by 1% per pound, but never past half speed.
- Fixed parts of the facility not showing on the drone camera when it was flown in from outside. Players using CullFactory (like me, probably why I didn't notice it) are unaffected.
- The magnet's carry speed setting (Magnet / CarrySpeedPercent) is now a slowdown from 0 to 100: 0 keeps full speed and 100 stops the drone. The default of 50 is unchanged. If you changed this setting, check it again.
- Eyeless dogs can now chow down on the drone when pissed off. Unintentionally or not. But it's not exactly tasty, so they won't bother eating the drone- that's not to say it won't cause damage...
- The pilot's voice now carries 25 m from the drone, up from 16 m.
- Fixed the rare issue when you purchased the drone station, flew to a moon, quit without saving, then loaded back into the save. This created multiple drone stations which you could repeat to keep spawning more. All of which were non-functional. 
- Fixed the docked drone shaking on its station while the ship lands or takes off. It now tilts with the ship.

## 0.7.5
### Changes
- I must apologize for anyone running into the "has not synchronized with host" bug. I always bought the drone station in orbit, but purchasing it on the moon itself caused it to spawn a broken station and no drone. It should now be resolved!
- The magnet now carries the cruiser at the same speed as a player. Hosts can change that speed in the config (Magnet / CarrySpeedPercent).
- Loot bugs now see a low-flying drone as loot. If the drone flies within about 2 m of the floor, a loot bug may grab it and carry it back to its nest. Stunning, zapping, hitting or killing the bug makes it drop the drone, and it always lets go after 20 seconds. Hosts can turn this off in the config.
- Fixed the trap hacker's blast radius warning being cut off
- Clicking LMB again while the trap hacker is working cancels the hack
- The zap gun now locks on to an enemy within a small cone in front of the drone, so it no longer has to be aimed exactly at the enemy.
- Flying into a hygrodere now damages the drone, as standing in one hurts a player.
- Butlers now attack a drone they find alone, with no player nearby, just as they do a lone player. Can't have whirring death machines cluttering up the place after all!
- Giant sapsuckers now attack a drone carrying one of their eggs in its cargo or reeling one in with the grapple. If a sapsucker destroys the drone, the drone drops the egg and nothing else.
- Tulip snakes now leap onto a drone if they can manage. One tulip snake is annoying, two means you'll have to watch the drone be carried away...
- Hosts can turn each of these creature interactions off in the config. Hygroderes, butlers and sapsuckers only hurt the drone with health turned on.
- A drone destroyed inside the facility now disappears right after its explosion instead of floating there while it waits to return. It still takes just as long to arrive back at the ship.
- When no drone station has been bought yet, the "drone" terminal command now explains how to buy one.

## 0.7.3
### Changes
- Turrets now treat the drone like a player: they spot it, lock on and fire, and each bullet hit deals 50 damage. Landmines click when the drone flies into them and blow when it flies off, and their explosion wrecks the drone within 5.7 meters. Other explosions hurt it too, as they would a player.
- The trap hacker now reaches 10 m, up from 8 m. A hacked trap's explosion can hurt the drone, so with health on a warning flashes when the drone is within its 6 m blast radius.
- Fixed a revived player staying invisible and silent for some clients until the next round. Players who joined after the revived player never saw the revive happen. Healing, magnet, grapple and needle targets used the same player lookup and are fixed too.

## 0.7.2
### Changes
- More testing for the new drone health system. Still disabled by default as it needs more testing, but it is optionally available to toggle on. 
- Added damaged states to the drone at 75%, 50%, and 25% HP. Each one makes the drone look more and more damaged, with 25% and lower actively nerfing the drone's flight speed and making the flashlight harder to use.
- The hit that takes the drone to 0 HP sets off a small explosion dealing 100 damage at point blank, but not hurting beyond 5 meters. This can kill players or enemies, so be careful. 
- Flying into a wall hard enough to thud now costs the drone 1 HP. 
- Hitting a drone with a shovel deals 20 damage to it. Shotguns can deal up to 100 at close range. 
- Nutcrackers now hunt a moving drone like a moving player. They can absolutely wreck the drone if you're too close, so be careful! This also allows you to draw aggro away from your teammates, giving them a chance to kill the nutcracker. Or you'll just get blown out of the air lmao.
- Updated the self-destruct alarm sound. The countdown now lasts 8.5 seconds so the blast lands as the alarm ends.

## 0.7.1
### Changes
- Testing a drone health system. Currently it starts off and should ideally STAY off. There's no reason to turn it on now unless you're curious. Currently, only a player shooting a shotgun can damage the drone. This is mostly for debugging purposes. 
- Two welding arms on the charging station now repair a damaged drone at 2 HP per second. This is configurable (Health / RepairRate, from 1 to 25). Its modules come off while it is repaired and go back on once it is whole. 
- You can now choose between the old style (simplistic) or the new style (stylized) for the drone's HUD. My friend didn't like the stylized so this is for him.
- Rewrote defibrillator logic to hopefully prevent horrible desync from occuring. Adds more checks to prevent situations of invisible players. 
- New Unstuck key (M by default, in Controls): moves the drone to a nearby open area (hopefully NOT outside the map!). 10 second cooldown. This is a quicker option than using the recall function.
- Fixed items dropped from the storage hatch (or by the drone) during a mission being invisible until someone picked them up. (Only with cullcompany enabled)

## 0.7.0
### Changes
- The Workshop starts locked, leaving only the presets, until the crew unlocks it for 500 credits (this can be disabled if you want the full drone unlocked by default. Just set the cost to 0 in the config). Getting fired locks it again.
- Added a Standard Drone preset. Just the basic drone with factory paint and no modules. It's the default, the classic, whatever you want to call it.
- The host can set each module's battery cost in the config, plus a flight cost from 0 (free) to 5 times the normal drain. 
- Updated the drone's flight sound, lengthening it and changing the pitch so it should avoid any audio clipping when it loops.
- The drone's hum quiets down while it hovers and grows louder and higher as it flies, a little more on a boost. I like to think the drone feels much more "drone-like" when flying now, combined with the momentum and camera changes.
- Reworked the drone's HUD to use the new "terminal" theme. Makes it feel a little more fitting to a drone piloted from the terminal.
- Added a HudScale setting under Display, so each player can make the drone's HUD anywhere from half to double size.
- Modules and presets can now be changed while the drone charges. Only swapping the battery waits for a full charge.
- Rewrote the info text for the trap hacker, lockpicker, magnet, pinger and grapple winch. Battery costs shown match the host's config.
- The recall key can't be held while lockpicking or hacking.
- Reduced the size of the door prompt ("Use door") under the crosshair (oops it was beeg).
- The active module window's background is more solid, so its text is easier to read. But the window now fades away after 5 seconds of no module use or swap in order to reduce clutter.
- Removed pointless crosshairs from some modules that didn't need them.
- Flying into a wall fast enough causes the drone to smack into it and play a clanking sound. 
- Added camera effects for weather. Try taking it into the rain or fog!

## 0.6.5
### Changes
- The drone folds its legs up in flight and lowers them again before landing on its station.
- The cargo window shows "Press E to close the cargo." now.
- The drone winds up its rotors on the station for 2 seconds before taking off, and they wind down after docking.
- Added a new drone boot up sequence when first piloting it after a charge. Takes a few seconds for the drone to spin up its rotors and get moving. 
- The drone tilts as it flies now.
- Takes longer to build up momentum
- The drone's flashlight starts off.
- Removed redundant popups when using modules unsuccessfully. 
- The ScreenEffect config for the drone camera's static and scanlines now goes from 0 to 2, and defaults to 1.5.
- Firing the shotgun with no shells flashes "NO AMMO LOADED!" under its reticle now.

### Fixes
- The grapple winch opens the ship's storage closet again.
- The scan pulse on the drone screen is centered on the crosshair.

## 0.6.3
### Changes
- The host can turn off any module in the config. It disappears from the Workshop, its presets and the drone.
- Added a feedback form, linked from the mod page.
- The pinger can place up to three pings: one red, one blue and one green. Scroll while typing the label to pick the color.
- While reviving, the defibrillator shows a blue bar that fills instead of a countdown.
- The grapple winch can now grab dead bodies for retrieval (especially handy for teammates who fall to their deaths)
- The host can limit the defibrillator to 1 to 10 revives per moon, shared by all drones, and set its cost from 25 to 100 battery points. This allows for the host to balance reviving to their liking.
- The drone holds still while reviving, so it doesn't immediately cancel if you move in the slightest.

### Fixes
- Fixed issues with the stim injector not hitting teammates in some situations.
- The grapple winch no longer flips light switches or breaker box switches because it thought they were cabinets (fix)

## 0.6.2
### Mod support
- The trap hacker works with the Devious Traps mod. They're much bigger, so a hack on one takes 15 seconds.

### Changes
- The trap hacker reaches 8 m, up from 5 m.
- The trap hacker sends a sonar ping every 5 seconds, marking traps within 20 m on the drone screen.
- The trap hacker's crosshair text is smaller and fainter, and its instructions are clearer.
- The magnet's crosshair text is smaller and fainter, matching the trap hacker.
- Updated the icons for the defibrillator, boombox, stim injector, lockpicker, shotgun and self destruct.
- Boost gives 1.5x speed for 1.5x battery cost, down from 2x, on every battery. The drone felt a little too fast at times. 

### Performance
- The defibrillator no longer searches for bodies; it checks each dead player's body directly.
- The thermal scanner and magnet only look for targets within 1.5x their range, once a second.
- The weed killer finds plants once a second without searching the whole level.
- The zap gun only looks for enemies within 15 m of the drone, once a second.
- The grapple winch only looks for items within its reach instead of across the whole level.
- The drone no longer stutters while the trap hacker is selected.

## 0.6.1
### Fixes
- Players who load in slowly no longer get stuck in the fog when joining. The host now waits until they're on the ship before sending them drone updates (fix).

## 0.6.0 
- I cleaned up and polished a LOT of modules and features this update. I wanted the drone to feel much more interactive, and to generally improve the feel of piloting!

### New module
- **Grapple winch** (front mount): click on an item or an employee up to 15 meters away to hook them and reel them in to the drone. Click again to let go early. Scrap over 20 lb is hooked but not pulled. Also yanks open cabinet and locker doors, and knocks snare fleas off ceilings and heads.

### Radar monitor
- You can now press C to switch the radar to the next crewmate (rebindable).

### Charging station
- The drone sits higher on the charger, with its feet on the yellow guides.
- Greatly improved and smoothed out drone docking. It should now fly through the center of the ship and turn quicker when piloting to the charger.
- Tweaked the prompt positions for the charging station so it's easier to interact with the different parts (like the customization screen or the storage hatch)
- Items from the storage hatch land further out in front of the charger to prevent clipping.
- Belt bags keep their items when taken out of the storage hatch instead of falling all over the floor.
- The storage hatch light is bigger and brighter when items are inside (was too hard to notice before)

### Drone
- The drone bobs and sways slightly while hovering in place (muh immersion).
- The red light beside the camera blinks while the drone is being piloted.
- The motor lights are all red and blink while the drone is deployed.
- The drone can open doors in custom interiors that build their own doors (why do people make custom door mechanics like seriously, it's just the same as a vanilla door!), and the grapple winch can yank their cabinets and lockers.
- Self destruct now shows a company notice to everyone when the drone is destroyed and when its replacement arrives.

### Drone view
- The drone's camera now looks like a monitor feed. The static gets heavier when the battery is low.
- Spray paint: removed the crosshair text.
- Stun launcher: only the grenade count and reload bar remain under the crosshair. The count turns red when empty.
- Shotgun: smaller spread circle, plus a shell count and reload bar under the crosshair.
- Healing beam: removed the crosshair text. The player's health now shows next to the heal icon.
- Healed players see a green screen glow that fades out after the beam stops.
- Updated the info window text for the pinger, shotgun, lockpicker and healing beam.
- The drone camera no longer sees through walls when the drone is pressed against them (fix).

### Sounds
- Quieted the sound effect while stimmed. 
- Weed killer sound now plays from the drone instead of the player (fix).
- New ping sound for the operator.

### Balance
- The drone uses half the battery while hovering still without using a module.
- Recalling the drone from inside the facility now takes 10 seconds before it appears above the ship. Recalls from outside are unchanged.
- A belt bag only counts its own weight toward the drone's load, like in the base game.
- The Scout preset now has a magnet instead of a battery charger so it doesn't share a module with the guidance drone.
- The EMS preset now includes the grapple winch.
- You can now overdose on syringes. Do with that what you will...

### Performance
- The drone code now skips every other item in the level, so maps with lots of scrap cost less.
- Drones and the drone camera are placed once per frame instead of twice.
- Less memory churn every frame, for fewer small stutters.
- Door prompts and the grapple winch look for targets a few times a second instead of every frame.
- The drone camera renders at the same resolution as your normal view, so resolution mods raise or lower it too.
- The host sends drone updates 10 times a second while nobody is flying one, and 20 while someone is.

### Config
- New host setting for max cargo weight, 5 to 50 lb (default 50).
- New setting for the strength of the camera's monitor effect, from 0 (off) to 1. In case people find the current limit too high and want to lower it.

## 0.5.6

### Fixes
- The radar monitor overlay no longer freezes while piloting. It now keeps updating at the same rate as the ship's monitor.

## 0.5.5

### Radar monitor
- Press X while piloting to show the ship's radar monitor in the bottom right corner of the drone view, and press it again to hide it. It shows whoever the monitor is following and updates just like the monitor itself. The key can be changed in the config.

### Drone model
- Updated the model for the charging station to add detail, as well as a new feature to the station.

### Charging station
- Docking drones now unload their cargo into the station's storage hatch (shotgun shells stay aboard for the shotgun). A yellow light by the hatch blinks while items are inside. Hold E on the hatch to open it and drop everything onto the floor in front of the station, where it counts toward the ship's loot like any item you drop. The hatch also empties itself when the ship takes off, so nothing is left inside.

### Drone view
- The controls along the bottom of the drone view are now a HUD panel like the rest of the drone's display, with each key in its own chip beside what it does. The keys shown follow your keybinds.

### Fixes
- Modules and batteries can only be changed in the Workshop or with a preset once the battery is fully charged, so swapping batteries mid-charge no longer skips a slow battery's charging. Paint can still be changed at any time.

### Balance
- The self destruct now deals 1000 damage to every player and enemy in its blast, so enemies that used to survive it are taken out too. Enemies that can't die are still stunned.
- The drone station now costs 500 credits by default, up from 300. Hosts can still change the price in the config.

## 0.5.0

### Flight
- The drone now has weight and momentum: it takes a moment to speed up and drifts briefly to a stop instead of starting and stopping instantly. Cargo, a carried player and especially the cruiser make it slower to respond.
- The drone camera leans into the flight: it banks toward the side you slide to and dips slightly flying forward. Only the view tilts, so your aim stays where you point.

### Drone model
- Updated the model for the healing, batter charger, magnet, and stim injector modules. They are all much more detailed and properly match the drone's color scheme.

### Mod icon
-Added a proper mod icon that isn't just a game screenshot.

## 0.4.6

## This may be the last update for a little while. Going to be playing a lot of WOW Forvever lol

### New modules
- **Pinger** (left mount): click to place a ping, then type its label. Every player sees the ping and its label through walls until the mission ends, and a new ping replaces the last one.

### Presets
- New **Guide drone** preset: boombox, battery charger, pinger and beacon with a lightning pack.

### Drone model
- The defibrillator is now a pair of real paddles: yellow shells with steel electrodes, ribbed grips, and coiled cords to a capacitor unit with a medical cross. The capacitor's lights fill as a revive charges, the electrodes glow brighter, and everything flashes when the shock lands.

## 0.4.5

### New modules
- **Battery charger** (bottom mount): look at the underside of the drone and hold E to fully recharge the equipment in your hand. Costs the drone 20 battery points.
- **Thermal scanner** (left mount): while piloting, enemies within 25 meters are highlighted red through walls.
- **Stun launcher** (bottom mount): click to fire a stun grenade with the pin already pulled. Holds 3 grenades, refilled one at a time while docked.
- **Beacon** (top mount): a lamp that lights everything within 35 meters while the drone is out. While it is fitted, every other player sees the drone's icon and distance through walls. Its brightness can be scaled with a multiplier in the config.

### Controls
- Hold G while piloting to recall the drone to its charging station. The key can be changed in the config.

### Management window
- Each Workshop window is now a dropdown. Opening one turns the preview to that part of the drone and zooms in, replacing the colored lines.
- Rotating the preview by hand zooms back out to the whole drone.
- The Workshop windows are wider and taller, so descriptions wrap instead of being cut off, and the fitted module's name is centered in its box.

### Drone model
- The zap guns now match the drone's body color and change with it in the Paint shop.

## 0.4.1 
- The drone's model has been upgraded to a more detailed model. 

## 0.4.0

### New modules
- **Boombox** (right mount): click to play music. It keeps playing while you use other modules, calms slimes like a real boombox, and works with Lategame Upgrades' Sick Beats.
- **Stim injector** (right mount): shoot a teammate to give them 20 seconds of extra speed, damage resistance, melee damage and stamina. Holds 4 needles, which reload one at a time on the charging dock.
- **Self destruct** (top mount): a large bomb that wipes out everything within 50 meters, with an alarm, a blinding flash and a mushroom cloud. It can be used once per mission, and the drone is disabled for 3 minutes after use.

### Batteries
- Pick a battery in the Workshop: Standard, Small, Heavy, Lightning pack, Power pack or Baby battery. Each one trades battery for flight speed or charging speed, and each has its own look.
- Battery is now shown in points, such as 100/100, and every module cost is listed in points.

### Management window
- New Presets page with ready-made drones: EMS, Attack drone, Scout drone and Nuclear drone.
- The Workshop's lines and mount titles are color-coded, so it's easy to see which window belongs to which part of the drone.
- Buttons now make sounds.
- The charging station's screen shows the new management window.

### Other changes
- The drone's camera now holds coilheads still, just like a player looking at them.
- The drone's camera and flashlight are easier to spot, and the flashlight glows while it's on.

### Fixes
- Fixed the healing beam's light glowing white instead of green.
- Fixed the mouse wheel switching modules in the same direction both ways.

## 0.3.0

- Added the healing beam module: hold LMB on an injured player to heal them up to their max health.
- Added the magnet module: pick up and carry an employee or the company cruiser.
- Added the trap hacker module: click a landmine, turret or spike trap to make it explode.
- Added the weed killer module: unlimited weed killer spray.
- Redesigned dock management with separate Workshop and Paint shop pages, a drone preview you can rotate, and a window for each mount.
- Lockpicking now shows a progress meter.
- The charging station now has collision.
- Recalled drones near the ship's door now fly in instead of teleporting.

## 0.2.7

- Fixed repeating errors after leaving a game, and the drone feed staying on screen if the host quit while a client was piloting.
- Fixed shotgun damage when the pilot is not the host. Enemies no longer react to the pilot when shot.
- Fixed scrap carried by the drone not counting toward the quota and end-of-day totals.
- Fixed the color swatches overflowing the management window.
- Each shotgun shot now costs 2% battery, and spray paint uses 0.25% battery per second.

## 0.2.6

- Fixed compatibility with BetterSprayPaint and LCUltrawide.
- Added a custom single-barrel shotgun with a centered spread crosshair, cargo-shell ammunition, and a 2-second reload countdown.

## 0.2.5

- Added a Recall Drone button to dock management.

- Drones inside or near the ship return to the dock without teleporting away.
- Added a bold red dock-required warning in management.
- Added a chime when the drone finishes charging.

## 0.2.3

- Updated the drone model, empty module connectors, and housing color coverage.
- Added dock-only module installation and color customization. Drones start with no modules; choose up to four.
- Added separate body and chassis color controls, with shared save data, Swap and Factory Reset.
- Included the front camera's upper and lower brackets in chassis paint.
- Updated the dock model and added a screen showing the management interface.
- Corrected the dock screen image orientation and mirrored text.
- Removed the unwanted hold progress bar from drone cargo; tap E to open it.

## 0.2.2

- Changed base flight speed to 8 m/s and dock charging to 1% per second.
- Cargo opens with a tap of E.
- Hold E on the dock for 0.5 seconds to open a visual management UI preview.

- Added a green paint sprayer to the side opposite the lockpicker.

- Fixed drone voice repeatedly restarting before its audio buffer could play.
- Improved voice positioning and diagnostics.
- Changed live voice output to reduce gaps caused by audio read-ahead.

## 0.2.1

- Added a chime when the drone finishes docking.

- Updated the charging station model.
- Dimmed the docked full-battery green light by 75%.
- Halved the recall descent and final landing speeds.

- Recall now starts 45 feet above the ship.
- The drone turns to face the charging pad before lowering gently onto it.

## 0.2.0

- Updated the dock model; its front charge bar shows the drone battery and updates once per second.
- Fixed cabinet visibility and furniture movement.
- Docked drone light stays red while charging and green when full; blinks green away from the station.

- Added the cabinet-style charging station and matching charging pads on the drone.

- Fixed the outside clock disappearing after leaving drone view.

- Replaced the equipment item with a purchasable drone station upgrade. Cargo access stays on R; players can no longer pick up the drone.
- At 0% battery, the operator disconnects and the drone returns to its station with its cargo. Added DRONE RECALL for manual returns.
- Recall starts above the ship, then descends before entering.
- Docked drones recharge at 1.5% per second (about 67 seconds from empty).

- Added unlimited spray paint with no shaking required.
- Removed spray mist and added a paint aiming marker.
- Added a radio voice feed so nearby players can hear the operator through the drone while ship speech stays audible.
- Cargo now holds up to four items and 50 lb, including belt-bag contents. Each pound slows flight by 1%, up to 50%.
- Updated the model with a battery pack, power button and side lockpicker.
- Hollowed out the cargo compartment and recolored the zap prongs.
- Moved the blinking light to the battery and added a battery meter that updates once per second.
- Adjusted the drone's size and camera position.
- Updated the mod and inventory icons.

## 0.1.2

- Added a green light to the top of the model, instead of an invisible light source.
- Updated the drone model with the antenna, front flashlight and rear cargo doors.
- Rear cargo doors use the corrected solid model and open 90 degrees outward while players view cargo.
- Completely overhauled the HUD, giving it a cleaner, sleeker, and more viewable interface for the drone.
- Increased open/close door range to 3.5 meters, facility/fire-exit range to 4 meters, and defibrillator targeting to 5 meters.
- Added revive-target medical-cross indicators and yellow lightning-bolt indicators for zappable enemies in the drone feed.
- Moved the flashlight origin down to the front of the lower black compartment, below the drone camera.
- Added a lockpicker module, which takes 10 seconds to lockpick a door. Freezes the drone in place while lockpicking.
- Tweaked the flashlight brightness and range to be much more viewable for the operator.
- Moved cargo access to R; hold E for one second still picks up the drone.
- Updated module device labels and usage instructions.

## 0.1.1

- Reworked multiplayer messaging to use explicit binary packets for drone movement, pilot control, cargo, battery state, and events.
- Added recovery when the game's networking message manager restarts.
- Added logging for successful state synchronization and rejected network packets, including build and protocol details.
- Added a terminal message when the drone has not synchronized with the host.
- Updated the on-screen software version to **0.1.1 BETA**.
- Updated the mod's package icon.

Host and all clients must use the same build. Multiplayer changes still need live testing.

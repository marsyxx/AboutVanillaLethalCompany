# Changelog
## [1.4.3]
- Removed old error message

## [1.4.2]
- Updated NavMesh regen
  - Now the navmesh will only regenerate when needed and if the moon in whitelist (so it should reduce the lag before landing on the moon)
- Removed patch for lamps when 2-story ship is installed
  - Blame mel if lights stop working

## [1.4.1]
- Updated compatibility with Ship Windows, ShipWindowsBeta is no longer needed

## [1.4.0]
- No fun allowed (after April 1st)

## [1.3.19]
- Fixed a bug with restoring walls after being fired with ShipWindowsBeta installed  
  - Please note that if this bug remains with a 2 story ship installed, then I have nothing to do with it, no need to report it to me in the thread  
- Removed old option in compatibility config "force dont move posters to true"  
- Now clients are forced to regenerate navmesh, the option to disable this has been removed
  - This is done to avoid POSSIBLE problems with enemy navigation, but you most likely will not notice any changes in this regard

## [1.3.18]
- Fixed offmeshlink for roof ladder

## [1.3.17]
- Updated compatibility for ShipWindows (2.3.3 -> 2.4.0)

## [1.3.16]
- Fixed models with ship windows (i forgor to update assetbundle)

## [1.3.15]
- ShipWindows compatibility changed to ShipWindowsBeta compatibility

## [1.3.14]
- Accidentally uploaded the wrong version, oops

## [1.3.13]
- Fixed light
- Fixed ShipInnerRoomBoundsTrigger with right side extended

## [1.3.12]
- Changed blacklist default values

## [1.3.11]
- Celestial Tint compatability (ship parts config option in celestial tint config)
- Fixed position of one trigger for right side of ship
- Fixed a bug where a prop with only the right side of the ship would appear when landing

## [1.3.10]
- LethalConfig compatibility re-added (not really re-added but it is)
- Fixed bug with loading freeze on Orion

## [1.3.9]
- Unwraped UV for floor with window
  - Thanks old me for saving this .blend in the past
- ShipColors compatibility fixed (Thanks, [darmuh](https://thunderstore.io/c/lethal-company/p/darmuh/) and Endoxicom)
- Small changes in navmesh prefab
- The position of the left speaker has been slightly changed

## [1.3.8]
- Fixed a bug where the map could not be loaded if there was no wall

## [1.3.7]
- Fixed a bug with broken monitors on some moons (again)
- Tried to fix navmesh on USC Votrex (Failed)
  - Mobs still can't get in/out of the ship
- Walls are back
  - With a 2StoryShip, only the left wall works.
  - The ship's navmesh also depends on walls (but not with a 2StoryShip)

## [1.3.6]
- Fixed light again
- Changed navmesh prefabs  
  - For some reason USC Vortex just doesn't want to be on good terms with me =(  
    - Most likely there are many more such moons than that one  

## [1.3.5]
- Fixed offmesh on some moons
  - Be aware that offmesh may still be broken on some moons due to differences in terrain, I'm looking for a solution

## [1.3.4]
- Fixed navmesh position on some moons

## [1.3.3]
- Fixed a bug with broken monitors on some moons

## [1.3.2]
- Removed fog exclusion zone stuff to increase fps  
- Corrected the position of the right rear ladder

## [1.3.1]
- Light fix for 2StoryShip

## [1.3.0]
This is the biggest patch for me and for this mod, so I'm obviously missing or forgetting something.  
- The entire changelog has been moved to a separate tab
- Removed Lethal Config from dependencies
- Added compatibility with Ship Windows
- Interior walls have been removed (I will bring them back in the future(tm)) but it's not a big loss for you considering most of them are being removed =(
  - Removed navmesh of walls
- Added blacklist/whitelist for navmesh
  - Blacklist - fast and buggy
  - Whitelist - old method, slow and less buggy
    - Moon is automatically added here if it is not in any list
  - Example list - An example of how to fill out a whitelist/blacklist
- Now navmesh is generated only on the host side by default (can be changed in the config)


## [1.2.5]
- The hole, which is only visible from inside the ship, has been closed
- Added option to config to disable scary red warning about static navmesh

## [1.2.4]
- Fixed a bug where the light on the ship turned off only partially for the right and left lamps closer to the door
- Tried fix the railings, they can stick out a bit (they stick out even on the vanilla ship, so now I can say they are closer to vanilla)

## [1.2.3]
- Unwrapped UV for floor
- Now navmesh change should work on Wesleys moons (expect bugs)

## [1.2.2]
- Added a warning if the moon has static navmeshes and Wider Ship cannot generate new ones.
- Unwrapped UV for ship
- Added [Matty Fixes Experimental](https://thunderstore.io/c/lethal-company/p/mattymatty/Matty_Fixes_Experimental/) as a dependency

## [1.2.1]
- Fixed a bug due to which the right part of the catwalk was not affected by the physics of the ship's movement
- Fixed a bug due to which monitors could "turn off" when entering the right part of the ship
- Fixed collision near left ladder
- Fixed a bug where mimics would use old non-existent ladder AI nodes instead of new ones

## [1.2.0]
- Added the second (right) part of the ship
- Added right inner wall, configurable in config (Client-side, like the left wall for now)
- Finally, posters are enabled, now poster mods work =)
- Please ignore the hanging light switch, it's temporary.
- The door monitor and door control panel have been moved closer to the lever (A lot of people will die from this)

## [1.1.5]
- Fixed a bug where the old ship navmesh was generated on Embrion and sometimes on Assurance
- Navmesh should now generate properly on modded planets

## [1.1.4]
- Mob behavior fixed (I hope the T-Rex went inside for the last time.)
- Railing material changed to vanilla

## [1.1.3]
- Mob behavior fixed (again)

## [1.1.2]
- Fixed a bug where the ship could not leave the Company Building

## [1.1.1]
- Fixed the problem with multiplayer

## [1.1.0]
- Fixed mob behavior with left side of ship (yay)
- Now the remote scrap switches the lighting on the left side of the ship 
- Temporarily removed from the config the setting for buying the left part of the ship

## [1.0.6]
- Lethal Config is now required
- Added config to change the left inner wall (client sided)
- Added unused config for ship upgrade (for future use)
- Ship asset is now split into 2 parts (walls&roof and floor)

## [1.0.5]
- Fixed textures (materials) of the ship, now it is not white (closer to vanilla)

## [1.0.1] - [1.0.4]
- Moved SideMachinaryLeft to the right side next to the long stairs
- Now the light switch also turns off the light on the left side of the ship

## [1.0.0]
- Initial release
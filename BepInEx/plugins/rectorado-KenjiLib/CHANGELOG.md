## 0.12.1

* Changed the modversion internally

## 0.12.0

* Added **KStunExplosion.**
* Added an optional **GUID** option for **KTriggerCooldownTimer** to have persistent cooldowns for objects that may get despawned or destroyed.
* Fixed **KWeatherExtras** logspamming "*customFloodedAudio is null*" even when the "**EnableCustomFlooded**" option was off
* Potential fix to **KWeatherExtras**' "**EnableCustomStormy**" thunderstorms not working on subsequent days.
* Cleaned a couple of WIP scripts that should not be showing in editor.
* Added a bit more context to some debug logs.

## 0.11.4

* *LightsBackOn()* from **KLightsEvent** now succesfully turns the lights back on after re-activating the power

## 0.11.3

* Fixed a problem with **KInteractableDisabler**

## 0.11.0

* KenjiLib now has some network capabilities I think, I need to learn how to incorporate them into the some of the scripts if I do that in the future i will document it. The ones networked will have it mentioned next to the name so you know you don't need something like JClientSync
* Succesfully exported as ***Release*** instead of ***Debug***
* Added **KTriggerCooldownTimer \[Networked]**
* Added **KTriggerInteractableDisabler** **\[Networked]**
* Added **KSignalCallSender** and **KSignalCallReceiver \[Partially Networked]**
* Added **KEventCounter \[Partially Networked]**
* Added **KLogUnlocker** **\[LLL]**
* Updated **KRunOnUpdate**

  * Now it is a bit more flexible, you can run on update for X seconds and activate the function every Y number of seconds instead of every frame.
* Cleaned the code a little

## 0.7.1

* Updated **KDestroyAroundShip**

  * Now has a bool (active by default) for the deletion area to be active (to account for the new additions)
  * Added "Destroy Around Main Entrance" and "Destroy Around Fire exits" as options, if active it will destroy the gameobject in a radius around the ones active.
* Updated **KLightsEvent**

  * Added a call to activate the oldbirds.

## 0.7.0

* Added 3 new scripts: **KRunOnUpdate**, **KWeatherExtras** and **KHealingArea**
* Updated KHudMessaged: Now you can call "displaySlimeOnFace" to display the Gunkfish's goop on the screen for 10 seconds
* Cleaned the code a lil and tweaked some of the logging.
* Now a config will generate for you to select the level of  logging.

## 0.4.0

* Added 2 new scripts: **KHudMessages** and **KHelmetCondensation**

## 0.2.1

* Added a new stuff to **KLightsEvent**

  * Trigger JLL Appy Event: If true, it'll trigger any associated event(s) from the JLL Appy Event (JLL needs to be present for this to work)
  * If the permanent power off is already active (From a vanilla apparatus pull or this script) it'll skip the PermanentPowerOff or AppyEvent routines.
  * Now the AppyEvent should work like the vanilla apparatus pull (Wake up oldbirds/spawn extra enemies)

## 0.2.0

* Added a new script: **KLightsEvent**
* Updated scripts so they have tooltips explaining what it does, and some minimal logging.
* Updated **KDestroyAroundShip** to now include a useful wireframe gizmo to see what's the area.

## 0.1.0

* First Release


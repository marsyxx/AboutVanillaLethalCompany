# 1.5.7
+ Updated assembly references for v73.

# 1.5.6
+ Removed modified carry weight feature when the Lategame Upgrades mod is enabled. This patch is generally not stable alongside other mods modifying/affecting carry weight, and I don't have enough time to create a workaround.

# 1.5.5
+ Made various code changes to help with combining modifiers from other mods with BetterStamina.<br>
Note: Depending on how other mods implement changing speed/jump force, they *may* overwrite any modifiers from this mod.

# 1.5.4
+ Added config option to enable sprinting to max speed instantly, instead of speeding up over time.

# 1.5.3
+ Fixed bug where the crouch speed multiplier in the config would keep getting set to 1 upon launching the game.

# 1.5.2
+ Added config options for stamina regen and consumption multipliers while inside and outside of the factory/dungeon.

# 1.5.1
+ Added config options for speed multipliers inside, and outside of the factory/dungeon.

# 1.5.0
+ Provides much more control over configs.
+ Added individual stamina regen multipliers for idle, walking and crouching. The crouching modifier will be applied on top of idle/walking.
+ Added individual speed multipliers for walking, crouching, sprinting and limping. Crouching and limping multipliers will be clamped between 0 and 1.
+ Added config to set the sprint speed multiplier on ladders. This is only a config so it can be reset to 1 if the vanilla game ever adds their own implementation of this.
+ Added individual drunk modifiers in the config that will also affect movement/climbing speed, stamina regen/consumption, and jump force.
+ Other tweaks/optimizations.

# 1.4.1
+ Fixed a bad math calculation when adjusting the weight penalty.
+ When using custom ladder climbing speed values (values that are not 1) when climbing an extension ladder that is not straight up and down, you will no longer move you straight up. You will now climb the direction of the ladder.

# 1.4.0
+ Configs should now update when creating/joining a game instead of needing to restart the game.
+ Added config for jump height multiplier.
+ Added config for ladder climb speed multiplier. Climbing animation speed also scales.
+ Updated mod description for latest update compatibility.

# 1.3.2
+ Updated my outdated description to make more sense for setting configs.

# 1.3.1
+ Hotfix for errors in update function.

# 1.3.0
+ All stamina/speed/weight values will remain vanilla unless in a lobby where the host has the mod, or if you are the host.<br>
No more more random players with infinite stamina or speed in your unmodded lobby. (sorry about that)
+ Fixed adjusted weight values affecting UI and other elements. (hopefully)

# 1.2.2
+ Temporarily disabling adjusting weight penalty while I fix its issues.

# 1.2.1
+ Fixed a bug where weight was not updating correctly when dropping or storing items.

# 1.2.0
+ Config settings from the host now sync with all clients.
+ Added a movement speed modifier to the configs.

# 1.1.2
+ Reverted some code that was causing some minor issues.

# 1.1.1
+ Edited descriptions.

# 1.1.0
+ Added configs that you can tweak.
+ Fixed bug where movement speed wasn't affected by lower weight penalty.

# 1.0.2
+ Added weight penalty reduction.

# 1.0.1
+ Tweaked some values.

# 1.0.0
+ Initial release
# 2.3.17
+ Added compat for BettererLethalVRM

# 2.3.16
+ Fixed potential edge cases for a few issues.

# 2.3.15
+ Fixed bug in latest update breaking the emote camera.
+ Fixed bug while closing the settings in game while emoting causing errors.

# 2.3.14
+ Fixed bug when accessing new utility slot in V80 while emoting.

# 2.3.13
+ Recompiled for v73.

# 2.3.12
+ Fixed errors caused when closing the keybinds menu from the main menu.
+ Removed Herobrine.

# 2.3.11
+ Updated compat with LCVR to work with the latest version.

# 2.3.10
+ Changed InputUtils to a hard dependency to prevent the recent errors occurring without the mod. I may not support keybinds without InputUtils anymore.

# 2.3.9
+ Fixed float parsing for configs: RandomEmoteDelay and RandomEmoteDuration to work on any system language.<br>
Previously, some system languages may cause the parse to fail, and both of these values would default to 0 0

# 2.3.8
+ Fixed the newest emotes being complementary. THey should now be assigned to a specific tier.
+ Emoting is now limited to once every 1/4 second to prevent spamming, and spamming network messages to the host/clients.

# 2.3.7
+ Added 14 new emotes: APT, Jake Bug Dance, The Spark, Touch, Brokenhearted, Clix Groove, Fast Slow Disco, Firework, Image, Mr Brightside, Royals, Squeak Step, Still Into You, and It's Ok I'm Ok
+ Updated MoreCompany .dll references to hopefully resolve issues.

# 2.3.6
+ Code tweaks to prevent more errors from V72. (More may exist, and I will fix them as I get them)

# 2.3.5
+ Updated references for V72 to hopefully resolve issues.

# 2.3.4
+ Fixed emote name typo in "That One Thing Black Window Do". Replaced "Window" with "Widow".
+ Fixed issue that occurs when favoriting certain emotes that performs a random emote, specically the Death emote. Favoriting them should not cause any more issues.

# 2.3.3
+ Some tweaks to help determine if players are performing custom emotes.

# 2.3.2
+ Replaced the Criss Cross emote with Rat Dance, and added the rat dance music. (moved to tier 2)
+ Adjusted logic for closing emote menu that will not trigger the quick menu manager closing anymore.<br>
This will prevent mods such as LethalConfig from lagging the game a bit every time the emote menu is closed.

# 2.3.1
+ Accidentally renamed the bye-bye-bye emote which prevented its audio from playing. This should be fixed now.

# 2.3.0
+ Adds 27 new emotes!<br>

    + **Tier 0**<br>
    Stand Off, Feelin' Royal, Illusive
    
    + **Tier 1**<br>
    Lucid Dreams, Line Dancin', Fly to Tokyo, California Love, Miku Miku Beam, Smooth Operator, Nuthin But a G Thang, Attraction, Touching The Sky, What You Want, Starting Prance, Skyward, Independence, Fantasy Faceoff, Deep Explorer, Cairo, Bratty
    
    + **Tier 2**<br>
    Miku Live, Bye-Bye-Bye, All I Want For Christmas Is You, Get H.O.T., Smitten, Soar Above, Desirable<br>
    
- Sorry, no Tier 3 or Complementary emotes.
- Added a few random variations to the death emote.
    

# 2.2.14
+ Fixed issue where you sometimes couldn't register quick emotes when pressing the hotkey in the emote menu.

# 2.2.13
+ Previously set incorrect conditions on whether the game should save values. This has been corrected.
+ Set more conditions for various TME keybinds so they won't activate if the pause menu is open.

# 2.2.12
+ Fixed issue with emotes sorting method varying between system languages, resulting in de-synced emotes between some clients.
+ Fixed issue where the game was saving unlocked emotes when the option to unlock all emotes is enabled. The game *shouldn't* normally touch the game's save file when this option is enabled. Preferencesa are still saved, but in a different location.
+ Re-added quick emotes. This *seems* to work fine, but not thoroughly tested. Please report any quirks on the discord, or on the github!<br>
Quick Emote keybinds can be set/changed with the InputUtils mod.
+ Invisible stairs emote is rotated correctly so the character faces where you're looking now.
+ Moved the Fishy Flourish to DMCA.

# 2.2.11
+ Accidentally prevented favorited emotes from loading in 2.2.10. This should be fixed now.

# 2.2.10
+ Removed some test code for an incomplete/experimental feature that was accidentally included in 2.2.9

# 2.2.9
+ Updated compat for MoreCompany to fix some outdated references.

# 2.2.8
+ Added a config option to disable displaying the current emote when performing a random emote. This can be helpful with memory management when using mods that cache all chat messages.

# 2.2.7
+ Performing a random emote will now display which emote is being played.
+ When using a controller *without* the InputUtils mod, you should no longer experience random bindings not working.
+ Prevents some unexpected errors with the MoreCompany cosmetics compat with certain mod combinations.

# 2.2.6
+ Added a random emote keybind. Default keybind: [M] (Rebindable with InputUtils)
+ Added quick emotes.<br>
You can assign quick emotes by hovering over an emote in the emote menu and pressing the quick emote hotkey.<br>
Note: InputUtils is required to update these keybinds.
+ Quick emotes will be paired with a specific emote. Attempting to perform a quick emote without having the emote unlocked will fail.

# 2.2.5
+ Potential fix for unexpected errors when saving emote menu, and third person emote preferences.

# 2.2.4
+ Half-support compatibility patch for LethalVRM/BetterLethalVRM to display the model in third person.
+ The model in the emote menu will not work, but the original model should re-appear, however.

# 2.2.3
+ Fixed issue with emote credits not always saving for non-host clients when PersistentEmotes and PersistentEmoteCredits is enabled.

# 2.2.2
+ Added a keybind to perform the next instrument if you are currently performing an instrumental emote, such as Jug band or Travelers.<br>
*Previously, to cycle through the instruments, you would have to perform the emote in the emote menu while already performing the emote.*
+ This keybind will appear as a control tip on the top right of the screen, only while performing instrumental emotes.<br>
They keybind can be changed if you have the InputUtils mod enabled.

# 2.2.1
+ Fixed issue with the mouse not locking to the game window while performing an emote. (I forgot to revert some testing code)
+ Fixed incorrect mod version in the PluginInfo.

# 2.2.0
+ Travelers Update!
+ Added a new multi-instrumental emote, *Travelers*, from Outer Wilds. (7 instruments) [Preview](https://www.youtube.com/watch?v=OL_Oka6sGE4)
+ I may add a method in the future for playing and recording audio so you can layer them without needing other players.
+ Added 15 new emotes: Carameldansen, Jar of Dirt, Feel Good, Groove Destroyer, Gwen-Step, Hellfire Dance, I'm Out, Invisible Stairs, Looking Good, Mine, No Tears, Outlaw, Shimmy Wiggle, Glitchin', and Entranced.
+ Jar of Dirt can also be found as an emote prop if you have the TooManyEmotes Scrap mod enabled.
+ Disabled the Ghost Girl patch (again?), as it's not needed anymore.

# 2.1.26
+ Fixed compat with AdvancedCompany.<br>
NOTE: I did not test extensively, but I did test that the cosmetics appeared on yourself while emoting. (It should work for other players, too, but you never know)

# 2.1.25
+ Slight changes to prevent some mod conflicts with AdvancedCompany.

# 2.1.24
+ Scan nodes should now properly be hidden while emoting in third person. Previously, they would appear, but in the wrong position, and it looked like a mess.
+ Swapping between third and first person while already performing an emote will now correctly hide/show any cosmetics from MoreCompany.

# 2.1.23
+ Reapplied patch that should correctly show the player's local body on the ship camera, and on players' helmet cameras (Helmet_Cameras mod)
+ Re-enabled the DressGirl (little girl) patch so she doesn't appear on these cameras. (or any other cameras viewing layer 23, such as MirrorDecor and probably others)

# 2.1.22
+ Fixed bug where your player model wouldn't appear while emoting.
+ You should no longer see a second pair of arms while emoting.
+ Updated mod description to show V55 compatibility.

# 2.1.21
+ Fixed an bug in 2.1.20 that prevented you from emoting with emote prop objects, such as the saxophone.

# 2.1.20
+ Revised how the third person renders the player to be more straight-forward and more consistent. (less prone to visual bugs)\n
Please note: If you find a visual bug that is present in 2.1.20, please let me know on the github. It's likely a small oversight I made.
+ This should fix the bug with MoreCompany were players without cosmetics would sometimes appear to wear the host's cosmetics.\n
The above bug was *sorta* fixed before, but often appeared with random mod combinations related to rendering the player.
+ Fixed (attempted) a bug that may occur if playing an emote from a prop object and you swap item slots.

# 2.1.19
+ Removed a zip of the previous TME version that was accidentally uploaded with 2.1.18. This will fix the doubled filed size.

# 2.1.18
+ Added the Company Jig emote! (fornite edition)
+ Added 8 more new emotes: Potted Groot, Planetary Vibes, Bad Guy, Rebellious, Boney Bounce, Dancery, Challenge, Brite Moves

# 2.1.17
+ Fixed bug with the MoreCompany patch that, while emoting, caused all players *not* wearing a cosmetic to appear to be wearing *your* cosmetics.
+ Removed old debugging logs.

# 2.1.16
+ Fixed issue in the emote menu where the additional emote settings on the right would not properly register when the mouse was hovering over it, making some of the settings impossible to interact with.
+ Added a few extra logs.
+ Temp fix for harmless errors complaining about not being able to destroy a Light component when creating the player's animation previewer object.
+ The camera will now follow the player's head during first person emotes, and if moving while emoting is disabled. This was removed in a recent update temporarily.<br>
Eventually, I will allow moving the camera a little, similar to when the game (soft) locks your camera in animations, such as using a terminal, or pulling the lever.

# 2.1.15
+ Marked the Toothless dance song as *not* DMCA-free.

# 2.1.14
+ Forgot to implement corrupted file checks when loading emote filter preferences.

# 2.1.13
+ General tweaks/fixes.
+ While moving while emoting, holding/toggling the rotate (freeze) character hotkey will now stop the character's emote if they move, just like when you move when moving while emoting is disabled.<br>
Previously, holding/toggling the rotate character hotkey would only freeze the rotation, but allow you to run straight, which sometimes felt like you were stuck in a bugged state when you really weren't.
+ You now shouldn't be able to zoom in/out while emoting if the emote menu is open.

# 2.1.12
+ When color coding emote names in the radial menu is enabled, complementary emotes will keep the default (white) color instead of being shown as green.
+ Additional checks and fixes for loading preferences from file, to help prevent errors.
+ Created separate control tips for emoting in third person to better manage the control tips without changing the original control tips. The original control tips will be hidden while emoting instead of clearing them.
+ Optimized the third person camera controller logic, and fixed some minor issues.

# 2.1.11
+ Fixed issue with the config: "AlwaysEmoteOnFirstEncounter" for masked enemies not being referenced, thus was the game was treating it as always enabled.
+ More potential automatic fixes in case of file corruption when loading favorited emotes. (or when loading old favorited emotes from old save location)
+ Added better solution/method for adding emote credits when group credits are earned. This *may* fix some issues with some players not receiving emote credits when supposed to.
+ After purchasing an emote, the terminal doesn't tell you which page the emote is on in the radial menu anymore. (this was old code before loadouts and filters were a thing)
+ Added verbose logs, and the option to enable them in the config.

# 2.1.10
+ Updated the disco patch to not cause errors on v49.

# 2.1.9
+ Converted the Controller crew emote prop (gamepad) to function as an emote prop is TooManyEmotesScrap is enabled.
+ You can now sync with *most* emotes that another player is performing. The performing emote does not have to loop anymore.
+ ~~Updated the disco patch to not cause errors on v49.~~
+ Mousing over emotes should now be accurate on all game window aspect ratios.<br>
Previously, hovering over an emote in the radial menu might not have triggered until you mouse really far away from the center if you aspect ratio was far off from the default.

# 2.1.8
+ Accidentally broke compat with ModelReplacementApi (I think). Should be fixed now.

# 2.1.7
+ Fixed control tips while emoting showing the controls for when moving while emoting is enabled, but the host has this force disabled.
+ Possible fix where favorited emotes from the old save location were not being transferred to the new save file.

# 2.1.6
+ Added a config option to allow the host to force disable moving while emoting for all players. This will remove the option from the emote menu for all players.
+ Removed duplicate TooManyEmotes dll. My bad

# 2.1.5
+ You can now allow moving while emoting from the radial menu. This setting has been removed from the config, and will not sync with all clients anymore.
+ You can now rebind zooming in/out while emoting instead of the default scroll in/out. This only applies if you have the InputUtils mod enabled.
+ Updated logic for rotating character and zooming in/out while emoting in third person.
+ While emoting, *if moving while emoting is enabled*, the rotate character hotkey will instead freeze the character's rotation, and the zoom camera hotkey will only work while the rotate character hotkey is held.
+ You can now jump while emoting, if moving while emoting is enabled. Crouching or pressing escape to open the quick menu will cancel the emote.
+ Emotes from emote props will not be unlocked by default when I am a Party Pooper is enabled in the config. The host can change this in the config.
+ Patched the new disco ball so it temporarily mutes itself if someone is performing an emote with audio nearby.
+ Added config option for global persistent unlocked emotes. Unlike the persistent unlocked emotes that was previously added, these emotes will be unlocked globally, and you will have access to these emotes in any game in which the host has this setting enabled.<br>
NOTE: When enabled, many emote settings in the config may be limited, or forced to their default values in order to prevent globally unlocking emotes easily. If you don't like this, consider only enabling persistent unlocked emotes. (not the global version)<br>
ALSO NOTE: Global persistent emotes may currently have bugs, so please post any issues on the github, thank you!
+ A separate file will now be used to keep track of all user save data that is not related to a specific save. (favorite emotes, globally unlocked emotes)<br>
This will be easier for people to fix corruption issues with this mod by deleting this file instead of the game saves.
+ Actually sorted emotes in the radial menu by name this time.
+ Emotes should stop immediately upon stop performing, unless performing an emote while already performing an emote. This will prevent camera jitter.
+ Fixed an error caused when performing an emote when quitting the game.
+ Fixed an issue where the ThirdPersonEmote preferences would try saving every time you close the emote menu, regardless on if you changed anything.

# 2.1.4
+ Readded some UI configs to add optional colors. These will be tweaked again to not look so bright.

# 2.1.3
+ V50 compatible!
+ Added 3 more emotes: Broke up, Pumping iron (prop emote), Paddle Royale (prop emote)
+ Assigned Take Me On (song) to Best Mates (dmca version)
+ Fixed issue with the MoreCompany patch preventing players' chat messages from reaching other players. Also, updated this to work on the latest MoreCompany update. (1.9.0)
+ More network optimizations.
+ Fixed issues with non-host clients not being able to perform the next emote in an emote sync group (such as Jug Band) when performing that emote on top of itself.
+ Fixed some emote menu preferences not saving when supposed to.
+ Emotes in the "All" section of the emote menu should be in alphabetical order now, with favorited emotes appearing first.
+ Fixed some links in readme/changelog not pointing to the right place.
+ Added some checks and protective measures in case of save corruption.

# 2.1.2
+ Removed grabbable emote props as scrap from *this* mod.
+ Grabbable emote props are now associated with a separate mod, which will require this mod.<br>
You can find this mod [here](https://thunderstore.io/c/lethal-company/p/FlipMods/TooManyEmotesScrap/).
+ You join other players, you both must either have the TooManyEmotesScrap mod enabled, or both have this mod disabled.<br>
If you are hosting a lobby for random players to join, or you are looking to join random servers, it might be best to disable TooManyEmotesScrap for the best compatibility.
+ Fixed bug where players couldn't emote after performing an emote on a grabbable emote prop. Future emotes were not able to be played, even from the radial menu.
+ Fixed issue where non-host clients would sometimes have their emote immediately stopped if performing an emote while they were already performing another one.
+ Slight network optimizations in case this mod was causing lag.
+ Fixed camera stutter that sometimes occured when stopping an emote.
+ Various other code optimizations.

# 2.1.1
+ Fixed issue with red collider box being rendered when performing an emote with props.

# 2.1.0
+ Added grabbable emote props which can be found on moons as scrap. If this has issues, please disable feature in the config, and creating an issue about this on the github would be appreciated!
+ Fixed issue with "random" emotes, such as rps, or sexy saxophone, not syncing correctly with other clients.
+ Added more potential fixes for errors caused by joining/creating a second session during a single launch.
+ Fixed the MoreCompany patch after I broke it. Whoops. Cosmetics shouldn't duplicate or appear the same for every player when emoting, and the errors from applying the patch should be gone. (hopefully for good)

# 2.0.11
+ Potential fixes for errors caused by joining/creating a second session during a single launch.
+ Renamed the emote props asset bundle to help prevent potential conflicts with other mods using the same name.
+ Loosened restrictions on if you can swap items during an emote.
+ Swapping items during an emote will now properly swap the holding animation.

# 2.0.10
+ Fixed issue with the first person setting in the emote menu not saving.
+ Fixed the "I don't care" emote having the wrong start animation.

# 2.0.9
+ Added 3 more emotes. AFK (complementary), Ma-ya-hi, and Snake Summoner.
+ Added first person emotes option in the emote menu. (experimental)
+ Added option in radial menu to only perform emote animations (no audio). This does not mute emotes, but prevents the audio from playing at all when you perform emotes. Other players will not hear the audio either.
+ Added the persistent emotes mode in the config. If enabled by the host, emotes will not be reset when the ship/game resets. Emote credits will still reset. These emotes are linked to the save in which they were unlocked on. (for now)
+ Applied patch for HelmetCameras to correctly render the local player's body from other players' cameras.
+ Revised some code a bit related to the emote audio to potentially fix issues with audio playing indefinitely. This is usually caused when emotes play audio on the player, rather than on the boombox/ship speaker.<br>
If this is still an issue after this update, I would appreciate it if you could share any details about this on this mod's github page, and if possible, how to replicate this so I can investigate this further.

# 2.0.8
+ Fixed issue with player's local helmet appearing on other players while they emote.

# 2.0.7
+ First person cameras now animates during emotes. This currently only affects mods that let you see players' cameras, such as helmet/body cams. First person emoting will be added in the future as experimental.
+ Updated compat with LCVR to support new API and new method for detecting players in VR mode. If you still run into issues after this update, please let me know on the discord!
+ Added config option to force reset favorited emotes upon next boot. This may fix issues in some cases where the user has some favorited emotes that aren't in the game anymore. (the config setting will reset back to false if set to true, and after it resets the favorited emotes)
+ Other minor bug fixes.

# 2.0.6
+ Potential fix for errors and camera issues caused by dying while emoting.
+ Added a config entry to disable the girl patch in case of it conflicting with another mod. This patch is to fix the issue with the emote camera seeing the girl while she's "unrendered".

# 2.0.5
+ Added filters in the emote menu when showing "all emotes". You can hide complementary emotes this way if you want.
+ Fixed issue when syncing the sexy saxophone. This emote can randomly play one of two emotes with music. Syncing now always syncs the song.
+ Playing emote music on the ship speaker *shouldn't* mute the speaker anymore.
+ Performing jug band while in the same sync group as someone else will now play the next instrument, but stay in sync.
+ Tweaked some emote audio timing.

# 2.0.4
+ Added Dance Moves music.
+ The ship speaker can now be used as an emote audio source. This can be disabled in the config.
+ Fixed emotes with multiple sub emotes from not being able to be favorited. (jug band, rps, etc)
+ Tweaked some audio settings.
+ Emote audio will now not play on a boombox if the boombox is currently playing its normal music. (normal music as in boombox music, not emote music)
+ Fixed terminal issue getting commands mixed up with ReservedItemSlotCore's new terminal command.

# 2.0.3
+ Re-added audio after fixing some issues.
+ Sorted dmca and dmca-free audio, and added a dmca-free toggle in the emote menu.<br>
In dmca-free mode, many songs will be muted, but some may be replaced by non-copyrighted versions.
+ Fixed issue with emote audio being heard (quietly) everywhere on the map.
+ Performing an emote with audio will always try to use the closest boombox to you now, instead of the closest boombox that was not playing audio. This helps when intending to perform audio on a specific boombox instead of another one that someone else had.
+ Re-added California Girls
+ Fixed issue with held item not moving with you during emotes.
+ Fixed another potential issue with favorited emotes disappearing.
+ For various emotes in the emote store, you may need to type the whole emote name if the name might conflict with an existing terminal command so the terminal command will not be blocked.
+ Fixed issue with controllers not being able to perform emotes.

# 2.0.2
+ Didn't mean to remove Jug Band in 2.0.1, oops.

# 2.0.1
+ Temporarily removing audio to fix some issues. Will reupload in a few days when I have free time.

# 2.0.0
+ Added emote audio! These will automatically play on a nearby boombox that's not currently playing audio from an emote.
+ Added emotes with props!
+ Some emotes, such as those with musical instruments will not need a boombox in order to play audio.
+ The Jug band emote has been added, which is 1 emote, but includes 4 emotes with their own musical instrument, and up to 4 players can sync with the emote to perform a different instrument. Preview: https://youtu.be/tWsCQk6ODEk<br>
More emotes like this may come in the future.
+ Added settings in the emote menu to mute and adjust emote audio volume, as well as added config options for more customization.
+ Fixed keybind display name issues. (hopefully for good)
+ Fixed compat with mods who manipulate the player's bones and cannot see emotes.
+ Please report any bugs with this update to me on the github page, thank you!
+ Re-added California Gurls

# 1.9.5
+ Fixed issue where Masked Enemies that die while emoting continue emoting.
+ Re-added California Gurls. Again. Why does it keep getting removed???
+ Added more emotes!
+ Re-compressed animations. Sorry for the large file sizes the past few updates!
+ It seems that the animation preview in the emote menu doesn't work for the very first emote now, but once you preview another one, it starts to behave itself. Will fix later.

# 1.9.4
+ Replaced the player's avatar to increase animation quality. This will help with most animations that rely on hand precision. Will be tweaking again soon.
+ Some emotes with a start and loop animation clip might not transition well. Or maybe it's just the windmill emote. I will fix this soon.
+ Fixed issue while syncing with a player's emote where your emote will always start from the beginning.
+ Finally fixed the issue with Corporate Restructure causing the emote camera to appear outside the ship. (spoiler, it was because their mod threw an error that prevented the game's player spawn animation method to run, which I hook onto)
+ Fixed the issue with not being able to scroll backwards on the emote menu pages when reversing the scroll direction in the config.
+ Removed false alarm errors about bones not being mapped. (should have been warnings instead, but removed anyways)
+ Not gonna lie, I feel like I broke something, but I can't find anything wrong... Sus. Just post any issues on the github please :)

# 1.9.3
+ Did a goof and accidentally pushed some testing files into 1.9.2 that caused emotes to not show, thus showing the underlying default dance emote.

# 1.9.2
+ Quickfix for LCVR compat mistake where all players running LCVR could not emote, even if VR mode was disabled.

# 1.9.1
+ Fixed issue with update 1.9.0 breaking the scrolling in game. Whoops!
+ Readded the GetCurrentlyPlayingEmote method to the PlayerPatcher temporarily so the ModelReplacementAPI mod reference for my mod won't be broken.
+ Readded the California Girl emote. I did not mean to remove it :(
+ Removed complementary emotes from the common emotes category. They should only appear in the complementary emotes loadout in the radial menu now.
+ Added checks for cases where multiples of the same emote were being imported in. Probably my fault if this happens, but the mod should continue fine, though.
+ Upon opening the emote menu for the first time each session, the favorites loadout will be defaulted if you currently have unlocked favorited emotes.

# 1.9.0
+ Restructured most of the code revolving around animations, animators, animator controllers, etc.
+ Animations from this mod now affect a separate animator and skeleton, and translates the animations to the actual player's rig realtime. (Thanks Nunchuck and Unsaved Trash from the Lethal Emotes API mod for the tips!)<br>
Animations will now be much more compatible with other mods that may use/edit the players' animator/animator controller.
+ This mod no longer needs to disable/enable the player's animation rigging components in order to correctly animate the arms and legs. This will full remove the previous issue with legs/arms often deforming after performing emotes.
+ Fixed issue with cosmetics that have lights rendering the lights in the first person layer.
+ Fixed some issues with the terminal thinking they're trying to buy an emote when they're trying to go to the company building.
+ Added more complementary emotes.
+ Added more emotes in general. (probably removed some, I don't remember)
+ Probably broke a lot of compatibility with mods that reference classes and methods from this mod, such as LethalCompanyVR and ModelReplacementAPI.
+ Now automatically disables emoting for self when LCVR is enabled. LCVR should be good to remove their compat patches with this mod, and I have notified them of this.
+ Now implements custom networking methods to send/receive emote updates for players/masked players instead of relying on the in-game functions, which is still surprising that it worked at all.<br>
These new methods should be much more reliable and future proof.
+ Various bugs and fixes that I can't even remember.
+ Probably broke some stuff, so please report bugs to me about this udpate!

# 1.8.6
+ Tweaked terminal commands to be more accurate and smart when determining if someone is attempting to purchase an emote.
+ Added extra check when performing an emote just in case another mod changed the layer or visibility of the player's main body.
+ Added an optional keybind (only bindable with InputUtils) to force refresh the player's character model and layers. Unbound by default.
+ Fixed issue with keybind display names in hud not updating when setting a new keybind mid-game.
+ Fixed warnings when InputUtils is not enabled.

# 1.8.5
+ Reverted one patch from 1.8.4 to fix character rig issue.

# 1.8.4
+ Fixed case-sensitive issue when typing emotes commands in the terminal.
+ Fixed potential issue with various scripts, or other mods, activating comonents from the animation previewer object. Any components on this object that are not needed should be destroyed properly now.
+ Fixed issue with scrolling pages in the emote menu while an emote is selected and the animation preview freezes up.
+ Rebalanced default emote credits gained multiplier in the config.
+ Favorited emotes that have been temporarily removed should not be removed from favorites to prevent spam in the console.
+ Fixed issue when holding a movement key while starting certain emotes, where you would cancel the emote, but remain in third person.
+ You now cannot emote while a centipede is on your head. If a centipede latches onto your head while emoting, the emote will be canceled.
+ Fixed some feet animations for some emotes. Will be fixing more!
+ Readded the "It's you!" (spiderman pointing) emote. It's a complementary emote.

# 1.8.3
+ Added full (or close to it?) controller support. Work best with the InputUtils mod, and the controls will be easily configurable.
+ Control tip lines should automatically update if you switch between keyboard/controller.

# 1.8.2
+ Updated path for patching BetterEmotes.
+ Swapped some emotes out to fix some bouncing issues.

# 1.8.1
+ Fixed camera not pitching up/down when allowing moving while emoting in the config.
+ Will not attempt to apply MoreCompany Cosmetics patch when AdvancedCompany is loaded.
+ Temporarily removed open emote menu keybind in HUD.
+ MaskedEnemies now shouldn't emote unless within a certain distance.
+ Updated README/Manifest description.

# 1.8.0
+ There are now over 200 emotes!
+ Added a config setting to allow moving while emoting. This will sync with all clients. Rotating while emoting by holding a hotkey will be disabled when this is enabled.
+ Accidentally set the audio listening source to the emote camera when you stopped emoting. Should be fixed now.
+ Refixed mirror decor. Oops!

# 1.7.18
+ Additional tweaks to maybe fix the emotes canceling for players at times.

# 1.7.17
+ Fixed compat with certain terminal mods, such as Neofetch.
+ Fixed echoing in third person emote camera.
+ Made some small changes to maybe prevent issue with player's animations getting canceled when another player cancels their. Probably didn't fix it.

# 1.7.16
+ Fixed compat with MoreCompany.
+ Fixed bug where emote credits were being depleted for everyone when one player purchased an emote with ShareEverything disabled.
+ Fixed bug with emotes/emote credits not being loaded correctly for non-host clients.

# 1.7.15
+ Re-added hook method ModelReplacementAPI used to grab currently playing emotes. (I forgot it was referencing it)

# 1.7.14
+ Tweaked some code to help a bit more with compat with mods that might edit the player's animator controller.

# 1.7.13
+ Fixed issue with "start" emote clips not transition to the "loop" emote clip.
+ Fixed issue with certain emotes not knowing when they ended.

# 1.7.12
+ Added checks in case the player's animator controller is not an AnimatorOverrideController.
+ Fixed issue with not being able to scroll in the emote menu.

# 1.7.11
+ Restructure and reorganization of some code.
+ Fixed issue when the ShareEverything setting in the config is disabled and a player purchases an emote, the emote credits shouldn't be deducted from each player anymore.
+ Some UI tweaks.
+ Possible fix for the issue when players cancel an emote, other players emotes "break".

# 1.7.10
+ Fixed issue with dress girl showing up in the third person emote camera when she's not supposed to be visible.
+ Attempted fix for lag caused by code when MoreCompany is not enabled. (Thanks legoandmars!)
+ Rotating while emote can now be set to toggle in the config.
+ Audio is now based off of the third person emote camera while emoting.

# 1.7.9
+ Added more compat checks for More_Emotes. You should now be unable to emote while performing an emote from More_Emotes. (maybe even from other mods that add emotes)
+ The third person emote camera should now reset and swap back to the main camera if you die.
+ Attempted to fix some issues with translation mods causing issues when checking for certain values in the terminal. (hopefully fixed)

# 1.7.8
+ Re-added Masked Enemies emoting for non-host clients. Will continue testing and tweaking if needed.

# 1.7.7
+ Disabling Masked Enemy emotes for non-host client until I fix the bug with them.

# 1.7.6
+ Fixed issue with some input bindings.

# 1.7.5
+ Edited config descriptions related to keybinds.

# 1.7.4
+ Added support for InputUtils, as a soft dependency. If this mod is enabled, you will be able to access any relevant hotkeys within the game's keybind menu.
+ Some visual tweaks to the radial menu.
+ Created a bit of the framework for controller support. Will release soon!

# 1.7.3
+ Fixed issue with some of the new configs not syncing.
+ Removed Herobrine.

# 1.7.2
+ Fixed errors spamming when looking at a player who is not emoting.

# 1.7.1
+ Fixed some minor issues.
+ Added compatibility for BetterEmotes.

# 1.7.0
+ Added a config option to enable/disable sharing emotes. Read about this in the README!
+ Added something special with the Masked Enemies! Read about this in the README!
+ Fixed deformed character rigs after certain animations.
+ Fixed a bug with being able to perform emotes/open the emote menu when in special animations with enemies.
+ Fixed a bug where the default group credits wouldn't update when used to purchase emotes.
+ Fixed a bug when non-host clients purchased emotes, that the emote credit balance wouldn't update.
+ Players joining after a player DC'd while emoting <i>should</i> not have a broken animation rig anymore for other players.
+ Added a few more emotes!
+ Added config option to disable emoting for yourself. If disabled, this mod will not alter the default player's animator/animator controller, and may fix issues with compatibility with certain mods, such as VR mods.

# 1.6.4
+ Fixed "pose" emotes ending early.

# 1.6.3
+ Minor bug fixes.

# 1.6.2
+ Fixed accidental infinite loop.

# 1.6.1
+ Potential fix for emotes in the store sometimes not syncing with everyone.

# 1.6.0
+ Can now sync emotes with other players by looking at them and pressing E. (will update soon to match interact key)<br>
Syncing with another player's emote will perform the same emote, and match their time in the animation.
+ You can now rotate your character while emoting while holding the Alt key. (can be changed in config)
+ You can now zoom in and out while performing a custom emote by scrolling. Distance is clamped at 1.5 and 5.
+ Various fixes.

# 1.5.10
+ Fixed issue where clients lock up in a dark shadow screen. (not sure about every instance)
+ Fixed issues in some cases where clients don't sync emotes with host properly when the host has all emotes unlocked at start.<br>
(hopefully fixed the main issues from the previous patch)

# 1.5.9
+ Fixed non-host clients in some cases showing that they have every emote unlocked when they don't, but it won't let them use them. They should sync correctly again with the host.
+ Reverted some settings to fix some rendering bugs.

# 1.5.8
+ Fixed issue with some model replacement mods where you see your player's body at times. (hopefully didn't break anything)

# 1.5.7
+ Broke MoreCompany, but refixed now.
+ Tweaked/polished many existing emotes.
+ Added more emotes!
+ Added a Rock Paper Scissors emote to the complementary emotes.<br>
The RPS emote played is random. (I will need to tweak the hands a bit, though)
+ Fixed bug with emotes clients not playing the same emotes for specific emotes.
+ Emote store seed now resets correctly upon resetting ship.
+ Temporarily removed some emotes so I can fix the feet IK. (Will do later)
+ Added more emote loadout tabs. One for complementary, and one for each emote tier. I'll try to get feedback about these later.

# 1.5.6
+ Fixed issue with config not syncing.
+ Fixed issue with favorites not saving/loading at times.

# 1.5.5
+ Forgot to update README.
+ Some formatting tweaks to the terminal.

# 1.5.4
+ Player's shadow should no render correctly while in first person. Should still be compatible with MirrorDecor.
+ Added emote loadouts to the left of the radial menu. Currently, there are only "Favorites" and "All".<br>
Favorited emotes will persist across sessions/saves or game launches. However, they will not be available unless unlocked in your current session.
+ Added a config to re-allow purchasing emotes with group credits again. Emote credits will still be prioritized over group credits.

# 1.5.3
+ Fixed some new emotes not looping when supposed to.

# 1.5.1
+ Emotes can now only be purchased with emote credits.
+ 50% of all normal group credits earned will also be gained as emote credits. This amount can be changed in the configs.
+ Added 17 new emotes!

# 1.5.0
+ Fixed items in local player's hand when performing emotes.
+ Wrote code for CanOpenEmoteMenu(), but never called it. Fixed this.
+ Set toggle open menu to toggle by default. (can still be changed)
+ Changed default change emote page direction. I don't even know which should be default direction for scroll up/down...
+ Default sorting is by rarity, then by name.
+ Brightened the animation preview a bit.
+ Few other changes.

# 1.4.9
+ Applied compatibility patch for BiggerLobby (Thanks legoandmars!)
+ Publicized most of my classes to help with compatibility.
+ Various minor fixes.

# 1.4.8
+ Fixed local player mesh disappearing in third person emotes after dying.
+ Fixed seed not updating and/or syncing properly.

# 1.4.7
+ Fixed issue with scrolling 4 pages at a time in the emote wheel.
+ Fixed syncing buffer overflow issue.

# 1.4.6
+ Added the california girls dance meme from friday the 13th (I think)

# 1.4.5
+ Reduced the price of the tier 4 emotes to 300 (from 500)
+ Fixed compatibility with MoreCompany and MirrorDecor. Hopefully for good this time.
+ Animation preview now shows current skin, as well as any MoreCompany Cosmetics. (might work with custom models)
+ Changed the way the animation preview lighting works.
+ By default, the emote menu is not toggled anymore. Press to open, release to close. Releasing will play the currently hover emote. You can set this back to toggled in the config.
+ Can now change emote page by scrolling. You can reverse the scroll direction can be reversed in config.
+ Updated how syncing works between client/server. <i>Should</i> fix the issues with not syncing emotes and such with server.

# 1.4.4
+ Reverted MirrorDecor and MoreCompany compatibility patch for now.

# 1.4.3
+ Fixed wrong asset bundle path.

# 1.4.2
+ Fixed an issue when disabling the rarity, where the likelyhood of an emote was based on the tier, regardless of how many emotes were in that tier.<br>
This made the emotes from the upper tiers appear more frequently than the ones from the ower tiers.

# 1.4.1
+ Forgot to normalize the prices if the rarity system was disabled in the config.
+ Added config entry for setting the base price for emotes when the rarity system is disabled.

# 1.4.0
+ Added compatibility with MoreCompany and MirrorDecor!
+ Now unlocks all emotes at the start of the game if host does not have the mod.
+ Added an emote rarity system. Each emote will have its own tier, which affects how often emotes of that rarity will appear in each store's rotation slot.<br>
If you're not a fan of the rarity system, you could set all the weights for each tier to the same number in the config.
+ Emotes are now priced based on their rarity. They are not all 100 anymore.
+ Emote store now shows 6 emotes by default instead of 3.
+ Because of this tier system, emote coupons have been removed, and have been replaced with emote credits.
+ Emote names in the terminal have been color coded based on their tier. (green, blue, purple, and red) The colors can be changed in the accessibility section in the config.
+ Weapon wheel not displaying the correct emote has been fixed.
+ The seed bug <i>should</i> be fixed now. It preventing the seed from updating after new games.

# 1.3.9
+ Fixed bug where emotes weren't rotating until after the first round.
+ Fixed bug where free emote coupons weren't registering correctly.

# 1.3.8
+ Forgot to move Asset Bundles into their folder.

# 1.3.7
+ Fixed emotes not rotating daily.
+ Fixed local player arms sometimes appearing during emotes.
+ Potential fix for issue with terminal thinking players are trying to buy an emote when they're not.
+ Moved Asset Bundles in the Assets folder.

# 1.3.6
+ Fixed issue with asset bundles

# 1.3.5
+ Added pages to emote radial menu.
+ Fixed issue with not seeing others emoting with More_Emotes mod installed.
+ Added config to start with all emotes unlocked. (Server only setting, and will sync with clients)
+ Added basic complementary emotes to start with.
+ Tweaked existing emote animations.
+ Emote selection rotates daily!
+ Now over 100 emotes!

# 1.3.2
+ Now automatically removes old config entries.
+ Forgot to re-place the preview animation render subject underneath the map again.

# 1.3.1
+ Accidentally left out updated README.

# 1.3.0
+ Added an Emote Radial Menu!
+ Fixed the issue with start animations not transitioning properly to their loop animation. (as far as I can tell)
+ Potential fix for not loading the asset bundle when not using a mod loader.
+ Various other fixes. There are likely some that I missed.

# 1.2.5
+ MoreEmotes compatibility patch did not upload with my last update. Should be fixed now.

# 1.2.4
+ Added compatibility patch for MoreEmotes!

# 1.2.3
+ Minor compatibility fixes

# 1.2.2
+ Fixed bug preventing you from buying items in terminal.

# 1.2.1
+ Quick fix for assetbundle not loading.

# 1.2.0
+ Initial official release
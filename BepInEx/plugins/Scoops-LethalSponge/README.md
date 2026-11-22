# Lethal Sponge

## v80 Update!
### Sponge's shader rewrite is now included in the base game! Future versions of Sponge will no longer include the shader changes, since they're now a part of vanilla.

Have you ever thought to yourself "Why does a game that looks like this, run so poorly?"

Me too.

Sponge is a fully clientside Performance and Diagnosis suite mod focused on making sure that Lethal Company runs as well and consistently as possible, while also helping you troubleshoot your modlist and find potential problem mods.

## What does it do?
Sponge is split into two categories: Cleanup and Investigation.

**Sponge Cleanup** are a number of patches I've written for lethal company that will increase the baseline FPS, reduce stutters, and allow you to customize your graphics settings to reduce processing power requirements.

**Sponge Investigation** is a set of console readouts that are aimed at helping you find out what mods (or base game objects) might be causing memory leaks or frame drops.

## What does it ACTUALLY do to improve FPS???
Currently, Sponge has the following performance features:
 - ~~A complete re-write of Lethal Company's main outline/posterization shader that cuts down on 1 draw call per object on screen.~~ (Now included in base game)
 - A rework of how Lethal Company hooks into Unity's Input System that fixes various input lag issues, including one where input lag would increase every time you loaded/started a game.
 - Optional asset De-duping to help VRAM and RAM usage on machines that need all the memory they can get.
 - Automatic Scrap culling, and optional LOD generation.
 - Various base lethal company script fixes to patch memory leaks and unnecessary calculations.
 - A number of camera patches so that only the main player camera is rendering graphically intensive effects. Security cams/the map cam have had their FPS limited and extra (usually unnoticeable) graphical effects disabled.
 - Numerous graphics options that allow you to reduce the level of graphical effects, usually ones that Lethal Company has set unusually high by default.
 - A patch to make Lights in game no longer effect volumetric fog at infinite distances.
 - A daily run of UnloadUnusedObjects, which will clean up various objects that were loaded into memory by moons/dungeons/monsters/etc.

## Sponge for Players
For the average player, Sponge is mostly drag-and-drop. The default settings should increase FPS without affecting visual quality in any noticeable way. There are extensive configs for customizing what Sponge changes, so if anything conflicts with another mod or changes your experience in a way you don't like, it can be disabled or altered. For players who need even more extreme FPS help, Sponge features config options to lower graphics settings even further.

## Sponge for Developers/Modlist Curators
For more advanced users, Sponge has a `Verbose Logging` config that can be enabled. This will reduce performance slightly and increase loadtimes while active, so only enable it while troubleshooting. This mode will attempt to track every object in your game and what AssetBundle it originated from. Sponge will perform daily readouts (optional, they can be run on command too) telling you what mods are potentially leaking memory. There are chat commands, including `/sponge modelcheck` and `/sponge texturecheck` which will give you a summary of every actively rendering model/texture in your game and what the largest/most dense ones may be. Using `/sponge help` in game will give you a list of commands.

### NOTE:
Sponge as a tool is meant to help you narrow down potential mods. To truly find the root of memory leaks and FPS issues I recommend using Unity's Profilers. Sponge can be inaccurate due to name-overlaps between mods, so always double check using more intensive profilers once you've narrowed things down.

**Never** use Sponge as a reason to harass mod developers. Small memory leaks are common, and Unity can be a confusing beast to work with. If you find a significant memory leak that is effecting your gameplay, be sure to submit a bug report **politely** and **professionally**.

## Planned Features
These are subject to change:
 - Improvements to the presentation and readability of Sponge's Evaluation logs.
 - The ability to change graphics settings in game, and save personal graphics settings not tied to Bepin config.
 - More base lethal company fixes.

## Feedback is welcome!
Graphics and Memory Leaks are tricky subjects, so while my testing group has seen great improvements with Sponge, the same may not be true for everyone. If Sponge effects your game in an adverse way, please let me know. Also if you know a place where base lethal company is doing something a little silly, let me know and I'll see about adding a fix for it to Sponge.

By nature of what it is doing, some features of Sponge may be incompatible with some mods. I haven't run into anything yet, but if you find a Sponge feature is disrupting another mod, let me know.

## Credits
- Zaggy1024: ILInjector Utility, from https://github.com/Zaggy1024/LC_PathfindingLagFix
- Whinarn: Mesh simplification library, from https://github.com/Whinarn/UnityMeshSimplifier
- Myliaa: Testing help
- Soap: Testing help
- Scoops (me): code
### [1.4.3]
* Shadow Atlas changes now set the Atlas Cache to 2x the selected Atlas size, to match base game. Before v80 the cache was unused, so it was being set smaller.

### [1.4.2]
* Removed the override for vanilla's radar framerate method, since vanilla now uses the same framerate cap method I was using. This also fixes bodycams not rendering with `Patch Camera Script` enabled.

### [1.4.1]
* Removed the compatibility patch for darmuhsTerminalStuff, as with the latest update it seems to no longer be needed and was causing errors.

### [1.4.0]

* Sponge's Shader is now base game!
* This means that all posterization shader related code has been removed from v80+ versions of Sponge. The mod is considerably lighter now that it doesn't need an assetbundle and all of the shader code.
* This is a quick update patch to remove all of that, I'll be checking v80 more thoroughly in the future to see if other Sponge features need to be tweaked for the new version.
* The option to disable posterization will be re-added once I find out what the new custom pass is named so I can disable it cleanly.

### [1.3.6]

* Added a compatibility patch for darmuhsTerminalStuff, so that the Map command renders correctly on the terminal while using Sponge's `Patch camera script`.
* Changed how `Change light fade distance` works, so it now also includes Shadow Fade Distance. Also ensured that Lights/Shadows can never have their distance increased over their defaults by this setting.

### [1.3.5]

* Added a try/catch to texture resizing so that Sponge will continue after running into materials that may be unexpectedly missing properties.

### [1.3.4]

* Added resizeTextureBlacklist config option to exclude specific Textures from resizing by name.

### [1.3.3]

* Fixed disableReflections not functioning and causing a black screen.

### [1.3.2]

* Fixed texture resizing issues, now mipmaps are correctly regenerated.

### [1.3.1]

* Changed default maxTextureSize to 2048, and renamed the config to maxResizeTextureSize so existing installs get the change.
* Known issue: Texture resizing has odd effects on normal maps, looking into it for next patch.

### [1.3.0]

* Finished reverse engineering the original shader. The new shader should now be 1:1 with the original.
* Moved the previous shader into the Legacy slot, accessible in the config.
* Defaulted Volumetric Compensation to false, as it's no longer needed.
* Disabled a number of transpilers and patches when not using the Legacy Shader, since they're not needed anymore.

### [1.2.0]

* Rewrote the replacement shader from scratch, basing it off the assembly of the original lethal company shader. It should be much closer now in visuals.

### [1.1.11]

* Updated the mod for v73.
* Retargeted the HDRP Transpiler since v73 is using a newer version of Unity HDRP.

### [1.1.10]

* Updated the mod for v70.

### [1.1.9]

* The LOD generation system will only use Full Detail and Culled when `generateLODMeshes` is false. LOD1 was just causing edge case issues without adding much benefit for that use case. The LOD2 option has been removed as well to simplify the config, since it also wouldn't have helped performance too much when enabled. This change should fix some edge case issues with scrap animation/textures (Emergency Dice not animating, for example).

### [1.1.8]

* The meshCombiner (used for LODs) will now skip over invalid meshes and not bother throwing exceptions.

### [1.1.7]

* `fixComplexMeshes` has expanded to include More Company Cosmetics. This can be toggled with the new `fixComplexCosmetics` config, and scrap/tools can be toggled with the new `fixComplexGrabbable` config.
* If `minimalLogging` is false, `fixComplexMeshes` will report what Mesh names, GameObject names, and vertex counts it is fixing.
* Added new `fixComplexMeshesGameObjectBlacklist`, so that specific GameObject names can be exempt from `fixComplexMeshes`. This will sometimes be more accurate/unique than `fixComplexMeshesBlacklist`.
* Improved performance of `fixComplexMeshes` slightly.
* Fixed LODs conflicting with model swap mods by adding a re-check of LODs on 'SyncScrapValuesClientRpc'.
* Added a new `vSyncCount` config to the `Rendering` section, for changing what VSyncCount level is used when the game is set to "Use monitor (V-Sync)".

### [1.1.6]

* Improved Texture dupe checking.
* Improved Audio dupe checking.
* Changed De-Dupe to be off by default. I think this is about as accurate as I can make it and there's still going to be some false positives. I still recommend it for anyone who has heavy VRAM/RAM limitations, as it will help with that.

### [1.1.5]

* Improved Texture dupe checking.
* Added `generateLODMeshes`, false by default, to enable generating meshes for LODs. With this change the LOD system will no longer simplify meshes by default and will only handle reducing lighting quality and culling at distance. This should help with load times, as the simpler LOD meshes weren't accomplishing much anyway.
* Added `JetpackItem` to `generateLODsBlacklist` by default.

### [1.1.4]

* Added `generateLODsBlacklist` to disable LOD generation for specific GameObjects. Extension Ladders and Lockpickers are on there by default.
* Added `fixComplexMeshesBlacklist` to disable simplification for specific meshes. Use this if there's an overly complex mesh that you don't want simplified.
* Changed default value of `cullStart` from `0.02` to `0.01`, this should fix pop-in on very small items.

### [1.1.3]

* Mesh Deduping will no longer dedupe collision meshes that are marked as non-readable.

### [1.1.2]

* Audio Deduping now takes into account Length, Frequency, and Channels.
* Texture Deduping will now never dedupe Font Atlases.

### [1.1.1]

* Fixed mesh deduping to also consider `isReadable` when comparing meshes.
* Fixed mesh simplification to preserve `isReadable` when simplifying.
* Added "scavengerplayermodel" to `deDupeTextureBlacklist` defaults.

### [1.1.0]

* Added new Deduping system, to remove duplicate assets from mod loads. Relevant configs are in the new `Dedupe` category. Every mod that uses a base game asset loads another copy of it into memory, and this system goes through and unloads those extras. I'll be revising and expanding this system over the next updates, if there are any issues please report them on the Github or Discord thread.
* Added new Texture Resizing system. Relevant configs are in the new `Textures` category. This will resize textures that are over a certain size and reduce VRAM usage.
* Added new Automatic LOD and Mesh Simplification system. Relevant configs are in the new `Meshes` category. I'll be revising and expanding this system over the next updates, if there are any issues please report them on the Github or Discord thread. Currently the system only works on GrabbableObjects (scrap and tools).
* Added `cameraRenderTransparent` config (true by default), which sets whether the ship/security cameras should render transparent objects.
* Added `compensationMoonBlacklist` config (empty by default), which is a list of moons where `volumetricCompensation` will be disabled while outside. Use this for modded moons that become too foggy with `volumetricCompensation`.
* Changed `minimalLogging` config to default to true.
* Changed `lightVolumetricDistCap` config to default to 250f instead of 150f, it was a bit overtuned for default.

### [1.0.9]

* Updated Config values to allow setting all values back up to LC defaults.
* If a value is set to max/LC default, then it will not be changed by Sponge. This should allow better compat with other mods that effect these values.
* Removed a leftover instance of Camera.render.
* Fixed ship cameras not updating properly while spectating and using `patchCameraScript`.

### [1.0.8]

* Fixed lights with 0 base range having their fade distance set to 0 with `changeLightFadeDistance` enabled. Now there is a minimum fade distance of 5m.

### [1.0.7]

* Made the WIP shader the new default replacement. You can access the previous shader with `useLegacyCustomShader`, if you preferred it. For people who were using the WIP shader, make sure to re-enable `useCustomShader`.
* Cleaned up the camera-finding methods.
* Fixed a typo in `reflectionAtlasSize` description.

### [1.0.6]

* Many more changes to the WIP Shader, text on the monitor should be more readable. This is approaching "good enough" level for me now, and if there aren't any major issues before next update it will likely be graduating from WIP.
* Added a new initial call to UnloadUnusedAssets upon reaching the main menu if `unloadUnused` is enabled.
* Fixed the security camera monitor by the ship door not updating if the other monitors were not in view.

### [1.0.5]

* Added `volumetricCompensation` config in Rendering (on by default) that will modify light intensity and fog thickness to account for the changes in the custom shaders. This should match vanilla's look more closely, especially in regards to fog density.
* Many changes to the WIP Shader, mainly fixing the color format and HSL calculations to fix artifacting.
* Fixed `minimalLogging` doing the opposite of what it said. (adding logging when true, removing when false)
* Fixed `useWIPCustomShader` requiring `useCustomShader`.

### [1.0.4]

* Added a new WIP shader (off by default, set using `useWIPCustomShader`) with more of that 'lethal company charm'. It's still not there yet, but this should be a little closer. It's disabled by default while I work out the visual stylings.
* Added `minimalLogging` config under Investigation. This will disable the readout of how many objects were cleaned up/how many objects are loaded total, which may reduce the stutter when cleanup occurs on day rollover.
* Fixed the interior camera using the exterior camera's framerate settings, which could cause it not to work if you had one set to limited framerate and the other not.
* Moved InputActions initialization later, which should hopefully solve issues with mouselook not working when `fixInputActions` was enabled.
* Removed `/sponge shader` chat command when `useCustomShader` is disabled, to avoid confusion.

### [1.0.3]

* Added a new chat command '/sponge shader' that will toggle between base lethal company's shader and sponge's replacement.
* Added more values to `reflectionAtlasSize` (whoops, didn't realize I initially tested with LethalPerformance on and thought their default was LC default.)
* Added config to set the security camera clip plane.
* Added configs to disable the camera quality overrides for the Ship/Security/Map cameras.
* Split the framerate configs for Ship/Security cameras.
* Changed camera fps configs so that if they are set to 0 the cameras will run at full framerate.
* Reworked Lethal Company's ManualCameraRenderer fps limiting to be more performant (Thanks Zagster)
* Fixed a compatibility issue with ModelReplacementAPI

### [1.0.2]

* Reworked custom pass to work better with additional cameras, fixes compatibility issues with Imperium and TooManyEmotes.
* Fixed NRE when `Remove posterization shader` was disabled but `Use custom shader` was not.

### [1.0.1]

* Reworked how the new Posterization shader is implemented to avoid compatibility errors with Diversity.

### [1.0.0]

* Initial Release.
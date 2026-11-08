# v1.6.3

- Actually fixed ReservedItemSlots issues
- Fixed inside ship and inside interior clock not showing up
- Added boolean config to keep health color static

<details><summary> Older versions </summary>

# v1.6.2

- Fixed ReservedItemSlots erroring and CustomHUD stuff not working properly (apperently I shipped the update with old bundle.. oops)
- In the next patch i'll work on some missing hud elements (batteries) and adding more features to custom hud stuff

## v1.6.1

- Added size configs for total value display and scrap value display
- Fixed weight coloring bug

### v1.6.0

- Fixed compact, realtime clock having a specific bug not formatting correctly when its 12 pm
- Added starter color config for the `ScrapValueDisplay`
- Added boolean config to have them all be the starter color or shift to white
- MASSIVE performance update, thanks to [DiFFoZ](https://thunderstore.io/c/lethal-company/p/DiFFoZ/)'s finds
- "[playerNum2] started the ship." finally fixed! Now it should properly say the player's name..
- `ScrapValueDisplay` now gets removed at all times
- [GoodItemScan](https://thunderstore.io/c/lethal-company/p/TestAccount666/GoodItemScan/) compatibility restored even for custom animated scannodes for custom scannodes (i'm taking over coloring tho, im lazy to figure out a good solution for it for now)
- PING!!!! is now working!
- Custom HUD API part was expanded on
  - custom healthbar weight display support, including vertical, horizontal, config-based and even ASCII layouts
  - added battery text option
- Theres probably a few other things i forgor about, i've been sitting on this for a few months and its a pretty big update
- No I couldnt solve the clock positioning, im sorry about that one, will figure it out one day like i finally did with the ping!

### v1.5.2

- Hotfix for TypeLoadException error (GIS compat)
- Hotfix for scannode colors/shapes

### v1.5.1

- Fixed GoodItemScan compatibility
- Fixed `LoadingScreen`, when reset, it'll be vanilla (I might have also reset this config)
- API
  - custom battery now hides vanilla batteries properly
  - custom ui element can now be done properly without doing shader works
  - [IMPORTANT!] Custom scanlines now need to start with `Scanline_`
  - made naming from manifest more reliable (you can have your mod inside another folder, which uh funny cuz thats how I do it too and thats not how I was planning it cuz my dev folder doesnt have that additional folder oh well)
- Coloring now works for `BatteriesOff` (v80 new addition, color is similarly darkened)
- `LethalConfigPatch`: Now theres a proper way to show where the custom ui/hud elements come from
- Clock should be fixed and also a bit more optimized
- Custom shapes and colors per scannodetypes (default, loot, creature), should work well with Dawnlib as well, but if it recolors those scannodes uhghh mmmm eee blame TestAccount idk

### v1.5.0

- v81 UPDATE, DO NOT UPDATE IF YOU WANT TO PLAY WITH THIS MOD ON v73!
- API Conversion
  - Scanlines, Scannodes can be either placed in a certain folder or can be bundled
  - Custom Healthbar, StaminaBar, BatteryBar, InventorySlot needs to be bundled
- Fixed risklevel being white when you disable the boolean config, now it uses header text color
- Fixed transmitted signal message silently failing and not showing up for clients
- Added Custom battery option but didnt include a custom one this time
- Added coloring option for battery in the health/stamina/weight configs, the CustomBatteryColor in custom ui configs is different, thats meant to be used for shader stuff, same way as the other custom ui colors
- Made sure inventoryslot fading frame also takes on the same coloring logic for all types
- Removed animated pingpong coloring for the seed on the loadingscreen as I like the new one better for the top text
- Colored all with the same config, you dont need to worry its done with taste :P

### v1.4.6

- hotfix... i didnt include the assetbundle .....

### v1.4.5

- After one major merge conflict, I'm finally back with a major update - tho this shouldve been 1.4.2... then 1.4.3.. but now its actually a big update so here goes nothin
- Health Coloring changes (numbers, colors but essentially the same thing)
- Tried to do something to the ping again like 2 months ago and failed miserably ;-;
- Custom UI elements
  - Custom Healthbar, some have fixed positions for the hp text
  - Custom Staminabar
  - Custom InventoryFrames
  - All of these have secondary coloring for extra features, visually not all are finished, but I gotta post an update now so look forward for some upgraded visuals in the future
  - These are right now all made by me BUT I want LethalHUD to act like an API so others could make their own custom hud elements
- Bootstrap config got the network disabler option so you can play with the mod in public lobbies or whereever
- Change the bundle name again, hi Mu
- Fixed tiny issues in some utility codes that have not been an issue before but while working on custom ui it became obvious
- Restructured a bunch of stuff
- I think I worked on some optimization stuff but dont stone me if thats a lie ionno man Im reading thru TWO MERGE CONFLICTS cuz as I was doing my readme and this changelog I just had a second one lmao
- Readme update cuz it was ancient, looked like something written by gipiti (which wasnt but I used emojis cuz smol brain thought its a good idea for readability..)
- Dont be fulled the readme is ahh, I gotta redo it but theres some info there I havent posted in this changelog, now go have fun and let me know what you guys think of these things ^^

### v1.4.1

- Added Player Icon coloring config (thanks SonicX8000 for coming up with the idea)
  - Vanilla
  - ColorFilled - which is pretty much how the hp is done (also reworked) 100: green (or custom color) which goes yellow orange bright red darkred or overheal is done with two light blue
  - RedFillUp - While ColorFilled was filled to slowly unfilling, this one fills up with bright red to dark red as a player gets lower and lower on hp
- HealthColor default color slightly tweaked, and the coloring is changed as ColorFilled player icon works
- Fixed an issue (well two in one) with ScrapDisplay when you rejoined a lobby the numbers started shifting weirdly and on hotbarplus slots they didnt show up at all
- Fixed an issue with the Total Loot info (the replacer config) where previous scanned number lingered around, also changed the speed of the counter (base game uses 1500 and I went with that but for some reason its a lot faster for me so we are going with 500 for now)


### v1.4.0

- Added different billboard mode (static, wave, pulse) and layout (vertical, horizontal, diagonal) and networked them properly thanks to pacoito's previous help and tip
- Finally added LootInfoManager:
  - Shiploot
  - Replace total scanned amount with the visuals of shiploot
  - config option to change the display time of these two
  - animcurve for them to fade in and out nicely and not just pop in and out of existence
- Added a billboard color config so you can still have your chatnames color but not your billboard
- Added a basic fallbackcolor which is the original color for planet info stuff (hopefully this is good lol I did this like a year ago, I dont even remember doing it)

- In the next update I want to declutter the screen a bit more by touching up the global notification
- Fixing an issue with rejoining the lobby with hotbarplus (on the additional bars the scrapvaluedisplays dont show up)

### v1.3.9

- Manul
- Idk
- Chu
- Web
- (thank Jacob, Kenji and Generic for this amazing update)

### v1.3.8

- Added coloring to nameplates (with a new config option in the misc section)
  - name coloring is hooked up with that chatname gradients and the chatnamecolor boolean config
- Fixed up an incompat issue with TME


### v1.3.7

- Merged Pacoito's PR [#9](https://github.com/s1ckboii/LethalHUD/pull/9/)
  - Fixes chatnamecoloring
  - Replaced reflections with publicized calls (for compats)
- using sharedMaterial instead of material for scan as it was gullible in the last rework with lazy init and is better overall

### v1.3.6

- Fixed performance issues (garbage collection / allocation / frame drops) thanks to Pacoito
- Networked name colors
- Probably fixed some issues but some were not really reproducable or at least I couldnt figure out a way so uhm.. if you encounter the previous issues again, let me know
- I know this update doesnt seem much but I did a lot and I might sound lazy, and thats because I am .-.

### v1.3.5

- Fixed small mishaps with compact/default and 24 hour clock messing up it's layout AND fixed the layout for 'murican (am/pm version) being messed up with compact clock

### v1.3.4

- Updated BepInEx dependency
- Removed Halloween related stuff (for now, I'll probably make a proper event override system for each season and then you can just enable it and perhaps change it via a dropdown, would probably rework quite a lot of features if I do do this)
- Signal Transmitter related stuff can be colored in a better way, all have the proper default colors
- `SpectartorTip` colors (4 new config options)
- Proper default colors for `PlanetSummaryColor`, `PlanetHeaderColor`, `LoadingTextColor`
- Compact layout for clock is centered better (idk what I was thinking moving them apart)
- `PlayerHPDisplay` will not shake for everyone when one person gets damaged
- Made sure to handle chatmessages better, meaning if another mod, like TooManyEmotes resizes, re-aligns, etc. the chat messages they send you wont see stuff like "<size=0>" or "<color=purple>", etc.

- I'm lazy to update my readme, so thats another promise for the future...
- Here are some plans for the future:

| Planned Feature | Description | Priority |
|----------------|---------------------|-----------|
| DawnLib loading screen thingy recoloring | Recoloring for the DawnLib loading screen thingy. | Low |
| Event-based system | You can modify and add your own stuff — I'm thinking of a JSON where you can provide a start - end date and then fill out whatever config option you'd want to override for that time. | Mid |
| Networked features | Finally getting into networked features (for example pingcounter that's currently not doing anything lol, chatnames appear in your selected colors but you also see someone else's name in their selected colors). | Mid-to-High |
| Dead screen / spectator HUD | Some deadscreen / spectator related hud/ui stuff. | Mid |
| `LootInfoManager` | Finish my `LootInfoManager` (basically ShipLoot but I want to make it so the basic counter outside the ship — the total value where it counts up the scrap — is also using that, as I highly dislike the basic one; it takes up too much of the screen and I don’t really like the design). | High |
| Global notifications | I want to change the GlobalNotifications, so warnings and errors look better — same reason as above. | Mid |
| In-game UI config menu | Potentially my own little UI menu which you can enable ingame and change the config options better. It’d be more user-friendly and easier to navigate. Going to the escape menu → configs → search lethalhud → search idk fps x y values for hud positions and then change those numbers around without properly seeing them is kinda meh.. | Low-to-Mid (could move up) |
| Loading screen custom images | Loading screen’s darkened screen gives me the possibility to add my own images to it, so I was thinking I could add some custom ones. This would also work nicely with the event system I was talking about earlier — imagine like Christmas where you have 3–4 images that could randomly appear on load to give it that extra lil immersion. This would be toggleable, obviously, and done in a nice aesthetic way. | Mid (depends on event system prio) |
| More fixes | There are probably some small issues still with the current version, I'm not aware of (well I know some but I either have no clue how to fix or haven't had time, these are mostly small hiccups) | High |
| Better implementations | There are some features or fixes that were done somewhat poorly (most of them are not affecting players), I want to improve them. | High |

<small> These plans are not like a "whats coming next week" type of plans, I'm pretty busy with life and my own game prototype (yes I'm finally calling it that lol) so don't get too hyped up about any of it, I'll get to them eventually but I might also drop (unlikely) or add (very likely) to the list </small>

<small><small> To friends: sorry to disappear, bit stressed about irl stuff, had to focus on those things, but I'm doing fine, will be back very soon ^^ </small></small>

### v1.3.3

- Halloween mode turn off by default
- Hopefully fixed warning spam when halloween mode is turned off (you can also just delete the halloween json and it will regenerate which fixes it)
- Thanks to MikuArt an issue was found where you would have fps drop, mrov helped me find the cause, now it should be fixed!
- HideHUD element was spamming errors when you set it to None, this is fixed
- This should also fix when sometimes it didnt actually hide the hudelements
- There are some things I'd still need to fix here and there but I didnt have much time to do so, let me know if theres anything thats need tweaking


### v1.3.2

- HOTFIX: A mod moved some hud gameobjects around, so I made a little scuffed fix for it, will clean the code up later- this doesnt affect players though.

### v1.3.1

- Added config option to change HideHUDButton which by default is Numpad5
- Added config option to split additional misc tooltips from controltips coloring (misc are FPS, Ping, Seed counters while control tips are the vanilla tips on top right like inspect xyz item)
- Added a solid color for misc tools if you decide to split them
- Changed boolean(checkmark) option for Hold Scan to an enum (dropdown list) of Scan Mode Type - default, hold, toggle
  - Fixed hold type having small issues with scannodes stucking in the screen and defaulting back to blue when Scannode fadaway was on and GoodItemScan was also present
  - Known issue: toggle type is having the issues I had before with hold type lol, bit of a headache but I'll figure this out, spent a good chunk of my time on figuring this but I know GIS is about to update soon so I might just wait for TestAccount to do a PR :p
- Forced compass to hide if a player is dead
- Added shadow (underlay) to HP text
- Changed some parts of ScrapValueDisplay to use StringBuilder instead
- Added shadow (underlay) to weight counter(s)
- Added two more patches to apply controltip colorapplication (so Zigzag's dynamic tooltips work well with it)
- Changed the way I find ScanVolume and how I handle it (this from my tests fixed when you inspected an item and it was blurry, also might've fixed my incompat with HDLCPatch lol - only tested this once)
- Improved SignalTransmission coloring and put the cap up to 12 as thats vanilla (idk why I thought 10 was the cap, did that change??)
- Improved PlayerInfoDisplay coloring
- I feel like I'm forgetting something, oh well if theres something I changed and isnt on the list take it as a halloween treat, now go trick or treating!
- I'm lazy to do README, will update it in the next patch


### v1.3.0

- "Fixed" Json saving colors (sorry for the inconvenience)
  - You would need to go to `\AppData\LocalLow\ZeekerssRBLX\Lethal Company\LethalHUD` and after HalloweenHUD is created, copy paste what Halloween.json had, I added some in the middle and I've been at this for a while but couldnt come up with a solution
- Reworked WeightCounter
  - Added TwoDecimalsDot, TwoDecimalsComma options
  - Multiple meassurement visibility
  - Changed sizing so they dont take up as much space
  - Halloween version colors differently
- Added back sprintmeterboolean, as I forgot some people are using PlayerHUDTweaks which was the reason why I had it in the first place
- Added halloween version for scrap and total value

### v1.2.9

- I'm lazy to update readme right now, will do next time

- Removed unnecessary SprintMeterBoolean and WeightCounterBoolean configs
- Reworked MiscToolsColors -> now theres a list config to change from Solid to Gradient, and two gradient colors a and b same way as everything else but only for misc stuff
- ControlTips (Tooltips) recolored with the same colors as fps, ping, seed counters
- Halloween mode is on by default now for the month
- Added ChatFadeDelayTime, SlotFade, SlotFadeDelayTime, TerminalFadeDelaysTime
  - ChatFadeDelayTime: you send a message and the chat will stay there for set seconds
  - SlotFade: this is the amount how visible it is
  - SlotFadeDelayTime: same as chat but for inventory
  - TerminalFadeDelaysTime: this is the time when the fade starts same as slot or chat fade delay time but this happens for everything when you enter or close terminal
- Swapped from MIT to GPL-3.0 (so the code will always stay open-source)

- Added more things to the halloween mode, have fun guys!

### v1.2.8

- PlanetInfoDisplay config options
  - PlanetInfoHeaderText color
  - PlanetInfoSummaryText color
  - PlanetRiskLevel custom coloring
- AnimatedLoadingText color config
- All have stuff for halloween mode

### v1.2.7

- Fixed wrong chatname gradients for halloween mode
- Fixed compass showing up while dead

### v1.2.6

- Halloween update! Turn Halloween mode on to override your colors (if you turn it off, your previous colors come back - colors are saved in a json file in the same place your global config is `\AppData\LocalLow\ZeekerssRBLX\Lethal Company\LethalHUD`)
- ChatController was reworked for future plans
  - No more solid color, only gradients as it was redundant (if you want a solid color, change both gradients to the same color)
- Swapped boolean FullHPColor config to hex string so it has its own way of recoloring the hp instead of relying on Main Color (This means in EladsHUD compat you can color border and bar easier)
- SignalTranslator is configurable now
- SprintMeter reworked a little bit
  - Instead of 3 seperate configs, theres a list where you can choose your way of coloring it and then theres only one coloring option to color them
- Changed back from using MonoDetour to Harmony as its currently not needed
- Removed MonoDetour dependency


### v1.2.5

- Fixed missing descriptions on clock configs
- Changed from patching `DestroyItemInSlotAndSync` to `DestroyItemInSlot` - this way it should properly remove the scrapvalues when you interact with stuff

### v1.2.4

- ImprovedClock merge
  - 24HourClock config option
  - ClockFormat config option (Regular or Compact - Compact basically being LCBetterClock, not entirely just the format without the dark first 0 and probably some other stuff dunno)
  - RealtimeClock config option
  - ClockSizeMultiplier config option
  - ClockNumberColor config option
  - ClockBoxColor config option
  - ClockIconColor config option
  - ClockShipLeaveColor config option
  - ShowClockInShip config option
  - ClockVisibilityInShip config option
  - ShowClockInside config option
  - ClockVisibilityInside config option
- Added two patches to fix names not having digits, underscore or dot

### v1.2.3

- HOTFIX: left out a null-check for my HP display (EladHUDs compat stuff or any other HUDmods that would rework UI elements completely)

### v1.2.2

- ***SOME CONFIGS WILL BE RESET UNFORTUNATELY, SORRY I HAD TO REFORMAT SOME THINGS***
- Fixed HP Indicator enable / disable config not changing mid-game
- Fixed ScrapValueDisplay not disappearing when you put items in ShipInventory
- Added Health Format config option (plain, percent, label) - they change in base size to fit in well
- Added Health Size config option
- Added Health Rotation config option (also changed its base rotation)
- Added ChatMessageColor (solid)
- Added Gradient Message Color A and B
- Changed Health Positions X and Y base numbers
- Switched from transpiler to just a postfix for AddChatMessage function, will probably mess with this a few more times in the future, not important for players


### v1.2.1

- Fixed EladHUDProxy not working on reload
- Fixed Sprintmeter sometimes fail to load in with your colors (only affected clients)
- Fixed Weightcounter sometimes fail to load in with your colors (only affected clients)
- Fixed ScrapValueDisplay not resetting when something makes you drop all of your items (numbers were lingering around until you scrolled thru your inventory / picked stuff back up)
- Added SeedDisplay in the Misc section

### v1.2.0

- ***Hopefully*** made it so you can now set the config up as local with another config - you need to restart the game for it change
- Added ChatInputText config option and changed some logic in it
- Added EladsHUD compatibility
  - stamina works with all three config options
  - healthbar works the same way as my text version
  - healthbar background box uses `Main Color`
  - carrytext again uses the same logic as mine, so you can have kgs, manuls, recolor em.. but manuls wont have asciis
- MiscToolsColor config - change color for fps and ping counter (reminder: ping is still not finished, it will only show 0ms)
- Added 2 new scannode shapes, you can now change scannode shapes (they were mostly made for testing purposes, I'd like to make a bit better choices
- **Hopefully** fixed rotation issues with the value displays and made sure the mod works well with hotbarplus
- Reparented total value text
  - Added config options to change its position
  - Added config option to remove delta info (+, - numbers)
  - Added config option to change how the total value is shown (Total Value: number, Total: number, or only the number)

### v1.1.6

- HOTFIX: on spawn these texts empty out

### v1.1.5

- Fixed issues with the Scrap Value Display
- Added total amount counter and a config option to enable it
- Added color logic to the indicators (highest valued scrap = green, lowest = greyish white
- Fixed HandsFull text coloring
- Changed position for the value indicators

### v1.1.4

- ScrapValueDisplay added
  - Config option to turn it on / off
  - Config option to switch it from the blocky credit to dollar sign
- Readme update (still no gifs...)

### v1.1.3

- CompassInvertMask config option to invert the inside part of the compass
- CompassInvertOutsides config option to invert the outside part of the compass
- CompassAlpha config option to make it less visible
  - if you invert outside, inside and set alpha to 0 the whole thing is visible, should be obvious but thought I'd still mention
- FPS and Ping counter reworked (kinda just switched from GUIStyle to just simple TextMeshProUGUI)
- Also reparented it to the ingamehud, because now we have a ***hide hud elements button*** with *numpad5*
- They also have two options in the config now `Horizontal` or `Vertical` and they are seperated by `─────────` or ` | `.
- Still no gifs in readme, I'll get to it someday lol.


### v1.1.2

- Hp reparented to a different gameobject (so it hides when you die, get left behind etc or it fades in the background when you interact with the terminal stuff like that)

### v1.1.1

- HOTFIX: Just noticed that scannodes linger around when you turn away (GoodItemScan patches that part too so I had to do a small adjustment in my code to work well with that)

### v1.1.0

- Added the other `MonoDetour` as dependency as well on thunderstore to avoid confusion
- Added compatibility to `BetterScanVision`, now its light takes on the chosen scancolor from this mod
- Fixed the `GoodItemScan` compat issues (the toggle option is fine now, and the fadeaway also should work nicely when GIS is present)

### v1.0.9

- Fixed a nice chat error for clients
- Added HP Indicator and 4 config options for it
  - Enable / Disable it
  - Recolor it to the inventory slot colors
  - Change X and Y positions
- HP Indicator starts with green color by default and then at 20 it goes bright red, under 20 (critical) its dark red and also shakes whenever you get hit or are under 20 hp (slowly stops as you heal back up)
- Still no gifs in readme, I'll get to it soon I promise

### v1.0.8

- Added ScanNode fadeaway (with two config entries for lifetime and fadeaway) - Compatible with GoodItemScan ^^

### v1.0.7

- Added Sprint and Weight boolean config option to toggle them on or off (basically a compat for PlayerHUDTweaks)
- Added compat for NiceChat (no more errorspam + it also recolors their chatframe)

### v1.0.6

- Sprintmeter configs -> 3 new configs: Solid, Gradient, Shades
  - Solid for just a simple color change
  - Gradient that changes hue value
  - Shades that go into a darker color BUT if its a dark color to begin with, it will go a bit lighter and then red
- [This should be working but in fact does not lol, so uhm ignore this] Modified chat transpiler and now we have slot colored default chat (xyz joined the game will now be colored the same way you set your inventory colors (doesnt work with rainbow stuff))
- Finally added chatframe to the inventoryslot's image array
- Added fps and ping counters - you can enable each seperately, change their position together (FPS is above ping so keep that in mind)
- Removed `Circlegon` scanline
- Changed `Circles` and `Hexagons` scanlines (still not happy  with circles but hexagons looks pretty nice now imo)
- Added `Noisy`, `Scifi`, `Circuits` scanlines (noisy and scifi look really good imo, ciruit looks a bit iffy, but it is what it is)
- Added `MonoDetour` as a dependency
- Added weight selections and starter color - basically the heavier the carryweight gets the color of the weightcounter turns bright red and then dark red

### v1.0.5

- Removed preset config option from chatcolors
- Changed how gradients work, they look nicer imo this way -> you have two config options for gradientA and B (if you reset both of them then the solid color config will be present, which to be fair feels a bit redundant right now but I'll leave it in for now.)
- ChatIndicator when player is typing is now taking inventoryslots color
- HandsFull text now has its own color config
- Chat placeholder, chatcaret are taking on the inventoryslots color and inputtext is using the complimentary color for those so its actually visible what you're typing ^^

### v1.0.4

- Added LethalConfig as a soft dependency
- Added LethalConfig as a dependency on thunderstore (since its basically a necessity for this mod)
- Added GlobalConfig <- if you sync up to someones profile, your setup wont get overridden
- Added Custom color for chatnames (currently its clientsided, in the future I'd like to make it effect only your name and then sync up others' chosen colors to their own name)
- ColoredNames to toggle seeing others chosen colors <- this isnt in effect yet, will be done in the future, LocalNameColor <- currently changes everyones name, NameColorMode - same as inventory slots you can choose a preset for custom colors

### v1.0.3

- Fixed alpha not changing when fadeout config is not toggled (oopsie)
- Fixed default invenotry slots color (oopsie, I based it off scancolor but its much less vibrant blue)
- Changed main color settingchanged behaviour -> if you reset it's color, now it actually resets both ScanColor and SlotColor
- Added rainbow slot color list config option (none <- default, rainbow, winter, summer, vaporwave, deepmint, radioactive, tideember)
- Removed randomcolor (might come back later idk)
- Added two strings as config options if you'd want to do custom wavy gradient for your inventory slots
- Compass recolor tied to slotcolor - works with custom wavy gradient, or any of the rainbow frames

### v1.0.2

- InventorySlots recoloring
- "Main Color" global config option - you can override this with ScanColor and SlotColor as well but unify them back together if you'd like to
- Modified base intensity for custom scanlines because they were too invisible (you can do -58 for the config option to go back down to 42)

### v1.0.1

- Added more info in README
- Fixed a small issue with apperantly Emblem naming their assetbundle the same way so I just renamed it with a very original name
</details>
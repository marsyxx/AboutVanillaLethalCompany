# LethalHUD

[v81 update]

Built around [LethalConfig](https://github.com/AinaVT/LethalConfig), with support for runtime changes — **everything can be changed mid-game**.
Config is fully local, meaning if you reimport someones profile your config won't be overridden.
You can change it from local to global in your modmanager there is going to be another config BUT you will need to restart the game to switch around.

Request features in the modding Discord - if it’s possible, I’ll probably add it.

---

# General info

- 2 configs: bootstrap.cfg and lethalhud.cfg
  - bootstrap is for changing configuration from local to global
    - local: for modpack creators, sharing your configuration, location of the file = default place like every otherr mods
    - global: for players who dont want to use others' configurations but their own without it being overridden everytime, location of the file = `\AppData\LocalLow\ZeekerssRBLX\Lethal Company\LethalHUD` (the only thing you gotta make sure is that in bootstrap.cfg you have this selected when you import someone's modpack)
    - for LINUX users it'd be something like `~/.local/share/Steam/steamapps/compatdata/1966720/pfx/drive_c/users/steamuser/AppData/LocalLow/ZeekerssRBLX/Lethal Company/LethalHUD/` or `~/.steam/steam/steamapps/compatdata/1966720/pfx/drive_c/users/steamuser/AppData/LocalLow/ZeekerssRBLX/Lethal Company/LethalHUD/`
  - networking disabling config: now you can play in public lobbies again, sorry for the delay..
- lethalhud.cfg
  - bunch of different configurations for:
    - Main Color (this is like a centralized color config for inventory slots, scanlines which should honestly control everything but I forgor about it)
    - Scan (ScanMode, FadeOut, RecolorScanLines, Scanline style, Scanline intensity, ScanColor, Alpha, Vignette Intensity, ScrapCounterVisual, ShipLoot and display time, lootcolor, loot x and y pos)
    - ScanNodes (FadeAway, Lifetime and FadeDuration, Shape)
    - Inventory (InventoryFade and its delay, FrameColor, FrameStyles, GradientColorA and B [honestly I dont really need FrameColor anymore but whatever..], HandFullColor, ShowItemValue, ItemValueColor, ItemValueGradient, ShowTotalInventoryValue, ShowTotalDelta, TotalPrefix, ChangeCurrency, TotalValueOffset X and Y Pos)
    - Chat (Fade delay, Colored Names config, GradientNameColorA and B, ChatInputTextColor, ChatMessageColor, GradientMessageColorA and B)
    - Health/Stamina/Weight (HealthIndicator, HealthFormat, HealthSize, HealthRotation, HealthColor, Indicator X and Y Pos, SprintMeterColoring, SprintMeterColor, SprintMeterStyle, WeightUnity, WeightDecimalFormat, WeightUnitDisplay, WeightColor, BatteryColor)
    - Compass (CompassInvertMask, CompassInvertOutsides, Alpha)
    - Clock (24Hour, ClockFormat, RealtimeClock, ClockSizeMultiplier, ClockNumber/Box/Icon/ShipLeave Colors, ShowClockInShip/Inside, VisibilityInShip/Inside)
    - SignalTranslator (CenterTransmitterText, SignalMessage/BackgroundMessage/Background/MessageBackground color, LetterDisplay [speed])
    - MoreDisplay (LoadingText/PlanetSummary/PlanetHeader colors, Planet Risk custom coloring)
    - Spectator (TipText/SpectatingPlayerText/HoldToEndGameText/HoldToEndGameVotesText colors)
    - Misc (Billboard GradientColor, Billboard Layout, Billboard GradientMode, PlayerIcon, TerminalFadeDelay, FPS/PING/SEED counters and their LayoutOptions and their X and Y positions, Control Tip ColorMode, Seperate Additional MiscTool Colors, Additional MiscTools Color, Misc Tools Gradient Color A and B)
    - Keybinds (HideHUDButton)
  - Custom UI
    - HealthBar, Stamina, InventoryFrames, Battery and all have a secondary coloring for additional stuff thats handled in shaders by `_FlowColor`
- Compaitibility
  - BetterScanVision
  - GoodItemScan
  - LethalConfig
  - HotbarPlus
  - NiceChat
  - EladsHUD (will drop it later probably)

So now that you read thru it, you can probably understand why Im way too lazy to make a proper readme with updated content stuff. Thinking of gifs, pictures whatever but theres a lot of content here.. so yeah lazyboi

Anyways here are the future plans for LethalHUD:
- Beltbag configurations, custom ui
- Compass custom ui?
- Custom UI changes
  - improve visuals for some
  - fixing tiny bugs
  - rework some of the code so others could make their own (so making lethalhud act as an API essentially for some stuff
- Oxigen custom ui?
- Clock X and Y positions
- GlobalNotifications configurations, custom ui
- ScrapValueDisplay position handled by custom inventoryframes so they wont look weird or out of place

---

# API part

## Editorless

- Scanlines
  - Place .png or .jpg pictures in `plugins/YourMod/Scanlines/` with the picture starting with `Scanline_`
  - The rest should go outside like normal so readme, changelog, manifest, icon and then zip these and your plugins folder together and upload
  - Dont forget to have this mod in your manifest as a dependency
- Scannode
  - Similarly to Scanlines you gotta put your .png (!) in `plugins/YourMod/Scannodes/`
  - Scannodes are made up of two images, an outer and an inner picture and they should be named accordingly
    - `Name_Outer.png`
    - `Name_Inner.png`


## In Editor

- File format: `*.lethalhudbundle`, so `yourbundle.lethalhudbundle`

- You can still have your scanlines and scannodes done the same way naming wise, here you are bundling them in so you dont need to worry about folders beside `plugins/YourMod`
- To make custom uis like healthbar, stamina, battery, inventoryslots you must to add `LethalHUD.dll` in your lethal company unity project
- You will find `LHHealthBarRefs`, `LHStaminaBarRefs`, `LHBatteryRefs`, `LHSlotRefs` as component scripts
- You should go to SampleSceneRelay to place them in the scene in 2D view to see things how they would be
- Components do not require you to fill out everything. If you dont have a secondary image for border, you can leave that be empty for example
- HUD Elements support some shader properties such as `_FillAmount`, `_OverhealAmount`, `_OverhealColor`, `_FlowColor` <- these are what you are meant to target in your own custom shader if you decide to make one yourself
- While making custom inventory slots, you want to use `hasItem` as string for trigger parameter
- Inventory slots also have an option to randomly rotate each differently, its not a constant rotation dont misunderstand

## Shader example snippets

> UI Color support so the shader uses `image.color`

```cs
struct appdata
{
float4 vertex : POSITION;
float2 uv     : TEXCOORD0;
float4 color  : COLOR;   // required
};

struct v2f
{
float4 vertex : SV_POSITION;
float2 uv     : TEXCOORD0;
fixed4 color  : COLOR;   // required
};

v2f vert(appdata v)
{
v2f o;
o.vertex = UnityObjectToClipPos(v.vertex);
o.uv = v.uv;
o.color = v.color;       // must pass through
return o;
}
```
> Fragment aka applying colors to the ui, inside `fixed4 frag`

```cs
fixed4 tex = tex2D(_MainTex, i.uv);
tex.rgb *= i.color.rgb;
tex.a *= i.color.a;
```

> Health fill with overheal still inside frag

```cs
float hpMask = step(i.uv.x, _FillAmount);
float ohMask = step(i.uv.x, _OverhealAmount);

fixed3 finalRGB = lerp(i.color.rgb, _OverhealColor.rgb, ohMask);

float visibility = max(hpMask, ohMask);
float finalAlpha = visibility * tex.a * i.color.a;

clip(finalAlpha - 0.001);

return fixed4(finalRGB, finalAlpha);
```

# Simple fills used for stamina, battery

```cs
float mask = step(i.uv.x, _FillAmount);
clip(mask - 0.001);

return tex;
```

---

## Credits
- [Niro](https://github.com/NiroDev) for creating ScanRecolor per my request and letting me continue it
- [Xu Xiolan](https://github.com/XuuXiaolan) for helping out with inventoryslots and with GoodItemScan compatibility
- [Zehs](https://solo.to/crithaxxog) for helping out with global configs
- [mrov](https://github.com/AndreyMrovol) for giving me some basic informations about transpilers, and help testing
- [Hamunii](https://github.com/Hamunii) for MonoDetour and giving me some pointers on how to use it
- [Pacoito](https://github.com/pacoito123) for helping figuring some performance issues out and fixing chatname color sync issue
- [Jacob](https://github.com/JacobG55) for "Idk" scannode and "Chu" scanlines
- [Kenji](https://thunderstore.io/c/lethal-company/p/rectorado/) for "Bol" scanlines with Generic and "Web" scannode, and help testing
- [Generic](https://thunderstore.io/c/lethal-company/p/Generic_GMD/) for "Bol" scanlines with Kenji
- [Wesley](https://thunderstore.io/c/lethal-company/p/Magic_Wesley/) cuz he has helped me so many times and I kept forgetting to put his name in here
- [DiFFoZ](https://thunderstore.io/c/lethal-company/p/DiFFoZ/) for finding some issues and pointing them out for me

---

## Support
If you’d like to support me or just want to see upcoming mods for **Lethal Company** or **R.E.P.O**:

[![ko-fi](https://trello.com/1/cards/685cd0fe669dd9a14cc85885/attachments/685cd1112939c3caf675d6f4/download/kofisupport.png)](https://ko-fi.com/s1ckboy)
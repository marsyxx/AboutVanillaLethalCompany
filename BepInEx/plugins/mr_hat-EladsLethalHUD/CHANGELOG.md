## 0.0.8
- Fixed the stamina bar not fully draining after another player joins the lobby

## 0.0.7
- Fixed battery icons overlapping when using hotkeys to turn on pocketed battery items

## 0.0.6
- Fixed battery not showing as non-host player

## 0.0.5
- Changed `Auto hide health` to reflect similar behaviour of other HUD elements to fix the health bar not appearing when `Health hide delay` + `Auto fade opacity` are both set to `0`
- Changed `HUD Scale` minimum from `0.5` to `0.1`
- Fixed weight appearing on HUD when first loading lobby when `Auto hide stamina` is enabled and `Stamina hide delay` + `Auto fade opacity` are both set to `0`
- Added `Minimum percentage until popup` config for stamina bar

## 0.0.4
- Fixed utility slot HUD animation playing on all clients if anyone picked up an item that went into the utility slot
- Removed some annoying logs

## 0.0.3
- Fixed a timing issue related to the battery that would cause it to not be visible
- Fixed an item frame from vanilla remaining on the utility slot

## 0.0.2
- Added [LethalConfig](https://old.thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/) as a dependency
- Added an `Oxygen Bar`, this functions like [OxygenDisplay](https://old.thunderstore.io/c/lethal-company/p/Dustin/OxygenDisplay/) but as a built in feature
	- Compatible with [Oxygen](https://old.thunderstore.io/c/lethal-company/p/consequential/Oxygen/)
	- Added the following settings from [NicheTweaks](https://old.thunderstore.io/c/lethal-company/p/mr_hat/NicheTweaks/):
		- `Autohide Oxygen Bar`
		- `Minimum percentage until popup`
- Added `HUD Scale` config and a bunch of other config options
- Stylised the voice activity icon and utility slot
- Added some animation
- A lot of other minor things

## 0.0.1
- Release
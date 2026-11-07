# EladsHUD+

[Elad's HUD](https://thunderstore.io/c/lethal-company/p/EladNLG/EladsHUD/), updated for `V81` and expanded. The clean health / stamina / battery HUD you know, plus the `utility slot`, a `mic icon`, a `status row` for buffs, injuries and warnings, an optional `crosshair`, `extra health` on the health bar, and battery support for AdvancedCompany's head items.

Everything is client side and can be turned off in the config.

> **Made to go with [TZPPlus](https://thunderstore.io/c/lethal-company/p/SleepyMiles/TZPPlus/), [FrailtyPlus](https://thunderstore.io/c/lethal-company/p/SleepyMiles/FrailtyPlus/), [FrailtyMedical](https://thunderstore.io/c/lethal-company/p/SleepyMiles/FrailtyMedical/) and [Drinkable](https://thunderstore.io/c/lethal-company/p/SleepyMiles/Drinkable/).** Their buffs and debuffs show up as icons under your health bar with the `time left` on each, and you can point at any icon to see what it does. Together they give you one place to see everything that is affecting you.

<img src="https://raw.githubusercontent.com/SleepyMiles/EladsHUDPlus/main/images/eladshud.gif" width="360">

---

## Features

<details>
<summary>🎒 Utility slot</summary>

- The V81 `utility slot` shows on the HUD again (Elad's HUD hid it together with the vanilla HUD).
- Sits at the right end of the health bar, without the `[TAB]` text.
- Move it with `OffsetX` / `OffsetY`, show `[TAB]` again with `HideKeyText`, or put it back above the HUD with `NextToHealthBar`.

</details>

<details>
<summary>🎙️ Mic icon</summary>

- An orange mic at the end of the stamina bar `while you talk`.
  - Push to talk: while the talk key is held (or you talk into a walkie-talkie).
  - Voice activation: while the game actually hears you. Vanilla never shows an icon in this mode.
- A red crossed-out mic while your `mic is turned off` in the game settings.
- 3 styles: `Solid`, `TopGap` and `FullGap` (outline with a gap inside, like the battery icon).
- Replaces the vanilla push-to-talk icon, so it never shows twice.

</details>

<details>
<summary>🩹 Status row</summary>

A row of icons under the health bar, with the `time left` or `value` on each:

- **Frailty** injuries (broken / fractured legs and arms, bleeding, laceration, blinded, concussed, paralyzed, and FrailtyPlus' Infected / Blooming / Paranoid with their `%`), if Frailty is installed (made with **FrailtyPlus** in mind). Frailty's own icons are hidden while this is on. The tooltips name FrailtyMedical's items when it's installed.
- **UsualScrap**'s Productivity Auto-Injector buff.
- **TZPPlus** buffs and debuffs (Damage Reduction, Lethal Dose, Withdrawal and the vanilla TZP effects), and icons from any other mod that uses the EladsHUD+ status API.
- The **vanilla warnings** (steam leak, Cadaver spores / fever, low oxygen) as text under the row. Elad's HUD used to hide them.
- Every icon has a moving frame (its dashes change a few times a second; setting `AnimatedFrames`). Optional: the health bar's four corners move with them (`AnimatedHealthFrame`, off by default).

</details>

<details>
<summary>❤️ Extra health</summary>

- Extra health from other mods (FrailtyMedical's **Racing Heart** after an Epinephrine shot) shows as a `green` part of the health bar, and the number shows health + extra.
- Damage takes the extra first, so the green shrinks first. The bar stays visible while it changes.
- `ExtraHealthBar`: `SameLength` (the bar keeps its length, green right after your health), `LastPart` (green is always the last part of the bar) or `Extend` (the bar grows past its end; the utility slot moves along).
- `ExtraHealthNumber`: `Total` (e.g. `130`) or `HealthPlusExtra` (e.g. `80 +50`).

</details>

<details>
<summary>➕ Crosshair</summary>

- The game has no crosshair: turn one on with `Enabled` (off by default).
- Always white and faint by default (`Opacity` 0.55) so it blends in, with a thin dark outline.
- Pick a frame (`Full`, `Corners`, `None`) and a mark (`OpenPlus`, `OpenPlusDot`, `Plus`, `Dot`, `None`), its `Size` and `Thickness`.
- Show it `Always`, only while `HoldingItem`, or only while `HoldingWeapon` (shovel, stop sign, knife, shotgun...).
- Hides while the game's hand icon shows (`HideOnInteract`), and when you're dead, at the terminal, in a menu, typing, in build mode or in cursor mode.
- Every change applies right away (e.g. with LethalConfig).

</details>

<details>
<summary>🖱️ Cursor mode</summary>

- Press `middle mouse` to show the cursor. The camera stops turning while it is shown.
- Point at an icon in the status row to see `what it does` (in plain words, no exact numbers).
- Doesn't open while a menu, the terminal or the chat is open.
- Change the key, or make it hold-to-show, in the `General` section.

</details>

<details>
<summary>🔦 Batteries</summary>

- **AdvancedCompany head items** (Helmet Lamp, Tactical Helmet, Headset, Vision Enhancer) show their battery with a lamp icon:
  - alone it uses the normal battery row; with a battery item or TZP in your hand it moves to a small row above.
  - the fill drains with the charge and shows light rays while it is on.
  - Tactical Helmet: shows the light-off time; with the light on it counts down faster and the numbers fade to red.
- `CompactBatteryUI`: held item, head item and flashlight all in one row.
- The pocketed flashlight icon has a small gap inside its outline, like the battery icon.

</details>

<details>
<summary>🎨 Small touches</summary>

- The stamina bar fades to `cyan` while UsualScrap's Productivity Auto-Injector is active (still red when stamina is low).
- Optional dark `outline` around the HUD numbers, the same way the vanilla tooltips are outlined (`OutlineNumbers`, off by default).
- The stamina `/sec` number no longer flickers between two values (e.g. 4.2 / 4.3).

</details>

<details>
<summary>🔧 Fixes</summary>

- A modded battery item without a battery no longer throws errors every frame.
- An error inside the HUD no longer stops the rest of the player's update (stamina etc.). It is logged once instead.

</details>

---

## Configuration

`BepInEx/config/me.eladnlg.customhud.cfg` (the same file as Elad's HUD, so your old settings carry over), or in-game with **LethalConfig**.

| Section | What |
|---|---|
| General | HUD scale, planet info, outline the HUD numbers, cursor key and hold to show |
| Health and Stamina | auto-hide health bar, how extra health shows, animated health bar corners, stamina text, injector stamina color |
| Status icons | warnings, Frailty injuries, buffs, colored buff numbers, animated frames, icons per row, position |
| Battery | flashlight battery, time left, head item battery, show while off, flashlight icon gap, compact battery UI |
| Utility slot | next to the health bar, hide `[TAB]`, position |
| Mic | show mic icon, show muted icon, icon style, position, size |
| Crosshair | on / off, frame and mark style, size, thickness, opacity, outline, when it shows, hide on interact |

---

## Compatibility

- **Replaces Elad's HUD and FixEladsHUD.** Don't install them together. EladsHUD+ uses the same plugin ID, so mods made for Elad's HUD still find it.
- **[TZPPlus](https://thunderstore.io/c/lethal-company/p/SleepyMiles/TZPPlus/)**: made to work together. Its buff / debuff icons go in the status row with time left and tooltips, and the bars change color with its effects.
- **Frailty / [FrailtyPlus](https://thunderstore.io/c/lethal-company/p/SleepyMiles/FrailtyPlus/)**: made to work together. The injuries show in the status row with time left and tooltips.
- **[FrailtyMedical](https://thunderstore.io/c/lethal-company/p/SleepyMiles/FrailtyMedical/)**: its buffs (Bandaged, Painless, Borrowed Time, Racing Heart, Pack Mule, ...) show in the status row with time left and tooltips, its buffs color the bars, and Racing Heart's extra health shows on the health bar.
- **[Drinkable](https://thunderstore.io/c/lethal-company/p/SleepyMiles/Drinkable/)**: every drink buff and debuff shows in the status row with time left and tooltips.
- **UsualScrap**: Productivity Auto-Injector icon and cyan stamina bar.
- **AdvancedCompany**: head item batteries. The Battery Pack time left and the real stamina `/sec` (with perks) come from the SleepyMiles v.81 version of AdvancedCompany.
- **LethalConfig**: change every setting in-game.

## Play them together

These mods are made by **SleepyMiles** to work as one set. They share one row of buff / debuff icons (or EladsHUD+'s status row) and the **DRINKS** / **MEDICAL** store sections, and they know about each other's effects. Each one also works on its own (FrailtyMedical needs FrailtyPlus).

| Mod | What it adds |
|---|---|
| [TZPPlus](https://thunderstore.io/c/lethal-company/p/SleepyMiles/TZPPlus/) | The TZP heals you, shows its gas, builds up damage reduction (fall damage too). Too much gives a Lethal Dose. |
| [FrailtyPlus](https://thunderstore.io/c/lethal-company/p/SleepyMiles/FrailtyPlus/) | Injuries: broken / fractured legs and arms, bleeding, concussions, Paralyze. A rebuilt Frailty. |
| [FrailtyMedical](https://thunderstore.io/c/lethal-company/p/SleepyMiles/FrailtyMedical/) | *Beta.* Bandage, First Aid Kit, Morphine (revive a teammate), Epinephrine, Antifungal, Steroids, Painkillers, Antibiotics and Sedatives to treat them. |
| [Drinkable](https://thunderstore.io/c/lethal-company/p/SleepyMiles/Drinkable/) | Drink the Red soda, 19 new drinks with their own buffs, the Soda Machine and the Drink Machine. |
| **EladsHUD+** (this mod) | Elad's HUD for V81, with a status row that shows every buff, debuff and injury from all of these with the `time left`. |

---

## Credits

- [Elad's HUD](https://thunderstore.io/c/lethal-company/p/EladNLG/EladsHUD/) by **EladNLG**, the original HUD this is built on (GPL-3.0).
- **xibao_love**'s FixEladsHUD, for the original V81 utility slot fix.
- `AssetUtils` from the **JotunnLib Team** (MIT).

## License

GPL-3.0, the same as Elad's HUD. See `LICENSE`.

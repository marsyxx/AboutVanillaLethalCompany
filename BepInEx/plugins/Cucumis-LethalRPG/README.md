# LethalRPG

<p align="center"><img src="https://raw.githubusercontent.com/feedback4cucu/LethalRPG/cc60ab94c2fdfe14afab333420600cc64f803961/media/k-menu-buy.gif" width="720" alt="The skill menu (K) in orbit: Stamina is bought twice, then to its maximum, and the budget drops"></p>

Personal progression for Lethal Company: buy upgrades with your share of the quota surplus and learn a skill tree of 31
skills with XP you keep.

> **Status:** tested in automated LAN games on one PC, with up to 8 players in orbit and 2 on moons, and with 10 mod
> combinations (see "Compatibility"). **Not tested yet: Steam lobbies with real players**; you may run into bugs there.
> Known limits are on the wiki page "Known issues & limitations".

## At a glance

- **21 upgrades** such as stamina, health, item slots and speed, bought with budget: your share of the money the crew
  earns above the quota. They last one run; `rpg rebuy` buys your build again after the crew gets fired.
- **31 skills**, 23 unique and 8 ultimate, such as X-Ray Scan, Second Wind and Field Hacker, learned with XP from days,
  scrap, kills and quotas. XP and skills stay in your profile on your own PC and go with you to every lobby and save
  (unless the host keeps them per save).
- **The host's settings are the rules:** personal or team upgrades, how payouts are split, difficulty and balancing,
  and whether XP and skills go with you or belong to the save (see "For hosts").
- **Works alongside LateGameUpgrades:** by default both mods' effects add up; one setting leaves the upgrades both have
  to LateGameUpgrades.
- **Everyone in the lobby needs LethalRPG 0.10.x.** 0.10.x does not work together with 0.9.x or older; please update
  together.

## Quick start

1. Everyone in the lobby installs LethalRPG. The mod manager adds BepInExPack.
2. Complete a quota: part of the surplus becomes your upgrade budget.
3. On the ship, press <kbd>K</kbd> or type `rpg` at the terminal, and buy.
4. XP comes in every day. Learn skills on the UNIQUE and ULTIMATE tabs once you have the required upgrade levels.

New to LethalRPG? The **GUIDE** tab (`rpg guide`) shows your next step and the rules of the ship you are on.

<p align="center"><img src="https://raw.githubusercontent.com/feedback4cucu/LethalRPG/cc60ab94c2fdfe14afab333420600cc64f803961/media/skill-tree-learn.gif" width="720" alt="The skill tree on the UNIQUE tab: Nerves is raised to 3 and Danger Sense is learned; a click frames a skill white, what it unlocks green and what it needs blue"></p>

## Skill menu and terminal

<kbd>K</kbd> opens a menu in the game's look that can do everything the terminal can:

- **BODY, TOOLS, UNIQUE, ULTIMATE:** every upgrade and skill with its level, effect and price. **BUY** buys one level,
  **MAX** as many as you can afford, **LEARN** a skill. Point at a row for its details; pointing at none shows
  **CAN BUY NOW**. A row you cannot afford says how much is missing (`NEED $40`, `NEED 12 XP`).
- **LIST / TREE** on UNIQUE and ULTIMATE: **TREE** shows the skills as a skill tree under the upgrades they need. Click
  a skill for **LEARN**, an upgrade for **BUY** and **MAX**.
- **STATS** (your stats, respec, profile backups), **CREW & BUILD** (your teammates, your saved build, rebuy),
  **RULES** (the rules on this ship, **WRITE BUG REPORT**, **SELF-TEST**) and **GUIDE** (your next step).

Buying, rebuy and respec work on the ship; everything else works anywhere. <kbd>K</kbd> or <kbd>Esc</kbd> closes the
menu.

<p align="center"><img src="https://raw.githubusercontent.com/feedback4cucu/LethalRPG/cc60ab94c2fdfe14afab333420600cc64f803961/media/k-menu-body.png" width="720" alt="The BODY tab of the skill menu with levels bought, the info box and CAN BUY NOW"></p>

<details>
<summary>Keys, menu size and the tree view</summary>

The key can be changed in `5. Interface`, and so can the menu's size (`MenuScale`, 0.75 to 1.5; larger for bigger
text), its scanlines (`Scanlines`) and whether UNIQUE and ULTIMATE open as list or tree (`SkillView`).
<kbd>1</kbd> to <kbd>8</kbd> (also on the numpad) open a tab, <kbd>Q</kbd> and <kbd>E</kbd> the previous and next one,
wrapping around (the tab bar names them as your keyboard labels them). If the menu key is one of these keys, it closes
the menu instead. With
[InputUtils](https://thunderstore.io/c/lethal-company/p/Rune580/LethalCompany_InputUtils/) installed,
set the menu key in the game's keybinds instead (LethalRPG > Skill menu, `MenuKey` is its default); a controller
button can be set there too.

In the TREE view, the upgrades are on top and each skill below with the level it needs. Owned skills are filled,
learnable ones framed, ones you lack XP or money for framed in red with **NEED**, the others dimmed. Click a skill and it
gets a white frame, the skills it unlocks a green one and the skills it needs a blue one (an ultimate needs a unique
skill, which can stand under another upgrade; an upgrade unlocks the skills under it); the legend above the tree shows the
colours. Click it again to let go. The choice holds for both tabs and stays on this PC (`SkillView`). When the host
has `RequirePrerequisites` off, there is no tree and the tabs show the list. Until your first purchase, the menu opens
on the GUIDE the first time in a lobby.

</details>

<p align="center"><img src="https://raw.githubusercontent.com/feedback4cucu/LethalRPG/cc60ab94c2fdfe14afab333420600cc64f803961/media/terminal-buy-max.gif" width="720" alt="The ship's terminal: rpg buy agility max, then rpg tree"></p>

| Terminal | |
|---|---|
| `rpg`, `rpg tools`, `rpg unique`, `rpg ult` | the four pages: body, tools, unique, ultimate |
| `rpg tree` | the skill tree: every skill under the upgrade it needs |
| `rpg sneak` | details of an upgrade or skill |
| `rpg buy sneak`, `rpg buy sneak 3`, `rpg buy sneak max` | buy one, three or all affordable levels |
| `rpg buy` | what you can buy right now, from every page, cheapest first |
| `rpg rebuy` | buy your saved build again |
| `rpg guide` | how LethalRPG works on this ship, and your next step |
| `rpg info` | the rules on this ship |
| `rpg report` | write a bug report |
| `rpg selftest` | compare every player's LethalRPG with the host's (host) |

<details>
<summary>All terminal commands</summary>

```
rpg              page 1: body upgrades, your budget and your XP
rpg tools        page 2: tool upgrades
rpg unique       page 3: unique skills
rpg ult          page 4: ultimate skills
rpg tree         the skill tree: each skill under the upgrade level it needs
rpg sneak        details of an upgrade or skill
rpg buy sneak    buy the next level
rpg buy sneak 3  buy three levels ("max" = up to the highest level)
rpg buy          what you can buy right now, cheapest first
rpg stats        your stats: health, slots, load, sprint time and all upgrades
rpg crew         what your teammates have; from 5 players one line each
rpg crew 3       the player in slot 3 (P3) in full
rpg build        your saved build; "rpg build save" saves your current upgrades
rpg rebuy        buy the missing levels of your build; "rpg rebuy auto" does it after every payout
rpg refund       respec: shows what you get back; "rpg refund confirm" resets everything
rpg profile      your permanent profile and its backups; "rpg profile restore 1" puts backup 1 back
rpg guide        how LethalRPG works on this ship and your next step
rpg info         the rules on this ship
rpg report       writes a bug report without names
rpg selftest     compares every player's LethalRPG with the host's; on a client it checks only this PC
```

`skills` works the same as `rpg`; `rpg start`, `rpg tutorial` and `rpg help` show the guide. The terminal's `other`
page lists `RPG` as well. After a typo the terminal suggests what you may have meant: `rpg buy stamnia 3` answers
`Did you mean: rpg buy stamina 3?`. Nothing is bought until you type the command.

</details>

## On the HUD

<p align="center"><img src="https://raw.githubusercontent.com/feedback4cucu/LethalRPG/cc60ab94c2fdfe14afab333420600cc64f803961/media/skill-box-moon.gif" width="720" alt="Inside the facility: the skill box on the right updates while the player walks and turns (Danger Sense, Exit, Scrap, Second Wind, Rally)"></p>

- **Skill box**, top right from the landing on: the skills that have something to say right now, such as `DANGER` (the
  monster that hunts you), `EXIT` (the walking way to the nearest exit), `SCRAP` (what is left on the moon) and Second
  Wind's charges left today. With `SkillBoxPosition = BelowWeight` (`5. Interface`) it shows below the weight instead.
- **Status line**, below the weight: your health above 100 (`HP 140/200`), the XP of a kill, `SKILL READY` when you can
  learn a new skill, and what Second Wind or Blast Shield saved you from (`SECOND WIND - SAVED FROM GUNSHOTS`).
- **Day report** after each day: XP earned, budget paid out, what saved you, and what you can afford now, in at most
  three rows of the game's tip box (shortened on a busy day).
- **Tips** for players new to LethalRPG, at most one per lobby. `ShowTips = false` (`5. Interface`) turns them and
  `SKILL READY` off for you.

Every row of the skill box is on the wiki page "Skill box and HUD".

## Upgrades

Bought with budget, several levels each: when a quota is completed, 50% of the surplus (sold minus quota) becomes
upgrade budget, by default split by the scrap each player brought to the ship. Every further level costs more (usually
×1.5 per level). Values are the defaults; `rpg <name>` and the menu show the values on your ship.

| Body (`rpg`) | Per level | Max | First level |
|---|---|---|---|
| Stamina | +15% sprint duration | 5 | $50 |
| Strength | −12% penalty from heavy items | 5 | $60 |
| Vitality | +20 maximum health | 5 | $75 |
| Agility | +5% movement speed | 5 | $60 |
| Jump | +6% jump force | 3 | $40 |
| Recovery | +20% faster stamina regeneration | 5 | $50 |
| Climber | +20% faster on ladders | 5 | $30 |
| Lungs | +25% longer breath under water | 4 | $30 |
| Medic | +0.2 health per second after 6 s without damage | 5 | $60 |
| Surefooted | −15% slowdown from mud, water, quicksand and webs | 5 | $40 |
| Crouch Walk | +10% faster while crouching | 5 | $30 |

| Tools (`rpg tools`) | Per level | Max | First level |
|---|---|---|---|
| Inventory | +1 item slot | 4 | $150 |
| Sneak | −12% range at which monsters hear your steps | 5 | $60 |
| Electrician | +25% battery life | 4 | $40 |
| Nerves | −15% fear build-up | 5 | $40 |
| Handyman | +25% faster valves, levers and prying | 4 | $40 |
| Analyst | +25% scanner range | 4 | $40 |
| Brawler | +1 damage with shovels and signs, +0.5 with a kitchen knife | 3 | $100 |
| Marksman | −15% shotgun reload time | 3 | $60 |
| Long Reach | +15% reach for picking up and using things | 4 | $50 |
| Bargain Hunter | −5% on what **you** buy at the terminal (not moon routes) | 5 | $80 |

## Skills

Learned once and kept in your profile (on a ship whose host keeps them per save, they belong to that save). They cost
XP (the host can switch to money) and need a level in a related upgrade; an ultimate also needs a unique skill. XP
comes from your rank at the end of a day, scrap you bring to the ship yourself (2 XP per $100), monsters you kill,
surviving a day and completing a quota. `rpg tree` and the TREE view of the skill menu show how the skills hang
together, one block per upgrade with your level:

```
ANALYST 2/4: 1 Compass* | 2 Prospector!
  3 X-Ray Scan | 4 Hunter Sense (+X-Ray Scan)
```

`*` marks a skill you have, `!` one you can learn now, `(+X-Ray Scan)` the skill an ultimate needs as well.

| Unique (`rpg unique`) | Effect | Requires | Price |
|---|---|---|---|
| Packmule | Two-handed items go into your inventory like normal items | Strength 3 | 40 XP |
| Second Wind | Once per day you survive lethal damage (not monsters that grab you) | Vitality 3 | 50 XP |
| Fast Hands | Switch slots almost instantly, pick items up twice as fast | Inventory 1 | 25 XP |
| Night Owl | Much stronger night vision inside the facility | Nerves 2 | 30 XP |
| Lockpicker | Open locked doors by holding <kbd>E</kbd> | Handyman 2 | 40 XP |
| X-Ray Scan | The scanner shows scrap through walls | Analyst 3 | 40 XP |
| Adrenaline | Below 10 HP: no limping, free sprinting, 20% faster | Recovery 2 | 35 XP |
| Compass | The skill box shows the nearest exit by walking distance, main entrance (`MAIN`) or fire exit (`FIRE`), which way to go, and `UP` or `DOWN` when the way leads to another floor; when a closed security door or a locked door blocks the way, it names the door (`DOOR R4` with its terminal code, `LOCKED DOOR`) | Analyst 1 | 20 XP |
| Iron Grip | Keep your items when a snare flea or kidnapper fox grabs you, and tulip snakes can no longer lift you into the air | Strength 2 | 30 XP |
| Bio Monitor | The skill box shows your cadaver infection, from clean to bursting | Medic 2 | 20 XP |
| Wristwatch | The clock stays visible inside the facility | Electrician 1 | 15 XP |
| Prospector | The skill box shows the scrap left on the moon and its value | Analyst 2 | 25 XP |
| Danger Sense | The skill box warns you when a monster hunts you, and names it | Nerves 3 | 35 XP |
| Concussive Blows | Shovel and sign hits stun the monster for a moment, kitchen knife hits stun half as often | Brawler 2 | 35 XP |
| Quartermaster | In the ship and in orbit, the skill box shows the scrap on board and whether it covers the rest of the quota | Bargain Hunter 2 | 25 XP |
| Trained Eyes | Fog is thinner for you: you see 2.5 times as far in foggy weather, in a moon's own fog (such as Rend's thick fog), in the facility's fog and where the steam of burst pipes hangs as fog | Marksman 2 | 30 XP |
| Gunk Proof | Backwater Gunkfish spit clears off your visor twice as fast, and its slime makes you slip half as much | Surefooted 2 | 25 XP |
| Pest Control | While you spray the weed killer, cadaver plants and Vain Shrouds die twice as fast, its tank lasts twice as long, and an infected teammate you spray heals twice as much per spray | Medic 2 | 30 XP |
| Gentle Collector | Noisy scrap, such as toys that play a sound when picked up, goes off half as often when you take it in hand, and no longer every time you take it out of your pocket | Sneak 2 | 25 XP |
| Soft Landing | Falls hurt you less: you take three quarters of the fall damage, so the highest fall takes 75 instead of 100 health and no longer kills you from full health. A pit that swallows you still kills | Jump 2 | 25 XP |
| Ghost Step | While you crouch, monsters no longer hear your footsteps. Players still hear them, and the noise of items, landings and your voice stays | Crouch Walk 2 | 30 XP |
| Overcharge | While you hold a monster with the Zap Gun, the gun heats up half as fast and the beam's pull grows harder half as fast, so you keep the monster stunned longer. Range, battery use and cooldown stay | Electrician 2 | 30 XP |
| Mechanic | While you drive the Cruiser, it takes half the damage from crashes, bumps and monsters, and the weed killer you spray at its oil pipe repairs twice as much per spray. A crash that destroys it at once still does | Handyman 2 | 30 XP |

| Ultimate (`rpg ult`) | Effect | Requires | Price |
|---|---|---|---|
| Hunter Sense | The scanner also shows monsters through walls | X-Ray Scan, Analyst 4 | 180 XP |
| Lightning Rod | Lightning never targets the metal you carry | Iron Grip, Electrician 4 | 150 XP |
| Blast Shield | Explosions deal 60 damage instead of killing you | Second Wind, Vitality 5 | 200 XP |
| Rallying Presence | Teammates within 15 m regain stamina faster and fear less | Adrenaline, Nerves 4 | 170 XP |
| Appraiser | Scrap you pick up outside the ship is worth 15% more | Packmule, Strength 4 | 200 XP |
| Salvage Insurance | If the whole crew dies, the scrap on the ship is kept | Iron Grip, Inventory 3 | 200 XP |
| Field Hacker | Hold <kbd>E</kbd> to switch off turrets, mines and spike traps, or to open and close security doors | Lockpicker, Handyman 4 | 200 XP |
| Coil Breaker | Hold <kbd>E</kbd> to shut a coil head down for 30 s | Concussive Blows, Brawler 3 | 180 XP |

## For hosts

<p align="center"><img src="https://raw.githubusercontent.com/feedback4cucu/LethalRPG/cc60ab94c2fdfe14afab333420600cc64f803961/media/rules-tab.png" width="560" alt="The RULES tab after a self-test: both players OK"></p>

Your settings are the rules for everyone in the lobby. They are in `BepInEx/config/cucumis.lethalrpg.cfg` or, with
[LethalConfig](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/), in the mod menu, and changes apply at
once. `rpg info` and the RULES tab show the rules on every PC.

- `RulesPreset` in `0. General` sets how upgrades are paid and who owns them in one line (also for a modpack):
  `Classic`, `CrewOwnUpgrades`, `CrewCredits`, `Team`, `Solo` or `LguCompanion`; `Custom` (the default) keeps the
  single settings.
- `Difficulty` and `Balancing` set how hard progress is: `Balancing = Generous` gives up to three times the levels,
  `Difficulty = Hard` with `Balancing = Strict` makes progress slow and expensive.
- `ProgressScope = Save` (from 0.10.0) makes XP and skills belong to your save, so every run starts from the same
  place; the players' own profiles stay untouched.
- `RecordRunJournal = true` (off by default) writes a journal of your runs on your PC, with players only as P1, P2, …,
  and sends nothing anywhere; players without a compatible LethalRPG are not told, so let them know yourself.

The wiki pages "Host guide", "Settings" and "Budget, prices and team mode" explain every preset and setting, switching
during a run, team mode and continuing a run with another host.

<details>
<summary>The rules presets</summary>

| `RulesPreset` | For a crew that wants … | What it sets |
|---|---|---|
| `Custom` (default) | its own mix | nothing: the single settings as they are |
| `Classic` | everyone for themselves, paid for the scrap they bring | personal upgrades from 50% of each quota's surplus, split by the scrap each player brought (the defaults) |
| `CrewOwnUpgrades` | personal builds, but nobody falls behind | personal upgrades, every player gets the whole 50% share |
| `CrewCredits` | upgrades that compete with the shop | personal upgrades paid from the group credits, every player pays their part of the price |
| `Team` | to get strong together, without bookkeeping | upgrades shared by the crew, paid from one team budget |
| `Solo` | to play alone | personal upgrades paid straight from the group credits |
| `LguCompanion` | to play with LateGameUpgrades | Classic, and the upgrades both mods have are left to LateGameUpgrades |

Any preset but `Custom` decides `Currency`, `SurplusSharePercent`, `TakeShareFromGroupCredits`, `ShareSplit`,
`SplitUpgradePrices` and `UpgradeOwnership` (`LguCompanion` also `OverlapPolicy`); their own values stay saved and apply
again with `Custom`. Only a switch between personal and team upgrades waits: to team upgrades until the ship is in
orbit, back to personal upgrades until the crew next lands.

</details>

## Compatibility

| Mod | Status |
|---|---|
| [LethalConfig](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/) | Recommended: all settings in the mod menu, without restart; what you cannot change there is greyed out with the reason |
| [InputUtils](https://thunderstore.io/c/lethal-company/p/Rune580/LethalCompany_InputUtils/) | Optional: the skill menu's key in the game's keybinds (LethalRPG > Skill menu), also for a controller |
| LateGameUpgrades | Works. 14 of its upgrades do the same as one of LethalRPG's; by default both add up, with `OverlapPolicy = PreferOtherMod` only the LateGameUpgrades one works |
| GeneralImprovements | Works. With `SprintOnLadders = Allow`, Climber has no effect |
| NightVision | Works. Overrides Night Owl while it is on |
| LethalThings | Works, also with the Inventory upgrade: the Utility Belt's 3 slots come on top |
| ReservedItemSlotCore, HotbarPlus, AdvancedCompany, LethalProgression | Work. They manage item slots, so the Inventory upgrade is off |
| LethalLevelLoader, DawnLib, JLL, MonsterManager, Fair AI | Played in a larger modpack with an early version (0.2.0); not tested with newer versions yet |
| MoreCompany | Tested in LAN with up to 8 players (automated, on one PC, in orbit; below); Steam lobbies are not tested yet |

LethalThings' Utility Belt adds its 3 slots on top of the Inventory upgrade's, and taking it off removes the last 3
slots and drops whatever is in them, even items in slots from the Inventory upgrade, as LethalThings does on its own.
LethalConfig and InputUtils are optional and do not come with LethalRPG; without them everything works as described.

Not listed means not tested. If another mod or a game update keeps a skill from working, only that skill is affected:
LethalRPG names it when you join a lobby, and `rpg info` and the RULES tab list it. LethalRPG needs Lethal Company v80 or
newer.

<details>
<summary>The tests of 0.10.0</summary>

LethalRPG together with these mods in automated LAN games on one PC. What needs real players on Steam or a player at
the keyboard has a row of its own.

<!-- compat-matrix:begin -->
| Mod | Tests of 0.10.0 |
|---|---|
| MoreCompany 1.14.0 | Tested in LAN with 8 players on 0.10.0 (26 Oct 2026; automated, on one PC, in orbit): crew, split upgrade prices, payouts by contribution, rejoin into the same slot, self-test |
| More Ship Upgrades 3.14.1 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, on a moon): Stack and PreferOtherMod, Iron Grip against a snare flea, self-test |
| ReservedItemSlotCore 2.0.55 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, in orbit): Inventory upgrade off for everyone, Fast Hands without Inventory, the same item slots on every PC, a player without the mod, self-test |
| HotbarPlus 1.8.5, ReservedItemSlotCore 2.0.55, ReservedFlashlightSlot 2.0.10, ReservedWalkieSlot 2.0.7 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, in orbit): Inventory upgrade off, a flashlight, a walkie-talkie and a shovel with the same item slots on every PC, self-test |
| GeneralImprovements 1.5.5 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, on a moon): terminal and skill menu, health with Vitality, item slots, self-test |
| WeatherRegistry 0.8.8, WeatherTweaks 1.1.11 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, on a moon): a calm moon for the landing, no weather effect on it, self-test |
| UltimateRevive 1.1.1 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, on a moon): Second Wind catches a first death, a second death and the mod's revive, levels and Vitality's max HP kept after the revive (health is the mod's 35), Second Wind's used charge stays used, self-test |
| ReviveCompanyRemake 1.0.0 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, on a moon): Second Wind catches a first death, a second death and the mod's revive, levels and Vitality's max HP kept after the revive (health is the mod's 35), Second Wind's used charge stays used, self-test; needs InputUtils, which its package does not name |
| LethalThings 0.10.13, SlendermanMod 1.2.3 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, on a moon): the monsters the game knows with their kill XP, a Boomba hit by a player explodes without a kill and without harm, self-test; the monsters of DawnLib (SCP-999, SCP-4666) are not tested |
| LateCompany | Not tested on 0.10.0 yet: LateCompany 1.0.18 lets nobody into a landed round, and an automated run ends no day (planned: joining between two days, crew and record of the new player, self-test) |
| A mod pack: LethalConfig 1.4.6, Starlancer AI Fix 3.13.2, Lethal Company Input Utils 0.7.13, CSync 5.0.1, LethalLib 1.2.0, MoreCompany 1.14.0, NavMeshInCompanyRedux 1.3.0, NavMeshLib 3.0.0 | Tested in LAN on 0.10.0 (26 Oct 2026; automated, on one PC, on a moon): terminal and skill menu, Gunk Proof next to Starlancer AI Fix, landing, self-test |
| MoreCompany in a Steam lobby with more than 4 players | Not tested yet (needs real players on Steam) |
| LateCompany on Steam: a new player takes the slot of one who left | Not tested yet (needs real players on Steam) |
| LateGameUpgrades with Stimpack and Explosion Resistance bought, Blast Shield in a real explosion | Not tested yet (needs a player at the keyboard) |
| Lightning Rod in a real storm (WeatherRegistry, WeatherTweaks) | Not tested yet (needs a player on a stormy moon) |
<!-- compat-matrix:end -->

</details>

## FAQ

- **Do I lose my skills when the crew gets fired?** No: upgrades and budget are reset (the host can turn that off), XP
  and skills stay in your profile, and `rpg rebuy` buys your build again. Only with `ProgressScope = Save` do they
  belong to that save, and then they are reset with the upgrades (unless the host turns that off).
- **Can I take my upgrades back?** `rpg refund` (or STATS) resets your upgrades and skills and gives back 50% of what you
  spent. Profile backups protect your XP and skills; if the game reports a corrupted settings file, LethalRPG copies
  your files to a backup first and resets only that file, so your saves stay.
- **Do 0.9.x and 0.10.x play together?** No: a player with another minor version plays without LethalRPG in that
  lobby. Different 0.10.x versions work together.
- **More than 4 players?** With MoreCompany, tested in LAN with 8 players in orbit; Steam lobbies are not tested yet.
- **Does it work in singleplayer?** Yes: alone you get the whole budget share; `RulesPreset = Solo`
  pays the upgrades straight from the group credits instead.

More questions and answers on the wiki page "FAQ", and every page in the
[wiki](https://thunderstore.io/c/lethal-company/p/Cucumis/LethalRPG/wiki/).

## Reporting a bug

`rpg report` in the terminal, or **WRITE BUG REPORT** on the RULES tab, saves a report as
`BepInEx/LethalRPG-report-<date>_<time>.txt` and copies it to the clipboard. Players appear only as P1, P2, …, never by
name or Steam ID. Report bugs on [GitHub](https://github.com/feedback4cucu/LethalRPG/issues/new/choose) and attach the
file, best from every player who saw the problem.

In multiplayer the host types `rpg selftest` first (or clicks **SELF-TEST** on the RULES tab), then `rpg report`. The
self-test asks every player's LethalRPG what it holds and applies and shows per player `OK` or what differs, such as
`Max HP: host 120, P2 100`. The host's report then has the result. On a client the self-test checks only that PC.

## What's next

Planned, not promised:

- **Bug fixes (0.10.x)** that work together with every 0.10.x, so nobody has to update at the same time.

## Author and license

- Author: **Cucumis**
- License: MIT (see the `LICENSE` file). No warranty.
- LethalRPG is an unofficial fan-made mod and is not affiliated with Zeekerss or Lethal Company. No game files are
  included. Use it at your own risk and keep backups of your saves.

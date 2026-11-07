# Changelog

## 0.10.0

Every player must update: yes (network protocol 4; 0.10.x does not work together with 0.9.x or older) · Config: compatible, new entries with defaults (host: `ProgressScope`, Profile; `SaveScopedSkills`, Both; this PC: `SkillView`, List) · Saves/profile: compatible; a save hosted with `ProgressScope = Profile` (the default) keeps its format and 0.9.x loads it, a save played with `ProgressScope = Save` is written in a new format that 0.9.x leaves untouched (with a notice that a newer LethalRPG saved it), and profiles keep their format

- Network protocol 4: everyone in a lobby needs LethalRPG 0.10.x, because the two new host settings below change what
  host and clients exchange. 0.10.x does not work together with 0.9.x or older; as before, a player with another minor
  version gets the warning that the versions do not work together and plays without LethalRPG in that lobby. Profiles
  keep their format. A save hosted with `ProgressScope = Profile` (the default) keeps its format too, and 0.9.x loads it
  and keeps the new fields it does not know; any reset on 0.9.x (game over, wipe or `rpg debug reset`) loses only what
  such a save keeps aside from an earlier `ProgressScope = Save` period. A save played with `ProgressScope = Save` is
  written in a new format that 0.9.x leaves untouched (with a notice that a newer LethalRPG saved it), because it would
  take that save's XP for a copy of the profiles. Two new messages carry the self-test below.
- XP and skills per save, the wish from issue #2 that every run starts from the same place: new host setting
  `ProgressScope` in '0. General (host decides)', default `Profile`, which keeps everything as before (XP and unique and
  ultimate skills in each player's own profile, taken to every lobby and save). With `Save`, XP and skills belong to the
  save, for everyone in it: every player starts there with 0 XP and no skills (a player who joins later too), earns and
  learns as usual, and what they earn stays on that save; with `ResetOnGameOver` (the default) XP and the save's skills
  are lost when the crew gets fired, like the money upgrades, and the saved build stays for 'rpg rebuy'. The players'
  own profiles are never changed on such a ship (with the default `SaveScopedSkills = Both` not even read): nothing
  earned, learned, reset or lost there reaches them, and no profile backups are made. 'rpg profile restore' still works
  and is used from the next lobby that keeps XP and skills in profiles. On the challenge moon nothing of it is kept. The
  game saves in orbit, so a host who quits on a moon loses that day's XP on such a save, like the credits.
- New host setting `SaveScopedSkills` (default `Both`), only with `ProgressScope = Save`: which skills belong to the
  save. `Both`: unique and ultimate skills; `Unique`: unique skills, ultimate skills come from each player's profile;
  `Ultimate`: ultimate skills, unique skills come from the profile. XP always belongs to the save. A skill from the
  profile works on the ship, but cannot be learned or reset there: the menu shows it as FROM PROFILE and the terminal as
  PROFILE (when your profile does not have it: PROFILE ONLY in the menu, 'profile only' and no price in the terminal),
  'rpg <name>' and the info box say 'Comes from your profile on this ship: it works here, but cannot be learned or reset
  here.', buying it is refused ('On this ship unique skills come from each player's profile, so X-Ray Scan cannot be
  learned here.'), it stays out of CAN BUY NOW, the GUIDE's NEED, the day report's 'Can buy' and SKILL READY, and 'rpg
  refund' leaves it. Requirements stay as they are: with `Ultimate`, an ultimate skill that needs a unique skill can
  only be learned with that skill in the profile. In LethalConfig's menu `SaveScopedSkills` is greyed out while
  `ProgressScope` is `Profile` ('Only with ProgressScope = Save: which skills belong to the save. Set ProgressScope to
  Save to choose.'); no rules preset decides either setting.
- A change of `ProgressScope` or `SaveScopedSkills` applies the next time the host's PC hosts a save; the open lobby
  keeps its own. Both are in the host's config, so they apply to every save that PC hosts. Until then 'rpg info' and the
  RULES tab say what will change ('From the next time this save is hosted: XP and skills belong to the save
  (ProgressScope = Save).'), and the GUIDE's THIS SHIP shows 'Progress: per save from the next lobby'. Switching back to
  `Profile` keeps the save's XP and skills aside in the save, and they are back with `Save`; a game over with
  `ResetOnGameOver` while the save uses profiles clears the XP and skills it keeps aside (their saved build stays).
  Before XP and skills move, the save's LethalRPG data is backed up as before other conversions.
- On a ship that keeps XP and skills per save, every player with LethalRPG gets the notice 'Progress on this ship' once
  per join (whatever `ShowTips` says), the host once per lobby, e.g. 'XP and skills here belong to this save: you start
  with 0 XP and no skills, and getting fired resets them. Your own profile is neither used nor changed on this ship.'
  'rpg info' and the RULES tab say what belongs to the save and what getting fired and 'rpg refund' take; the GUIDE has
  a THIS SHIP line 'Progress' and says it in its steps; the menu header shows 'SAVE XP 40', the terminal 'Save XP: 40',
  'rpg stats' 'XP 40 on this save (spent 25)'; the PROFILE box and 'rpg profile' say what of the profile counts there;
  and after a game over 'New run' says 'Money upgrades, budgets, XP and skills on this save were reset: every run starts
  from the same place.' With `ProgressScope = Profile` all these texts stay as they were.
- `ResetOnGameOver`'s description says what a game over takes with each `ProgressScope`. The bug report's SESSION names
  where XP and skills are kept ('Progress: in profiles', or 'Progress: per save (SaveScopedSkills Both); profiles are
  not used or changed in this lobby', with a change that waits for the next lobby), and its rules line has ', progress
  per save' (with '(Unique)' or '(Ultimate)') when the host set `Save`. The run journal's `session` line has the lobby's
  `scope` and `saveSkills`, its `rules` line the configured `progressScope` and `saveSkills`, and `fired` has
  `"progress":true` when XP and skills of the save were reset; the journal's format stays v1.
- Skill tree: the new terminal command 'rpg tree' shows every unique and ultimate skill under the upgrade it needs a
  level of, one block per upgrade with your level and the skills by the level they ask for, e.g. 'ANALYST 2/4: 1
  Compass* | 2 Prospector!' and '3 X-Ray Scan | 4 Hunter Sense (+X-Ray Scan)': '*' marks a skill you have, '!' one you
  can learn now, '(+X-Ray Scan)' a second skill an ultimate needs. 'rpg unique' and 'rpg ult' point to it ('Skill tree:
  rpg tree'). In the skill menu the UNIQUE and ULTIMATE tabs have a toggle LIST / TREE in the tab bar: TREE draws the
  same tree, the upgrades on top, each skill below with the level it needs, owned skills filled, learnable ones framed,
  the ones you lack money or XP for ('NEED 12 XP') or that can only be bought in orbit ('IN ORBIT ONLY') framed in red,
  the others dimmed. Click a skill or upgrade to see its details in the info box below (click it again to let go), with
  its buttons at the right of the box's title: LEARN for a skill, BUY and MAX for an upgrade, greyed out while you cannot
  buy it; with none selected the box shows what you can buy now and 'Click a skill for details.' The click also lights
  up how the skills hang together, with a frame outside the node (so its own state stays readable): the node you clicked
  gets a white frame, the skills it unlocks a green one (a unique skill: the ultimates that need it, even under another
  upgrade; an upgrade: the skills under it that ask for a level of it) and the skills it needs a blue one (an ultimate:
  its unique skill); a legend above the tree names the colours. The choice holds for both tabs and stays on this PC: the new setting `SkillView` in '5. Interface (this
  PC)' (`List` or `Tree`, default `List`) keeps it, and the toggle sets it. When the host has `RequirePrerequisites` off
  there is no tree: the toggle is not shown, the tabs show the list and 'rpg tree' says why.
- New 'rpg selftest' and SELF-TEST on the RULES tab of the skill menu: the host asks every player's LethalRPG what it
  holds and applies (version, state, settings, data, levels, the levels of the others as that PC sees them, the
  lobby's player slots, the inventory slots, max HP, speeds and failed patches) and shows per player OK, MISMATCH with
  the values that differ ('Max HP: host 120, P2 100'), WARNING (the values agree, but that PC has a problem of its own,
  such as a failed patch group) or 'no answer (older version or no mod)', with the round trip; players with a LethalRPG
  that does not work with the host's are not asked ('off: LethalRPG 0.9.2'). A player whose state, settings or data
  differ gets the host's state once more and is asked again: RESYNCED when that fixed it (a lost or rejected state),
  'Resynced: still different' when not. Values that may lag for a moment (slots, the max HP and speeds applied, the
  crew) are asked for again after a second, and only a difference that stays counts. The page appears at once and is
  replaced by the result when every answer is in (at most 5 s per question); away from the terminal the result comes
  as a tip, from the menu in its status line, with the page below the button. On a client the command and the button
  check only that PC (its slots, HP and speeds as its data says, the host's version, the state taken and the host's
  last check) and say that only the host can compare every player. The host also checks each player quietly once, 15 s
  after they joined (log and bug report only, nothing on screen). The bug report has a new SELF-TEST section with the
  last run and the checks after each join, players by slot and a short hash only; on a client its own part and the
  host's last ping it answered.
- Fixed (host, Steam): a player who takes a slot someone left (also on a moon, with LateCompany) still carries that
  player's Steam id until the game sends the new player's values, and until then their kill and scrap XP went to the
  record of the one who left (kept for their next visit) or, when that player was back in another slot, to them. The
  host now takes such a slot for nobody until the values arrive. Scrap a player brought to the ship before leaving on
  a moon is now booked to them when they leave; until now it went to whoever had the slot at the next collection (or
  to nobody). Protocol and save format are unchanged.
- Compatibility: the READMEs and the wiki page Compatibility have a new table 'Tests of 0.10.0': per mod the version
  it was tested with, the day and what was checked (automated LAN games on one PC), and as 'not tested yet' every test
  that has not passed so far, also those that need real players on Steam or a player at the keyboard. MoreCompany 1.14.0
  is now tested with 8 players in LAN (in orbit).
- Fixed: the day report no longer runs out of the game's tip box. It was as many lines as the day brought, and a
  long one wrapped, so 'Spend at [K] or terminal: RPG' could sit below the yellow box. The report now takes at most
  three rows of the box. When there is more, it gets shorter in steps: XP and budget on one line without the XP's
  sources ('+2 XP, +$100 upgrade budget'), 'Can buy' with two names without prices or with only the cheapest, the
  saves without their causes, and what auto rebuy bought as 'Saved build rebuilt'. Only then does it drop 'Can buy',
  then the hint. A new player keeps the command for a first purchase instead of 'Can buy'.

## 0.9.0

Every player must update: yes (network protocol 3; 0.9.x does not work together with 0.8.x or older) · Config: compatible, new entries with defaults (host: `RulesPreset`, Custom; this PC when hosting: `RecordRunJournal`, false; each PC: `ShowTips`, true; `MenuScale`, 1; `Scanlines`, true; plus the sections of the nine new skills); Wristwatch's `RequiredLevel` keeps its value but now means the Electrician level · Saves/profile: compatible, same format; 0.8.x loads 0.9.0's files and keeps the new skills it does not know

- Network protocol 3: everyone in a lobby needs LethalRPG 0.9.x, because the new skills change what host and clients
  exchange. 0.9.x does not work together with 0.8.x or older; as before, a player with another minor version gets the
  warning that the versions do not work together and plays without LethalRPG in that lobby. Saves and profiles keep
  their format: 0.8.x loads what 0.9.0 wrote and keeps the new skills it does not know (they do nothing there), and back
  on 0.9.x they work again. A respec or wipe on 0.8.x refunds their XP and removes them.
- New unique skill Quartermaster (needs Bargain Hunter 2; 25 XP, or $200 where skills cost money): in the ship's room
  and in orbit, the skill box shows the scrap on the ship, e.g. 'SHIP $1240, 14 ITEMS', and what is left of the quota:
  'QUOTA $360 LEFT, COVERED' in green when the ship's scrap is worth that much (at full value, as on the last day),
  '$360 LEFT, $120 SHORT' in orange when not, 'MET' once it is met. It counts scrap as the game does (in the ship's room
  or carried there, with Appraiser's values; not what lies on the Company's counter), only on your PC. In orbit these
  are the box's only rows.
- New unique skill Trained Eyes (needs Marksman 2; 30 XP, or $250 where skills cost money): fog is thinner for you, so
  you see 2.5 times as far in foggy weather, in a moon's own fog (the haze over the whole moon and its fog banks, such
  as Rend's thick fog), in the facility's fog and where the steam of burst pipes hangs as fog. The host sets how far in
  `3. Unique: Trained Eyes` (`VisibilityMultiplier`, 1 to 10; 3.5 on Generous, 2 on Strict). Only your own view
  changes, on your PC: other players see the fog as before, and monsters see you as before. The jet of steam from a
  pipe, the drops on your visor and the 'VISIBILITY LOW!' warning stay. While you are dead or spectating, the fog is as
  thick as without the skill. With `OverlapPolicy = PreferOtherMod` it can be learned without Marksman, which is then
  left to LateGameUpgrades.
- New unique skill Gunk Proof (needs Surefooted 2; 25 XP, or $200 where skills cost money): the spit of the Backwater
  Gunkfish clears off your visor twice as fast (about 9 s instead of 18), and its slime makes you slip half as much. The
  host sets both in `3. Unique: Gunk Proof`: `VisorClearSpeed` (1 to 10; 3 on Generous, 1.5 on Strict) and
  `SlipReduction` (0 to 1, 1 = no slipping at all; 0.7 on Generous, 0.3 on Strict). It works on your PC: other
  players still see the slime on you, and the slime stays where it is. While you are dead or spectating, the spit
  and the slime are as without the skill.
- New unique skill Pest Control (needs Medic 2; 30 XP, or $250 where skills cost money), against the cadaver growth:
  while you spray the weed killer, the cadaver plants inside and the Vain Shrouds outside shrink twice as fast, so they
  die after half the spraying; its tank lasts twice as long (30 s of spraying instead of 15); and an infected teammate
  you spray (before the infection bursts) heals twice as much per spray for the same damage, so a cure takes about
  half the sprays and half the damage. The host sets all three in `3. Unique: Pest Control`: `KillSpeed` (1 to 10; 3
  on Generous, 1.5 on Strict), `TankSaving` (0 to 0.9, the share of the tank saved; 0.7 on Generous, 0.3 on Strict)
  and `HealSpeed` (1 to 10; 3 on Generous, 1.5 on Strict). It goes with the weed killer in your hands, so every
  player sees the plants die at the same pace. Spray paint stays as it is, and so does the weed killer while you are
  dead or in the hands of someone without the skill.
- New unique skill Gentle Collector (needs Sneak 2; 25 XP, or $200 where skills cost money), for noisy scrap such as
  toys that play a sound and an animation when picked up: when you take it in hand it goes off half as often as in the
  game, and when you take it out of your pocket it no longer goes off every time but just as rarely. The host sets
  `QuietChance` in `3. Unique: Gentle Collector` (0 to 1, how much less often; 1 = never; at 0 pickups are as in
  the game, but taking it out of your pocket still rolls; 0.7 on Generous, 0.3 on Strict). It goes with the scrap in
  your hands, so every player hears the same. Dropping it and the noise it makes while it plays stay as they are, and
  so does the scrap while you are dead or in the hands of someone without the skill.
- New unique skill Soft Landing (needs Jump 2; 25 XP, or $200 where skills cost money): falls hurt you less. You take
  three quarters of the game's fall damage, rounded down: 22, 37, 60 and 75 instead of 30, 50, 80 and 100, so even the
  highest fall no longer kills you from full health (the two highest still can when you are already hurt; the lower ones,
  as in the game, leave you at 5 health unless you are critically injured). A pit that swallows you still kills; a
  jetpack crash and everything else that is not a landing hurt as in the game. The host sets `FallDamage` in `3. Unique:
  Soft Landing` (0 to 1, the share of fall damage you still take; 0 = none; 0.6 on Generous, 0.85 on Strict). With
  LateGameUpgrades' Reinforced Boots both count: the skill takes its share of what the boots leave. With
  `OverlapPolicy = PreferOtherMod` it can be learned without Jump, which is then left to LateGameUpgrades.
- New unique skill Ghost Step (needs Crouch Walk 2; 30 XP, or $250 where skills cost money): while you crouch, monsters
  no longer hear your footsteps, so you can creep past an Eyeless Dog without your steps drawing it. Players still hear
  your steps as before. Only the footsteps go quiet: the noise of items (noisy scrap is Gentle Collector's), of your
  landing after a jump or fall and of your voice stays, and standing up makes your steps heard again. The host sets
  `NoiseLeft` in `3. Unique: Ghost Step` (0 to 1, how far monsters still hear your crouching steps compared to without
  the skill; 0 = not at all; also 0 on Generous, 0.3 on Strict). Sneak still shortens the range of your other steps as
  before, and of what is left of your crouching ones. With `OverlapPolicy = PreferOtherMod` it can be learned without
  Crouch Walk, which is then left to LateGameUpgrades.
- New unique skill Overcharge (needs Electrician 2; 30 XP, or $250 where skills cost money), for the Zap Gun: while you
  hold a monster with it, the gun heats up half as fast and the beam's pull grows harder half as fast, so you can keep
  the monster stunned longer before the gun overheats. Cooling down, the range, the battery use and the cooldown after
  the shock stay as in the game. The host sets `HeatSpeed` and `PullGrowth` in `3. Unique: Overcharge` (0 to 1, each as
  a share of the game's speed; 0 = never; both 0.3 on Generous, 0.7 on Strict). It goes with the Zap Gun in your hands,
  so every player sees the same beam. Zapping a teammate or a breaker box stays as in the game. With LateGameUpgrades'
  Aluminium Coils both count. With `OverlapPolicy = PreferOtherMod` it can be learned without Electrician, which is then
  left to LateGameUpgrades.
- New unique skill Mechanic (needs Handyman 2; 30 XP, or $250 where skills cost money), for the Company Cruiser: while
  you drive it, it takes half the damage from crashes, bumps and monsters, and the weed killer you spray at its
  oil pipe repairs twice as much per spray (8 instead of 4 health). What a hit leaves below a whole point adds up over the
  next hits, so many small bumps count too. A crash that destroys the Cruiser at once still does. The host sets
  `DamageTaken` (0 to 1, a share of the game's damage; 0.3 on Generous, 0.7 on Strict) and `RepairSpeed` (1 to 10;
  3 on Generous, 1.5 on Strict) in `3. Unique: Mechanic`. Only the driver's skill counts for the damage, the sprayer's for
  the repair, and every player sees the same health. With `OverlapPolicy = PreferOtherMod` it can be learned without
  Handyman, which is then left to LateGameUpgrades.
- Iron Grip now also works against tulip snakes: they can still cling to you, but no matter how many of them flap,
  they can no longer lift you into the air and carry you off. Only your own Iron Grip counts, and every player sees you
  stay on the ground. 'rpg tulip' finds the skill. Against the snare flea and the kidnapper fox it works as before.
- Brawler and Concussive Blows now work with the kitchen knife. The knife strikes about twice as often as a shovel
  swings, so it gets half of Brawler's bonus: on Standard, with Brawler 1 it deals 1 and 2 damage in turn on the same
  monster, with Brawler 2 it deals 2, with Brawler 3 it deals 2 and 3 in turn (a shovel deals 2, 3 and 4 at Brawler 1,
  2 and 3). Half points add up over the hits on the same monster, also between knife and shovel. A knife hit stuns like
  a shovel hit with Concussive Blows, but the same monster can be stunned again only after twice the cooldown: 12 s
  instead of 6 s (3 and 6 s on Generous, 9 and 18 s on Strict). As with the shovel, a knife hits other players with its
  normal force. 'rpg brawler' and 'rpg concussive' say so.
- Wristwatch now needs Electrician 1 instead of Analyst 1, so Analyst 1 no longer opens two skills at once (it now
  opens only Compass). A Wristwatch you already have stays: prerequisites only count when you learn a skill. The setting
  `RequiredLevel` of '3. Unique: Wristwatch' keeps its value and now means the Electrician level.
- Compass shows the nearest exit by walking distance instead of as the crow flies, and which exit it is: e.g. 'EXIT
  MAIN 42m AHEAD' for the main entrance or 'EXIT FIRE 15m LEFT, UP' for a fire exit. The direction follows the way
  through the facility to its next turn, so it no longer points through walls or back the way you came, and UP or DOWN
  says when the way leads to another floor. In the Mineshaft the way to the main entrance can lead through the elevator
  ('MAIN 60m LEFT, ELEVATOR'), and the arrow points at the elevator until you ride it. Where two exits are about as far,
  the compass stays with the one it shows until another is clearly nearer. A door the game says is blocked (some modded
  interiors have more fire exits than their moon) is never shown. Where no walkable way leads out because a closed
  security door or a locked door is in the way, the compass names that door: e.g. 'MAIN 51m BEHIND, DOOR R4' with the
  door's terminal code, or 'FIRE 30m LEFT, LOCKED DOOR'; the arrow points at the door and the distance is the way
  through it. Where it finds no walkable way otherwise, it says so with ROUGHLY and the distance as the crow flies.
  With `SkillBoxPosition = BelowWeight` the line below the weight shows the same, e.g. '< EXIT FIRE 15m UP' or
  'v EXIT MAIN 51m DOOR R4 v' ('?' after the distance for a rough way). The way comes from the facility's own
  navigation data, as the ship's radar line to the exit does, and is worked out on your PC only.
- New host setting `RulesPreset` in '0. General (host decides)' sets how upgrades are paid and who owns them in one
  line: `Classic` (the defaults: personal upgrades from 50% of each quota's surplus, split by the scrap each player
  brought), `Solo` (personal upgrades paid straight from the group credits), `CrewOwnUpgrades` (every player gets the
  whole 50% share), `CrewCredits` (personal upgrades paid from the group credits, every player pays their part of the
  price), `Team` (upgrades shared by the crew) and `LguCompanion` (Classic, and LethalRPG leaves the upgrades
  LateGameUpgrades has as well to it). Any preset but `Custom` decides `Currency`, `SurplusSharePercent`,
  `TakeShareFromGroupCredits`, `ShareSplit`, `SplitUpgradePrices` and `UpgradeOwnership` (`LguCompanion` also
  `OverlapPolicy`); their own values stay in the config and apply again with `Custom`. `PriceShareCap` and everything
  else stay yours. `Custom` is the default, so a config without the line keeps its rules. A modpack needs only this one
  line, and players need nothing new: the host sends the rules the preset sets, as before. `rpg info` and the RULES tab
  name the preset as their first rule ('Rules preset: Classic.', or 'Rules preset: Custom.' for rules of no preset),
  the same on every PC because it comes from the rules themselves. The bug report names it too and marks the entries
  the preset decides with '(RulesPreset decides)'; the host's log names it at the start of a lobby, with the entries
  whose own values it covers.
- A switch to team upgrades (`UpgradeOwnership = Team` or `RulesPreset = Team`) made on a moon now waits until the ship
  is back in orbit, so the rules do not change while the crew is out on a moon: the host is told 'Upgrades become shared
  by the crew when the ship is back in orbit.', and the switch happens when the ship is ready to land again (or when the
  lever is pulled). In orbit it happens at once, as before; a switch back to personal upgrades still waits until the
  crew next lands. While a switch waits, `rpg info` and the RULES tab say so ('Upgrades become shared by the crew when
  the ship is back in orbit.' or 'Upgrades become personal when the ship next lands.'), and THIS SHIP in the GUIDE shows
  'your own until back in orbit', as it shows 'team until the ship starts' while a switch to personal upgrades waits.
- With LethalConfig (1.4.1 or newer) installed, LethalRPG's settings in its menu no longer say they may require a
  restart: they apply at once (a switch between personal and team upgrades still waits, see above). The menu shows
  LethalRPG's own description and greys out what you cannot change there, with the reason when you point at it: as a
  player in someone else's lobby every setting but those in '5. Interface (this PC)' and '6. Run journal (when you
  host)' ('The host's value applies in this lobby'), and the settings `RulesPreset` decides while it is not `Custom`
  ('RulesPreset decides this. Set RulesPreset to Custom to change it here.'). LethalConfig stays optional and does not
  come with LethalRPG; without it nothing changes, and with an older one its menu shows the settings as before (the log
  says so).
- With InputUtils installed, the skill menu's key is in the game's keybinds: section LethalRPG, row 'Skill menu'. Its
  default is `MenuKey` (K unless you changed it), so nothing changes until you set another key there; that binding then
  opens the menu, and every text that names the key ('[K]') names yours. The controller field stays empty for you to
  set a button (the menu itself is used with the mouse). `MenuKey` is then only that default, read when the game starts;
  LethalConfig greys it out ('With InputUtils, set this key in the game's keybinds (LethalRPG > Skill menu).'). The log
  says 'Skill menu key: InputUtils (LethalRPG > Skill menu, default <Keyboard>/k)'. InputUtils stays optional and does
  not come with LethalRPG; without it `MenuKey` works as before.
- Run journal for hosts (off by default): new setting `RecordRunJournal` in '6. Run journal (when you host)' (only for
  this PC, never synced). While you host a save with it on, your game writes one JSON line per day (moon, weather,
  deaths and their causes, XP by source, scrap, kills, the players' levels) and one per purchase, respec, quota, payout,
  firing and rule change to `BepInEx/LethalRPG/journal-<save>.jsonl`, to help balance LethalRPG. Players appear only as
  P1, P2, ... (their slot), never by name or Steam ID; nothing is sent anywhere. At 1 MB the file moves to .1 (up to .3
  are kept). 'rpg info' and the RULES tab on the host say so ('Run journal: on. Each day goes to
  BepInEx/LethalRPG/journal-LCSaveFile1.jsonl on this PC (players as P1, P2, ...; nothing is sent).'), and every player
  with a compatible LethalRPG who joins gets the notice 'Run journal' once per join. Nothing changes for players
  without the setting; the journal adds no network message and does not change the save format.
- New GUIDE tab in the skill menu (the last tab, after RULES) and the terminal command 'rpg guide': how LethalRPG works on
  the ship you are on, from the host's settings as they are now. It shows the next step for you (the cheapest upgrade
  you can buy now, after getting fired your saved build with 'rpg rebuy', or what you still need for it), the ship's
  rules at a glance (your own or team upgrades, where the money comes from, what skills cost, what getting fired takes,
  who has LethalRPG) and five short steps from money to getting fired. 'rpg start', 'rpg tutorial' and 'rpg help' show
  it too. 'rpg help' showed the full rules before; they are in 'rpg info'. 'rpg help (name)' still shows an upgrade's
  details.
- Until you buy your first upgrade or skill, the skill menu opens on the GUIDE tab the first time you open it in a lobby,
  and the terminal's upgrade pages say 'New here? rpg guide explains this ship.' Nothing is saved for this: it ends by
  itself with your first purchase (on a ship with team upgrades, which belong to the crew: with your first skill).
- The day report's 'Can buy' keeps its second place for a skill you can learn when the two cheapest purchases are
  upgrade levels, so cheap levels no longer hide it. Until your first purchase (on a ship with team upgrades: your first
  skill), a day report that brings budget ends with the command for the cheapest upgrade that money buys, e.g. 'Spend
  it on the ship: [K] or rpg buy climber' ('The crew can spend it: ...' for team budget), instead of 'Spend at [K] or
  terminal: RPG'.
- Tips for players new to LethalRPG (at most one per lobby): until your first purchase, a lobby welcomes you with
  'Upgrades and skills: press [K] on the ship. The GUIDE tab explains how they work on this ship. Terminal: type RPG.',
  unless you have already opened the skill menu or an 'rpg' page there. If you have bought before and join someone
  else's ship, you get its rules once per lobby instead, e.g. 'This ship: your own upgrades from 50% of each quota's
  surplus, split by scrap. Skills cost XP. More: [K] > GUIDE'; the host gets no such tip. A tip waits for a calm moment:
  in orbit, at least 20 s after you joined and after the day report, when no other tip has shown for 8 s, and not while
  you are dead, at the terminal, in a menu or typing in the chat. Nothing is saved for these tips. The new setting
  `ShowTips` in '5. Interface (this PC)' (on by default; each player decides for themselves, never synced) turns off the
  welcome, the ship's rules and SKILL READY. Notices about errors, versions and your profile always show; the skill menu
  opening on GUIDE, 'New here?' and 'Spend it on the ship' do not depend on it.
- SKILL READY on the status line below the weight (off with `ShowTips` or `ShowStatusHud`): when you can learn a skill
  you could not learn before (the upgrade it needs and enough XP, or money for skills paid with money, also on a moon),
  the line shows e.g. 'SKILL READY - COMPASS, WRISTWATCH [K]' for 6 s ('(RPG)' without a menu key), once per skill and
  lobby. Skills you can already learn when you join are not named. It waits while you are dead or spectating, at the
  terminal, in the skill menu or while the new quota or the firing is on screen, and leaves out a skill you learned in
  the meantime. XP for a kill shows next to it.
- When Second Wind or Blast Shield saves you, the status line below the weight names what it saved you from for 6 s
  (off with `ShowStatusHud`), e.g. 'SECOND WIND - SAVED FROM GUNSHOTS' or 'BLAST SHIELD - SAVED FROM AN EXPLOSION'
  ('BLAST SHIELD, 2ND WIND - ...' when Second Wind caught the damage Blast Shield left). XP for a kill shows next to it.
  The day report lists the day's saves, e.g. 'Saved today: Second Wind 2x (gunshots, a fall), Blast Shield (an
  explosion)'. The line names how you would have died, not what did it (a turret's shots are gunshots). Only you see
  your saves.
- 'rpg info' and the RULES tab say what everything on the ship costs from nothing to the last level, from the host's
  settings as they are now, e.g. 'Everything on this ship: all upgrades $13422 per player, all unique skills 690 XP, all
  ultimates 1480 XP.' Only what can be bought counts (not upgrades that are off or left to another mod), at the prices of
  the difficulty and balancing. With split prices it shows your part for the players in the lobby and the full sum, in
  team mode the crew's sum ('for the crew'), and dollars for skills paid with money.
- When money or XP is short, the skill menu says how much is missing instead of showing the price only in red: the row
  shows e.g. 'NEED $40' or 'NEED 12 XP' in place of BUY and MAX or LEARN (the price stays next to it), also on a moon,
  and the info box says 'NOT ENOUGH MONEY: NEED $40 MORE'. 'rpg (name)' adds 'You need $40 more.' under the price. It
  counts the price you would pay now (with split prices your part) against the money that pays it: your budget, the
  team budget or the crew's credits.
- 'rpg buy' without a name shows CAN BUY NOW: what you can buy right now from every page, cheapest first, with the
  prices you would pay (e.g. '* Climber 1 ($30)'), up to six and then 'and 12 more'. With nothing to buy it says so and
  shows the GUIDE's next step. Before, it showed how to use the command. In the skill menu the box below the list shows
  the same while the mouse is on no row, above 'Point at an upgrade for details.' (away from the ship: 'Buying works on
  the ship.'). A typo like 'rpg byu' never shows the list.
- When the terminal does not know a name, it suggests what you may have meant below 'Unknown upgrade or command': 'rpg
  buy stamnia 3' answers 'Did you mean: rpg buy stamina 3?', and 'rpg stast' answers 'Did you mean: rpg stats?'. It
  names up to three upgrades or commands close to what you typed (none for words of up to 3 letters), in the form you
  used ('rpg info', 'rpg help', 'rpg buy', 'rpg debug as'), with the amount you typed, and after a command with the
  words you typed after it ('rpg rebyu auto' suggests 'rpg rebuy auto'). It only suggests: nothing is bought or changed
  until you type the command.
- The skill menu's tabs work with the keyboard: 1 to 8 (also on the numpad) open a tab, Q and E go to the previous and
  next tab, wrapping around at the ends. The tab bar shows '[Q] [E]' as your keyboard labels these keys (e.g. '[A] [E]'
  on AZERTY). If the menu key is one of these keys, it still closes the menu, and the tab bar leaves it out ('[E]' with
  the menu key on Q). No key switches tabs while you type in the chat.
- New settings in '5. Interface (this PC)' for the skill menu (each player decides for themselves, never synced; a change
  shows at once): `MenuScale` (1 by default, 0.75 to 1.5) sets its size. Larger makes the text bigger and the panel
  lower, so the lists scroll sooner; the menu never gets wider than the screen. On the Steam Deck, 1.5 shows the
  smallest text at about 18 px instead of 13. Smaller gives a smaller panel on large screens. `Scanlines` (on by
  default) can switch off the faint scanlines for a plain dark panel. With 1 the menu looks as before; only on a nearly
  square or portrait screen and in a window smaller than 620 x 360 it now gets smaller, so the right end of its rows
  (price, BUY and MAX) is no longer cut off.
- The GUIDE tab scrolls as a whole page when it does not fit, both columns together (before, only the left one did). In
  CREW & BUILD everything below the CREW title scrolls together, from 5 players also the player shown in full beside the
  crew lines (before, only the crew list did); the saved build and its buttons stay in place.
- LethalThings' Utility Belt no longer turns the Inventory upgrade off: with the belt in the shop (`PouchyBelt = true`,
  LethalThings' default) both work together. Inventory adds and removes only its own slots: slots another mod added stay
  as they are, and Inventory levels bought while you wear the belt add their slots on top of the belt's. Taking the belt
  off removes the last 3 slots and drops what is in them, also items in the Inventory upgrade's slots, as LethalThings
  does on its own. In such lobbies Fast Hands needs Inventory 1 again.
- Item slots stay the same on every PC, so switching and picking up land in the same slot everywhere: someone who joins
  while another player wears a Utility Belt gets that player's 3 belt slots at once (LethalThings adds them only when the
  wearer selects the belt again), and someone who joins learns which slot each player has selected, also with empty
  hands (the game only tells them for a player holding an item). A player slot someone left goes back to the game's 4
  inventory slots and the first slot selected on every PC, so whoever takes it next has the same slots everywhere.
- The bug report ('rpg report', WRITE BUG REPORT) and 'rpg debug crew' show each player's inventory slots as that PC
  holds them and where they come from, e.g. 'Slots of P2: 15 = 4 + 8 Inventory + 3 Utility Belt' ('+ 2 other mods' for
  slots of other mods).
- Fixed a bug in the game itself with the weed killer (with or without Pest Control): sometimes a cadaver plant you
  sprayed stayed at its smallest size and could not be destroyed. The game then tried again every frame, sprayed
  destroy particles each time and lagged, until the plant finally went away much later. Now it goes away at once. This
  could happen after an earlier kill near the middle of the same room or corridor had also removed that room's spore
  clouds and scan markers.
- Fixed: on a small screen (e.g. a 960 x 540 window) the skill menu's RULES tab could cut off its last line, 'Found a
  bug? rpg report writes a bug report.' (seen for the host with `DebugCommands` on), and the WRITE BUG REPORT button sat where
  that line should be. Every text in the menu that wraps (RULES, GUIDE, STATS, CREW & BUILD, the status line and the
  info box below the upgrade list) now gets the height it needs at the menu's size; the page below moves down and
  scrolls when it runs out of room. The info box could also lose the last line of a long text, e.g. 'Nothing you can buy
  right now. NEXT STEP: ...' with `MenuScale = 0.75`; when a text needs more room there, the box grows and the upgrade
  list above it gets shorter or scrolls. On a 1080p screen at the normal size nothing changes.
- Fixed: with the Minimap mod (LethalCompanyMinimap) the skill menu closed right after it opened. It now stays open;
  Escape still closes it, and the pause menu opens and closes as usual afterwards.

## 0.8.1

Every player must update: no (fixes only; network protocol 2, same as 0.8.0, so 0.8.0 and 0.8.1 play together; each player gets the fixes by updating) · Config: compatible, nothing new · Saves/profile: compatible, nothing changed

- The emotes on 1 and 2 no longer play while the skill menu is open.
- Holding E while the skill menu is open no longer finishes an interaction (a door, a valve) you were aiming at, and
  pressing E with the menu open while spectating no longer switches the player you watch.
- 'rpg rebuy' followed by anything other than 'auto' (for example a typo) no longer buys your build; it explains the two
  forms instead.
- Field Hacker no longer runs a hack that does nothing at an open big security door while the facility's power is off (a
  breaker switch is off, the apparatus was taken, or the day started without power): the game keeps the big doors open
  then and closes none, also not from the terminal. The prompt there now says 'Override door : no power' instead of
  '[Hold E]'. When the power comes back, the doors the outage opened close by themselves and Field Hacker closes the
  others as before; after the apparatus is taken, the power stays off for the rest of the day.

## 0.8.0

Every player must update: yes (network protocol 2) · Config: compatible, new entries with defaults (host: `OverlapPolicy`, Stack; `PriceShareCap`, 4; each PC: `SkillBoxPosition`, TopRight); Brawler's `EffectPerLevel` now goes down to 0.5 · Saves/profile: compatible, saves also keep XP earned while joining (0.7.x loads them and ignores that XP); on Generous, levels above the new caps stay saved but only work up to the cap, and Brawler's damage per level changes (see Balancing Generous below)

- The 0.7 line ends with this version: 0.7.x gets no more fixes; please update. Everyone in a lobby needs 0.8.x (network
  protocol 2, see below).
- XP a player earned while joining, before their profile had arrived at the host, was lost when the host quit and hosted
  the save again in that moment. The save now keeps it until the profile arrives, and then it is added on top of the
  player's XP, once; 'rpg debug restore' keeps it as well.
- If another mod conflicts with one of LethalRPG's changes to the game's player code, only the skills behind that change
  stop working (often a single one, e.g. Strength or Sneak). Before, one such conflict switched off about ten skills at
  once.
- LethalRPG now changes the game's code after other mods that change the same places (e.g. LateGameUpgrades), whatever
  order the mods load in, so both mods' bonuses apply. Surefooted also works when another mod scales the sinking speed
  in quicksand at the same place.
- The bug report ('rpg report', WRITE BUG REPORT) lists skills that may not work fully because another mod or a game
  update changed the code they need, with the reason, also for each skill of a patch group that failed to load.
- Network protocol 2: LethalRPG 0.8.x does not work together with 0.7.x or older; everyone in a lobby needs 0.8.x. A
  player with 0.7.x in a 0.8 lobby only learns the host's version: they get the warning that the versions do not work
  together, cannot buy, and play without LethalRPG there; their profile stays as it was. The other way round, a 0.8
  player takes nothing from a host with 0.7.x or another minor version (before, its data was used where it fit) and
  sends it no purchases. 'rpg report' on a client also names the host's build.
- Right after joining, purchases wait until the host's LethalRPG data has arrived: until then the terminal and the skill
  menu say 'The host has not sent its LethalRPG data yet. Try again in a moment.' If the first message to the host gets
  lost, LethalRPG sends it again for 30 seconds.
- The host's updates are much smaller: they carry only the players in the lobby instead of everyone who ever played in
  the save, and the host's rules only when they change. Clients no longer receive the Steam IDs and names of earlier
  players. A bug report written on a client ('rpg report', WRITE BUG REPORT) leaves out the names of the players it saw
  in the lobby and counts 'Records sent by the host'.
- The host's updates now also travel packed (GZip): about 2 KB for 4 players with full builds. A PC that cannot unpack
  them gets them unpacked; LethalRPG tests this when the game starts and says so in the log and in 'rpg report'. An
  update above 256 KB on the network (half of what Steam carries in one message) is not sent: the host logs an error
  once per lobby, and 'rpg report' (WRITE BUG REPORT) lists it under problems. A player's game unpacks at most 8 MB of
  one update (50 players with full builds take about 0.3 MB), so a broken or modified host cannot freeze it with one.
- Each player's game now confirms every update from the host. Without a confirmation the host sends the update again,
  twice, 5 seconds apart, then logs a warning. While a player's game does not have the host's data (it could not use an
  update, an update was too large to send, or it did not confirm one even after it was sent again), the host refuses
  that player's purchases and other requests such as refunds and rebuys with 'The host could not send you its LethalRPG
  data. Rejoin if this stays.', in the terminal and the skill menu alike, so nothing is bought that would never reach
  their profile (a skill paid with money could lose its money that way). An update that is only on its way blocks
  nothing. 'rpg report' (WRITE BUG REPORT) and 'rpg debug crew' show for each player which update their game confirmed
  last.
- If a save is played with LethalRPG 0.7.x in between and then with 0.8 again, the XP earned while joining that the save
  kept is not added a second time: 0.7.x may have put it into the player's profile already, so the host drops it for
  every player who was in the 0.7.x lobby when that version last saved. For a player who had left before, the host
  cannot tell, and the XP is added when they join.
- Handyman and Analyst no longer fail together: if another mod conflicts with Analyst's change to the scanner, only
  Analyst stops working and Handyman's faster hold interactions stay, and the other way round (before, such a conflict
  switched off both).
- If another mod or a game update keeps a skill from working fully, LethalRPG now says so where you look: the message
  when you join a lobby names the skills (before, it only appeared when a patch group failed to load and named none),
  'rpg info' and the Rules tab list them with the mods LethalRPG detected on this PC, and the skill's details say 'May
  not work with your mods (rpg info)' in the terminal ('rpg strength') and 'May not work with your mods (RULES)' in the
  skill menu. The bug report ('rpg report', WRITE BUG REPORT) also names, for each game method such a skill needs, the
  other mods that change it.
- New host setting OverlapPolicy in '0. General' for the upgrades LateGameUpgrades has as well. Stack (the default) keeps
  both, and their bonuses add up as before. PreferOtherMod switches LethalRPG's upgrade off for everyone while the same
  upgrade is on in the host's LateGameUpgrades config; unique and ultimate skills always stay. Levels already bought are
  kept but do nothing until the host sets Stack again, and 'rpg refund' gives them back as usual. The terminal pages and
  the skill menu show such an upgrade as 'in LateGameUpgrades', its details ('rpg stamina', the info box) say why and
  name the LateGameUpgrades upgrade that does the same (for Stamina: Bigger Lungs), and
  'rpg info' and the Rules tab list them. As with an upgrade the host disables, skills need no level in an upgrade that
  is off: with PreferOtherMod and the matching LateGameUpgrades upgrade on, Packmule, Second Wind, Lockpicker, X-Ray
  Scan, Compass, Iron Grip, Wristwatch, Prospector and Concussive Blows can be learned without their upgrade, and Hunter
  Sense, Lightning Rod, Blast Shield, Appraiser, Field Hacker and Coil Breaker without the upgrade level they need (they
  still need their first skill).
- OverlapPolicy PreferOtherMod knows the 14 upgrades LateGameUpgrades 3.14.1 has as well: Stamina (Bigger Lungs),
  Agility (Running Shoes), Jump (Strong Legs), Strength (Back Muscles), Vitality (Stimpack), Crouch Walk (Carbon
  Kneejoints), Lungs (Oxygen Canisters), Handyman (Quick Hands), Brawler (Protein Powder), Long Reach (Mechanical Arms),
  Analyst (Better Scanner), Climber (Climbing Gloves), Electrician (Lithium Batteries) and Marksman (Sleight of Hand).
  The host reads 'Enable <name> Upgrade' from its LateGameUpgrades config once per lobby, when it starts with
  PreferOtherMod or else when the host first switches to it (a change there after that counts from the next lobby): an
  upgrade switched off there stays in LethalRPG, and an entry that is missing counts as on (LateGameUpgrades' default)
  with a warning in the log. Medic stays beside Medical Nanobots, and unique and ultimate skills are never left to
  LateGameUpgrades.
- New host setting PriceShareCap in '0. General' (1 to 64, default 4): with SplitUpgradePrices a price is divided by at
  most this many players, so prices no longer drop without end in large lobbies (with 16 players a $75 upgrade cost $5
  each, now $19); 1 means everyone pays the full price. Lobbies of up to 4 players see and pay the same as before. The
  prices in the terminal and the skill menu, the note next to the credits, the cost of the missing levels of a saved
  build and the purchase itself all use the cap; 'rpg info', the Rules tab and the rules line of the log and of 'rpg
  report' name it. Tested in LAN with 5 players (MoreCompany, automated, on one PC); larger lobbies and Steam lobbies
  are not tested yet.
- From 5 players with LethalRPG in a lobby (MoreCompany), 'rpg crew' and the Crew & Build tab of the skill menu list one
  line per player, by slot: slot, name (long names are shortened to fit the terminal), budget, XP, levels and skills,
  instead of a block for each player that filled several terminal screens. 'rpg crew 3' (or 'rpg crew p3') shows the
  player in slot 3 in full, and so does a click on that player's line in the skill menu; for an empty slot or a player
  without LethalRPG it says so. Up to 4 players both look as before. Tested in LAN with 5 players (automated, on one
  PC); more players are not tested yet.
- With MoreCompany, or in a lobby with other than 4 player slots, LethalRPG's first log line of a lobby and the bug report
  ('rpg report', WRITE BUG REPORT) name the slots, e.g. '5 players, slots 32 (MoreCompany)'.
- Balancing Generous is now a longer progression rather than a power fantasy: six more upgrades stop at a cap. With the
  default settings on Normal: Vitality at 8 levels (260 HP instead of 400), Medic at 8 (1.6 HP/s instead of 3), Recovery
  at 8 (+160% instead of +300%), Strength at 7 (84% less weight penalty instead of 100%), Brawler at 8 levels of half a
  point of damage each (5 damage per hit with a shovel instead of 10) and Inventory at 8 (12 slots instead of 16).
  Brawler's half points add up over the hits on the same monster: at level 5 (+2.5) a shovel deals 3 and 4 damage in
  turn, and the pages and 'rpg brawler' show '3.5 dmg/hit'. On Generous, Concussive Blows asks for Brawler 4 and Coil
  Breaker for Brawler 6, on Normal the damage Brawler 2 and 3 have on Standard. The caps limit the effect, also on Easy
  (effects x1.2), and so do the caps Agility, Jump, Climber and Long Reach already had (10, 6, 7 and 8 levels): on Easy
  Vitality, Medic and Recovery stop at 6 levels (244 HP, 1.44 HP/s, +144%), Agility at 8 (+48% instead of +60%), Jump at
  5 (+36% instead of +43%) and Long Reach at 6 (+108% instead of +144%), and Strength, Brawler and Climber keep the
  levels of Standard, because Generous would not end above them: Strength 5 (72%), Brawler 3 whole levels (5 damage per
  hit, Concussive Blows and Coil Breaker ask for Brawler 2 and 3), Climber 5 (+120% instead of +168%; above +140%
  climbing outruns the game's ladder check). Generous never ends below Standard. Inventory stays at 8, and Hard keeps
  the levels. As before, the last level of these upgrades costs what the last level costs on Standard, so with fewer
  levels in between each level costs a bit more than before (Vitality level 2 $95 instead of $84; all 8 levels $1548,
  the 15 levels before cost $2860). The other upgrades keep their Generous levels as before; Standard and Strict are
  unchanged. Brawler's `EffectPerLevel` setting now goes down to 0.5; a fraction per level adds up the same way on every
  preset, a whole number deals whole damage as before.
  **Saves with higher levels:** levels bought on Generous above the new caps stay in the save, but they only work up to
  the cap (Vitality 15 gives 260 HP; on Easy Agility 10 works as 8, Jump 6 as 5, Long Reach 8 as 6 and Climber 7 as 5).
  'rpg vitality' and the skill menu's info box say so, e.g. 'Level 15, effect capped at 8 by Generous', and the saved
  build ('rpg build', 'rpg build save', the Crew & Build tab) names the cap, e.g. 'Vitality 15 (capped at 8)'; the
  pages, 'rpg stats' and the crew view show the level and the effect that work. Brawler levels bought on Generous count
  half a point each now: Brawler 9 works as 8 (5 damage per hit), Brawler 7 deals 4.5 per hit instead of 8. There is no
  free refund: 'rpg refund' resets your upgrades and gives back RefundPercent (50% by default) of what you paid, as
  always, also for the levels above the cap. In team mode 'rpg refund' only resets your own skills, as always: the
  crew's upgrades stay, capped ones included.
- The skill box: Danger Sense, Bio Monitor, Compass, Prospector, Second Wind, Adrenaline, Lightning Rod and Rallying
  Presence now show in a box in the top right of the HUD, below the space of the game's control tips (all four lines,
  so it does not jump when their number changes) and flush with their text, in Lethal Company's look (orange labels
  and frame on a dark background, header 'SKILLS'). It has a row only for skills with something to say right now,
  disappears without them, stays hidden in orbit (it shows from the landing on), hides with the HUD and is gone while
  you use the terminal. A value getting shorter (e.g. after picking up scrap) never makes it narrower, so the labels do
  not move sideways; it only gets narrower when a row goes away or after it was hidden. The values' colours mean
  something, and their words say the same without colour: DANGER shows a grey 'WATCHING' on a moon while nothing hunts
  you and 'HUNTED BY THUMPER' in blinking red while a monster does (no green 'safe': Danger Sense does not notice every
  threat). BIO-SCAN is a traffic light: CLEAN green, INFECTED yellow up to 39%, orange from 40% and red from 80%
  (where the game's fever starts), HEALING in green under the shower, BURSTING in blinking red with 'USE WEED KILLER'.
  EXIT shows the way out in words ('23m AHEAD', '12m LEFT', '40m BACK RIGHT', '8m BEHIND'), SCRAP what is left
  ('IN 5 $320  OUT 2 $90', 'NONE LEFT'), 2ND WIND the
  charges left (green, grey at 0), then ADRENALINE (yellow), LIGHTNING ROD (light blue) and RALLY: 'RALLIED' in green
  while a teammate's Rallying Presence reaches you. With the skill yourself, RALLY shows as soon as another player is in
  the lobby: how many living teammates are in its range ('2 IN RANGE' in green, a grey '0 IN RANGE' while none), and
  'RALLIED, 2 IN RANGE' when both apply; playing alone, there is no row for it. Your health
  above 100 HP and short messages such as the XP for a kill stay in the status line below the weight; ShowStatusHud
  switches off only that line, not the box. New
  setting SkillBoxPosition in '5. Interface (this PC)': TopRight (the default) or BelowWeight, the lines below the
  weight as before, e.g. if another HUD mod uses the top right. It applies at once, also from LethalConfig, and is
  never synced. If the box keeps failing, it switches itself off until the next lobby and the skills show as lines
  below the weight.
- The game's 'file corrupted' screen no longer deletes your saves. When the game cannot load or save its settings file
  (LCGeneralSaveData, e.g. after a crash while it was saving, or while another program holds it), its only button
  deleted that file and all three saves. With LethalRPG the button first copies the game's files (the saves included)
  and your LethalRPG profile into a new folder `LethalRPG Backups\<date>_<time> damaged settings` in the game's save
  folder, then resets only the settings file (your settings, key binds, the selected save file and the game's rank are
  in it) and closes the game as before (after 8 s); the saves stay, even if the copy fails. The screen says so before
  you press the button, and afterwards what happened. If another mod also changes this button, the screen keeps the
  game's own text. This happens on each player's own PC.
- Skill prices in the terminal now read '40 XP', as in the skill menu and everywhere else (before: '40xp' on the pages
  'rpg unique' and 'rpg ult' and in a skill's details such as 'rpg packmule').

## 0.7.2

Every player must update: no · Config: unchanged · Saves/profile: compatible

- The package page links to the issue repo github.com/feedback4cucu/LethalRPG (no code), where bugs, problems with
  other mods and balance feedback have their own forms; 'Reporting a bug' in the README links to them.
- A patch group that fails to load (usually a conflict with another mod) no longer leaves half of its patches behind:
  they are removed again, so the other skills and other mods' patches on the same game methods keep working.
- A bug report written on a client ('rpg report', WRITE BUG REPORT) no longer lists the players who joined before that
  client as the host.
- Players with an incompatible LethalRPG (0.6.x or older, or another minor version) no longer count for the crew: a 0.6.0
  player in a 0.7.x lobby got a quota share in a budget nobody could spend and lowered the split upgrade prices of the
  others. The host now ignores their profile and XP and refuses all their purchases, and the notices say that LethalRPG is
  off for them in this lobby. 'rpg crew' and CREW & BUILD now list them under 'Without LethalRPG, another version or
  still joining'.
- 20 inventory slots plus a Utility Belt bought before the host switched it off no longer break the HUD: the belt's slots
  had no frames, and switching to one threw an error on every player's PC.
- The Lightning Rod line on the HUD (ROD ACTIVE) now also shows in combined weathers with a storm from WeatherRegistry
  or WeatherTweaks. The skill itself already worked there.
- Very large saves: the warning about a large sync names Steam's limit of 512 KB per message (it said 1024 KB). Above
  it, Steam drops the sync and clients get no updates; the host now logs an error once per lobby, and 'rpg report' (WRITE
  BUG REPORT) lists it under problems.
- XP a player earned while joining, before their profile had arrived at the host, was lost when the crew got fired (or
  with 'rpg debug reset') in that moment: XP survives a game over, this XP now too.
- Hosting again in the same game run no longer keeps the inventory mods of the last lobby's players: the Inventory
  upgrade could stay off in the new lobby until someone joined or left.
- A bug report written on a client no longer calls a player 'profile ignored' because of a lobby that PC hosted before.
- The host ignores purchase results and notices sent by a client. Only a modified client sends those, and it could show
  any text on the host's screen.
- Danger Sense also warns about a monster of another mod that has no name ('HUNTED BY MONSTER'). Before, such a monster
  hunting you caused an error several times a second until Danger Sense switched itself off, and when the host
  controlled it you got no warning at all. The XP for killing it now says 'monster' too.
- A Utility Belt bought before the host switched it off now also keeps its 3 slots when the game puts it into its own
  utility slot (TAB), as it does while that slot is free. Before, LethalRPG removed them again there: with 20 slots the
  belt added none, its 3 extra slot frames lay on top of the others, and dropping it threw out what your last 3 slots
  held.
- Field Hacker no longer shows 'Override door : [Hold E]' while you walk through an open big security door or look
  through its opening: before, the prompt stayed until you had passed the door. To close an open door, look at the door
  beside its opening (what still shows of its panels, or its striped frame), from up to 4 m away. Closed doors work as
  before. While the facility's power is off, big doors stand open and count as open; they cannot be closed until the
  power is back, also not at the terminal (the game refuses it).

## 0.7.1

Every player must update: yes · Config: compatible, new entries with defaults (`SplitUpgradePrices`, off; the section of the new skill Coil Breaker) · Saves/profile: compatible

- New ultimate skill Coil Breaker (Concussive Blows + Brawler 3; 180 XP or $1000): hold E while looking at a coil head
  up to 4 m away (2 s, faster with Handyman) to shut it down for 30 s. It slumps over, cannot hurt anyone and can be
  walked through, and everyone sees it, also players without LethalRPG. Then the skill recharges for 5 minutes, counted
  by the host for each player and shown in the prompt, also right after leaving and rejoining. Its config section has
  `Seconds`, `Duration` and `CooldownSeconds`; Balancing Generous and Strict scale them.
- Field Hacker and Coil Breaker share the crosshair: of a turret, mine, spike trap, big door or coil head in reach, the
  one you look at gets the hold-E prompt; a coil head in a doorway wins over the door, and of two coil heads standing
  together the one under the crosshair wins over a closer one just beside it.
- New setting `SplitUpgradePrices` (off by default): with `Currency = GroupCredits` and personal upgrades every player
  pays only their part of a body or tool upgrade, the price divided by the players in the lobby who have LethalRPG,
  rounded up (e.g. $25 each instead of $75 with three players). When everyone buys a level, the crew pays one price, as
  in team mode where one upgrade counts for everyone, but everyone picks their own upgrades. It is the group-credits
  counterpart of `FullShareEach`. Skills paid with money keep the full price, as in team mode. Prices follow the lobby:
  they drop when someone joins and rise when someone leaves; players without LethalRPG do not count.
- The terminal, skill menu, stats, day report and saved build show the part you pay ("Credits $600 (upgrades 1/3
  price)"); `rpg <name>`, the menu's info box and purchase messages also show the full price ("$25 (1/3 of $75)"). A
  respec gives back RefundPercent of what was actually paid. 'rpg info' and the Rules tab explain the split, or say that
  everyone pays the full price, and the refund line now says "of what you paid".
- Stamina no longer makes the pause after an empty bar longer: you can sprint again as soon as you have recovered as much
  sprint time as without upgrades (the bar then shows less than 30%, e.g. about 16% with +225%), and jumping comes back
  correspondingly earlier. A bar emptied below 10% (jumping at the end of a sprint) refills that part at the normal pace.
  A full bar lasts as long as before, and stamina does not regenerate faster overall. 'rpg stats' shows the real sprint
  length (sprinting stops at 10% of the bar).
- Field Hacker now targets the whole big security door: look anywhere at its panels (at an open door, at the opening
  where they close) or a little past their edges, from up to 4 m away, measured to the panels. Before, only a spot on
  the floor in the middle of the doorway counted, so you had to look down at the threshold. Standing in the doorway or
  walking past the door shows no prompt unless you look at it; looking along the panels (within 5 degrees) never
  counts, so the prompt no longer comes and goes with a fraction of a degree next to them. A turret, mine or spike trap
  you look at still wins over a door behind or around it.
- Lightning Rod: picking up an item that already crackles with static now also stops the sparks and the crackle for
  everyone (the strike was already called off), and a strike can no longer hit such an item in the very frame it is
  picked up. `ROD ACTIVE` also counts metal tools in the utility slot.
- Extra inventory slot frames no longer flash on top of the last hotbar slot for a moment before moving into place (seen
  right after joining, when a player with a bigger inventory switched slots).
- Terminal pages ('rpg', 'rpg tools', 'rpg unique', 'rpg ult') and 'rpg stats' line up their columns for long names and
  two-digit levels (Balancing Generous) and never get wider than the terminal's 51 characters. Field Hacker's short text
  is now "hack traps & doors"; the team mode note no longer wraps.
- Decimal numbers (Medic HP/s, sprint time, rules) always use a decimal point, also on systems with a comma locale.
- Errors in LethalRPG no longer break the game around them: each skill, HUD line, the skill menu and the per-frame player,
  item and storm code catch their own errors, log each one once and, after 10 errors, switch themselves off until the next
  lobby with a message. 'rpg info' and the Rules tab list what was switched off. Before, an error in the movement code
  could stop the player every frame, and one broken HUD line took the lines below it along.
- A terminal or skill menu command that fails shows "LethalRPG error, see log" and the terminal keeps working; a client's
  request that fails on the host now gets an answer instead of none.
- Second Wind and Blast Shield use no charge when another mod already prevented the death or cancelled the hit, and they
  judge damage after other mods reduced it. If a save fails with an error, the player dies as without the mod and keeps
  the charge; an error in the message shown after a save no longer turns it into a death.
- The version check no longer needs the exact same version: players need a compatible LethalRPG, meaning the same
  network protocol and the same minor version (0.7.1, 0.7.2 and 0.7.5 work together, 0.7.x and 0.8.x do not), because
  patch versions only fix bugs. Another patch version no longer triggers the warning; 'rpg info' and the Rules tab name
  it instead. Another minor version, LethalRPG 0.6.0 and older (no protocol), any other protocol and a version that
  cannot be read still get the warning, which now names the rule (e.g. "Everyone needs LethalRPG 0.7.x.", with the newer
  of the two minor versions; for a test build of the unpublished 0.7.0 "Everyone needs LethalRPG 0.7.1 or a later
  0.7.x."). A version name with other characters than letters, digits, dots and dashes is shown as "an older LethalRPG
  version".
- Players with LethalRPG 0.6.0 or older cannot buy, respec or rebuild from a 0.7.1 host: their version cannot receive
  the host's upgrades (it knows one fewer), so nothing they bought would work on their PC. They are told to update
  instead of the crew's credits paying for it; the host keeps the levels they already have. The other way round, a
  player in the lobby of a host with 0.6.0 or older (or another incompatible version) no longer sends purchases,
  refunds or rebuilds the host would book without the player ever seeing them: the terminal and the skill menu answer
  right away and name the rule (e.g. "Everyone needs LethalRPG 0.7.x.").
- The host sends one sync per frame with everything that changed instead of one per change: the end of a day with four
  players now sends one update instead of about a dozen (around 170 KB instead of 2 MB on Generous). Purchases still
  arrive together with the new levels. Changing only your own interface settings (menu key, status line, day report)
  no longer resends the rules to everyone.
- If a client cannot use the host's data (for example an incompatible version), it now shows a message instead of only
  writing to the log. From a host with the same version it asks whether everyone uses the same build (test builds).
- Players without LethalRPG (and players whose LethalRPG profile has not arrived yet) no longer count for payouts: they
  get no quota share, no quota or survival XP and no part of the team budget when upgrades become personal. With `Even`
  the crew no longer loses a part of the share to a budget nobody can spend; with `ByContribution` only the crew's scrap
  counts. 'rpg crew' and the Crew & Build tab name them on one line ("Without LethalRPG (or still joining)") instead of
  showing an empty block, the host's log says why they got nothing (by slot, e.g. "P3 (4f20c1) has no LethalRPG: no
  share"), and 'rpg info' says that only employees with LethalRPG get a share.
- With `Currency = GroupCredits`, while someone else is at the terminal, upgrades, rebuys and respecs that change the
  credits only go through when typed in the terminal itself; the skill menu answers "Someone is using the terminal. Buy
  there or wait a moment." Before, a store order made at the same moment could make the upgrade free or lose the order.
  Skills paid with XP and personal budgets are not affected, nor is the host at its own terminal. 'rpg info' and the
  Rules tab mention it. A player who leaves the game while at the terminal (crash, kick) does not keep these purchases
  waiting, and only a player sitting at the terminal does (not one on a ladder or swinging a shovel).
- Profile backups: before a change makes your permanent profile smaller (less XP in total or fewer skills, e.g. a
  respec, a wipe or a host with another version), the old profile is kept next to it (`LethalRPG_Profile.bak1` to
  `.bak3`, newest first). New command `rpg profile` and a Profile box in the Stats tab list them with date and reason;
  `rpg profile restore <n>` and its Restore button put one back. It is used from the next lobby you join, and the profile
  it replaces becomes backup 1; the other backups keep their order, so none is lost or kept twice. If a backup cannot be
  written, the backups already moved are put back as they were. A profile that cannot be read now shows a message instead
  of only a log line.
- The host keeps a copy of the save's upgrades (`LethalRPG_Data.bak` in the save) before upgrades switch between
  personal and team, before a reset (game over, `rpg debug reset` or `wipe`) and before a save from an older version is
  first written in the new format. With DebugCommands, `rpg debug restore` and the Restore save button in the Rules tab
  swap it with the current upgrades in orbit (a second restore undoes it); XP and skills of the players in the lobby
  stay as they are.
- Profiles and saves now carry a format number. A file written by a newer LethalRPG with a newer format is never
  overwritten: you get a message, and the session runs without it. Fields and skills from a newer LethalRPG are kept
  when this version saves; they are not sent to other players, so the network format is unchanged. This protection
  starts with 0.7.1: going back to 0.6.0 or older can still lose skills that version does not know and the XP spent on
  them.
- Test commands for hosts (DebugCommands): `rpg debug budget` and `rpg debug xp` take a player, `all` or a slot
  (`rpg debug xp 100 2`; P1 is the host). `rpg debug as <slot> <command>` buys, refunds or rebuys for another player as
  if they had used their skill menu. `rpg debug endday` books LethalRPG's part of the end of a day (scrap, kill and
  survival XP) without ending the game's day. `rpg debug crew` shows every player's slot, version, profile state, budget
  and XP without names. The Rules tab has the same: a player choice (with one other player chosen, every tab shows and
  acts for them; with All, the buttons in every tab act for the whole crew, like `rpg debug as all`, whatever the host
  owns or can afford), End day and Crew state.
- New command `rpg report` and a Write bug report button in the Rules tab: they save a bug report as
  `BepInEx/LethalRPG-report-<date>_<time>.txt` and copy it to the clipboard. It holds the versions of LethalRPG (with its
  build), the game and BepInEx, host or client, Steam or LAN, the rules in effect and the config entries that differ from
  their defaults (on a client also its own rules where they differ from the host's), failed patches, parts switched off
  after errors, profile and save problems, the players by slot with their version and profile state, the loaded plugins
  and LethalRPG's last 200 log lines. Players appear only as P1, P2, ... with a short code of their identity; names,
  Steam IDs and the Windows user folder are left out. 'rpg info' and the Rules tab point to it.
- LethalRPG's log lines no longer name players by their Steam name: they use the slot and a short code of the identity
  instead, e.g. "P2 (3fa1c9) bought stamina level 1 for 50", which also shows a player who comes back in another slot.
  Messages in the game keep the names.
- The first LethalRPG line of every lobby in the log sums up the session: version and build, host or client, Steam or
  LAN, players, protocol, the host's rules, game version, detected mods (LateGameUpgrades, inventory mods, ...) and failed
  patches. A client logs the host's rules when its first sync arrives.
- Every build carries the git commit it was made from: "0.7.1+a1b2c3d" in the log, the bug report and the DLL's product
  version; the version itself (BepInEx, manifest, version check) stays "0.7.1". Clients tell the host their build, so two
  test builds of the same version show up as another, compatible build (a log line and a note in 'rpg info') instead of
  going unnoticed.
- LethalRPG checks the game version: on a game older than v80 it says that it needs v80 or newer, on a game newer than
  v81 (the version it was tested with) that it was tested on v81, each once per game run (a message still waiting when
  you leave the lobby comes in the next one). Both are logged as warnings, and 'rpg info' and the Rules tab show them
  too.
- The terminal's `other` page now lists `>RPG`, so players who got LethalRPG with a modpack find it; commands that other
  mods add to that page stay. The day report (when you got budget or can buy something) and the payout's answer end with
  "Spend at [K] or terminal: RPG".
- The skill menu key is shown as your keyboard labels it (in the menu's footer, on the terminal pages and in the new
  hint), so a key on another keyboard layout is no longer named by its US position. Without a menu key the footer and
  the terminal no longer show "[None]".
- Inventory mods on a client's PC now count too: clients tell the host about theirs, and the Inventory upgrade is off for
  everyone while such a player is in the lobby, with a message to everyone (before, only the host's mods counted). Slots
  are only resized when both the host and your own PC allow it, so empty slots that another mod added are no longer cut.
  The maintained LethalProgression fork is now recognized as an inventory mod. LethalThings now turns Inventory off for
  everyone when the host has it with its Utility Belt on (`PouchyBelt` in the host's LethalThings config, on by default;
  off, the belt is not sold and Inventory works, and a belt bought before keeps its three extra slots while it is
  carried). On a client's PC it does not count: only the host's setting decides whether the lobby has the belt.
  'rpg debug crew' and the bug report name each player's inventory mod.

## 0.6.0

- New choice `FullShareEach` for the setting `ShareSplit`: with personal upgrades every player gets the whole quota share
  for their own body and tool upgrades (e.g. $187 each instead of $187 split between three), and the group credits pay
  it only once (when it is taken from them). That is what the share is worth in team mode, where one upgrade counts for
  everyone, so everyone progresses as fast as in team mode but follows their own priorities.
- Switching between personal and team upgrades with full shares: going personal, every player aboard gets the whole team
  budget; going back to team mode, the copies are not added up: every upgrade keeps its highest level as before, and the
  team budget is one share minus everything anyone bought with the shares (upgrades and skills, at least $0), so toggling
  cannot refill a budget.
- 'rpg info' and the Rules tab explain the payout with an example and say that only players in the lobby when the quota
  is met get a share. The descriptions of ShareSplit, SurplusSharePercent, TakeShareFromGroupCredits and UpgradeOwnership
  explain the options.

## 0.5.1

- Iron Grip now also works against the kidnapper fox: when its tongue drags you off, you keep all your items (and their
  weight), so you can hit the tongue to break free. A zap gun used against the fox while it drags you keeps working.
  Terminal aliases 'fox' and 'kidnapper' find the skill.

## 0.5.0

- New upgrades: Long Reach (grab and interaction range) and Bargain Hunter (cheaper store items, vehicles and ship
  upgrades for the buyer).
- New unique skills: Wristwatch (clock inside the facility), Prospector (scrap left on the moon on the HUD), Danger Sense
  (HUD warning while a monster hunts you) and Concussive Blows (shovel and sign hits stun).
- New ultimate skill: Field Hacker (switch off turrets, mines and spike traps or open big doors by holding E, like their
  terminal code).
- Skill menu can now do everything the terminal can: MAX button (buy as many levels as you can afford), respec in the
  Stats tab, a Rules tab with 'rpg info' and, with DebugCommands, the test commands. Clicks are applied in the next frame.
- New setting `Balancing` (Standard / Generous / Strict), separate from the difficulty: Generous gives up to three times
  the levels (only as far as they still do something, at most 20) with slower rising prices, faster lockpicking and
  hacking and stronger skills; Strict gives 40% fewer levels and weaker skills. Shown in 'rpg info', the Rules tab and
  the stats.
- Skills never ask for a higher prerequisite level than the upgrade's max level (e.g. after lowering MaxLevel).
- The status line shows your health when Vitality raises it above 100 (the game's display only shows damage below 100).
- Fixes from a full review:
  - Vitality above 100 HP is no longer reset on clients after a hit (the game's damage echo).
  - Buying Vitality or a Second Wind save that brings you to 20 HP or more ends a critical injury, so you no longer limp
    and bleed at full health for the rest of the day.
  - Surefooted no longer slows the sinking in quicksand twice (the game sends the sinking speed back to its owner).
  - Kill XP also counts kills of monsters a client controls, and hitting a corpse no longer takes the kill; Danger Sense
    works for every monster on every client.
  - Switching from team to personal upgrades happens when the ship is started next, so the team budget is split between
    the players aboard, not with players who only played this save once or are still joining.
  - Carrying scrap in and out of the ship no longer farms XP or payout share; rank XP counts once per day, also after
    the crew was fired.
  - Skill menu: pages with many entries fit again, long messages wrap, respec and MAX ALL only on the ship. Dropping,
    scanning, inspecting, Q/E item actions, lockpicking and hacking are blocked while it is open, and confirm buttons
    wait a moment so a double click cannot respec or wipe.
  - Terminal: names with several words, ambiguous beginnings list the candidates instead of buying the first match;
    'rpg buy' without a name explains itself.
  - Lockpicker and Field Hacker show the hold bar, no longer react while you look at an item, and Field Hacker needs a
    line of sight; Wristwatch only shows the clock inside the facility.
  - With UniqueSkillCurrency = Money, skills are learned and respecs done in orbit only, and the game is saved right
    away, so quitting later that day cannot keep a skill without its price; skills wait until your profile has loaded.
  - An inventory mod on the host turns the Inventory upgrade off for everyone.
  - A save whose upgrades cannot be read is no longer overwritten with empty data.
  - Several smaller fixes (profile saving after a read error, version check timing and false warnings for reused player
    slots, refund overflow, saved build only counts levels this ship offers, Adrenaline description).

## 0.4.0

- Team mode (setting `UpgradeOwnership`): money upgrades are shared by the whole crew and paid from a team budget that
  quota payouts go into. Unique and ultimate skills and XP stay personal.
- Switching to team mode keeps the highest level any player had of each upgrade and pools all budgets. Switching back gives
  every player the team's levels and splits the team budget evenly; from there everyone continues alone.
- In team mode `rpg refund` resets only your own skills; saved build, `rpg rebuy` and auto rebuy apply to the team.
  Terminal, skill menu, crew overview and day report show the team budget and the shared upgrades.

## 0.3.1

- Player README in English and German (gameplay only): requirements, controls, all upgrades and skills, budget and XP,
  settings, multiplayer, compatibility, credits, license (MIT) and disclaimer; LICENSE file in the package.
- Note: singleplayer is tested; multiplayer is still being tested.
- Plugin id is now `cucumis.lethalrpg`, so the config file is `cucumis.lethalrpg.cfg`. Earlier test versions used another
  id: remove them before installing this one, and copy over the old config file if you want to keep its settings.

## 0.3.0

- New unique skill Bio Monitor (needs Medic 2): on moons with cadaver growth the HUD shows "BIO-SCAN: CLEAN" or how far
  an infection has spread, whether you are healing, and the countdown once it is about to burst.
- Skill menu fits on one screen: one line per upgrade with a details box for the hovered one, stats and crew in two columns.
- HUD lines below the weight (infection, compass, status) sit directly below it and stack without gaps.

## 0.2.0

- More ways to earn XP, each with its own slider: scrap you bring to the ship, monster kills (by power level),
  surviving a day and completed quotas, plus a multiplier for the rank XP.
- Difficulty preset (Easy / Normal / Hard) that scales prices, XP gains and upgrade effects.
- Skill menu on a key (default K) in the game's look (3270 font, HUD color) with buy buttons, stats, crew overview and saved build.
- Terminal: `rpg stats`, `rpg crew`, `rpg buy <name> <n|max>`, `rpg build`, `rpg rebuy` (optionally automatic after each payout).
- Saved builds: the money upgrades of a run are remembered when the crew gets fired and can be bought again.
- Day report after each day: XP by source, budget and what you can afford now.
- HUD status line: Second Wind charges, Adrenaline, Lightning Rod, Rallying Presence, XP from kills.
- Version check between host and clients; warning for players without the mod.
- Patches are applied one class at a time: a conflict with another mod only disables the affected skills.
- Thunderstore package built by `dotnet build -c Release`.

## 0.1.0

- First version: 19 leveled upgrades, 9 unique and 6 ultimate skills, skill tree, surplus share and XP, permanent profile,
  respec, LethalConfig support.

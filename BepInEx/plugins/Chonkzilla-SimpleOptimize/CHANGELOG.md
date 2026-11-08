# Changelog

## 0.1.17

- **Fixed: as a client you could see the docked Company Cruiser poking into the ship room** (vanilla bug;
  Josh, client, friend hosting). While the Cruiser hangs on the magnet, every player's game holds it in
  place on its own, at a spot each game works out for itself (and on a save load in orbit, each game
  docks it again by itself). Nothing makes a client's spot match the host's, so on your screen the truck
  could sit partly inside the ship wall. Now, when the magnet pull finishes, your game asks the host for
  its docked spot and uses it. If the host doesn't run SimpleOptimize, your game instead pushes the truck
  out through the wall it is clipping, if it overlaps the ship room. Only where the truck is drawn on
  your screen changes; nothing else does. Logged once per correction as
  `[CruiserDock] docked cruiser moved to ...: offset (x, y, z) = N m`. No new settings.

## 0.1.16

- **Fixed: the terminal `scan` was never exact** (vanilla bug). After landing, the game doesn't add up
  the real scrap values: for each item it rolls a random number from the item's base price range and
  only loosely keeps it near the real value (the allowed gap grows with every item, and more upward than
  downward), so the total usually ran high. It now adds the real values. Also, between pulling the lever
  and the doors opening, the game counted every piece of scrap on the moon as "in the ship"; that line
  now only counts what is really in the ship. After landing, "outside the ship" means every piece of
  scrap not inside the ship room: on the ground, held by players, in a belt bag or in the Cruiser.
  Your terminal only, no network messages; works the same for host and clients. `[Fixes] FixExactScan` (on).

## 0.1.15

- **Fixed: you couldn't hear a late joiner while you were dead** (vanilla bug; Josh, hosting). Each
  player slot keeps a link to that player's voice. The game only ever fills in a missing link; it never
  clears one, not even when the player leaves. So when someone is kicked or disconnects and a new player
  joins into that slot, the slot can stay tied to the old player's voice (and, because the voice system
  reuses its voice objects, sometimes to another player's). Alive you don't notice: a voice nobody
  adjusts plays normally. But when you are dead the game has to change other players' voices (a dead
  player's voice becomes "in your head" at full volume instead of coming from their body), and with the
  wrong link that change goes to the wrong voice, so the new player stays silent for you. Now every
  player's voice link is checked against who is really in that slot 3, 8 and 16 seconds after anyone
  joins, 1 and 5 seconds after someone leaves, when you die, when you switch who you spectate, and every
  4 seconds while you are dead. A wrong link is cleared and the game's own voice refresh runs again. A
  leaving player's slot also drops its voice link right away, so the next player in it starts clean.
  Your machine only, no network messages; nothing changes when every link is already right. Logged as
  `[Voice] slot N (name, dead): <what was wrong>; re-linked to voice '...'`. `[Fixes] FixLateJoinVoice` (on).
- **Fixed: the dead players' list showed the previous player's Steam picture** (vanilla bug). The boxes
  on the left while you are dead are made once per player slot and only hidden between rounds, so a
  player who joined into someone else's old slot got that person's picture. The picture (and name) is
  now redrawn whenever the slot's player changed. (The pause menu list itself has no pictures in v81;
  the picture you see while dead is this list.) Your screen only. Logged as `[Avatars]`.
  `[Fixes] FixSpectatorAvatars` (on).

## 0.1.14

- **Fixed: the ship monitor sometimes showed the wrong name** (vanilla bug, any lobby). The monitor
  followed one player but labelled them as someone else, and the teleporter beamed whoever it was really
  following. Cause: turning on a radar booster re-sorts the monitor's list, and every time someone joins
  afterwards the game writes everyone's name by player slot number into the re-sorted list. Every monitor
  entry is now named after the player it really follows, after any change and before the terminal reads
  the names (`switch <name>` works again). Two players with the same name show as `Name` and `Name2`.
  Logged as `[Radar] monitor entry #N follows ... now 'X'`. `[Radar] FixRadarNames` (on).
- **The host lines up everyone's monitor order.** Switching the monitor is sent to everyone as a list
  position, so a player whose list was in a different order watched someone else. The host now sends its
  order to players running SimpleOptimize 0.1.14, so a switch lands on the same person for them. Players
  without it keep the vanilla behaviour. `[Radar] SyncRadarOrder` (on).
- Unknown names: a name fetched from Steam now goes on that player's monitor entry, not on whichever entry
  sat at their slot number (the same mistake as the vanilla bug above).
- **Fixed: a late joiner saw "unknown" for everyone except the host** (Josh, hosting). The game never
  sends names over the network: each player's game asks its own Steam, and Steam only names friends and
  people in the same Steam lobby. After the first landing everyone but the host has left that lobby, so a
  late joiner can only name the host (and earlier players see the joiner as "unknown" too). The host now
  follows up every join 2, 8 and 20 seconds later: it sends its own names to players running
  SimpleOptimize, who apply them at once to anyone still "unknown" (matched by Steam ID, never replaces a
  real name), and it re-sends the game's own per-player Steam ID message so every player, modded or not,
  looks the names up again once their Steam has caught up. Only the host needs it for the first part to
  reach SimpleOptimize joiners; a joiner WITHOUT SimpleOptimize still can't be told a stranger's name
  (the game has no message for it), the re-send only helps with names their own Steam can know.
  Logged as `[Names] host pass N/3: ...` on the host and `[Names] player N is X (from host)` on the
  joiner. `[Names] HostNameSync` (on), `[Names] ResendSteamIds` (on).

## 0.1.13

- **Faster item juggling moved out** into its own separate mod. The `[Juggle]` settings, the
  `/opt juggle` command and the input buffer / shorter switch and drop waits are gone from
  SimpleOptimize. Everything else is unchanged, including the 0.1.12 belt-bag fixes. Old `[Juggle]`
  lines left in `local.simpleoptimize.cfg` are ignored and can be deleted.
- **Picking items up before the lever moved out** to the same separate mod. The `[Fixes]
  GrabBeforeStart` setting is gone from SimpleOptimize; that mod copies your old value the first
  time it runs. During SimpleHQ tournaments pre-start grabbing is SimpleHQ's own (unchanged).
- **Fixed: joiners saw $0 on most items of a big hoard.** The host sends a joining player the value
  of every item on the map, but the game stops after about 500 items (scrap and gear alike, nearest
  the map centre first); everything past that showed $0 for the joiner. Host only, and vanilla
  joiners get the right values too. New settings `[Fixes] FixJoinScrapValues` (on) and
  `[Fixes] JoinSyncMaxItems` (4000, 500 to 20000). The values still go in the game's own single join
  message (about 8 bytes per item, ~32 KB at 4000; it can't be split without breaking vanilla
  joiners); past the limit the farthest items still show $0 and the host log says so.
- **Fixed: a cruiser seat stayed taken after its player disconnected or was kicked**, so nobody
  could drive or ride for the rest of the session. Every game now frees the seat itself when the
  player leaves (the same clean-up the game does when a player gets out normally), and the host
  takes back control of the truck if the leaver had it. No new network messages. `[Fixes]
  FixCruiserSeatDisconnect` (on). Best when everyone has it.
- **More sounds at once.** Unity's limit of 40 sounds playing together is raised to 128 once at
  startup, so busy moments stop cutting sounds out. Your machine only. `[Audio] RaiseVoiceLimit`
  (on), `[Audio] RealVoices` (128, 32 to 255), `[Audio] VirtualVoices` (512). Never lowers the
  limit. `/opt` shows the current voices.
- README: SimpleOptimize is described as what it is, a non-cheaty bug-fix and performance mod.

## 0.1.12

- **Fixed: a ghost copy of an item left floating after putting it in a belt bag on the ship** (the
  0.1.10 fix didn't work). The game takes a bagged item out of its parent and moves it far out of
  sight with the item's own per-frame update. The item keeps its "on the ship" flags, so the ship
  item sleeper put it back to sleep one frame before that last move. It stayed where the bag
  animation left it, next to the bag in mid-air, with no collision. Items in a bag are now tracked
  exactly and never slept.
- **Fixed: items taken out of a belt bag could be invisible but still grabbable.** While an item
  sat in a bag (still flagged as a ship item, but 3 km away), distance culling hid it. Taken out
  anywhere off the ship, it no longer counted as a ship item, so it was never shown again until
  someone picked it up. Bagged items are never culled now, any item the culler hid is shown again
  as soon as it stops being far-away ship hoard, and an item is un-hidden the moment it goes in or
  out of a bag.
- **`[BeltBag]` log lines** for every belt-bag add and remove, on host and clients. A
  `GHOST?` / `INVISIBLE?` warning appears if an item is still visible half a second after going
  in, or still hidden a second after coming out.
- **Drops are a bit slower with juggling on**, so they no longer feel instant. G now waits 0.15 s
  after a slot switch or a confirmed pick-up (vanilla 0.2 s; 0.1.11 used 0.1 s). This is a new
  setting, `[Juggle] DropDelay` (0.05 to 0.2). Pick-ups, scrolling and the input buffer are
  unchanged.

## 0.1.11

- **Faster item juggling** (`[Juggle]`, on by default, `/opt juggle on|off`). A pick-up, drop,
  scroll or utility-slot press that the game would silently ignore for being too early (right after
  a switch, during a grab, before the host confirmed your last drop) is remembered for 0.35 s and
  done as soon as the game allows, in the order pressed. The waits themselves are shorter: 0.1 s
  after a switch (was 0.2), 0.15 s between scrolls (was 0.3), half the settle after a confirmed
  grab. Grabs still wait for the host and drops for its answer; no new network messages; your
  machine only. Off during SimpleHQ tournaments unless `[Juggle] InTournaments = true`.

## 0.1.10

- **Fixed: putting something in a belt bag left a ghost copy behind**, on the ship and at the Company.
  The game animates an item into a bag by writing its target position every frame and letting the
  item's own update apply it; the ship-item optimizer was skipping exactly that update, so the item
  went in and the picture stayed put. Items in a bag, or on their way into one, are never slept now.

## 0.1.9

- The startup line no longer counts SimpleBuy: its commands are part of SimpleSell now.
- **Spray colours are per player now.** Everyone running SimpleOptimize sees each player's own
  colour on the marks that player sprayed, instead of the host's colour for everything. Vanilla
  players are untouched: they see the game's seeded colour, as always. Set yours with
  `/opt spray #RRGGBB` and it follows you into any lobby where the others run SimpleOptimize.

## 0.1.8

- **Fixed: cruiser cargo floating next to the ship.** Items in a magneted cruiser are kept in place
  by the game writing their position every frame, and the item sleeper was skipping that, so cargo
  drifted loose during the flight and hung in the air at the next landing. Cruiser cargo now only
  sleeps while the truck is unmagneted (driving around on a moon), and items are only snapped into
  place when they're parented to the ship itself.

## 0.1.7

- **The fast cargo dropship only works at the Company now.** On a moon, deliveries arrive at
  vanilla speed, so ordering gear mid-run is no faster than the game intends. The "waits until
  opened" guard follows it, since it exists to protect fast deliveries. `/opt dropship` says which
  applies where you are.

## 0.1.6

- **Tournament mode (SimpleHQ):** while a tournament runs, this mod's commands are off and its gameplay extras switch off, so the game plays as close to vanilla as possible. `/opt` is off, the cargo dropship goes back to vanilla speed and timing, and grabbing items before the game starts is reverted. The performance work and bug fixes stay on.

## 0.1.5

**Fixed: a dead body could cost you frames for the rest of the day.** A body's blood effect can be
pointed at a mesh with no surface, and the game then warns about it every single frame: 57,398
lines in one measured session, each one written to the log and the console. That emitter is now
switched off when it's spotted, which loses nothing, since it could never have drawn anything.
Turn it off with `[Fixes] FixZeroAreaParticleSpam`.

## 0.1.4

**Fixed: late joiners show up as "unknown".** Names aren't sent between players; each game
looks them up through Steam, and Steam only shares a name with friends or people in the same
Steam lobby. Everyone leaves that lobby when the ship first lands, so someone who joins later
(SimpleSell's late join) and the players already in the game can't see each other's names.
Your game now asks Steam for any missing name and puts it on the player, the pause menu list
and the radar. It fixes your own screen only, so every player who wants names needs
SimpleOptimize. Turn it off with `[Fixes] FixUnknownNames` in `local.simpleoptimize.cfg`.

## 0.1.3

- Credit line: written by Claude & Chonk.

## 0.1.2

**Fixed: weed killer error flood.** While you spray weed killer on a cadaver plant or mold,
the game keeps track of which plant it's hitting. If that plant got destroyed first, by
another player or the growth shrinking, the game threw an error every single frame until
you stopped spraying. One measured session logged 25,691 of them.

Now the game forgets the destroyed plant right away, so it happens once instead of thousands
of times. You can turn this off with `[Fixes] FixWeedKillerIndexSpam` in
`local.simpleoptimize.cfg`.

These floods build up over a session, which is why lag could come back after a few runs and
go away after a restart.

## 0.1.1

- **Commands now work with `!`.** You can type `!opt`, `!opt spray #FF69B4`, and so on. If
  `/opt` replies "unknown command", another chat mod is grabbing `/` first, and those mods
  ignore `!`.
- **Fixed: magnet lag with a big hoard.** When the cruiser attached to the ship, the game
  wrote three log lines for every item within 25 m. A few hundred items meant around 700
  lines each time, measured at 772 ms. That logging is now skipped.
- `/opt` also writes its numbers to the BepInEx log, so you can read them after a lag spike.

## 0.1.0

First release.

- Better performance with big hoards: item sleeper, shadow remover, instant settle and
  distance culling.
- Fast cargo dropship (`/opt dropship`) with speed levels, and an option to keep it waiting
  until it's opened so you can't miss an order.
- Spray paint colour (`/opt spray`), shared with other players who have SimpleOptimize.
- Bug fixes: cadaver kill lag, spider web slow, stuck interact, cruiser yeet, ship item cap,
  early grab and throw error spam.

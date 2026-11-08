# SimpleOptimize

*Written by Claude & Chonk*

A non-cheaty bug-fix and performance mod for Lethal Company v81, built for runs where hundreds
of collected items tank the frame rate. It fixes things the game gets wrong and makes big hoards
cheaper to draw; it doesn't change how fast you play or give you an edge over vanilla players.
The few convenience extras (cargo dropship and ship landing at the Company) are switched off
during SimpleHQ tournaments.

**Anyone can install it.** Almost everything runs only on your own machine and doesn't
affect other players. The exceptions are called out below.

## Commands

Type these in the in-game chat.

| Command | What it does |
|---|---|
| `/opt` (`/optimize`) | Status: items sleeping, shadows removed, items culled, frame time and FPS, and which features are on |
| `/opt sleep on\|off` | Item sleeper |
| `/opt snap on\|off` | Instant settle for items dropped aboard |
| `/opt shadows on\|off` | Shadow remover |
| `/opt cull on\|off` | Hides ship items when you're far away |
| `/opt yeet on\|off` | Cruiser launch fix |
| `/opt spray` | Spray colour status, and how many colours are available |
| `/opt spray <index>` | Use one of the game's own spray colours. Your colour is yours: other players running SimpleOptimize see it on the marks you spray, and you see theirs |
| `/opt spray #RRGGBB` | Use any colour, for example `/opt spray #20B2AA` |
| `/opt spray off` | Back to the normal colour |
| `/opt dropship` | Cargo dropship status and the speed table |
| `/opt dropship on\|off` | Fast cargo dropship |
| `/opt dropship speed <1-4>` | 1 is normal (40s), 2 is 20s, 3 is 13s, 4 is 10s to arrive. Only at the Company: deliveries on a moon stay vanilla |
| `/opt dropship wait on\|off` | Keeps the dropship waiting until someone opens it |
| `/opt transit on\|off` | Fast player ship landing and takeoff (off by default) |

`/opt cargo` and `/opt paint` work the same as `dropship` and `spray`.

**Every command also works with `!`**, such as `!opt` or `!opt spray #FF69B4`. Use it if `/opt`
replies "unknown command". That message comes from another chat mod grabbing every `/`
command before SimpleOptimize sees it, and those mods ignore `!`.

Settings are saved in `local.simpleoptimize.cfg`.

## Performance

1. **Item sleeper.** The game updates the position of every item that isn't being held,
   every frame, forever. Items resting aboard now skip that update, and wake up the moment
   they're grabbed, moved or used.
2. **Shadow remover.** Resting items stop casting shadows, and get them back when picked up
   or taken off the ship. Hundreds of shadows is the biggest graphics cost of a big hoard.
3. **Instant settle.** A falling item can't sleep, so after a big merge or organize, hundreds
   of items spend seconds dropping into place. Items dropped aboard now land instantly.
   Items dropped outside still fall normally.
4. **Distance culling.** While you're out collecting, items on the ship stop being drawn
   once you're more than 45 m away, and reappear as you come back.

Items in a belt bag are left alone by all four: the game moves a bagged item out of sight on
its own every frame, and takes it back out the same way. Every belt-bag add and remove writes a
`[BeltBag]` line to the BepInEx log, with a warning if an item is still visible after going in
or invisible after coming out.

## Cargo dropship

Normally you wait **40 seconds** for a terminal order to arrive, and the dropship leaves
after **30 seconds** whether or not anyone opened it. If it leaves unopened, you lose the
items and the credits.

SimpleOptimize makes it arrive faster and, by default, **keeps it waiting until someone
opens it**, so you can't miss an order you paid for. Once it's opened, it leaves at your
chosen speed.

> **Only the host needs this.** The host controls the dropship timer, so players without
> the mod still see it arrive early, with nothing out of sync.

## Spray paint colour

The game picks each spray can's colour at random. `/opt spray` lets you choose it.

> **Only players with the mod see your colour.** The game never sends spray colour between
> players, so anyone without SimpleOptimize sees the original colour on the same cans and
> the same paint. Nothing breaks. You just see different colours.
>
> Players who have SimpleOptimize get the host's colour automatically when they join and
> whenever it changes. To keep your own colour, set `[Spray] AcceptHostColor = false`.

Picking one of the game's colours always works. A custom colour depends on the paint
material supporting it. If it doesn't, the command tells you instead of silently doing
nothing.

Paint that's already on a wall keeps its colour. Only new spray changes.

## Bug fixes

| Fix | What it fixes |
|---|---|
| Cadaver kill lag | Killing a cadaver plant made the game throw an error and write several debug lines. Clearing a growth turned into thousands of them: 2,110 errors and a 24 MB log in one measured session. |
| Spider web slow | Breaking a web while you're caught in it left you permanently slower, and it stacked each time. |
| Stuck interact | A lost network message could leave you unable to interact, scroll or use items until you rejoined. Now it clears itself after 2 seconds. |
| Cruiser yeet | Teleporting into the cruiser, or walking into it on TZP, could launch it across the map. |
| Ship item cap | Raises the limit of 400 items saved on the ship. |
| Throw error spam | Silences a flood of harmless errors caused by other players dropping items. |
| Magnet lag | When the cruiser attached to the ship, the game wrote three log lines for every item within 25 m. With a big hoard that took 772 ms. Now skipped. |
| Weed killer error flood | If the plant you were spraying got destroyed first, the game threw an error every frame until you stopped: 25,691 in one measured session. Now it happens once. |
| Blood particle flood | A dead body's blood effect can be aimed at a mesh with no surface. The game then warns every frame: 57,398 log lines in one session, and a real frame cost for the rest of the day. That emitter is switched off. |
| Joiners see $0 scrap | When someone joins, the host sends them every item's value, but the game stops after about 500 items (gear counts too), so on a big hoard the rest show $0 for the joiner until the next save. The limit is now 4,000 (`[Fixes] JoinSyncMaxItems`). **Host only**: the host's mod fixes it for every joiner, modded or not. |
| Cruiser seat stuck after a disconnect | If someone left or was kicked while sitting in the cruiser, their seat stayed taken: nobody could drive (or sit in the passenger seat) for the rest of the session. Now each game frees the seat the moment that player leaves, and the host takes the truck back. No extra network messages. **Everyone should have it**: each player's game frees its own copy of the seat. |
| Can't hear a late joiner while dead | When someone joined into a slot a kicked or disconnected player had, the game could keep that slot tied to the old player's voice. Alive you heard them normally, but once you died and were spectating you couldn't hear them. Now every player's voice link is checked a few seconds after any join, after a disconnect, when you die or switch who you spectate (and every few seconds while dead), and any wrong link is fixed with the game's own voice refresh. Your machine only, no network messages. `[Fixes] FixLateJoinVoice` (on), logged as `[Voice]`. |
| Wrong Steam picture in the dead players' list | The boxes on the left while you are dead are made once per player slot, so someone who joined into another player's old slot showed that player's picture. The picture and name are now redrawn when the slot's player changed. Your screen only. `[Fixes] FixSpectatorAvatars` (on), logged as `[Avatars]`. |
| Too few sounds at once | The game only plays 40 sounds at once, so in busy moments footsteps, doors and enemies cut out. Raised to 128 (`[Audio] RealVoices`) once at startup. Your machine only. |
| Unknown names | Players who joined late showed up as "unknown" (a late joiner saw only the host's name). Steam only shares names between friends or people in the same Steam lobby, and everyone but the host leaves that lobby when the ship first lands. Now your game asks Steam for the missing names, and a host running SimpleOptimize sends its own names to joiners who have it (2, 8 and 20 s after each join) and re-sends the game's own Steam ID message so everyone looks the names up again. `[Fixes] FixUnknownNames`, `[Names] HostNameSync`, `[Names] ResendSteamIds` (all on). Best with SimpleOptimize on the host AND the joiner; a joiner without it can still only name friends and people in its Steam lobby. |
| Wrong names on the ship monitor | The monitor followed one player but showed another's name, so the teleporter beamed the "wrong" person and `switch <name>` picked the wrong one. A vanilla bug in any lobby: turning on a radar booster re-sorts the monitor's list, and every later join writes names by player slot into the re-sorted list. Each entry is now named after the player it really follows (two players with the same name show as `Name` and `Name2`). The host also sends its monitor order to players who have SimpleOptimize, so a switch shows the same person for everyone. `[Radar] FixRadarNames`, `[Radar] SyncRadarOrder` (both on). Fixes your own screen, so each player needs SimpleOptimize 0.1.14; players without it keep the vanilla behaviour. |

These floods of errors and log lines build up over a session. That's why lag can creep back
after a few runs and go away when you restart.

## Fast player ship (off by default)

Speeds up your own ship's landing and takeoff. This is **off** by default, and it isn't
the same thing as the fast cargo dropship.

> **Needs everyone in the lobby.** Each player's game plays the landing on its own, so this
> only turns on when every player has SimpleOptimize, or you're playing solo. It checks
> automatically and switches itself off if someone joins without the mod.

## Install

Drop `SimpleOptimize.dll` into `BepInEx/plugins`, or install with a mod manager. Requires
BepInEx 5.4.2100. Every feature can be turned off in `local.simpleoptimize.cfg`.

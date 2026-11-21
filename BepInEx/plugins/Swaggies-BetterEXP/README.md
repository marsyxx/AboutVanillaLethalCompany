# BetterEXP

**BetterEXP** is a Lethal Company mod looking to spice up the current experience system in the game. Right now the XP system feels a bit clunky and not a good (enough) measure of a player's true experience working for The Company, so I've created BetterEXP to hopefully make the XP system feel much more satisfying and rewarding/punishing, depending on your actions. *Thanks for 800K! :3*

BetterEXP is **client-sided**, it will run independently in lobbies you create or join, and should function just fine in vanilla lobbies.

For feature requests, bug reports, or anything else, find me on Discord `@swaggies` :3

![Showcase](https://github.com/Swaggies/LCModAssets/blob/main/v2/v2%20performance.gif?raw=true)
![Round Summary](https://github.com/Swaggies/LCModAssets/blob/main/v2/v2%20round%20summary_main.png?raw=true)

## Reworked EXP System
- You will now gain EXP primarily based on scrap collected, and your ability to fully clear the facility of scrap.
- The higher the moon value, the more EXP potential, but only if you can loot well!
- Depending on how much you contributed for your team, you may receive bonus EXP as a reward.
  - When playing solo, you are always granted a 20% bonus to your scrap collected.
  - "Contribution" includes finding scrap, having you or your teammates return your found scrap, and returning other's (or your own) scrap to ship.
- Dying will lose you a chunk of EXP, but you'll lose more and more the higher rank you are.
  - The EXP Penalty for dying is capped at -50, however you may lose up to 60 if your team is wiped.
- Killing or assisting in killing entities will earn you EXP for every enemy killed.
  - Damaging an entity will "tag" it permanently. If at any point it dies, you receive kill credit and EXP.
  - Multiple players can tag the same entity, and all of them will receive the kill credit if it dies.
  - If an enemy dies without being tagged, any players reasonably closeby will be given credit.
  - The more enemies you kill, the less EXP you will get for each kill.
- If there is active weather on the moon, you may receive a bonus to your EXP earned from finding and collecting scrap, the amplifier of which depending on what weather it is.

## End-of-game Notes
This mod adds new player notes and reworks the current ones! Notes are distributed a bit differently so hopefully up to 3 notes can be shown consistently (per player). Each of the notes (vanilla and custom) can be enabled or disabled in the config, and you may choose if each one can only appear in multiplayer or in solo too.

Each note comes with its own color style, and can also display its corresponding statistic (eg. how much scrap the most profitable employee collected, see image above). Both of these can be disabled in the config if you'd like.

*Note that this may conflict with mods that also tamper with player notes, I haven't tested compatibilities, report to me on Discord with findings!! BetterEXP is 100% compatible with [Coroner](https://thunderstore.io/c/lethal-company/p/EliteMasterEric/Coroner/).*

Here's the list of notes including old and new:
- "The laziest employee" -> **The Laziest**: took the fewest amount of steps.
- "The most paranoid employee" -> **Most Paranoid**: turned the most.
- "Sustained the most injuries" -> **Most Injured**: took the most damage.
- "Most profitable" -> **Most Profitable**: collected the most scrap value.
- **Most Active**: took the most amount of steps.
- **Least Profitable**: collected the least scrap value.
- **Most Lethal**: killed the most entities in a day.
- **The Pacifist**: was the *only* employee to not kill an entity.
- **#1 Scavenger**: found the most scrap inside the facility.
- **Empty-Handed**: found zero scrap during the whole day.
- **Team Backbone**: found the most scrap that made it back to ship.
- **The Venturer**: explored the most of the facility.
- **Least Venturing**: explored the least of the facility.
- **Most Jumpy**: jumped the most in a day.
- **Most Grounded**: jumped the least in a day.
- **The Bravest**: spent the most time inside the facility.
- **Most Outdoors**: spent the most time outside.
- **The Ship Guy**: spent the most time in the ship.

If any two (or more) players tie on any of these stats, they will all get the note!

### Enemy Bonuses
*Invincible enemies are included as if they could be killed under normal circumstances (another mod is required to actually make them killable of course, BEXP just assumes they are killable for the sake of EXP gains).*

| Enemy | Base EXP |
| ----- | :---------: |
| Old Bird | **18 EXP** |
| Earth Leviathan | **16 EXP** |
| Forest Giant | **15 EXP** |
| Giant Sapsucker | **13 EXP** |
| Maneater | **12 EXP** |
| Eyeless Dog | **12 EXP** |
| Kidnapper Fox | **10 EXP** |
| Feiopar | **9 EXP** |
| Bracken | **8 EXP** |
| Barber | **7 EXP** |
| Nutcracker | **7 EXP** |
| Bunker Spider | **6 EXP** |
| Cadaver Bloom | **6 EXP** |
| Masked | **6 EXP** |
| Thumper | **6 EXP** |
| Butler | **5 EXP** |
| Hoarding Bug | **5 EXP** |
| Mask Hornets | **5 EXP** |
| Baboon Hawk | **5 EXP** |
| Circuit Bees | **5 EXP** |
| Ghost Girl | **4 EXP** |
| Snare Flea | **4 EXP** |
| Backwater Gunkfish | **4 EXP** |
| Spore Lizard | **3 EXP** |
| Coil-head | **2 EXP** |
| Hygrodere | **2 EXP** |
| Jester | **2 EXP** |
| Tulip Snake | **1 EXP** |
| Manticoil | **1 EXP** |
| Roaming Locusts | **1 EXP** |
| Lasso Man | **1 EXP** |
| Any Modded Enemy | **3 EXP** |

### Weather Bonuses
| Weather | EXP Bonus |
| :-----: | :-------: |
| None | 0% |
| Rainy | **+5%** |
| Foggy | **+10%** |
| Flooded | **+15%** |
| Stormy | **+20%** |
| Eclipsed | **+25%** |

### New Rank Table
"Displayed Vanilla Rank" is the badge on your player's suit representing your rank. This will be seen by other players with or without BetterEXP installed.

| Rank | EXP | Displayed<br>Vanilla Rank |
| ---- | :-: | :------------------------ |
| Intern | **0 EXP** | **Intern** |
| Trainee | **25 EXP** |  |
| Apprentice | **50 EXP** | **Part-Timer** |
| Part-Timer | **100 EXP** |  |
| Full-Timer | **200 EXP** | **Employee** |
| Employee | **300 EXP** |  |
| Leader | **400 EXP** | **Leader** |
| Manager | **600 EXP** |  |
| Sr. Manager | **800 EXP** |  |
| 3rd Boss | **1000 EXP** | **Boss** |
| 2nd Boss | **1300 EXP** |  |
| 1st Boss | **1600 EXP** |  |
| 3rd Vice President | **2000 EXP** |  |
| 2nd Vice President | **2300 EXP** |  |
| 1st Vice President | **2600 EXP** |  |
| 3rd Executive V.P. | **3000 EXP** |  |
| 2nd Executive V.P. | **3300 EXP** |  |
| 1st Executive V.P. | **3600 EXP** |  |
| 3rd Deputy President | **4000 EXP** |  |
| 2nd Deputy President | **4300 EXP** |  |
| 1st Deputy President | **4600 EXP** |  |
| President | **5000 EXP** |  |
| Sr. President | **5500 EXP** |  |
| Vice Chairman | **6000 EXP** |  |
| Chairman | **6500 EXP** |  |
| First-Class Asset| **7500 EXP** |  |
| Co-Founder | **8500 EXP** |  |
| Founder | **10000+ EXP** |  |


## Things to note
### Does BetterEXP replace the vanilla system?
Nope, the BetterEXP system will run **alongside** the vanilla EXP system. After a game, the EXP bar will show your BetterEXP Rank and EXP amount, but your vanilla EXP will still be accounted for in the background. At any time, you may open the pause menu to see your current vanilla rank alongside your BetterEXP rank (if you have the pause menu display enabled). You will be able to safely uninstall and reinstall the mod without affecting your vanilla EXP, however, you can not progress your BetterEXP level without having the mod active (duh).

### Headstart
If you've played Lethal Company quite a bit, you'll start at a higher BetterEXP rank than just Intern. 
| Vanilla Rank | BetterEXP Starting Rank |
| :----------: | :---------------------: |
| Part-Timer | **Trainee** |
| Employee | **Apprentice** |
| Leader | **Part-Timer** |
| Boss | **Employee** |
| Boss (1000 EXP) | **Manager** |
| Boss (1500 EXP) | **3rd Boss** |
| Boss (2000 EXP) | **2nd Boss** |
| Boss (2500 EXP) | **1st Boss** |
| Boss (3000 EXP) | **3rd Vice President** |
| Boss (4500+ EXP) | **2nd Vice President** |

---

betterexp version 3.0.1

lethal company version 81+
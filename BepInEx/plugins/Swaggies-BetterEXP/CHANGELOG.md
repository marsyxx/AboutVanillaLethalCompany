# 3.0.1
rolled back some changes done in 3.0.0, also hopefully fixed morecompany compatibility

# 3.0.0
**Please reconfigure your BetterEXP config after updating!**

- Revamped how stats get tracked. If the host has BetterEXP installed, stats of BetterEXP users will be synced at the end of each day. This should hopefully significantly reduce desyncs on the Performance Report screen. If the host doesn't have BetterEXP, the mod will fallback to relying on vanilla systems to try and keep everything synced (but won't be perfect).
- Revamped the notes system to make it much easier to add more in the future.
- You can now customize your "EXP" label (not shoving the whole ""BXP"" thing down throats anymore idk why i did that. if you want you can make it say you have "1500 carjacks" instead of 1500 bxp or something idk do what u want)
- You can now change what is displayed next to a player's name on the Performance Report screen. Right now the options are Scrap Collected (what it originally was), the player's BetterEXP rank, or the player's EXP amount (keep in mind rank and EXP cant be shown if they dont have betterexp)
- You can now enable or disable each note individually. You can also change if they should always be displayed or if they should display in multiplayer lobbies only.
- The EXP formula will now use the scrap value multiplier as if it was the vanilla `0.4x` when calculating EXP gains. This way, if scrap value is inflated, the EXP gains are not.
- Improved the detection of when scrap is found, and added full compatibility for [BagConfig](https://thunderstore.io/c/lethal-company/p/mattymatty/BagConfig/) - scrap placed in a Belt Bag will be immediately marked as "found", and scrap dropped in ship from a Belt Bag will count as collected for the player holding the Belt Bag.
- Improved the detection of enemy kills. If a player damages an enemy, that enemy will be permanently "tagged". If a tagged enemy dies at any point, all players who tagged it will be given kill credit and EXP. If an untagged enemy dies, any closeby players (must be alive and within 16 units) will be given kill credit and EXP.
  - EXP is not split between players. If multiple players are given credit, they are all given the full EXP from the enemy. They are all also awarded a kill (counted for Most Lethal note) - meaning multiple people can get a kill from one enemy. Should incentivize working as a team a lot more.
- Added kill bonuses for v80 and invincible enemies (in the case they are made killable by another mod), and rebalanced the existing kill bonuses
- other stuff i didnt write down, have a good day, you're awesome

:3

# 2.6.3
- v80.
- removed enemy kill notifications

# 2.6.2
- added a "special feature" for a certain betterexp veteran.

# 2.6.1
- Fixed an issue where the "Scrap You Found" color would be gray if you collected 0 scrap (its never supposed to be gray)
- Fixed an issue where the job length would not be reset and The Venturer note would be bugged when using Lethal Level Loader
- Fixed an issue where nodes within 5 units of a player weren't getting marked as "cleared"
- Removed the Weather Bonus from showing up in the round summary and instead combined its bonus xp with scrap collected
- Renamed and reordered most of the hidden ranks to make it feel more special and progressive
- Job logs will now includes the total clearance for the entire team
	- Job logging can be enabled in the config

# 2.6.0
- Added two new notes - "The Venturer" and "Least Venturous" - enabled by default, given to players who explores the most and least of the facility respectively.
	- Exploration is counted based on each player's field of view. Most of the time, seeing into an area is enough to count it as "explored" for yourself (as long as area isn't obstructed by any solid objects). The Venturer and Least Venturous will show the percentage of the indoor area explored by that player. There is currently no tracking for exploration as a team, only individually.
- Job logs will now include if indoor fog is present, and the area exploration stats for each player as explained above
- Lowered the weather bonuses given and removed weather bonuses affecting the death penalty.
	- *(dev note) imo it was just weird that dying was punished more depending on the weather, and in addition, dying should not be the only thing that negatively affects bxp, however due to the general major usage of mods, i dont believe creating a more complex system is necessary (for the time being at least).*

# 2.5.0
- Added a new note - "Team Backbone" - given to the player(s) with the most found scrap that made it to ship
	- The scrap does not have to be returned by the player who found it, any player will count for it
- Fixed an issue where the headstart feature was not working correctly
- BetterXP will now update as soon as the Performance Report appears so it is 100% consistent with what is logged with the Job Logging feature
- Your player's badge that appears to teammates will now be based on your BetterEXP rank instead of vanilla rank (see README for table)
- Some adjustments have been made to the Death Penalty reduction and contribution & scrap found bonuses
- Job logs will now include the random map seed, whether or not a meteor shower occurred, and if a single-item day was active
- Vanilla EXP will now show up on the pause menu, underneath your BetterEXP rank
	- Chat commands have been removed
- Jumps will now be counted properly, the jump notes should now work properly
- Removed support for the version 1.x builds of BetterEXP

# 2.4.0
- job logging will now include the moon's interior on that day, along with number of objects collected per player
- job logging will now include scrap collected in the file name when saved to disk
- fixed an issue where you would gain BXP for being killed by teammates or being left behind
- ive made it so that all scrap gets automatically collected when joining another player's lobby - this should help with desync issues and cheesing bxp by picking up objects that have already been collected and "recollecting" them (which would also fix some specific player notes being desynced as well). this will be ignored if you're hosting the lobby as it would already function properly.
- added and adjusted some properties for v62 (maneater gives 14bxp)
- ive given enemy kills a default value of 3 bxp so if you kill an enemy thats not on the list (like a modded enemy) youd get 3 bxp for it. its a low amount cause i cant account for everything and i want the system to remain vanilla-favored. :3

# 2.3.0
### EXPERIMENTAL JOB LOGGING
- Added a configuration option to log the statistics of each day to a local JSON file on your hard drive. These stats will include:
  - Scrap collected over total available scrap
  - The moon & weather conditions
  - How long the day lasted
  - When the day was played (in irl unix time)
  - A breakdown of BXP gained (including each category)
  - Detailed information for each player, including:
	- Player statistics, including:
		- Damage taken
		- Steps taken
		- Scrap delivered
		- Total number of turns
		- Time spent inside
		- Time spent outside
		- Time spent in ship
		- Scrap value found
		- Scrap they found that made it to ship
		- Enemies killed
	- Player username
	- Player steam ID
	- Player death information (if they died), including:
		- Cause of death
		- When they died using the in-game clock
		- When they died in real life time after the day started
	- If the player disconnected mid-day
	- Notes written for this player (eg. Most profitable)
- This could be used as a way to look back on your previous games and see how you performed compared to everyone else. In the future, I may implement easier access to these logs and maybe even an in-game menu to look at your records. These could also be used by developers should you want to make anything cool with it.
- This option is disabled by default. As long as its enabled before the day ends, everything will be logged properly! The day's log will be saved as soon as the Performance Report screen appears.
- Logs are saved to `AppData/LocalLow/ZeekerssRBLX/Lethal Company/swaggies/BetterEXP/Jobs`.
	- Every time you start up the game, a new session folder will be used to keep things organized.
	- Inside each session folder will be JSONs of each day. JSONs will be saved with the name of the moon you went to, along with a unix timestamp.
- **WARNING:** this feature hasnt been thoroughly tested, especially with other mods. if things break, please report to me on discord! this *should* be completely safe with any custom scrap and morecompany. custom moons untested. *this is your reminder that this mod was originally intended for mostly-vanilla gameplay. :3* 

# 2.2.1
minor update. i made the xp display animation update every frame instead of every 60ms, looks way cooler when its smooth like that. i also removed the "rank up" and "rank down" texts, itll just switch to your new rank's title in yellow or blue instead, and made the number shown in "today's results" a brighter pink if its +100 or above. i primarily made this just to clean up the code a little. have fun :3


# 2.2.0

### Additions
- Two new notes:
	- **#1 Scavenger**: The player(s) who found the most scrap in a day
	- **Empty-Handed**: The player(s) who found zero scrap in a day
	- Scrap is "found" when a player picks it up for the first time. Non-scrap items (like keys), nor items that already existed before landing will count as found. Only the first player to pick up the scrap is counted. Delivering the scrap will count towards Most Profitable.
	- These two notes are enabled by default, but you can disable them in the config.
- Additionally, scrap found will now contribute to how much BXP you earn per day. The more scrap you personally find, the more you'll be rewarded. You'll gain bonus BXP if the scrap you found makes it back to the ship safely, and *even more* if you were the one to deliver it yourself.
	- You'll still gain BXP for scrap found even if you have The Scavenger notes disabled in the config

### Changes
- Reverted the death penalty change from 2.1.1 - when all players die, you lose an additional 20% BXP (instead of 25% from previously)
- Bonus for Flooded weather: `15% >>> 20%`
- Bonus for Eclipsed weather: `25% >>> 30%`
- Slightly adjusted the speed at which the BXP visually counts up/down during the level up animation to help prevent the rank sound from looping

### Fixes
- fixed a little oopsie where gaining bxp would not show an animation and would just snap to however much bxp you had!! :3
- Adjusted the BXP display on the pause menu so it doesnt go offscreen on different resolutions - it should now always stay on-screen regardless of your aspect ratio (though depending on the aspect ratio it *could* cover up other game elements, most likely the crew list - feel free to report to me should this happen)


# 2.1.1

### Changes
- The statistic value on the Performance Report screen will no longer be colored pink if Performance Report Styling is disabled. It will instead be the normal blood-red color.
- Temporarily made it so that all players dying will not affect BXP loss for death. (this will be reverted in 2.2.0 when i add a new way to gain bxp)
- Added Kidnapper Fox to the list of killable entities
- Switched to using Coroutines for the BXP display process (this should prevent potential crashes)
- Potential fix for jumps not counting properly on your own client


# 2.1.0

### Changes
- Added configuration options for each custom note, allowing you to toggle them on or off.
- Added a configuration option to toggle "negative notes" on or off, "The Laziest" or "Least Profitable" for example.
- Added a configuration option to toggle on or off the styling and colors of notes on the Performance Report screen.
- Added a configuration option to choose whether or not to show the exact statistic that comes with a note, for example, "Most Active **(1234 steps)** <-".
- Added five new custom notes:
	- **Most Jumpy** / **Most Grounded**: Given to the employees with the most and least amount of jumps respectively.
	- **The Bravest** / **Most Outdoors**: Given to the employees with the most time spent inside and outside respectively.
	- **The Ship Guy**: Given to the employee who spent the most time in the ship.
	- **All of these new notes are disabled by default. You can enable them in the config.**


# 2.0.0

This version revamps the BetterXP system to allow the mod to be more open with other mods that may amplify or reduce scrap amounts/value and enemy counts, while also attempting to balance the system for vanilla. **With this revamped system, ranks will be reset. You will start at a slightly higher rank depending on how high your old BetterXP is at the time of updating.** You may choose to continue to use 1.2.2, however this version is untested on v50, I will not support the V1 system with future Lethal Company updates.

**Version 2 is NOT compatible with LC v49. Do NOT update if you intend to continue playing on v49.**

### New
- BetterXP system reworked. You will now gain BXP based on your ability to full-clear moons, gaining more BXP with higher "scrap potential" (total available scrap in a moon). Depending on your previous BXP, you may be placed at a higher starting point for V2.
	- With the way V2 experience is implemented, you should hopefully be able to switch between V1 and V2 without messing with either of the saved values. V1 and V2 BXP will be saved separately to allow for version switching, however again, I cannot guarantee that V1 will work properly in future LC updates.
	- When using V2, BXP will be colored in a bright yellow instead of the game's default orange color. This should keep V1 and V2 distinct.
	- The rank cap was raised from 99,999 BXP to 999,999,999 BXP for all of you overachievers out there, with new ranks after The Company to fill in the space.

![image](https://github.com/Swaggies/LCModAssets/blob/main/v2/v2.png?raw=true)

- The new killable enemies were added to the BXP bonus list.
- Added two new notes: "Most Lethal" for most enemies killed in a day, and "The Pacifist" if you were the only crew member to not kill an enemy in a day.

![image](https://github.com/Swaggies/LCModAssets/blob/main/v2/v2%20note.png?raw=true)

### Balancing
- Rainy Weather Bonus: `+5% >>> +10%`
- Foggy Weather Bonus: `+10% >>> +15%`
- Enemies killed will now only give a BXP bonus if they were killed by you. This means using lightning, landmines, or other third-party damage sources to kill enemies no longer rewards you. There is one exception however - if a player damages the entity beforehand, they will be rewarded with the kill. If multiple players damage the entity, the most recent player to deal damage will be rewarded.
	- This system will likely be reworked in the future to allow for cases where one player does all the work, while another can "steal the kill", but for the time being this is only a minor issue.
- Enemy BXP bonuses have been adjusted. See the relevant table in the readme for the new values.

### Changes
- The configuation option for Show Enemy Kill Notifications was split into allowing players to only receive kill notifications if they killed the entity, rather than any crew member.
- Contribution bonus is now no longer based on if you were Most Profitable, but rather how much you contributed compared to everyone else.
- When your rank changes, your new rank title will switch color for a brief period to notify you.
![image](https://github.com/Swaggies/LCModAssets/blob/main/v2/v2%20rank%20up.gif?raw=true)
- Death penalty now uses a different algorithm but roughly follows the same system as "higher BXP means higher penalty".
- The Round Summary was slightly adjusted so that the BXP gains are lined up with each other.
![image](https://github.com/Swaggies/LCModAssets/blob/main/v2/v2%20round%20summary.png?raw=true)
- The BXP requirements for ranks leading up to 11,000 BXP have been slightly adjusted, but generally follow the same progression as it did before. See the rank table in the readme.
- Removed the initial notification when first playing with BetterEXP.


# 1.2.2

i literally uploaded the wrong build. mb guys
since im uploading another patch again i thought i might as well change something so i made death penalty very slightly more forgiving at high ranks (`-1 per 240bxp >>> -1 per 250bxp`).


# 1.2.1

### Changes
- Added a new configuration option for choosing where to display the "scrap collected" value for each player on the Performance Report screen. ([compatibility hotfix for AdvancedCompany](https://github.com/Swaggies/BetterEXP/issues/5))


# 1.2.0

### New
- On the Performance Report screen, the amount of scrap collected for each player will be shown under their name.

### Balancing
- The weather bonus will now also apply to the MVP Bonus and Enemy Kill Bonus, instead of only Scrap BXP. Death penalty multiplier remains unchanged.
- Adjusted the curve of scrap value to BXP so that you can get a maximum of 80 BXP for $1500 instead of 75. You should also notice a slight difference in BXP gain when collecting $400 or more.
- Weather bonus for Rainy: `4% >>> 5%`
- Weather bonus for Foggy: `8% >>> 10%`

### Changes
- For player notes, if a specific stat is tied, all players with equal values will receive the note, instead of just the first player in the list of player scripts.
	- For example, if three players each collect $350 worth of scrap, they will all be shown with the "Most Profitable" note.
- Adjusted the way player notes get written to allow compatibility with Coroner.
- If a player disconnects mid-day, they will (or at least should) not have notes applied to them.
- Notes will now have different colours tied to them to make them stand out more. "Most Profitable" will also have slightly larger text.
- Death Penalty will no longer show up on the Round Summary if you did not die.
- Adjusted the starts and ends of ranks to make way for two new ranks: "Apprentice" (between Trainee and Part-Timer), and "Founder" (after Co-Founder).
- When playing solo, "MVP BONUS" will instead say "SOLO BONUS".
	- It's intended that you always receive the 25% bonus in solo to balance the death penalty being higher due to the team-wipe BXP reduction. If balance issues with solo gameplay are found, I'll make another patch, but for now it should be fine.

### Fixes
- Fixed a bug where if you tied with another player for Most Profitable, there would be a chance you would not get the MVP Bonus (see the first listed change).
- Adjusted the `bxp/level` command output to prevent the first line from wrapping due to a long rank name.


# 1.1.2

### New
- Added a config file with the following tweakable settings:
	- Whether or not to show the enemy kill notifications.
	- Whether or not to show the round summary at the end of each day.
	- Whether or not to show your current rank in the pause menu.

### Balancing
- Adjusted the weather bonuses for the following conditions:
	- Rainy: `5% >>> 4%`
	- Foggy: `5% >>> 8%`
	- Flooded: `10% >>> 15%`
	- Stormy: `15% >>> 25%`
	- Eclipsed: `35% >>> 25%`

### Changes
- Removed the most/least jumps player note (as it just didn't work lmao).
- Removed the `bxp/killnotif` command, please use configuration instead.
- Adjusted the colors used in the round summary to better contrast with the bright yellow background of the UI element.
	- BXP gains will use purple
	- BXP bonuses will use magenta
	- BXP losses will use a darkish blue
	- No changes in BXP will use a darker gray

### Fixes
- Fixed a visual bug where the MVP bonus was seemingly doubled in the round summary.
- Fixed a swag moment bug where the weather bonus color in the round summary was the normal gain color instead of the bonus gain color.


# 1.1.1

- Moved the pause menu rank display to the bottom right of the menu (maximize compatibilities with mods like Compatibility Checker).


# 1.1.0

### Additions
- Added a command to toggle enemy kill notifications: `bxp/killnotif`. This setting will be saved upon change.
- You can now view your current BetterXP Rank from the pause menu in-game. The `bxp/level` command will still exist but this viewing method pretty much makes it obsolete.
	- **BIG** courtesy to [claymor_wan's Lethal Ranking mod](https://thunderstore.io/c/lethal-company/p/claymor_wan/Lethal_Ranking/) which helped a ton with this feature.
	- Courtesy as well to Andrejklol for the suggestion.
- BXP from collecting scrap will now be increased based on the weather of the moon you visited. Be aware however, as the Death Penalty will **also** be raised by the **same amount**.

| Weather | Multiplier |
| :-----: | :--------: |
| None | 0% |
| Rainy | **+5%** |
| Foggy | **+5%** |
| Flooded | **+10%** |
| Stormy | **+15%** |
| Eclipsed | **+35%** |

### Changes / Balancing
- The base Death Penalty was increased: `10 >>> 15`.
- Reduced the increase to the Death Penalty based on your rank: `+1 per 200 BXP >>> +1 per 240 BXP`.
- Getting team wiped will now amplify the Death Penalty by a smaller amount: `2.0x >>> 1.5x`.
- Enemy kill bonuses will now be reduced by 75% if you die.
	- This reduction is unaffected by your rank, or if a team wipe occurs.
- Adjusted the round summary graphic to show a bit more information in terms of the breakdown of BXP.
	- An MVP bonus will be displayed next to the scrap bonus. Will not show if you did not receive Most Profitable.
	- Enemy kill bonus is now its own category. Will not show if no enemies were killed.
	- Weather bonus is also its own category. Will not show if the moon had no weather.
- You will no longer get a bonus for being Most Profitable if your entire team dies.
- The following changes to enemy bonuses were made:
	- Bunker Spider: `8 >>> 9`.
	- Eyeless Dog: `24 >>> 15`.
	- Masked: `9 >>> 10`.
	- Nutcracker: `8 >>> 9`.

### Fixes
- Fixed a bug where the rank-up animation may not play if you were ranking up from a rank equal to or higher than Manager.
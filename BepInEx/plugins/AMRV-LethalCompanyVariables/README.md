# Lethal company variables

[Contact me](###Additional-info)  
[Compatibility](###Compatibility)

This mod allows the user (you) to modify the game to your liking. All within the game with a configuration menu that allows you to have different settings on each save file.

There are still plenty of configurations im planning to add in the future. You can submit your suggestions by contacting me or in the corresponding thread in the [Lethal company modding discord](https://discord.com/invite/lcmod).

> **PRO TIP**
> If you decide to contact using the discord, make sure you ask in the corresponding channel, to navigate to it look up for a channel called _mod-releases_ and search **Lethal company variables**.

_To access the in-game menu, click the host button, you will be able to see the menu at the top-left of the screen, this is because you can change configurations for each individual save file you have. (Keep in mind, in-game does not mean mid-game, you can only modify configurations while in the main menu)_

![Ingame image of the configuration menu](https://i.imgur.com/jOVwgaH.png)

**[`Host`]**: Only the host of the game needs the mod.  
**[`Host` & `Client`]** Both host and the client need the mod installed. (Configuration will sync automatically).

The list of options you can modify:

## Enemies

-   ### **Enemy spawn multiplier** [`Host`]

    Modifies the amount of enemies that spawns each in-game hour. You can add a multiplier per player.  
    _Keep in mind that there are two other limit for the amount of enemies that can be spawned, those are **Enemy power level** and **Enemy capacity**._

    > **WARNING**  
    > This configuration does not affect the spawn of turrets or landmines, you should use it's configuration.

-   ### **Enemy spawn range** [`Host`]

    Clamps the minimum and maximum amount of enemies that can spawn at the same time or in the same batch of enemies.  
    _This value is not affected by **Enemy spawn multiplier** settings._

-   ### **Starting enemies** [`Host`]

    Change the amount of enemies that will be on the planet when you land.
    _This value is not affected by **Enemy spawn multiplier** settings._

-   ### **Enemy power** [`Host`]

    Modifies the power that each enemy uses when spawning. You can also modify the maximum power of enemies for each level.

-   ### **Enemy capacity** [`Host`]

    Modifies the maximum amount of each individual enemy.
    _For example there is a maximum of 5 coil-heads in the level, now you can modify that!_

## Items

-   ### **Battery** [`Host` & `Client`]

    Change the duration of item's batteries causing them to last longer/shorter

-   ### **Conductivity** [`Host`]

    Modify whenever an item is considered a metal or not.  
    Metal items will cause lighting to strike on them.

-   ### **Weight** [`Host` & `Client`]

    Allows you to modify the weight of every single item, scrap and shop items

-   ### **Shop price** [`Host` & `Client`]

    This configuration allows you to set the buy price for every buyable item in the game

-   ### **Scrap value** [`Host`]
    Modifies the scrap value range (minimum and maximum value) for the scrap items in the facility  
    _This only affects static scraps and items such as hives and the apparatus might not be affected_

## Gameplay

-   ### **Deadline days** [`Host`]

    Changes the amount of days you have to fulfill the quota.

-   ### **Starting quota** [`Host`]

    Sets the quota that the game starts with, this value is only used then you create the game file.

-   ### **Quota increase base** [`Host`]

    Sets the quota that will be added after each completed deadline. _Keep in mind that this is the base value and will be scaled according to the rest of the configuration_.

-   ### **Quota steepness** [`Host`]

    Smooths out how much quota is raised after each deadline. Higher values mean the quota will raise slower.

-   ### **Quota multiplier** [`Host`]

    Allows to modify how much quota is added after each deadline by multiplying the `increase base`. You can also add a multiplier per player.

-   ### **Starting money** [`Host`]

    Allows you to customize the amount of money you start the game with

-   ### **Player death penalty** [`Host` & `Client`]

    Changes the percent of credits that you lose for each individual death (setting it to 100% will clear all your money, even if just **one** player dies and their body is not recovered)

-   ### **Ship door power** [`Host`]

    Modifies how long can the ship door remain closed.  
    Greater values mean the door will remain closed for a longer time before overheating, thus automatically opening.

-   ### **Time speed** [`Host`]

    Allows you to change the speed at which the day passes. Higher values mean the day will last less.

-   ### **Time speed mode** [`Host` & `Client`]

    Changes how hours should elapse in the game, has two modes.

    -   **Balanced**: Works as vanilla, the day starts the moment you enter on a planet.
    -   **Fixed**: Time won't pass until the ship lands.

-   ### **Game seed** [`Host`]

    Forces the game to use a specific seed for level, weather and much more, meaning you can make deterministic matches.

## Extras

-   ### **Experience multiplier** [`Host` & `Client`]

    Allows to modify how much experience you earn by playing. (Keep in mind experience can be both gained or lost and this multiplier will affect both).

-   ### **Disable ship item limit** [`Host`]

    Disables the default limit of items inside the ship.

-   ### **Always display clock** [`Host` & `Client`]

    This setting will make the moon-clock be always on display after landing on a planet.

## Planets

-   ### **Turret spawn multiplier** [`Host`]

    Changes the amount of turrets that can spawn with the level (high values will grant at least one spawn)

-   ### **Landmine spawn multiplier** [`Host`]

    Changes the amount of landmines that can spawn with the level (high values will grant at least one spawn)

-   ### **Level risk information** [`Host` & `Client`]

    Changes the information displayed about the risk value of the levels

-   ### **Weather selection mode** [`Host` & `Client`]

    Change the algorithm used to determine the weather of the planets.

    -   **Disabled**: The vanilla algorithm is used and configurations are ignored.
    -   **Simple**: Weather is choosen based on a normal probability.
    -   **Complex**: Weather is choose based on normal probability but it gets harder the better you play.

-   ### **Allow every weather** [`Host` & `Client`]

    Allows every planet to have any weather (Keep in mind that this might be unfair in some maps with specific weathers).

-   ### **Weather probabilities** [`Host` & `Client`]

    Set the probability or weight chance for any weather to occur, you can set this value globally or individually for each planet.

-   ### **Scrap price multiplier** [`Host`]

    Changes the value of the scrap. This works for every kind of scrap, even items such as Keys and the Apparatus

    You can modify this value globally or for each individual moon.

-   ### **Scrap amount multiplier** [`Host`]

    Changes the amount of scrap that spawns in the level, this only counts towards dynamic scrap so items such as the apparatus and beehives are not affected.

-   ### **Factory size** [`Host` & `Client`]

    Modify the interior size of each level individually.

-   ### **Enemy power** [`Host`]

    Change the maximum amount of power that the level can have, you can set this value individually for indoors and outdoors.

-   ### **Scrap amount** [`Host`]

    Modifies the minimum and maximum amount of scrap that will appear in the level.  
    _(Does not change the amount of beehives, apparatus or other static scrap)_

-   ### **Route price override** [`Host` & `Client`]

    This configuration lets you change the cost to travel to a specific moon, this setting works as an override so disabling it will mean that the vanilla value is used, otherwise (if enabled) it will use whaterver value you set and ignore vanilla behavior for that moon.

-   ### **Enemy spawning** [`Host`]

    Change the probability for each enemy to spawn in the level. You can even allow custom enemies to spawn in custom moons.

    > **INFO**  
    > If LCV can't detect the probability of an enemy, it will not be taken into consideration unless you enable it.

-   ### **Item probability** [`Host`]

    Change the probability for each item to spawn in the level. This option will allow you to add custom items into custom moons.

    > **INFO**  
    > If LCV can't detect the probability of an item, it will not be taken into consideration unless you enable it.

## Player

-   ### **Player jump force** [`Host` & `Client`]

    Make players jump higher by appliying a force.  
    (This does not prevent fall damage).

-   ### **Sprint duration** [`Host` & `Client`]

    Makes the player sprint last longer but comes with the downside that it will also take longer to recover.

-   ### **Movement speed** [`Host` & `Client`]

    Change the walking and sprinting speed of the player.  
    (This does not affect swimming or climbing speeds)

-   ### **Defense** [`Host` & `Client`]

    Makes the player take less damage from all sources by applying a reduction of damage. However enemies that would normally insta-kill the player such as the **Forest keeper** or the **Earth leviathan** won't have a damage reduction.

-   ### **Health** [`Host` & `Client`]
    Change the player's health. Keep in mind player regeneration thereshold will still be at 20 points of health.

This mod makes use of my other mod [ConfigurableCompany](https://thunderstore.io/c/lethal-company/p/AMRV/ConfigurableCompany/). If you are a modder, you might want to look at it ;)

---

### Compatibility

No compatibility issues.

Every mod that adds a new enemy is compatible and the eneny will be shown in the config.

If you find any, contact me and will be fixed asap.

> **WARNING**  
> Keep in mind if another mod changes the same features as this mod, these settings might not work.

---

### Additional info

![the_ansuz](https://img.shields.io/badge/Discord_user-the__ansuz-5865F2?style=flat&logo=discord&logoColor=%237f8afa&link=https%3A%2F%2Fdiscordapp.com%2Fusers%2F341967365908594700)

![Modding discord](https://img.shields.io/badge/Discord_server-Lethal_company_modding-5865F2?style=flat&logo=discord&logoColor=%237f8afa&link=https%3A%2F%2Fdiscord.com%2Finvite%2Flcmod)

If you find any issue or have any suggestion, feel free to [contact me](https://discordapp.com/users/341967365908594700) or ask in the [discord's corresponding channel](https://discord.com/channels/1168655651455639582/1193627690960437369).

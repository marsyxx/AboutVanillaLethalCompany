https://ko-fi.com/boxoemilios is my ko-fi if you'd like to support me, either way, enjoy the mod!

## Dependencies
1. LethalLib 
2. BepInEx
3. Csync
4. Dawnlib
5. Latest version of Lethal Company for latest mod versions

---

![Preview](https://i.imgur.com/PO7dK7S.png)

---

Important - Modded Moon Spawn Configs

This mod automatically detects installed moon mods and generates default spawn weight config entries for them.

If you've just installed this mod or added new moon mods and want to edit the spawn weights:

1. Launch the game.
2. Load into the main menu and enter a save.
3. Close the game.
4. You can now edit the generated config with your new moon entries.

This only needs to be done once whenever new moon mods are added.

---

<details>
<summary>Purchasable Equipment</summary>
&nbsp;&nbsp;
<details>
<summary>Handlamp</summary>

The handlamp is an alternative option to the usual flashlight; it makes light in an area around it instead of the beam of a normal flashlight

Mod Issue! - Mods messing with lighting can drastically change the lamp's light, there won't be a univeral way for me to fix this issue

</details>

<details>
<summary>Bandages</summary>

Bandages are a 3 use consumable item that heals instantly

Compared to the medical kit, bandages... 

- Heal instantly

- Weigh less

- Are cheap and disposable

</details>

<details>
<summary>Medical Kit</summary>

The Medical Kit heals the user overtime using it's internal stored health pool. It's max internal health caps out at 150 and will passively replenish when idle.

Compared to Bandages, the medical kit... 

- Is infinite healing if giving time to replenish

- Can be used to heal teammates at 2x the healing rate

</details>

<details>
<summary>Productivity Auto-Injector</summary>

The Auto-Injector is a single use item that applies the following buffs to the player who used it for 3 minutes...

- Speed boost

- Jump Height boost

- Negates the slow effects of carry weight

</details>

<details>
<summary>Shift Controller</summary>

The Shift Controller is a short-range portable teleporter that saves your coordinates and can teleport you back to that location as long as it has battery remaining.

- The shift controller saves its coordinates when used the FIRST time and will then teleport you back to those initial saved coordinates when used a SECOND time, at the cost of some battery.

- The shift controller consumes battery slowly when you've saved coordinates but haven't yet teleported

- The saved coordinates can be wiped under certain conditions, like when it's recharged via the ship recharger

- The Shift controller is disabled in orbit

Shift controller connectivity mechanic:
 - The Shift Controller's connectivity is based on how far you are from the saved coordinates, the farther you are --> the lower the connectivity. With full connectivity the controller works as expected but when connectivity starts to get low the controller will have a chance to "glitch". When the controller glitches it will wipe your saved location and prevent you from using the controller for a short time.

</details>

<details>
<summary>Blowtorch</summary>

The Blowtorch can be used to dismantle traps and will produce scrap when done successfully. It can also be using as a poor ranged weapon.

To dismantle a trap, stand by it and hold left click. You should hear and see sparks, and after short period of time (the time depends on the trap) the trap will be dismantled and drop a number of scrap items.

- THe torch has a heat mechanic and gains heat while being used. When heat reaches a high enough point you begin being burned.

</details>

<details>
<summary>Defibrillator</summary>

The defibrillator uses battery to revive deceased employees.

- The defibrillator has a charge meter (Not related to its battery) on the side of it. The charge meter indicates how long you've held down the use button and it must be fully charged to revive a player.

- When the charge meter is full you can continue to hold the use button and be ready to revive at a moments notice but its battery will be constantly drained when holding its charge.

- A battery cost is deducted when you release the use button at full charge regardless of whether a player was revived or not.

</details>
</details>

---

<details>
<summary>Discoverable Scrap</summary>
&nbsp;&nbsp;
<details>
<summary>Fuel Cylinder</summary>

The Fuel Cylinder is a two handed item that is activated when picked up for the first time. When activated, an internal timer will begin counting down to 0 that will cause the tank to explode. The only way to deactivate the timer is by bringing the tank back to your ship safely.

- The internal timer can be any time around 3 minutes, better get moving!

- Hitting the tank with a melee weapon will cause it to explode immediately

- Each time the tank is dropped, its remaining time will be reduced by a percentage; if it is dropped and the timer is low enough it will explode immediately.

- After being brought to the ship the tank will enter an inactive state, stopping the timer. In the inactive state, Hitting it will still cause it to explode and it can be reactivated by dropping it a few times (Dropping it will only cause a explosion if it is active).

- When grabbed there is a small chance it doesn't activate.

Tips for survival - Take it first or take it last and know the way back to the ship, the last thing you want to do while carrying the cylinder is waste time trying to find your way back to the ship. Leaving it is always an option.

</details>

<details>
<summary>Radioactive Cell</summary>

The Radioactive Cell produces a sickly green light in a radius around it infinitely. When the cell is held, the holder will regularly take ramping damage until they drop it or perish.

- The Cell inflicts ramping damage on a rough curve going from 5 to infinity.

Tips for survival - The damage ramps based on how long you've held it continuously, so just drop it to reset the damage ramp to take minimum damage, sometimes it's just safer to leave it behind.

</details>

<details>
<summary>Capsules</summary>

Gloomy Capsule - Charges when outdoors during the evening and night, when fully charged it will randomly teleport it's holder either indoors or outdoors depending on their current location.

Frigid Capsule - Charges when outdoors, when fully charged it sends out waves of snow that will apply stacks of slowness to all nearby players.

Sacrifical Capsule - Drains health from all nearby players in a radius when outdoors at night.

</details>


<details>
<summary>Crowbar</summary>

The Crowbar is a melee weapon that does average damage but can unlock locked doors by smacking the door 3 times.

</details>

<details>
<summary>Sledgehammer</summary>

The Sledgehammer is a melee weapon that does high damage but swings slower and is pretty heavy.

</details>


<details>
<summary>Padlock</summary>

The Padlock is a consumable item that keeps doors open or closed when used on them.

</details>

<details>
<summary>Walking Cane</summary>

Increases your movement speed when held, meaning not when in your pocket.

</details>

<details>
<summary>Sizable Scissors</summary>

Sizable Scissors are a two handed melee weapon that deals double damage and randomly damages it's holder if they are sprinting.

Tips for survival - don't run with them or drop them. If the scissors kill you then you ran with scissors, what'd you expect?
</details>

<details>

<summary>Candy Dispenser</summary>

The Candy Dispenser is a melee weapon that rarely drops a piece of vintage candy when swung (1/25 chance each swing).

- The candy dropped by the dispenser is worth a small amount and gives a small buff when eaten.
- The candy dispenser will not drop candy if the ship is in orbit or if it is on moon that has no time cycle like the company building.

</details>

<details>

<summary>Rose</summary>

The rose damages you a small amount when picked up, equipped, and dropped.

</details>

<details>

<summary>Tickets of Exchange</summary>

The Regular Ticket of Exchange is a consumable item that can be used to transform any ONE item into a gift box, essentially exchanging it for something else.

The Golden Ticket of Exchange is a extremely rare variant that does the same thing it predecessor does but will turn many surrounding items into gift boxes instead of just one.

- Ticket are disabled while inside the company cruiser to avoid severely breaking the game.

</details>
</details>

---

<details>

<summary>Scrap Packs</summary>
&nbsp;&nbsp;
<details>
<summary>Mansion Scrap Pack</summary>

- Servant Bell
- Pocket Watch
- Chess Pieces
- Puzzle Piece x
- Hourglass x

</details>

<details>
<summary>Facility Scrap Pack</summary>

- Traffic Cone
- Mere Gear
- Toy Rocketship x
- Circuit Board x

</details>

</details>

---

Any versions of this mod found on other sites were not posted and are not supported by me!

---

MASSIVE thanks to Malcolm on YouTube for their LC modding guides and examples!

MASSIVE thanks to Evaisa and anyone else who has contributed to LethalLib!

MASSIVE thanks to everyone working on BepInEx!

MASSIVE thanks to TeamXiaolan for Dawnlib!

Thanks to everyone and anyone helping in the lethal company modding discord!

And finally the BIGGEST thanks to Zeekers for creating Lethal Company!

---

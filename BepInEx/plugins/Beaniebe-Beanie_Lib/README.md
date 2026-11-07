# Beanie Libby Libnits

Beanie libnits is a very basic library with a small amount of useful scripts. If for any reason you ever wanted to use them in your own projects, they are listed below along with how to set them up. They can easily be connected to JLL scripts.

NOTE: SCRIPTS ARE NOT SYNCED! Please use a JLL client sync script for them!

My discord is Beaniebe if you have any questions or suggestions, always open to help!


<details>
<summary>Trigger Counter / Text Counter Explanation</summary>

# Trigger Counter

The trigger counter script reads how many times it has been triggered, and upon reaching a certain threshold set by the user, invokes a unity event. Using the array, you can set up multiple thresholds per script, so you can count and invoke events based on multiple instances of reaching a number. Normally it counts up by 1, but by using the Trigger Counter (int) you can specify how much to count up by. This can be used in situations such as having 3 different buttons that need to be pressed to open a door, or a scoring system. It also means you can have an interactive display that lights up depending on how many buttons have been pressed. Please note that if the button can be pressed multiple times, it would still count towards opening the door (even if the same one is pressed three times). You can add a whole array of integers in editor, allowing you to invoke different events when different counting numbers are reached.

The Text counter is identical, but it now accepts negative values for counting down, you can set a starting value, and it must be connected to a TMP text box to display its value.

 Additionally, there is two events tied to the script that you can invoke. These are TurnOffCounter which disables the object from counting further, and ResetCounter which sets its counting back to zero. Additionally, there is a box for each of these if you want to trigger events when its turned off or reset.

EXAMPLES:

Having a configurable amount of buttons necessary to open a door, and having a light turn on to track the progress each time a button is hit.

Having to use something a configurable amount of times for the actual effect to trigger

Having something require several different criteria to activate

The below image is an example of how to set up the array. For integers 1 and 2, you would fill in what you want it to do (Play a sound, turn on a light, etc) meanwhile the third trigger could do something like activate a door animation. The final integer 3 trigger also activates the TurnOffCounter, which prevents it from counting up further.

<a href="https://ibb.co/fG1dhWT2"><img src="https://i.ibb.co/n8gMHyKb/image.png" alt="image" border="0" /></a>

</details>

<details>
<summary>Dynamic Tag-Based Texture Swapping Explanation</summary>

# Dynamic Tag Texture Swapping

This script is relatively simple: Applying it on an object will bring up a list. In this list, for each entry you can set a material and then put in a corresponding LLL Moon tag. When a moon with this tag is present, the texture is automatically swapped to the corresponding material. This allows for interiors to easily swap visuals on a tag basis without having to copy entire rooms or models. Good for avoiding bloat in editor.

EXAMPLES:

Having a room change colors on a certain tag

Having rocks change color to match the biome the moon is in

Here is an example of how in deepcore mines, I use dynamic tags to swap textures. Each material is assigned a tag. The top material takes priority.

<a href="https://ibb.co/Kx8xDRdY"><img src="https://i.ibb.co/3mPmvG2w/image.png" alt="image" border="0" /></a>

</details>

<details>
<summary>Signal Receiver and Signal Sender</summary>

# Signal Receiver and Signal Sender

The signal sender and receiver are two very similar scripts that act as a whole. They are used to invoke unity events globally so long as the sender and receiver have a matching tag. To set up Signal Sender, give it a word in its Word ID text box. Then, when you want to send the message, activate it by using the Send Signal unity event. For the receiver, make sure its Word ID matches the Sender. When it receives the signal, it will preform whatever events are in its "OnSignalReceived" box. These scripts are sort of like a radio for invoking events. If you use a Signal Sender with the ID DoorOpen, and have three Signal Receivers tied to the ID Door Open, All three Receivers will invoke their OnSignalReceived events.

EXAMPLES:

Having a script tied to an interior communicate / activate something on a moon. (Like pulling a lever in the interior to unlock a door to a fire exit)

Having scripted objects in different interior tiles communicate with each other. (Like having a button in the main tile reveal a secret door in the apparatus tile)

Having two prefabs with scripts communicate with other things. (A randomly spawned prop that when pressed could set off a bunch of other randomly spawned props)

</details>

<details>
<summary>Custom Breaker Box</summary>

# Custom Breaker

The custom breaker is a modified breaker box script that must be placed onto a breaker prefab, connected, and then the original breaker box script must be removed. This one is a little strange to set up, so I'd just recommend direct messaging me on discord about it. (My discord is Beaniebe)

If you would like to learn more, it is identical to a normal breaker except for the fact that the breaker invokes a unity event when it turns on or off, and the amount of levers initially turned off are set by the user via a variable, rather than being randomized on or off entirely. It also does not mess with the interiors power at all.

EXAMPLES:

Having a special breaker box that turns on/off power for only one room

Using a breaker box to activate things like any other interact trigger (buttons, levers, etc)

</details>

<details>
<summary>J Client Parent To View</summary>

# J Client Parent To View

Thank you to Jacob for writing the original of this script!

Identical to the JLL client attach script, but for the players view instead of their body. Means you can essentially tack extra stuff onto the players visor/hud.

Make sure you reference which object will be parented to it. The first checkbox attach to player enables the script and the second checkbox, lerp, makes it instead loosely follow the player rather than being rigidly stuck to them. I wouldn't recommend turning lerp on, since you are mainly using this for hud elements.

</details>

<details>
<summary>Rigidbody Launcher</summary>

# Rigidbody Launcher

This rigidbody launcher component does exactly what it says it does! Put in any prefab with a rigidbody in the unity event used to activate it, and then configure the launch force in the launcher script itself. The rigidbodies originate from the launcher location and also inherit its rotation. Useful if you wanna launch funny little critters around. NOTE: You will have to add other scripts to make them despawn over time. You shouldn't spam too many.

</details>

<details>
<summary>Physics Collision Detector</summary>

# Physics Collision Detector

This Physics Collision Detector script specifically goes on rigidbodies you are going to use for physics interactions, and is used to activate unity events when an object collides with enough force on anything with the Room tag. You can set the required force for the event to trigger. Useful for something like making objects cause noises when hitting surfaces, or making volatile objects like barrels explode on heavy impacts. You can set which specific layers that will be considered for collisions.

</details>

<details>
<summary>Custom Shotgun</summary>

# Custom Shotgun Script

Runs a unity event when the gun is fired, allowing you do make the gun do alot of stuff. Very useful with the aforementioned rigidbody/physics scripts... have fun.

This scripts also takes the vanilla shotgun and adds a ton of useful settings, such as:

Use Gun Blast - If true, vanilla shotgun blast will occur alongside running the unity event. If off, only the unity event will occur.

Use Left Reload Anim Only - If true, the left barrel reload animation will always play regardless of ammo count. Useful for custom weapons with only one loading area for the barrel. Otherwise, keep it off.

Max Shells - Sets max ammo that can be loaded

Reload Speed - What to multiply the time it takes to reload by. Higher number = takes longer. For example 2 means twice as long and 0.5 means half as long. ANIMATIONS MUST BE SHORTENED IN EDITOR TO MATCH THIS NEW SPEED, and player animations wont speed up with it.

Shotgun Radius - Sets the width of the shotgun blast. Only impacts width, not length! Make it a lower value to require more precise shots.

PLAYER STUFF:

Player: SuperCloseDamage, CloseDamage, MediumDamage, FarDamage - Self explanatory, lets you set damage values for each range against players.

Player: SuperCloseRange, CloseRange, MediumRange, FarRange - Self explanatory, lets you set the distance for each damage range against players.

RagdollForce - How hard a player gets blown back when killed via shotgun blast.


ENEMY STUFF:

MaximumEnemyRange - Specifies the max range the enemy damage raycast can travel. Always set higher EnemyCloseRange and EnemyFarRange.

FullDamage,MediumDamage,LowDamage - Specifies the damage to entities at each range

EnemyCloseRange - Enemies within THIS range from the shotgun will take (what you set as EnemyFullDamage). Past wherever this range ENDS is when the (EnemyMediumDamage) area begins. For example, if its set as 4, anything within 4 units of the shotgun takes full damage and past that it is considered medium.

EnemyFarRange - Enemies between THIS range and the edge of the CLOSE range will take (EnemyMediumDamage) and anything PAST this range will take (EnemyLowDamage) So this basically sets the endpoint for medium damage and the start point for low damage. For example if the close range is 4 and t range is 8, anything within 4 units of the shotgun takes full damage, Anything within 4-8 units takes medium damage, and anything past 8 takes low damage.




</details>



# Credits

Thank you to [Jacob](https://thunderstore.io/c/lethal-company/p/JacobG5/) for helping and writing the original JClientAttach script!

Thank you to Xu Xiaolan for creating the tag based texture swapping script and helping to review many of the scripts!

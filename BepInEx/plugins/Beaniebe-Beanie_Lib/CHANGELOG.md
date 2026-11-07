- 1.1.0

	-added scripts for my personal enemy variants

- 1.0.8

	-Added text counting script, identical to trigger counter but will visually display the current count value, and can count downwards!

	-Added "ammo reloaded per item consumed" variable to custom shotgun

	-Rigidbody Collision script now lets the user define specifically what tags it can consider for collisions

	-fixed parent to view script persisting in space (plird shot head)

- 1.0.7 

	-implemented dawnlib compat for texture tag replacements

	-shotgun script now properly syncs, go kill your friends 

- 1.0.6

	-cleaned up CustomShotgun by adding categories, and added customization for enemy damage (sorry zeekers split enemy and player damage I had no idea)

- 1.0.5

	-added a new script, CustomShotgun. Allows you to make a shotgun scrap with almost every single variable changable, including being able to run any unity event when fired.

- 1.0.4

	-Added rigidbody launcher, which launches prefab rigidbodies with custom force

	-added physics collision detector, used on rigidbodies to detect when an object collides with enough force

	-removed custom placement zone scripts as they have a functioning replacement in someone elses api

* 1.0.3

 	-Added ParentToView, a script written by Jacob, but instead made for locally parenting objects to the players camera. Useful for physical fake hud elements.

 	-Trigger Counter now has an IncreaseCount(int) option for increasing by multiple numbers at a time

	-Added CustomPlacementZone Manager and Trigger, these aren't fully functional yet so I wont tell you what they are or how to use them >:p

* 1.0.2

 	-added new dependency (it would be almost impossible to not have this installed yet so this is more of a "just incase" thing)

 	-synced the breaker box script

* 1.0.1

 	-added two (three) new scripts: Custom Breaker Box and Signal Sender and Signal Receiver

 	-updated counting script to allow for large arrays of numbers, meaning you can activate events not just when one final number is reached, but at a highly configurable amount

 	-updated readme with new documentation and readability improvements

* 1.0.0

 	-beanie is here, this fills you with fear


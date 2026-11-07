# CoilHeadStare - made by TDP

This mod makes the Coil Head slowly turn its head to stare at you after standing close to it for a while. Because it just isn't scary enough already.

*You are looking right at it. It shouldn't be able to move, right? Slowly, its head begins to turn towards you, menacingly. Mocking you. It knows you can't escape.*

This is a client-side mod, anyone who has it installed will see the Coil Head stare at the closest player.

Values like the speed at which the head turns are tweakable in the config file.

**Extra feature:** If you hit the Coil Head with a shovel or something similar, the head spring will bounce. This is on by default but can be changed in the config file.

‎
-----------

![preview gif](https://i.imgur.com/8dEI8Kg.gif "lccoilheadstare")

-----------

**Important: This mod is incompatible with many model replacements or reskins of the Coil Head.**

But even if the head doesn't turn, hitting it with a shovel for the recoil animation should almost always work.

&nbsp;

To modders: If you want to guarantee your reskin to work with CoilHeadStare, set it up like the default coilhead:
- Make sure to have the head and body mesh seperate, and have the head as a child transform of "springBone.002".
- The transform hierarchy does not have to be the exact same as the default Coil Head, but this mod does a recursive search for a transform with the name "springBone.002", and its last child is expected to be the head.
- If there are multiple "springBone.002", perhaps because the original hierarchy is not deleted, the last one the recursive search finds is used.
- If you use the default rig, there is another animated bone as child of "springBone.002". The head needs to be behind that in the hierarchy.
- The head needs to have a MeshRenderer, not a SkinnedMeshRenderer.
- Make sure the head transform origin / pivot point is at the neck, where you would expect it to turn.
- Lastly, if the Z axis of your head is not pointing forward, make it the child of an empty gameobject at the same position which points forward.

There does seem to be a way to not have the head seperate but instead entirely weighted to the single bone under springBone.002, however if this works in all cases is uncertain.

CoilHeadStare has a thread in the unofficial "Lethal Company Modding" community on discord. Feel free to ask any questions you have about the process there.
# Melee Fixes
Fixes bugs with the shovel, kitchen knife, and shotgun:
- Fixed shovel sometimes becoming unusable if a player died while holding it mid-swing.
- Fixed hit effects (shovel bounce, knife blood spray) sometimes playing even if your swing misses, providing poor feedback.
- Shovels and knives no longer collide with dead enemies.
  - Before v80, enemies would still play their "impact sound" when their corpse was attacked, however this is no longer the case.
  - However, hit effects (the aforementioned bounce/blood spray) will still play which seems unintended.
  - This is a bigger deal for the knife, which is only allowed to damage one target per swing - now that damage can no longer be wasted on a corpse in proximity to the enemy you are targeting.
- Whenever a melee weapon plays a hit sound, it calls an expensive search function that can cause lag or frame drops on less powerful computers.
  - It now references a cached instance of the required object, which should help combat remain more smooth and fluid.
- [Damage should now be handled more consistently when firing the shotgun at multiple enemies](https://thunderstore.io/c/lethal-company/p/Zaggy1024/NutcrackerFixes/)
- Fixed duration of ear-ringing effect when a shotgun is shot in point-blank range (usually, because you are the one holding it)
- Shovels and knives now play hit effects when colliding with trees, as was the case before v55.
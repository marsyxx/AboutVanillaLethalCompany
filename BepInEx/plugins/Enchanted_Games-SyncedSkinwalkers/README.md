# SyncedSkinwalkers

Now updated for Mirage v1.29.0 and above

A config preset for Mirage that doesn't change the vanilla behaviour of Masked enemies and lets other monsters mimic player voices.
This modpack aims to provide a similar experience to the Skinwalkers mod while making sure everyone can hear the same voicelines.

Feel free to use this modpack as a base for your own modpacks or just use it as is. Basically do whatever you want with it

## Config values this modpack changes
### Masked enemy behaviour
- Disables Mirage's custom spawning to ensure Masked spawn the same way as they do in vanilla
- Enables the mask for Masked enemies
- Enables the arms up animation for Masked enemies
- Disabled the change for masked to spawn with items/scrap

### Voice Imitation
- Masked enemies can imitate voices every 12-25 seconods
- Other monsters can imitate voices every 20-40 seconds
- All monsters have an 80% chance of being able to imitate voices
- All enemies can mimic voices except the following: `bees, butler bees, locusts, hygrodere, ghost girl, earth worm, manticoil, barber, old bird, tulip snake & modded enemies`
   - If you want to change this, go into the `Mirage.Enemies.cfg` config and enable or disable enemies you want to be able imitate voices

## Notes:
- Masked enemies can still convert/infect players into other masked enemies as usual
# JKConfig

**JKConfig** is a JLL addon that allows modders to add the ability to config Items and Enemies registered in LethalLevelLoader. 

Keep in mind that this addon alone will **NOT** generate scrap/enemy configs by itself, Enemy and Item modders need to add this addon alongside JLL on their own (Information on how to do it below). Also this only works if your Items or Enemies are registered with **LLL (LethalLevelLoader)**

If you'd like to support me, check out my ko-fi. https://ko-fi.com/itiskenji.

## Information for modders: How to I add this to my project?

If you're familiar with how to install libraries and APIs it should be fairly simple. 

1. Add **JKConfig** and [**JLL**](https://thunderstore.io/c/lethal-company/p/JacobG5/JLL/) to your project by downloading both packages manually, and dropping the corresponding DLLs into your project, in the same folder where you have any other libraries (*LethalLevelLoader, ManuLib,...*), It does not matter where as long as you know where it's located, for convenience sake.

2. Once you have both, turn off  ``Validate References`` on the 3 JLL dll files. It will take a bit for Unity to gather the scripts.

3. Go to where your mod is located (or a folder of your convenience), ``Right Click > Create > JLL > Addon > JKConfig``, that should create the JKConfig file.

4. Add any Items (*ExtendedItems*) or Enemies (*ExtendedEnemyTypes*) that your mod has (or that you want to generate configs for) to the JKConfig file.

5. After that, create a **JLLMod** file ``Right Click > Create > JLL > JLLMod`` , inside that file set the ``Mod Author`` and ``Mod Name``. **Make sure that they are the same as the ones on your *LethalLevelLoader ExtendedMod* or it will not work**. 

6. On the Addons part of the JLLMod file, add the JKConfig file. Bundle both your JLLMod and JKConfigFile in the same ``.lethalbundle`` as your mod and you should be good to go! Keep in mind that your mod needs to depend on **JKConfig** for it to work.

### Editable attributes

For the moment, the editable fields that will generate for each are:

#### Items
- **Min Value**: The minimum value for the item. In-game it'll show the value multiplied by 0.4
- **Max Value**: The maximum value for the item. In-game it'll show the value multiplied by 0.4
- **Weight**: The weight for the item.
- **Two Handed**: Indicates if the item is two-handed.
- **Is Conductive**: Indicates if the item is conductive.
- **Item injection settings**: Adds the Item to a Level's randomisation pool based on different matching properties.
#### Enemies
- **Power Level**: The power level for the enemy.
- **Max Count**: The maximum count for the enemy.
- **Enemy injection settings**: Adds the Enemy to a Level's randomisation pool based on different matching properties.


Please if you want to contact me with some feedback, bugs or suggestions my discord is isma_kenji or you can [go to github](https://github.com/ismakenji/JKConfig). You can also find [this mod's thread](https://discord.com/channels/1168655651455639582/1527436157984706821) in the Lethal Company Modding Discord server.

## Credits:

**Kenji**: The idea man, the config code part of the configs. <br><br>
**JacobG5**: Creating JLL and helping with the setup (like most of it basically)<br><br>
**Mrov**: Helping me debug and adding some fixes to the code for it to work <br><br>
<a href="https://ko-fi.com/itiskenji"><img src="https://storage.ko-fi.com/cdn/brandasset/v2/support_me_on_kofi_dark.png" alt="Ko-Fi" width="200"/></a>
<br>
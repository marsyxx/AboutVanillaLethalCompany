
v80+ introduced a new item slot for utilities. Zeekerss updated most of his code for item removal accordingly but missed a single line on `PlayerControllerB.DestroyItemInSlot(int itemSlot)` which results in an index out of bounds error.
A lot of mods including many of my own use `PlayerControllerB.DestroyItemInSlotAndSync(int itemSlot)` to delete an item from a players slots on all clients which now throws a nasty error when destroying an item in the new slot.

This mod patches that method to fix this problem so I dont have to rewrite a bunch of code. I decided to upload it as a standalone mod that way other modders who were also using this method and are confused about the error can just depend on it.

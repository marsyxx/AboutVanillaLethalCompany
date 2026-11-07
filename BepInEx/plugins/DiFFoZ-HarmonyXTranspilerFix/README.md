# HarmonyXTranspilerFix
Includes some fixes from upstream repository of HarmonyX.

## Fixes & Improvements
- Fix HarmonyTranspiler [causing patched method to behave incorrectly](https://github.com/BepInEx/HarmonyX/issues/65).
- [Fixes a bug where Harmony calls TargetMethods() always twice](https://github.com/pardeike/Harmony/commit/7ab508c95e336619d0b77eb0918d74ac10fbcc2c).
- [SymbolExtensions.GetMethodInfo allow parameterless method decoding](https://github.com/BepInEx/HarmonyX/commit/2c4ddf02de2391101f4e6d5f5fa9f2cb5ea6a7f6).
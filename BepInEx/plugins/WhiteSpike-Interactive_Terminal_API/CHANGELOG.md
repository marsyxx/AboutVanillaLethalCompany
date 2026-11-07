<details>
<summary> 1.3.3 </summary>

- Fixed simultaneous scrolling (up and down press at the same time) would cause one of them to never be terminated due to the reference being lost and no longer possible to terminate it.
- Fixed double scroll down callback when changing pages.
	- As consequence, the last element is selected when iterating through the screens backwards.

</details>

<details>
<summary> 1.3.2 </summary>

- Added field "scrollDelay" which represents the delay before initiating the scroll routine.
	- By default, it starts with a value of 0.5.

</details>

<details>
<summary> 1.3.1 </summary>

- Fixed ``CursorMenu`` display text breaking format when defined ``Description``.
- Refactored ``BaseCursorMenu.ResetCursor()`` to include the responsibility of finding the next selectable element rather than the application doing that.
- Refactored ``InteractiveTerminalManager`` to remove useless code.
- Implemented ``terminalAudio`` audio source and ``terminalKeyboardAudioClips`` array of AudioClip fields that can be changed through either completely overriding ``SetDefaultKeyboardAudio()`` or setting the fields to your desired values. The source and array is then used on ``PlayKeyboardSounds()`` method which can be overriden if required.
- Refactored ``PageApplication`` to not reset the cursor index whenever moving through pages, ignoring ``previous`` parameter from ``SwitchScreen``
- Refactored ``InteractiveCounterApplication`` to use the local field rather than wasting time getting the same thing twice.
- Implemented ``scrollingRoutine`` coroutine and ``scrollRate`` fields which are used for when the user is holding down on the button used for scrolling through the screen's cursor entries, implemented in ``RepeatScrolling(bool scrollUp)``.
- Refactored ``Keybinds``, ``IngameKeybinds`` and ``InputUtils_Compat`` to remove the unnecessary and wrong comments on each field/property.

</details>

<details>
<summary> 1.3.0 </summary>

* Reworked generic definitions in both CursorMenus and Applications to remove the necessity of creating a new cursor menu for each new cursor element type.

  * Breaking change: expect dependents to break upon release of this version. They should only need to specify the highest type of cursor element they wish to use for their applications (e.g if they do not use counters, they should use `CursorElement` in the application specification)

* Implemented `CounterPageApplication` which allows both pagination and counter interaction (the respective controls from both are on separate key binds).
* Added default return to `CursorOutputElement.ApplyFunction()` if no `Func` was provided.
* Changed display of the output string in `BoxedOutputScreen` to allow unity string tags without affecting the box of the screen used, such as coloring of the text.

</details>

<details>
<summary> 1.2.0 </summary>

* Implemented `BaseHierarchyElement` (and its derived `TextHierarchyElement` for text only) and `BaseCursorHierarchyElement` (for selectable options) which aim to provide that visual of hierarchy splits or a tree.
* Fixed some issues with `CursorElement` displaying its text when it has description.
* Actually implemented `PreviousScreen` correctly for terminal application classes.

</details>

<details>
<summary> 1.1.4 </summary>

* `PageApplication` will change between screens when it reaches one of the boundaries of the current cursor menu
* `PageCursorElement` doesn't display the page counter if it only contains one screen.

</details>
<details>
<summary> 1.1.3 </summary>

* Added `RegisterApplication` method where you can specify if the listed commands can be case sensitive or not

</details>

<details>
<summary> <h1>1.1.2</h1> </summary>

* Fixed issue with title being too big that would break the whole screen.

</details>
<details>
<summary> <h1>1.1.1</h1> </summary>

* Abstracted application types and added base classes

</details>
<details>
<summary> <h1>1.1.0</h1> </summary>

* Added `InteractiveCounterApplication` as possible application to use by the developers.
* Added `CursorCounterElement` and `CursorCounterMenu` which are entries where players manipulate their counter
* Added `BoxedOutputScreen` which allows to show configurable output in the bottom right to what the developers want to show
* Added `Active` and `SelectInactive` attributes to `CursorElement` to distinguish between entries with expected output and entries which will output an error when attempt.
* Added sorting functionality to all applications which sort relevant cursor menus to defined sorting methods.

</details>

<details>
<summary> <h1>1.0.0</h1> </summary>

* Initial release

</details>


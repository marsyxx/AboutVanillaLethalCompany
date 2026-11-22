

<img src="https://gcdn.thunderstore.io/live/repository/icons/IntegrityChaos-LCCutscene-1.0.0.png.128x128_q95.png" width="150" height="150"/>

# LCCutscene (W.I.P.)
[LCCutscene](https://thunderstore.io/c/lethal-company/p/IntegrityChaos/LCCutscene/) is a tool to create cutscenes and immersive animations to Lethal Company. (Originaly made for [Diversity](https://thunderstore.io/c/lethal-company/p/IntegrityChaos/Diversity/))
## Examples
Cutscene:

<img src="https://i.imgur.com/BR1mJxy.gif" width="700" height="400"/>

Quickscene W.I.P.:

<img src="https://i.imgur.com/hQJlOWg.gif" width="400" height="400"/> <img src="https://i.imgur.com/Mg1PGgh.gif" width="400" height="400"/>

## Usage
### Cutscenes
- Add the following namespace:
```c#
using LCCutscene;
```
- Create your custom Cutscene using the ``Cutscene`` class:
```c#
Cutscene(Transform cutsceneTransform, Float cutsceneDuration)
{
    AllowPlayerDeath = false,
    AllowPlayerMovement = false,
    TransitionIn = Transition.FadeOut,
    TransitionOut = Transition.FadeOut,
    TransitionInSpeed = 1f,
    TransitionOutSpeed = 1f
};
```
> <img src="https://static-00.iconduck.com/assets.00/info-icon-2048x2048-tcgtx810.png" width="15" height="15"/> **NOTE**:
>  
> The cutscene transform is used for the cutscene camera to clamp on and is recommended to be animated!
- Play your new cutscene using the ``CutsceneManager``:
```c#
CutsceneManager.Instance.PlayScene(Cutscene myCutscene);
```
> <img src="https://static-00.iconduck.com/assets.00/info-icon-2048x2048-tcgtx810.png" width="15" height="15"/> **NOTE**:
> 
> Only one cutscene can be played at once!  *(Per player.)*
#### Full Example:
```c#
using System.Collections;
using System.Collections.Generic;
using LCCutscene;
using UnityEngine;

public class MyCutscene : MonoBehaviour
{
    public Transform animatedTransform; // The Transform where the cutscene camera will clamp on. //
    public Animatior animator; // Animator for animating your cutscene. //

    private Cutscene scene; // The cutscene itself. //

    void Start()
    {
	    // Create your cutscene. //
        scene = new Cutscene(animatedTransform, 10f)
		{
		    AllowPlayerDeath = false,
		    AllowPlayerMovement = false,
		    TransitionIn = Transition.FadeOut,
		    TransitionOut = Transition.FadeOut
		};
    }

    public void PlayMyCutscene(Collider col)
    {
        CutsceneManager.Instance.PlayScene(scene); // Play your cutscene. //
        animator.SetBool("PlayCameraAnimation", true); // Play your custom animation for your animated transform and much more! //
    }
}
```
### Quick Scenes [W.I.P.]
- Add the following namespace:
```c#
using LCCutscene;
```
- Create your custom Quickscene using the ``Quickscene`` class:
```c#
QuickScene(float duration, Transform targetPosition, float targetRotation, AnimationClip clip, Transform targetEndPosition = null, float? targetEndRotation = null, POVType pov = POVType.Full, PlayerPose startingPose = PlayerPose.Standing, PlayerPose endingPose = PlayerPose.Standing, bool snapPlayersToPositions = true, float snapSpeed = 10f)
```
> <img src="https://static-00.iconduck.com/assets.00/info-icon-2048x2048-tcgtx810.png" width="15" height="15"/> **NOTE**:
>  
> The ``AnimationClip clip`` can also be changed to ``List<AnimationClip> clips`` for more complex animations for the player. These clips will be played one after another in order. 

> <img src="https://cdn-icons-png.flaticon.com/512/10263/10263805.png" width="15" height="15"/> **IMPORTANT**:
> 
> ``POVType.ArmsOnly`` is currently in W.I.P. and won't be available until updated!
- Play your new Quickscene using the ``CutsceneManager``:
```c#
CutsceneManager.Instance.PlayScene(Quickscene myQuickscene);
```
> <img src="https://static-00.iconduck.com/assets.00/info-icon-2048x2048-tcgtx810.png" width="15" height="15"/> **NOTE**:
> 
> Only one quickscene can be played at once! *(Per player.)*
#### Full Example (Vaulting over desk example):
```c#
using System.Collections;
using System.Collections.Generic;
using LCCutscene;
using UnityEngine;

public class MyQuickscene : MonoBehaviour
{
    public Transform startTarget; // The start position and rotation for the player to clamp on. //
    public Transform endTarget; // The end position and rotation for the player to clamp on. //
    public AnimationClip clip; // The animation clip for the Player. //

    public QuickScene scene; // The Quickscene itself. //

    void Start()
    {
	    // Create your Quickscene. //
        scene = new Quickscene(clip.length, startTarget, startTarget.rotation.eulerAngles.y, clip, endTarget, endTarget.rotation.eulerAngles.y);
    }

	// If the local player reaches a designated area while running, it triggers the quickscene. //
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.TryGetComponent(out PlayerControllerB player))
        {
            if (player == StartOfRound.Instance.localPlayerController && !CutsceneManager.Instance.cutscenePlaying && player.isSprinting)
            {
                CutsceneManager.Instance.PlayScene(scene); // Play your Quickscene. //
            }
        }
    }
}
```
#### Syncing animations for other players *(Quickscene only)*:
- ``PlayAnimationOnOtherPlayer`` is used to play the same animation set to other players that aren't the local player:
```c#
public void PlayAnimationOnOtherPlayer(PlayerControllerB player, List<AnimationClip> clips)

public void PlayAnimationOnOtherPlayer(PlayerControllerB _player, QuickScene _quickScene)
```
> <img src="https://icons.veryicon.com/png/o/object/simple-life-fill-icon/lighting-1.png" width="15" height="15"/> **TIP**:
> 
> ``PlayAnimationOnOtherPlayer`` has two different option, either you use your own Quickscene or another animation set meant for third person viewing if it is needed.

- ``StopAnimationOnOtherPlayer`` is used to play the same animation set to other players that aren't the local player:
```c#
public void StopAnimationOnOtherPlayer(PlayerControllerB player)
```
> <img src="https://static-00.iconduck.com/assets.00/info-icon-2048x2048-tcgtx810.png" width="15" height="15"/> **NOTE**:
> 
> There's no need for any animation set or Quickscene for this function. This function's only purpose is to stop any custom animation on other players and returning to it's default state.

> <img src="https://cdn-icons-png.flaticon.com/512/10263/10263805.png" width="15" height="15"/> **IMPORTANT**:
> 
> There is no networking implemented in LCCutscene. It is recommended that you make your own networking to sync animations between players.
#### Subscribable events:
Here's a list of events that can be subscribed within the ``CutsceneManager``:
- ``OnSceneStart``
- ``OnSceneEnd``
- ``OnSceneTransitionIn``
- ``OnSceneTransitionOut``
> <img src="https://icons.veryicon.com/png/o/object/simple-life-fill-icon/lighting-1.png" width="15" height="15"/> **TIP**:
> 
> Those can be used for more complex tinkering for cutscenes. They also have no arguments since everything is accessible within the manager itself.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstantFall : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        if (GameplayManagers.InputManager.IsInputActive)
        {
            GameplayManagers.GameManager.CurrentTetrominoe.FallComponent.InstantFall();
            EventManager.TriggerEvent(Channels.TUTORIAL_CHANNEL, TutorialEvent.MOVE, MoveButtonType.INSTANT_FALL);
        }
    }
}

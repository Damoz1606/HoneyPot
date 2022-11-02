using UnityEngine;

public class GameOverState : _StatesBase
{
    public override void OnActivate()
    {
        EventManager.TriggerEvent(Channels.POP_CHANNEL, PopEvent.POP_ALL_WITHOUT_DISTINGUITION, null);
        // GameplayManagers.AudioManager.PlayPopupOpen();
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIGameOver);
        GameplayManagers.UIManager.GameOverPopup.OnActivatePopup();
    }

    public override void OnDeactivate()
    {
        // GameplayManagers.UIManager.GameOverPopup.OnDeactivatePopup();
    }

    public override void OnUpdate()
    {
    }
}
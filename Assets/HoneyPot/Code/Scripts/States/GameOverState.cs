using UnityEngine;

public class GameOverState : _StatesBase
{
    public override void OnActivate()
    {
        EventManager.TriggerEvent(Channels.POP_CHANNEL, PopEvent.POP_ALL_WITHOUT_DISTINGUITION, null);
        // GameplayManagers.AudioManager.PlayPopupOpen();
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIGameOver);
        GameplayManagers.UIManager.GameOverPopup.OnActivatePopup();
        Debug.Log("<color=green>Game Over State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        // GameplayManagers.UIManager.GameOverPopup.OnDeactivatePopup();
        Debug.Log("<color=red>Game Over State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        Debug.Log("<color=yellow>Game Over State</color> OnUpdate");
    }
}
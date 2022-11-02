using UnityEngine;

public class PauseState : _StatesBase
{
    public override void OnActivate()
    {
        GameplayManagers.UIManager.PausePopup.OnActivatePopup();
        GameplayManagers.GameManager.IsGameActive = false;
        GameplayManagers.InputManager.IsInputActive = false;
        Time.timeScale = 0f;
    }

    public override void OnDeactivate()
    {
        // GameplayManagers.AudioManager.PlayPopupClose();
        GameplayManagers.UIManager.PausePopup.OnDeactivatePopup();
        GameplayManagers.GameManager.IsGameActive = true;
        GameplayManagers.InputManager.IsInputActive = true;
        Time.timeScale = 1;
    }

    public override void OnUpdate()
    {
    }
}
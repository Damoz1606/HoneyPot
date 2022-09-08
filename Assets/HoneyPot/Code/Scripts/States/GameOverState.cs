using UnityEngine;

public class GameOverState : _StatesBase
{
    public override void OnActivate()
    {
        GameplayManagers.UIManager.GameOverPopup.OnActivatePopup();
        Time.timeScale = 0.01f;
        Debug.Log("<color=green>Game Over State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        GameplayManagers.UIManager.GameOverPopup.OnDeactivatePopup();
        Time.timeScale = 1;
        Debug.Log("<color=red>Game Over State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        Debug.Log("<color=yellow>Game Over State</color> OnUpdate");
    }
}
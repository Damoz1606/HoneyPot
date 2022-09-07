using UnityEngine;

public class PauseState : _StatesBase
{
    public override void OnActivate()
    {
        GameplayManagers.UIManager.PausePopup.OnActivatePopup();
        Time.timeScale = 0.01f;
        Debug.Log("<color=green>Pause State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        GameplayManagers.UIManager.PausePopup.OnDeactivatePopup();
        Time.timeScale = 1;
        Debug.Log("<color=red>Pause State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        Debug.Log("<color=yellow>Pause State</color> OnUpdate");
    }
}
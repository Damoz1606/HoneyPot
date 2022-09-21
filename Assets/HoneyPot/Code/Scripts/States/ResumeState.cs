using UnityEngine;

public class ResumeState : _StatesBase
{
    public override void OnActivate()
    {
        if (!GameplayManagers.GameManager.IsGameActive)
        {
            GameplayManagers.GameManager.IsGameActive = true;
        }
        if (!GameplayManagers.InputManager.IsInputActive)
        {
            GameplayManagers.InputManager.IsInputActive = true;
        }
        Debug.Log("<color=green>Resume State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        GameplayManagers.GameManager.IsGameActive = false;
        GameplayManagers.InputManager.IsInputActive = false;
        Debug.Log("<color=red>Resume State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        if (GameplayManagers.GameManager.IsGameActive && GameplayManagers.GameManager.CurrentTetromino != null)
        {
            GameplayManagers.GameManager.CurrentTetromino.FallController.FreeFall();
        }
        Debug.Log("<color=yellow>Resume State</color> OnUpdate");
    }
}
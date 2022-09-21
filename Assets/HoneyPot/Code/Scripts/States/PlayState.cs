using UnityEngine;

public class PlayState : _StatesBase
{
    public override void OnActivate()
    {
        if (!GameplayManagers.GameManager.IsGameActive)
        {
            GameplayManagers.UIManager.ActivateUI(UITypes.INGAME);
            GameplayManagers.AudioManager.PlayMusic();
            GameplayManagers.SpawnManager.TetrominoSpawnManager.Spawn();
            GameplayManagers.GameManager.IsGameActive = true;
        }
        Debug.Log("<color=green>Play State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        GameplayManagers.GameManager.IsGameActive = false;
        GameplayManagers.InputManager.IsInputActive = false;
        Debug.Log("<color=red>Play State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        if (GameplayManagers.GameManager.IsGameActive && GameplayManagers.GameManager.CurrentTetromino != null)
        {
            GameplayManagers.GameManager.CurrentTetromino.FallController.FreeFall();
        }
        Debug.Log("<color=yellow>Play State</color> OnUpdate");
    }
}
using System.Collections;
using UnityEngine;

public class PlayState : _StatesBase
{
    public override void OnActivate()
    {
        if (!GameplayManagers.GameManager.IsGameActive)
        {
            GameplayManagers.UIManager.ActivateUI(UITypes.INGAME);
            GameplayManagers.AudioManager.PlayMusic();
            StartCoroutine(this.StartGameCoroutine());
            // GameplayManagers.SpawnManager.TetrominoePoolSpawner.Spawn();
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
        if (GameplayManagers.GameManager.IsGameActive && GameplayManagers.GameManager.CurrentTetrominoe != null)
        {
            GameplayManagers.GameManager.CurrentTetrominoe.FallComponent.FreeFall();
        }
        // GameplayManagers.GoalManager.CheckCompletedGoals();
        Debug.Log("<color=yellow>Play State</color> OnUpdate");
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        GameplayManagers.SpawnManager.TetrominoeNormalSpawner.OnSpawn();
        GameplayManagers.GameManager.IsGameActive = true;
    }
}
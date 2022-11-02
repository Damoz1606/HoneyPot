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
        }
    }

    public override void OnDeactivate()
    {
        GameplayManagers.GameManager.IsGameActive = false;
        GameplayManagers.InputManager.IsInputActive = false;
    }

    public override void OnUpdate()
    {
        if (GameplayManagers.GameManager.IsGameActive && GameplayManagers.GameManager.CurrentTetrominoe != null)
        {
            GameplayManagers.GameManager.CurrentTetrominoe.FallComponent.FreeFall();
        }
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        if (!GameplayManagers.GameManager.HasStarted)
        {
            GameplayManagers.SpawnManager.TetrominoeNormalSpawner.OnSpawn();
            GameplayManagers.GameManager.HasStarted = true;
        }
        if (!GameplayManagers.InputManager.IsInputActive) GameplayManagers.InputManager.IsInputActive = true;
        GameplayManagers.GameManager.IsGameActive = true;
    }
}
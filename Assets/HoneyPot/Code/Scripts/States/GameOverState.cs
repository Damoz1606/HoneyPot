using UnityEngine;

public class GameOverState : _StatesBase
{
    public override void OnActivate()
    {
        Debug.Log("<color=green>Game Over State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        Debug.Log("<color=red>Game Over State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        Debug.Log("<color=yellow>Game Over State</color> OnUpdate");
    }
}
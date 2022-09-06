using UnityEngine;

public class PauseState : _StatesBase
{
    public override void OnActivate()
    {
        Debug.Log("<color=green>Pause State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        Debug.Log("<color=red>Pause State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        Debug.Log("<color=yellow>Pause State</color> OnUpdate");
    }
}
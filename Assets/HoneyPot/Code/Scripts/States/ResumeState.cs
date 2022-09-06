using UnityEngine;

public class ResumeState : _StatesBase
{
    public override void OnActivate()
    {
        Debug.Log("<color=green>Resume State</color> OnActive");
    }

    public override void OnDeactivate()
    {
        Debug.Log("<color=red>Resume State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        Debug.Log("<color=yellow>Resume State</color> OnUpdate");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialState : _StatesBase
{
    public override void OnActivate()
    {
        Time.timeScale = 0f;
    }

    public override void OnDeactivate()
    {
        Time.timeScale = 1f;
    }

    public override void OnUpdate()
    {
        // throw new System.NotImplementedException();
    }
}

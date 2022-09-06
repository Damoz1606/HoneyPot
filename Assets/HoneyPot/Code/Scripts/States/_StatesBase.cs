using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _StatesBase : MonoBehaviour
{
    public abstract void OnActivate();
    public abstract void OnUpdate();
    public abstract void OnDeactivate();
}

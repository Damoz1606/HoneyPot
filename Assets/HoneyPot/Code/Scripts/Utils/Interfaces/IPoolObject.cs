using UnityEngine;

public interface IPoolObject
{
    void OnActivate();
    void OnUpdate();
    void OnDeactivate();
}
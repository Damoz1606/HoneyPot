using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoalUI : IGameObject
{
    void OnActivate(object message);
    void OnUpdate(object message);
    void OnDeactivate(object message);
}

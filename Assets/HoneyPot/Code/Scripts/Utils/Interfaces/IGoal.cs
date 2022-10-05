using System.Collections.Generic;
using UnityEngine;

public interface IGoal
{
    void Complete();
    void UpdateGoal(object message);
    void CloseGoal();
}
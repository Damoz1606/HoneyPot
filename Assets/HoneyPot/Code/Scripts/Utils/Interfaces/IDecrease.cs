using System.Collections.Generic;
using UnityEngine;

public interface IDecrease
{
    void DecreaseAllAbove(List<Vector3Int> targets);
    void DecreaseAbove(Vector3Int target);
}
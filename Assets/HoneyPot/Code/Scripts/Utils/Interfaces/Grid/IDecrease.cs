using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IDecrease
{
    void DecreaseAllAbove(List<Vector3Int> targets);
    Task DecreaseAbove(Vector3Int target);
}
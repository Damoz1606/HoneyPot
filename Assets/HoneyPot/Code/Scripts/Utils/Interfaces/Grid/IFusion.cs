using System.Collections.Generic;
using UnityEngine;

public interface IFusion<T>
where T : IBlock
{
    void FusionCellsTo(T block, List<T> blocks);
}
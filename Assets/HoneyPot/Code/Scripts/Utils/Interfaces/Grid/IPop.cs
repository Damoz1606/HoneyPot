using System.Collections.Generic;
using UnityEngine;

public interface IPop<T>
where T : IBlock
{
    bool CanPop();
    bool CanPop(T block);

    void Pop(T block);
    void Pop();
    void PopAllBlocks(object block);
    // void PopAllBlocks();
    // void PopAround(T block);
    void PopAround(object block);

    // void DestroyBlock(Vector3Int vector);
}
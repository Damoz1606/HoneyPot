using UnityEngine;

public interface IPop<T>
where T : IBlock
{
    bool CanPop();
    bool CanPop(T block);

    void Pop(T block);
    void Pop();
    void PopAllBlocks();
    void PopAround(T block);

    // void DestroyBlock(Vector3Int vector);
}
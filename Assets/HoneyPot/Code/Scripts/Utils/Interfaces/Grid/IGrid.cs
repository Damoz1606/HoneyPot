using UnityEngine;

public interface IGrid<T>
where T : IBlock
{
    Column<T>[] Grid { get; }
    int Width { get; }
    int Height { get; }

    void InitGrid(int width, int height);
    bool IsInsideBounds(int x, int y);
    T GetAt(int x, int y);
    void SetAt(int x, int y, T block);
}
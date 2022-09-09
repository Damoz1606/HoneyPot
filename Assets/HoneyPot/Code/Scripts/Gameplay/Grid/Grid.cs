using UnityEngine;

public abstract class Grid : MonoBehaviour
{
    public abstract void ClearGrid();
    public abstract void DecreaseTile(Vector2 tile);
    public abstract void RemoveTile(Vector2 tile);

    [SerializeField] protected int _gridHeight;
    [SerializeField] protected int _gridWidth;
    protected Transform[,] _grid;

    public void InitGrid(int width, int height)
    {
        this._grid = new Transform[width, height];
        this._gridWidth = width;
        this._gridHeight = height;
    }

    public bool IsInsideBounds(Vector2 position)
    {
        int x = Mathf.RoundToInt((int)position.x);
        int y = Mathf.RoundToInt((int)position.y);
        return (x >= 0 && x < this._gridWidth && y >= 0);
    }

    public Transform GetTileAtGridPosition(Vector2 position)
    {
        int x = Mathf.RoundToInt((int)position.x);
        int y = Mathf.RoundToInt((int)position.y);
        if (this.IsInsideBounds(position))
        {
            return this._grid[x, y];
        }
        return null;
    }


}
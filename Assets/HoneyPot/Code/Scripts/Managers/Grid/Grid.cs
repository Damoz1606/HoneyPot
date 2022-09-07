using UnityEngine;

public abstract class Grid : MonoBehaviour
{
    public abstract void ClearGrid();
    public abstract void DecreaseTile(Vector2 tile);
    public abstract void RemoveTile(Vector2 tile);

    protected Transform[,] _grid = new Transform[Constants.GRID_WIDTH, Constants.GRID_HEIGHT];

    public int Grid_Height { get { return this._grid.Length; } }
    public int Grid_Width { get { return this._grid.Length; } }

    public bool IsInsideBounds(Vector2 position)
    {
        int x = Mathf.RoundToInt((int)position.x);
        int y = Mathf.RoundToInt((int)position.y);
        return (x >= 0 && x < Constants.GRID_WIDTH && y >= 0);
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
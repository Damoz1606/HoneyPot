using UnityEngine;

public abstract class Grid : MonoBehaviour
{
    public abstract void ClearGrid();
    public abstract void DecreaseTile(Vector2 tile);
    public abstract void RemoveTile(Vector2 tile);

    protected Transform[,] _grid = new Transform[Constants.GRID_WIDTH, Constants.GRID_HEIGHT];

    public bool IsInsideBounds(Vector2 position)
    {
        return ((int)position.x >= 0 && (int)position.x < Constants.GRID_WIDTH && (int)position.y >= 0);
    }

    public Transform GetTileAtGridPosition(Vector2 position)
    {
        if (this.IsInsideBounds(position))
        {
            return this._grid[(int)position.x, (int)position.y];
        }
        return null;
    }


}
using UnityEngine;

public abstract class Grid : MonoBehaviour
{
    public abstract void ClearGrid();
    public abstract void DecreaseTile(Vector2 tile);
    public abstract void RemoveTile(Vector2 tile);

    [SerializeField] protected int _gridHeight;
    [SerializeField] protected int _gridWidth;

    protected Column[] _gameGridColumn;

    public void InitGrid(int width, int height)
    {
        this._gridWidth = width;
        this._gridHeight = height;

        this._gameGridColumn = new Column[width];
        for (int x = 0; x < width; x++)
        {
            this._gameGridColumn[x] = new Column(height);
        }
    }

    public bool IsInsideBounds(Vector2 position)
    {
        Vector2 rounded = VectorRound.Vector2Round(position);
        return (rounded.x >= 0 && rounded.x < this._gridWidth && rounded.y >= 0);
    }

    public Transform GetTileAtGridPosition(Vector2 position)
    {
        int x = Mathf.RoundToInt((int)position.x);
        int y = Mathf.RoundToInt((int)position.y);
        if (this.IsInsideBounds(position))
        {
            return this._gameGridColumn[x].row[y];
        }
        return null;
    }
}
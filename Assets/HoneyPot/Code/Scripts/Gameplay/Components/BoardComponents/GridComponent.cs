using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridComponent : MonoBehaviour, IGrid<IBlock>
{
    private Column<IBlock>[] _grid;

    public Column<IBlock>[] Grid { get { return this._grid; } }

    public int Width { private set; get; }
    public int Height { private set; get; }

    public void InitGrid(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this._grid = new Column<IBlock>[width];
        for (int x = 0; x < width; x++)
        {
            this._grid[x] = new Column<IBlock>(height);
        }
    }

    public bool IsInsideBounds(int x, int y) => this.IsInsideBounds(new Vector2Int(x, y));
    public bool IsInsideBounds(Vector2 position) => this.IsInsideBounds(VectorRound.Vector2Round(position));
    public bool IsInsideBounds(Vector2Int position)
    {
        return (position.x >= 0 && position.x < this.Width && position.y >= 0);
    }

    public IBlock GetAt(int x, int y) => this.GetAt(new Vector2Int(x, y));
    public IBlock GetAt(Vector2 position) => this.GetAt(VectorRound.Vector2Round(position));
    public IBlock GetAt(Vector2Int position) => (IsInsideBounds(position)) ? this._grid[(int)position.x].row[(int)position.y] : null;

    public void SetAt(int x, int y, IBlock block) => this.SetAt(new Vector2Int(x, y), block);
    public void SetAt(Vector2 position, IBlock block) => this.SetAt(VectorRound.Vector2Round(position), block);
    public void SetAt(Vector2Int position, IBlock block)
    {
        if (IsInsideBounds(position))
            this._grid[position.x].row[position.y] = block;
    }
}

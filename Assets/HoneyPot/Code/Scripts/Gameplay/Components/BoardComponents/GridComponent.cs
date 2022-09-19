using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridComponent : MonoBehaviour
{
    private Column<Block>[] _grid;

    public Column<Block>[] Grid { get { return this._grid; } }

    public int Width { private set; get; }
    public int Height { private set; get; }

    public void InitGrid(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this._grid = new Column<Block>[width];
        for (int x = 0; x < width; x++)
        {
            this._grid[x] = new Column<Block>(height);
        }
    }

    public bool IsInsideBounds(int x, int y) => this.IsInsideBounds(new Vector2Int(x, y));
    public bool IsInsideBounds(Vector2 position) => this.IsInsideBounds(VectorRound.Vector2Round(position));
    public bool IsInsideBounds(Vector2Int position)
    {
        return (position.x >= 0 && position.x < this.Width && position.y >= 0);
    }

    public Block GetBlockAt(int x, int y) => this.GetBlockAt(new Vector2Int(x, y));
    public Block GetBlockAt(Vector2 position) => this.GetBlockAt(VectorRound.Vector2Round(position));
    public Block GetBlockAt(Vector2Int position)
    {
        return (IsInsideBounds(position)) ? this._grid[(int)position.x].row[(int)position.y] : null;
    }

    public void SetBlockAt(int x, int y, Block block) => this.SetBlockAt(new Vector2Int(x, y), block);
    public void SetBlockAt(Vector2 position, Block block) => this.SetBlockAt(VectorRound.Vector2Round(position), block);
    public void SetBlockAt(Vector2Int position, Block block)
    {
        if (IsInsideBounds(position))
            this._grid[position.x].row[position.y] = block;
    }

    public void ClearGrid()
    {

    }
}

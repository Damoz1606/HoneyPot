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

    public bool IsInsideBounds(Vector2 position)
    {
        return (position.x >= 0 && position.x < this.Width && position.y >= 0);
    }

    public Block GetBlockAt(Vector2 position)
    {
        return (IsInsideBounds(position)) ? this._grid[(int)position.x].row[(int)position.y] : null;
    }

    public void SetBlockAt(Vector2 position, Block block)
    {
        if (IsInsideBounds(position)) this._grid[(int)position.x].row[(int)position.y] = block;
    }

    public void ClearGrid()
    {

    }
}

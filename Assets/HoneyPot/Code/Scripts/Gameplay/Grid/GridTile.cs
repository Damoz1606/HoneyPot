using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridTile : Grid
{
    public void AddToGrid(Transform tile)
    {
        this._gameGridColumn[(int)tile.position.x].row[(int)tile.position.y] = tile;
    }

    public void SwapTiles(Transform tile, Transform desireTile)
    {
        Vector3 pos = tile.position;
        tile.position = desireTile.position;
        desireTile.position = pos;
        this._gameGridColumn[(int)tile.position.x].row[(int)tile.position.y] = tile;
        this._gameGridColumn[(int)desireTile.position.x].row[(int)desireTile.position.y] = desireTile;
    }

    public bool HasMatches(Transform tile)
    {
        List<Transform> horizontal = this.FindHorizontalMatches(tile);
        List<Transform> vertical = this.FindVerticalMatches(tile);
        return horizontal.Count >= Constants.MIN_MATCH_COUNT || vertical.Count >= Constants.MIN_MATCH_COUNT;
    }

    public List<Transform> FindHorizontalMatches(Transform tile)
    {
        Vector2 tilePosition = tile.position;
        List<Transform> matches = new List<Transform>();
        for (int x = (int)tilePosition.x + 1; x < this._gridWidth; x++)
        {
            if (this._gameGridColumn[x].row[(int)tilePosition.y] == null || !this._gameGridColumn[x].row[(int)tilePosition.y].CompareTag(tile.tag))
            {
                break;
            }
            matches.Add(this._gameGridColumn[x].row[(int)tilePosition.y]);
        }

        for (int x = (int)tilePosition.x - 1; x >= 0; x--)
        {
            if (this._gameGridColumn[x].row[(int)tilePosition.y] == null || !this._gameGridColumn[x].row[(int)tilePosition.y].CompareTag(tile.tag))
            {
                break;
            }
            matches.Add(this._gameGridColumn[x].row[(int)tilePosition.y]);
        }
        return matches;
    }

    public List<Transform> FindVerticalMatches(Transform tile)
    {
        Vector2 tilePosition = tile.position;
        List<Transform> matches = new List<Transform>();
        for (int y = (int)tilePosition.y + 1; y < this._gridHeight; y++)
        {
            if (this._gameGridColumn[(int)tilePosition.x].row[y] == null || !this._gameGridColumn[(int)tilePosition.x].row[y].CompareTag(tile.tag))
            {
                break;
            }
            matches.Add(this._gameGridColumn[(int)tilePosition.x].row[y]);
        }

        for (int y = (int)tilePosition.y - 1; y >= 0; y--)
        {
            if (this._gameGridColumn[(int)tilePosition.x].row[y] == null || !this._gameGridColumn[(int)tilePosition.x].row[y].CompareTag(tile.tag))
            {
                break;
            }
            matches.Add(this._gameGridColumn[(int)tilePosition.x].row[y]);
        }
        return matches;
    }

    public void RemoveNeigbourMatches(Transform tile)
    {
        List<Transform> horizontal = this.FindHorizontalMatches(tile);
        List<Transform> vertical = this.FindVerticalMatches(tile);

        if (horizontal.Count >= Constants.MIN_MATCH_COUNT)
        {
            horizontal.Add(tile);
            horizontal.ForEach(element =>
            {
                this.RemoveTile(element.position);
                GameplayManagers.GridManager.GridTetromino.RemoveTile(element.position);
            });
            this.DecreaseHorizontalTiles(horizontal);
        }
        else if (vertical.Count >= Constants.MIN_MATCH_COUNT)
        {
            vertical.Add(tile);
            vertical.ForEach(element =>
            {
                this.RemoveTile(element.position);
                GameplayManagers.GridManager.GridTetromino.RemoveTile(element.position);
            });
            this.DecreaseVerticalTiles(vertical);
        }

    }

    public void DecreaseHorizontalTiles(List<Transform> tiles)
    {
        tiles.ForEach(tile =>
        {
            Vector2 position = tile.position;
            position.y += 1;
            this.DecreaseTilesAbove(position);
        });
    }

    public void DecreaseVerticalTiles(List<Transform> tiles)
    {
        float maxY = tiles.Max(tile => tile.position.y);
        float minY = tiles.Min(tile => tile.position.y);
        float maxX = tiles[0].position.x;
        for (int y = (int)maxY; y >= minY; y--)
        {
            Vector2 abovePosition = new Vector2(maxX, y + 1);
            this.DecreaseTilesAbove(abovePosition);
        }
    }

    public void DecreaseTilesAbove(Vector2 abovePosition)
    {
        for (int i = (int)abovePosition.y; i < this._gridHeight; i++)
        {
            GameplayManagers.GridManager.GridTetromino.DecreaseTile(new Vector2(abovePosition.x, i));
            this.DecreaseTile(new Vector2(abovePosition.x, i));
        }
    }

    public override void ClearGrid()
    {
        throw new System.NotImplementedException();
    }

    public override void DecreaseTile(Vector2 tile)
    {
        int x = (int)tile.x;
        int y = (int)tile.y;
        if (this._gameGridColumn[x].row[y] != null)
        {
            this._gameGridColumn[x].row[y - 1] = this._gameGridColumn[x].row[y];
            this._gameGridColumn[x].row[y] = null;
            this._gameGridColumn[x].row[y - 1].position += new Vector3(0, -1, 0);
        }
    }

    public override void RemoveTile(Vector2 tile)
    {
        int x = (int)tile.x;
        int y = (int)tile.y;
        if (this._gameGridColumn[x].row[y] != null)
        {
            Destroy(this._gameGridColumn[x].row[y].gameObject);
            this._gameGridColumn[x].row[y] = null;
        };
    }
}
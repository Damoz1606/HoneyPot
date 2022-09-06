using System.Collections;
using UnityEngine;

public class GridTetromino : Grid
{
    public bool IsValidGridPosition(Transform tetromino)
    {
        return true;
    }

    public void UpdateGrid(Transform tetromino)
    {

    }

    public override void ClearGrid()
    {
        throw new System.NotImplementedException();
    }

    public override void DecreaseTile(Vector2 tile)
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveTile(Vector2 tile)
    {
        throw new System.NotImplementedException();
    }
}
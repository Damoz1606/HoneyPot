using System.Collections.Generic;
using UnityEngine;

public class GridTile : Grid
{
    public void AddToGrid(Transform tile)
    {

    }

    public void SwapTiles(Transform tile, Transform desireTile)
    {

    }

    public bool HasMatches(Transform tile)
    {
        return true;
    }

    public List<Transform> FindHorizontalMatches(Transform tile)
    {
        return null;
    }

    public List<Transform> FindVerticalMatches(Transform tile)
    {
        return null;
    }

    public void RemoveNeigbourMatches(Transform tile)
    {

    }

    public void DecreaseHorizontalTiles(List<Transform> tiles)
    {

    }

    public void DecreaseVerticalTiles(List<Transform> tiles)
    {

    }

    public void DecreaseTilesAbove(Vector2 abovePosition)
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeComponent : MonoBehaviour
{
    public void SwipeVertical(bool isUp)
    {
        int swipePosition = isUp ? 1 : -1;
        Vector3 desirePosition = this.transform.position;
        desirePosition.y += swipePosition;
        if (GameplayManagers.GridManager.GridTile.GetTileAtGridPosition(desirePosition) == null) return;
        this.SwipeTile(GameplayManagers.GridManager.GridTile.GetTileAtGridPosition(desirePosition));
    }

    public void SwipeHorizontal(bool isRight)
    {
        int swipePosition = isRight ? 1 : -1;
        Vector3 desirePosition = this.transform.position;
        desirePosition.x += swipePosition;
        if (GameplayManagers.GridManager.GridTile.GetTileAtGridPosition(desirePosition) == null) return;
        this.SwipeTile(GameplayManagers.GridManager.GridTile.GetTileAtGridPosition(desirePosition));
    }

    public void SwipeTile(Transform nextTile)
    {
        GameplayManagers.GridManager.GridTile.SwapTiles(this.transform, nextTile);
        bool nextHasMatches = GameplayManagers.GridManager.GridTile.HasMatches(this.transform);
        bool currentHasMatches = GameplayManagers.GridManager.GridTile.HasMatches(nextTile);

        if (nextHasMatches)
        {
            GameplayManagers.GridManager.GridTile.RemoveNeigbourMatches(this.transform);
        }

        if (currentHasMatches)
        {
            GameplayManagers.GridManager.GridTile.RemoveNeigbourMatches(nextTile);
        }

        if (!nextHasMatches && !currentHasMatches)
        {
            GameplayManagers.GridManager.GridTile.SwapTiles(this.transform, nextTile);
        }

    }
}

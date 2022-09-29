using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeComponent : MonoBehaviour
{
    public Vector2 GetNextVerticalPosition(bool isUp)
    {
        int swipePosition = isUp ? 1 : -1;
        Vector3 desirePosition = this.transform.position;
        desirePosition.y += swipePosition;
        return desirePosition;
    }

    public Vector2 GetNextHorizontalPosition(bool isRight)
    {
        int swipePosition = isRight ? 1 : -1;
        Vector3 desirePosition = this.transform.position;
        desirePosition.x += swipePosition;
        return desirePosition;
    }

    public void SwipeBlock(IBlock nextBlock)
    {
        IBlock current = this.GetComponent<IBlock>();
        GameplayManagers.GridManager.Board.SwapBlock(current, nextBlock);
    }
}

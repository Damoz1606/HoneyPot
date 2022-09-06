using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeComponent : MonoBehaviour
{
    public void SwipeVertical(bool isUp)
    {
        int swipePosition = isUp ? 1 : -1;
        this.SwipeTile(null);
    }

    public void SwipeHorizontal(bool isRight)
    {
        int swipePosition = isRight ? 1 : -1;
        this.SwipeTile(null);
    }

    public void SwipeTile(Transform nextTile)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderUIAnimation : MonoBehaviour
{
    [SerializeField] RectTransformMoveAnimtation[] _rects;

    public void StartAnimation()
    {
        foreach (RectTransformMoveAnimtation rect in _rects)
        {
            rect.EnterAnimation();
        }
    }

    public void EndAnimation()
    {
        foreach (RectTransformMoveAnimtation rect in _rects)
        {
            rect.ExitAnimation();
        }
    }
}

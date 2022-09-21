using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class FusionUtils
{
    public static void FusionCells<T>(T cell, List<T> connections, Column<T>[] grid, float tweeningTime = Constants.TWEENING_FUSION_TIME)
    where T : Block
    {
        foreach (T connection in connections)
        {
            connection.transform.DOMove(cell.transform.position, tweeningTime)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => cell.Destroy());
        }
    }
}
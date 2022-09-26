using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public static class FusionUtils
{
    public static async void FusionCells<T>(T cell, List<T> connections, Column<T>[] grid, float tweeningTime = Constants.TWEENING_FUSION_TIME)
    where T : Block
    {
        await FusionCellsAsync(cell, connections, grid, tweeningTime);
    }

    public static async Task FusionCellsAsync<T>(T cell, List<T> connections, Column<T>[] grid, float tweeningTime = Constants.TWEENING_FUSION_TIME)
    where T : Block
    {
        foreach (T connection in connections)
        {
            await connection.transform.DOMove(cell.transform.position, tweeningTime)
                .SetEase(Ease.Linear)
                .Play()
                .AsyncWaitForCompletion();
        }
    }
}
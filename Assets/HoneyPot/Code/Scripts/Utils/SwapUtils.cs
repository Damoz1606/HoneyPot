using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public static class SwapUtils
{
    public static async void Swap<T, C>(T currentCell, T nextCell, float tweeningTime = Constants.TWEENING_SWAP_TIME)
    where T : Block
    where C : Tile
    {
        currentCell.IsSwapping = true;
        nextCell.IsSwapping = true;
        await SwapAsync<T, C>(currentCell, nextCell, tweeningTime);
        currentCell.IsSwapping = false;
        nextCell.IsSwapping = false;
    }

    public static async void Swap<T, C>(T currentCell, T nextCell, Column<T>[] grid, float tweeningTime = Constants.TWEENING_SWAP_TIME)
    where T : Block
    where C : Tile
    {
        currentCell.IsSwapping = true;
        nextCell.IsSwapping = true;
        await SwapAsync<T, C>(currentCell, nextCell, tweeningTime);

        bool currentCanPop = PopUtils.CanPop<T>(currentCell);
        bool nextCanPop = PopUtils.CanPop<T>(nextCell);

        if (currentCanPop)
        {
            Vector2[] targets = PopUtils.Pop<T>(currentCell, grid, tweeningTime);
            DecreaseUtils.DecreaseAllAbove<T>(targets, grid);
        }

        if (nextCanPop)
        {
            Vector2[] targets = PopUtils.Pop<T>(currentCell, grid, tweeningTime);
            DecreaseUtils.DecreaseAllAbove<T>(targets, grid);
        }

        if (!nextCanPop && !currentCanPop)
        {
            await SwapAsync<T, C>(currentCell, nextCell, tweeningTime);
        }
        currentCell.IsSwapping = false;
        nextCell.IsSwapping = false;
    }

    private static async Task SwapAsync<T, C>(T currentCell, T nextCell, float tweeningTime = Constants.TWEENING_SWAP_TIME)
    where T : Block
    where C : Tile
    {
        C currentChild = (C)currentCell.Child;
        C nextChild = (C)nextCell.Child;

        if (currentCell == null || nextChild == null) return;
        var sequence = DOTween.Sequence();
        sequence.Join(currentChild.transform.DOMove(nextChild.transform.position, tweeningTime)).SetEase(Ease.OutBack)
        .Join(nextChild.transform.DOMove(currentChild.transform.position, tweeningTime)).SetEase(Ease.OutBack);

        currentChild.transform.SetParent(nextCell.transform);
        nextChild.transform.SetParent(currentCell.transform);
        currentCell.Child = nextChild;
        nextCell.Child = currentChild;

        await sequence.Play().AsyncWaitForCompletion();


    }
}
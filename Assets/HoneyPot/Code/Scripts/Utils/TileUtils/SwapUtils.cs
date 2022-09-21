using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public static class SwapUtils
{
    public static async Task SwapAsync<T, C>(T currentCell, T nextCell, float tweeningTime = Constants.TWEENING_SWAP_TIME)
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

        ComboSwap<T>(currentCell);
        ComboSwap<T>(nextCell);
    }

    public static void ComboSwap<T>(T cell)
    where T : Block
    {
        if (cell.Child.Type.Equals(TileTypes.COMBO))
            cell.Child.OnEffect(cell);

    }
}
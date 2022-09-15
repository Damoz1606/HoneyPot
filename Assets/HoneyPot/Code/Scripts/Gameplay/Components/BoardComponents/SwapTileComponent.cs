using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SwapTileComponent : MonoBehaviour
{
    [SerializeField] private float tweeningTime = 0.25f;
    [SerializeField] private Transform _swappingBoard;

    public async void SwapTiles(Block currentBlock, Block nextBlock, Column<Block>[] grid)
    {
        await this.SwapAsync(currentBlock, nextBlock);
    }

    private async Task SwapAsync(Block currentBlock, Block nextBlock)
    {
        Tile currentTile = currentBlock.Child;
        Tile nextTile = nextBlock.Child;

        var sequence = DOTween.Sequence();

        currentTile.transform.SetParent(_swappingBoard);
        nextTile.transform.SetParent(_swappingBoard);

        sequence.Join(currentTile.transform.DOMove(nextTile.transform.position, tweeningTime)).SetEase(Ease.OutBack)
        .Join(nextTile.transform.DOMove(currentTile.transform.position, tweeningTime)).SetEase(Ease.OutBack);

        await sequence.Play().AsyncWaitForCompletion();

        currentTile.transform.SetParent(nextBlock.transform);
        nextTile.transform.SetParent(currentBlock.transform);

        currentBlock.Child = nextTile;
        nextBlock.Child = currentTile;
    }
}

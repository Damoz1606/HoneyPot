using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SwapComponent : MonoBehaviour, ISwap<IBlock>
{
    [SerializeField] private TweeningModel _tweening;

    public void ComboSwap(IBlock block)
    {
        if (block == null) return;
        if (block.tile.type.Equals(TileTypes.COMBO))
            block.tile.OnEffect(block);
    }

    public async Task Swap(IBlock currentBlock, IBlock nextBlock)
    {
        if (currentBlock == null || nextBlock == null) return;
        ITile currentTile = currentBlock.tile;
        ITile nextTile = nextBlock.tile;

        var sequence = DOTween.Sequence();
        sequence.Join(currentTile.transform.DOMove(nextTile.transform.position, this._tweening.tweeningTime).SetEase(Ease.OutBack))
        .Join(nextTile.transform.DOMove(currentTile.transform.position, this._tweening.tweeningTime).SetEase(Ease.OutBack));

        await sequence.Play().AsyncWaitForCompletion();

        currentBlock.AttachTile(nextTile);
        nextBlock.AttachTile(currentTile);

        this.ComboSwap(currentBlock);
        this.ComboSwap(nextBlock);
    }
}
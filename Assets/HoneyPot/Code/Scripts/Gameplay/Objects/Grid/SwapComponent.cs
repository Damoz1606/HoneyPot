using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GridComponent))]
public class SwapComponent : MonoBehaviour, ISwap<IBlock>
{
    [SerializeField] private TweeningModel _tweening;
    private GridComponent _gridComponent;

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
    }

    public void ComboSwap(IBlock block)
    {
        if (block == null) return;
        if (block.tile == null) return;
        if (block.tile.type.Equals(TileNormalType.COMBO))
            this._gridComponent.RemoveAt(block.Position.x, block.Position.y);
    }


    public async Task Swap(IBlock currentBlock, IBlock nextBlock)
    {
        if (currentBlock == null || nextBlock == null) return;
        ITile currentTile = currentBlock.tile;
        ITile nextTile = nextBlock.tile;

        if (currentTile == null || nextTile == null) return;

        var sequence = DOTween.Sequence();

        await sequence.Append(currentTile.transform.DOMove(nextTile.transform.position, this._tweening.tweeningTime))
        .Append(nextTile.transform.DOMove(currentTile.transform.position, this._tweening.tweeningTime))
        .Play()
        .SetEase(Ease.OutBack)
        .AsyncWaitForCompletion();

        currentBlock.AttachTile(nextTile);
        nextBlock.AttachTile(currentTile);
        this.ComboSwap(currentBlock);
        this.ComboSwap(nextBlock);
    }
}
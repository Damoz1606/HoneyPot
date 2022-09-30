using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GridComponent))]
public class DecreaseComponent : MonoBehaviour, IDecrease
{
    [SerializeField] private TweeningModel _tweening;

    private GridComponent _gridComponent;

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
    }

    public async Task DecreaseAbove(Vector3Int target)
    {
        int width = this._gridComponent.Width;
        int height = this._gridComponent.Height;

        int x = (int)target.x;
        for (int y = (int)target.y + 1; y < height; y++)
        {
            IBlock block = this._gridComponent.GetAt(x, y);
            if (block == null) continue;
            if (!block.CanDecrease) continue;
            this._gridComponent.SetAt(x, y - 1, block);
            this._gridComponent.SetAt(x, y, default);
            await block.transform
            .DOMove(block.transform.position + Vector3.down, this._tweening.tweeningTime)
            .SetEase(Ease.OutBounce)
            .Play().AsyncWaitForCompletion();
        }
    }

    public async void DecreaseAllAbove(List<Vector3Int> targets)
    {
        targets = targets.OrderByDescending(o => o.y).ToList();
        foreach (Vector3Int target in targets)
            await this.DecreaseAbove(target);
    }
}
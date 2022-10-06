using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SwipeComponent))]
public class BlockNormal : MonoBehaviour, IBlock, IPoolObject
{
    [SerializeField] private TweeningModel _tweening;

    public bool CanPop { get; set; }
    public bool CanDecrease { get; set; }
    public bool CanSwap { get; set; }
    public bool IsSwapping { get; set; }
    public bool IsDecreasing { get; set; }
    public ITile tile { get; set; }

    public Vector3Int Position => VectorRound.Vector3Round(this.transform.position);

    public IBlock Top => (this.Position.y < GameplayManagers.GridManager.GridHeight - 1) ? GameplayManagers.GridManager.Board.GetAt(this.Position.x, this.Position.y + 1) : default;

    public IBlock Bottom => (this.Position.y > 0) ? GameplayManagers.GridManager.Board.GetAt(this.Position.x, this.Position.y - 1) : default;

    public IBlock Right => (this.Position.x < GameplayManagers.GridManager.GridWidth - 1) ? GameplayManagers.GridManager.Board.GetAt(this.Position.x + 1, this.Position.y) : default;

    public IBlock Left => (this.Position.x > 0) ? GameplayManagers.GridManager.Board.GetAt(this.Position.x - 1, this.Position.y) : default;

    public List<IBlock> Neighbourhood => new List<IBlock> { Top, Left, Right, Bottom };

    public List<IBlock> NeighboursVertical => new List<IBlock> { Top, Bottom };

    public List<IBlock> NeighboursHorizontal => new List<IBlock> { Left, Right };

    private void Update()
    {
        if (this.IsSwapping) return;
        if (this.tile == null) return;
        if (this.tile.transform.localPosition != Vector3.zero)
        {
            this.tile.transform.DOLocalMove(Vector3.zero, 0.1f).Play();
        }
    }

    public void AttachTile(ITile tile)
    {
        tile.transform.localScale = Vector3.one;
        this.tile = tile;
        this.tile.transform.SetParent(null);
        ObjectPosition.ObjectResetPosition(this.tile.gameObject);
        this.tile.transform.SetParent(this.transform);
        ObjectPosition.ObjectResetLocalPosition(this.tile.gameObject);
        ObjectPosition.ObjectResetRotation(this.tile.gameObject);
    }

    public void DeattachTile()
    {
        this.tile.transform.SetParent(null);
        this.tile = null;
    }

    public List<IBlock> GetConnections(List<IBlock> exclude = null)
    {
        List<IBlock> result = new List<IBlock> { this, };
        if (exclude == null)
            exclude = new List<IBlock> { this, };
        else
            exclude.Add(this);

        foreach (IBlock neighbour in Neighbourhood)
        {
            if (this.tile == null) continue;
            if (neighbour == null) continue;
            if (exclude.Contains(neighbour)) continue;
            if (!neighbour.tile.type.Equals(this.tile.type)) continue;
            result.AddRange(neighbour.GetConnections(exclude));
        }
        return result;
    }

    public List<IBlock> GetConnections(AxisTypes axis, List<IBlock> neighbours = null, List<IBlock> exclude = null)
    {
        List<IBlock> result = new List<IBlock> { this, };
        if (exclude == null)
            exclude = new List<IBlock> { this, };
        else
            exclude.Add(this);

        if (neighbours == null)
            if (axis.Equals(AxisTypes.HORIZONTAL))
                neighbours = this.NeighboursHorizontal;
            else if (axis.Equals(AxisTypes.VERTICAL))
                neighbours = this.NeighboursVertical;

        foreach (IBlock neighbour in neighbours)
        {
            if (neighbour == null) continue;
            if (neighbour.tile == null) continue;
            if (this.tile == null) continue;
            if (exclude.Contains(neighbour)) continue;
            if (!neighbour.tile.type.Equals(this.tile.type)) continue;
            if (axis == AxisTypes.HORIZONTAL)
                result.AddRange(neighbour.GetConnections(axis, neighbour.NeighboursHorizontal, exclude));
            else if (axis == AxisTypes.VERTICAL)
                result.AddRange(neighbour.GetConnections(axis, neighbour.NeighboursVertical, exclude));
        }
        return result;
    }

    public void OnActivate()
    {
        this.transform.localScale = Vector3.one;
        this.CanDecrease = false;
        this.CanSwap = false;
        this.CanPop = false;
        this.IsSwapping = false;
        this.IsDecreasing = false;
    }

    public void OnDeactivate()
    {
        this.transform.localScale = Vector3.one;
        this.CanDecrease = false;
        this.CanSwap = false;
        this.CanPop = false;
        this.IsSwapping = false;
        this.IsDecreasing = false;
    }

    public void OnEffect(ParticlesTypes type = ParticlesTypes.DEFAULT)
    {
        this.tile.OnEffect(this);
        EventManager.TriggerEvent(Channels.PARTICLE_CHANNEL,
        ParticleEvent.START_PARTICLE,
        new Dictionary<string, object> { { Constants.POSITION, this.Position }, { Constants.TYPE, type } });
        this.transform.DOScale(Vector3.zero, this._tweening.tweeningTime)
        .SetEase(this._tweening.tweeningEase)
        .Play()
        .OnComplete(() => GameplayManagers.SpawnManager.BlockNormalSpawner.OnKill(this));
    }

    public void OnUpdate()
    {

    }

    public void MoveTo(Vector3Int vector)
    {
        this.transform.DOMove(vector, this._tweening.tweeningTime)
        .SetEase(this._tweening.tweeningEase)
        .Play();
    }
}
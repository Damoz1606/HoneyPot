using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Block : _PoolObjectBase
{
    private bool _isSwapping = false;
    private bool _canSwipe = false;
    private bool _canDecrease = false;
    private bool _canPop = false;
    private Tile _child;
    private ParticlesTypes _particle = ParticlesTypes.DEFAULT;
    public Vector2Int IntegerPosition => VectorRound.Vector2Round(this.transform.position);

    public ParticlesTypes Particles { set { this._particle = value; } get { return this._particle; } }

    public Block Top
    {
        get
        {
            try
            {
                if (this.IntegerPosition.y < GameplayManagers.GridManager.GridHeight - 1)
                    return GameplayManagers.GridManager
                    .Board.GetBlockAt(this.IntegerPosition.x, this.IntegerPosition.y + 1);
                else
                    return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }

    public Block Bottom
    {
        get
        {
            try
            {
                if (this.IntegerPosition.y > 0)
                    return GameplayManagers.GridManager.Board
                    .GetBlockAt(this.IntegerPosition.x, this.IntegerPosition.y - 1);
                else
                    return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
    public Block Right
    {
        get
        {
            try
            {
                if (this.IntegerPosition.x > 0)
                    return GameplayManagers.GridManager.Board
                    .GetBlockAt(this.IntegerPosition.x - 1, this.IntegerPosition.y);
                else
                    return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
    public Block Left
    {
        get
        {
            try
            {
                if (this.IntegerPosition.x < GameplayManagers.GridManager.GridWidth - 1)
                    return GameplayManagers.GridManager.Board
                    .GetBlockAt(this.IntegerPosition.x + 1, this.IntegerPosition.y);
                else
                    return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }

    public Block[] NeighboursHorizontal => new[] { Right, Left };
    public Block[] NeighboursVertical => new[] { Top, Bottom };

    public bool CanSwipe { set { this._canSwipe = value; } get { return this._canSwipe; } }
    public bool CanDecrease { set { this._canDecrease = value; } get { return this._canDecrease; } }
    public bool CanPop { set { this._canPop = value; } get { return this._canPop; } }
    public bool IsSwapping { set { this._isSwapping = value; } get { return this._isSwapping; } }
    public Tile Child { set { this._child = value; } get { return this._child; } }

    void Start()
    {
        /* GameplayManagers.SpawnManager.TileSpawnManager.Spawn(out GameObject tile);
        tile.transform.position = this.transform.position;
        tile.transform.SetParent(this.transform);
        this._child = tile.GetComponent<Tile>(); */
        // tile.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        this.CheckAndHoldChild();
    }

    public async void ActivateChild()
    {
        await this.ActivateChildAsync();
    }

    private async Task ActivateChildAsync()
    {
        if (this.Child == null) return;
        await this.Child.transform.DOScale(Vector3.one, 0.25f).AsyncWaitForCompletion();
        await this.Child.transform.DORotate(Vector3.forward, 0.25f).AsyncWaitForCompletion();
    }

    public async void CheckAndHoldChild()
    {
        await this.CheckAndHoldChildAsync();
    }

    private async Task CheckAndHoldChildAsync(float tweeningTime = 0.25f)
    {
        if (this._isSwapping) return;
        if (this.Child == null) return;
        if (this.Child.transform.localPosition != Vector3.zero)
        {
            await this.Child.transform.DOLocalMove(Vector3.zero, tweeningTime).AsyncWaitForCompletion();
        }
    }

    public (List<Block>, List<Block>) GetAllConnections()
    {
        return (GetConnectionsHorizontal(), GetConnectionsVertical());
    }

    public List<Block> GetConnectionsVertical()
    {
        return GetConnections(AxisTypes.VERTICAL, NeighboursVertical);
    }

    public List<Block> GetConnectionsHorizontal()
    {
        return GetConnections(AxisTypes.HORIZONTAL, NeighboursHorizontal);
    }

    private List<Block> GetConnections(AxisTypes type, Block[] blocks, List<Block> exclude = null)
    {
        List<Block> result = new List<Block> { this, };
        if (exclude == null)
            exclude = new List<Block> { this, };
        else
            exclude.Add(this);

        foreach (Block neighbour in blocks)
        {
            if (this.Child == null) continue;
            if (neighbour == null) continue;
            if (exclude.Contains(neighbour)) continue;
            if (!neighbour.Child.Type.Equals(this.Child.Type)) continue;
            if (type == AxisTypes.HORIZONTAL)
                result.AddRange(neighbour.GetConnections(type, neighbour.NeighboursHorizontal, exclude));
            else if (type == AxisTypes.VERTICAL)
                result.AddRange(neighbour.GetConnections(type, neighbour.NeighboursVertical, exclude));
        }

        return result;
    }

    public override void OnActivate()
    {
        this._canDecrease = false;
        this._canPop = false;
        this._canSwipe = false;
    }

    public override void OnUpdate()
    {
        throw new NotImplementedException();
    }

    public override void OnDeactivate()
    {
        this._canDecrease = false;
        this._canPop = false;
        this._canSwipe = false;
        this.Child.OnEffect();
        // GameplayManagers.ParticlesManager.InstantiateParticles(this.transform.position, this._particle);
        // this.Child.OnDeactivate();
        // this.Particles = ParticlesTypes.DEFAULT;
    }

    public void AttachChild(Tile tile)
    {
        this._child = tile;
        this._child.transform.SetParent(null);
        this._child.transform.position = Vector3.zero;
        this._child.transform.SetParent(this.transform);
        this._child.transform.localPosition = Vector3.zero;
        this._child.transform.rotation = Quaternion.identity;
    }

    public void DeattachChild()
    {
        this._child.transform.SetParent(null);
        this._child = null;
    }
}

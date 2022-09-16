using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    private bool _isSwapping = false;
    private bool _canSwipe = false;
    private bool _canDecrease = false;
    private bool _canPop = false;
    private Tile _child;
    protected DestroyWithParticles _destroyWithParticles;

    public Block Top
    {
        get
        {
            try
            {
                if ((int)this.transform.position.y < GameplayManagers.GridManager.GridHeight - 1)
                    return GameplayManagers.GridManager
                    .Board.Grid[(int)this.transform.position.x].row[(int)this.transform.position.y + 1];
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
                if ((int)this.transform.position.y > 0)
                    return GameplayManagers.GridManager.Board
                    .Grid[(int)this.transform.position.x].row[(int)this.transform.position.y - 1];
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
                if ((int)this.transform.position.x > 0)
                    return GameplayManagers.GridManager.Board
                    .Grid[(int)this.transform.position.x - 1].row[(int)this.transform.position.y];
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
                if ((int)this.transform.position.x < GameplayManagers.GridManager.GridWidth - 1)
                    return GameplayManagers.GridManager.Board
                    .Grid[(int)this.transform.position.x + 1].row[(int)this.transform.position.y];
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

    private void Awake()
    {
        this._destroyWithParticles = GetComponent<DestroyWithParticles>();
    }

    void Start()
    {
        GameplayManagers.SpawnManager.TileSpawnManager.Spawn(out GameObject tile);
        tile.transform.position = this.transform.position;
        tile.transform.SetParent(this.transform);
        this._child = tile.GetComponent<Tile>();
        // tile.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        this.CheckAndHoldChild();
    }

    /*     public async void ActivateChild()
        {
            await this.ActivateChildAsync();
        }

        private async Task ActivateChildAsync()
        {
            if (this.Child == null) return;
            await this.Child.transform.DOScale(Vector3.one, 0.25f).AsyncWaitForCompletion();
            await this.Child.transform.DORotate(Vector3.forward, 0.25f).AsyncWaitForCompletion();
        } */

    public async void CheckAndHoldChild()
    {
        await this.CheckAndHoldChildAsync();
    }

    private async Task CheckAndHoldChildAsync(float tweeningTime = 0.25f)
    {
        if (this._isSwapping) return;
        if (this.Child.transform.localPosition != Vector3.zero)
        {
            await this.Child.transform.DOLocalMove(Vector3.zero, tweeningTime).AsyncWaitForCompletion();
        }
    }

    public void Destroy()
    {
        this._destroyWithParticles.Destroy();
    }

    public (List<Block>, List<Block>) GetConnections(List<Block> exclude = null)
    {
        if (exclude == null)
        {
            exclude = new List<Block> { this, };
        }
        else
        {
            exclude.Add(this);
        }

        return (GetConnectionsHorizontal(exclude), GetConnectionsVertical(exclude));
    }

    protected List<Block> GetConnectionsVertical(List<Block> exclude = null)
    {
        List<Block> result = new List<Block> { this, };
        if (exclude == null)
        {
            exclude = new List<Block> { this, };
        }
        else
        {
            exclude.Add(this);
        }

        foreach (Block neighbour in NeighboursVertical)
        {
            if (neighbour == null) continue;
            if (exclude.Contains(neighbour)) continue;
            if (!neighbour.Child.Type.Equals(this.Child.Type)) continue;
            result.AddRange(neighbour.GetConnectionsVertical(exclude));
        }
        return result;
    }

    protected List<Block> GetConnectionsHorizontal(List<Block> exclude = null)
    {
        List<Block> result = new List<Block>();
        result.Add(this);
        if (exclude == null)
        {
            exclude = new List<Block> { this, };
        }
        else
        {
            exclude.Add(this);
        }

        foreach (Block neighbour in NeighboursHorizontal)
        {
            if (neighbour == null) continue;
            if (exclude.Contains(neighbour)) continue;
            if (!neighbour.Child.Type.Equals(this.Child.Type)) continue;
            result.AddRange(neighbour.GetConnectionsHorizontal(exclude));
        }

        return result;
    }
}

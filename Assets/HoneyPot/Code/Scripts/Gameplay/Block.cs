using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(DestroyWithParticles))]
[RequireComponent(typeof(DestroyWithPoints))]
[RequireComponent(typeof(SwipeComponent))]
public class Block : MonoBehaviour
{
    private bool _canSwipe = false;
    private bool _canDecrease = false;
    private Vector2Int _position;
    private Tile _child;
    private DestroyWithParticles _destroyWithParticles;


    public Block Top => _position.y < GameplayManagers.GridManager.GridHeight - 1 ? GameplayManagers.GridManager.Board.Grid[_position.x].row[_position.y + 1] : null;
    public Block Bottom => _position.y > 0 ? GameplayManagers.GridManager.Board.Grid[_position.x].row[_position.y - 1] : null;
    public Block Right => _position.x > 0 ? GameplayManagers.GridManager.Board.Grid[_position.x - 1].row[_position.y] : null;
    public Block Left => _position.x < GameplayManagers.GridManager.GridWidth - 1 ? GameplayManagers.GridManager.Board.Grid[_position.x + 1].row[_position.y] : null;

    public Block[] NeighboursHorizontal => new[] { Right, Left };
    public Block[] NeighboursVertical => new[] { Top, Bottom };

    public bool CanDecrease { set { this._canDecrease = value; } get { return this._canDecrease; } }
    public bool CanSwipe { set { this._canSwipe = value; } get { return this._canSwipe; } }
    public Vector2Int Position { set { this._position = value; } get { return this._position; } }
    public Tile Child { set { this._child = value; } get { return this._child; } }
    public DestroyWithParticles DestroyWithParticles { get { return this._destroyWithParticles; } }

    private void Awake()
    {
        this._destroyWithParticles = this.GetComponent<DestroyWithParticles>();
    }

    void Start()
    {
        GameplayManagers.SpawnManager.TileSpawnManager.Spawn(out GameObject tile);
        // tile.transform.localScale = Vector3.zero;
        tile.transform.position = this.transform.position;
        tile.transform.SetParent(this.transform);
        this._child = tile.GetComponent<Tile>();
        // tile.transform.DOScale(Vector3.one, 1).SetEase(Ease.InCubic).Play();
    }

    private void Update()
    {
        this._position = new Vector2Int((int)this.transform.position.x, (int)this.transform.position.y);
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

        return (GetConnectionsHorizontal(exclude), GetConnectionsVertical());
    }

    private List<Block> GetConnectionsHorizontal(List<Block> exclude = null)
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

        foreach (Block neighbour in NeighboursHorizontal)
        {
            if (neighbour == null) continue;
            if (exclude.Contains(neighbour)) continue;
            if (neighbour.Child.Type != this.Child.Type)
            {
                exclude.Add(neighbour);
                continue;
            };
            result.AddRange(neighbour.GetConnectionsHorizontal(exclude));
        }
        return result;
    }

    private List<Block> GetConnectionsVertical(List<Block> exclude = null)
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

        foreach (Block neighbour in NeighboursHorizontal)
        {
            if (neighbour == null) continue;
            if (exclude.Contains(neighbour)) continue;
            if (neighbour.Child.Type != this.Child.Type)
            {
                exclude.Add(neighbour);
                continue;
            };
            result.AddRange(neighbour.GetConnectionsVertical(exclude));
        }
        return result;
    }
}

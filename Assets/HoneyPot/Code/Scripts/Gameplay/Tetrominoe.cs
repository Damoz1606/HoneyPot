using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HorizontalMovement))]
[RequireComponent(typeof(FallComponent))]
[RequireComponent(typeof(RotationComponent))]
public class Tetrominoe : _PoolObjectBase
{
    [SerializeField] private List<Vector3> _pivots;
    [SerializeField] private TetrominoeTypes _type;
    private List<Block> _blocks = new List<Block>();
    private HorizontalMovement _movementController;
    private FallComponent _fallController;
    private RotationComponent _rotationController;

    public HorizontalMovement MovementController { get { return this._movementController; } }
    public FallComponent FallController { get { return this._fallController; } }
    public RotationComponent RotationController { get { return this._rotationController; } }
    public TetrominoeTypes TetrominoeTypes { get { return this._type; } }
    public List<Block> Blocks { get { return this._blocks; } }

    private void Awake()
    {
        this._movementController = this.GetComponent<HorizontalMovement>();
        this._fallController = this.GetComponent<FallComponent>();
        this._rotationController = this.GetComponent<RotationComponent>();
    }

    public override void OnActivate()
    {
        this.transform.position = new Vector3(GameplayManagers.GridManager.GridWidth / 2, GameplayManagers.GridManager.GridHeight - Constants.GRID_GREACE_HEIGHT, 0);
        this.transform.rotation = Quaternion.identity;

        foreach (Vector3 item in this._pivots)
        {
            Block block = GameplayManagers.SpawnManager.BlockSpawner.OnSpawn();
            block.transform.SetParent(this.transform);
            block.transform.position = Vector3.zero;
            block.transform.localPosition = item;
            this._blocks.Add(block);
        }

        if (!GameplayManagers.GridManager.Board.IsValidPosition(this))
        {
            GameplayManagers.InputManager.IsInputActive = false;
            GameplayManagers.GameManager.SetState(GameStates.GAMEOVER);
            GameplayManagers.SpawnManager.OnKill(this);
        }
    }

    public override void OnDeactivate()
    {
        GameplayManagers.GameManager.CurrentTetrominoe = null;
        this._blocks.Clear();
    }

    public override void OnUpdate()
    {

    }

    public void PlaceTilesOnGrid()
    {
        foreach (Block child in this.Blocks)
        {
            child.CanSwipe = true;
            child.CanDecrease = true;
            child.CanPop = true;
            // child.ActivateChild();
            child.transform.SetParent(GameplayManagers.GameManager.BlockHolder);
        }
        GameplayManagers.SpawnManager.OnKill(this);
    }
}
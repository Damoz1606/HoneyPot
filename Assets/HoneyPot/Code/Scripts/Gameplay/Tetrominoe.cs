using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HorizontalMovement))]
[RequireComponent(typeof(FallComponent))]
[RequireComponent(typeof(RotationComponent))]
public class Tetrominoe : _PoolObjectBase
{
    [SerializeField] private List<GameObject> _pivots;
    private List<Block> _blocks = new List<Block>();
    private HorizontalMovement _movementController;
    private FallComponent _fallController;
    private RotationComponent _rotationController;

    public HorizontalMovement MovementController { get { return this._movementController; } }
    public FallComponent FallController { get { return this._fallController; } }
    public RotationComponent RotationController { get { return this._rotationController; } }
    public List<Block> Blocks { get { return this._blocks; } }

    private void Awake()
    {
        this._movementController = this.GetComponent<HorizontalMovement>();
        this._fallController = this.GetComponent<FallComponent>();
        this._rotationController = this.GetComponent<RotationComponent>();
    }

    public override void OnActivate()
    {
        this.gameObject.SetActive(true);
        this.enabled = true;

        this._pivots.ForEach(pivot =>
        {
            GameplayManagers.SpawnManager.BlockPoolSpawner.Spawn();
            GameplayManagers.SpawnManager.BlockPoolSpawner.CurrentBlock.transform.position = pivot.transform.position;
            this._blocks.Add(GameplayManagers.SpawnManager.BlockPoolSpawner.CurrentBlock);
            GameplayManagers.SpawnManager.BlockPoolSpawner.CurrentBlock.transform.SetParent(this.transform);
        });

        if (!GameplayManagers.GridManager.Board.IsValidPosition(this))
        {
            GameplayManagers.InputManager.IsInputActive = false;
            GameplayManagers.GameManager.SetState(GameStates.GAMEOVER);
            this.OnDeactivate();
        }
    }

    public override void OnDeactivate()
    {
        this.enabled = false;
        this.gameObject.SetActive(false);
        this._blocks.Clear();
        GameplayManagers.GameManager.CurrentTetrominoe = null;
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
            child.transform.SetParent(GameplayManagers.GameManager.BlockHolder);
            // child.ActivateChild();
        }
        this.OnDeactivate();
    }
}
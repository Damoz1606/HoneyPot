using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HorizontalMovement))]
[RequireComponent(typeof(FallComponent))]
[RequireComponent(typeof(RotationComponent))]
public class Tetromino : MonoBehaviour
{

    [SerializeField] private Block[] _blocks = new Block[4];

    private HorizontalMovement _movementController;
    private FallComponent _fallController;
    private RotationComponent _rotationController;

    public HorizontalMovement MovementController { get { return this._movementController; } }
    public FallComponent FallController { get { return this._fallController; } }
    public RotationComponent RotationController { get { return this._rotationController; } }

    public Block[] Blocks { get { return this._blocks; } }

    private void Awake()
    {
        this._movementController = this.GetComponent<HorizontalMovement>();
        this._fallController = this.GetComponent<FallComponent>();
        this._rotationController = this.GetComponent<RotationComponent>();
    }

    private void Start()
    {

        if (!GameplayManagers.GridManager.Board.IsValidPosition(this))
        {
            GameplayManagers.InputManager.IsInputActive = false;
            GameplayManagers.GameManager.SetState(GameStates.GAMEOVER);
            Destroy(this.gameObject);
        }
    }

    public void PlaceTilesOnGrid()
    {
        foreach (Block child in this.Blocks)
        {
            child.CanSwipe = true;
            child.CanDecrease = true;
            child.CanPop = true;
            // child.ActivateChild();
        }
    }
}

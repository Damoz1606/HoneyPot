using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HorizontalMovement))]
[RequireComponent(typeof(FallComponent))]
[RequireComponent(typeof(RotationComponent))]
public class Tetromino : MonoBehaviour
{
    private HorizontalMovement _movementController;
    private FallComponent _fallController;
    private RotationComponent _rotationController;

    public HorizontalMovement MovementController { get { return this._movementController; } }
    public FallComponent FallController { get { return this._fallController; } }
    public RotationComponent RotationController { get { return this._rotationController; } }

    private void Awake()
    {
        this._movementController = this.GetComponent<HorizontalMovement>();
        this._fallController = this.GetComponent<FallComponent>();
        this._rotationController = this.GetComponent<RotationComponent>();
    }

    private void Start()
    {
        if (!GameplayManagers.GridManager.GridTetromino.IsValidGridPosition(this.transform))
        {
            GameplayManagers.GameManager.SetState(GameStates.GAMEOVER);
            Destroy(this.gameObject);
        }
    }

    public void PlaceTilesOnGrid()
    {
        foreach (Transform child in this.transform)
        {
            if (child.CompareTag("Block"))
            {
                child.GetComponent<Block>().CanDecrease = true;
            }
            Tile tile = child.GetComponentInChildren<Tile>();
            if (tile != null)
            {
                tile.transform.SetParent(GameplayManagers.GameManager.TileHolder);
                GameplayManagers.GridManager.GridTile.AddToGrid(tile.transform);
            }
        }
    }
}

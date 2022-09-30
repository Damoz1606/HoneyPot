using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FallComponent), typeof(HorizontalMovement), typeof(RotationComponent))]
public class TetrominoeNormal : MonoBehaviour, ITetrominoe, IPoolObject
{
    [SerializeField] private TetrominoeNormalModel _data;
    public TetrominoeTypes type { get => this._data.type; }
    public List<IBlock> Blocks { get; set; }
    public FallComponent FallComponent { get; set; }
    public HorizontalMovement HorizontalMovement { get; set; }
    public RotationComponent RotationComponent { get; set; }

    private void Awake()
    {
        this.Blocks = new List<IBlock>();
        this.FallComponent = GetComponent<FallComponent>();
        this.HorizontalMovement = GetComponent<HorizontalMovement>();
        this.RotationComponent = GetComponent<RotationComponent>();
    }

    public void OnActivate()
    {
        ObjectPosition.ObjectResetLocalPosition(this.gameObject);
        ObjectPosition.ObjectResetPosition(this.gameObject);
        ObjectPosition.ObjectResetPosition(this.gameObject);
        this.transform.localPosition = new Vector3(GameplayManagers.GridManager.GridWidth / 2, GameplayManagers.GridManager.GridHeight - Constants.GRID_GREACE_HEIGHT, 0);

        this._data._pivots.ForEach(vector =>
        {
            IBlock block = GameplayManagers.SpawnManager.BlockNormalSpawner.OnSpawn();
            ObjectPosition.ObjectResetPosition(block.gameObject);
            block.transform.SetParent(this.transform);
            ObjectPosition.ObjectResetLocalPosition(block.gameObject);
            ObjectPosition.ObjectResetRotation(block.gameObject);
            block.transform.localPosition = vector;
            this.Blocks.Add(block);
        });

        if (!GameplayManagers.GridManager.Board.IsValidPosition(this))
        {
            GameplayManagers.InputManager.IsInputActive = false;
            GameplayManagers.SpawnManager.TetrominoeNormalSpawner.OnKill(this);
            GameplayManagers.GameManager.SetState(GameStates.GAMEOVER);
        }
    }

    public void OnDeactivate()
    {
        GameplayManagers.GameManager.CurrentTetrominoe = null;
        this.Blocks.Clear();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void PlaceBlockOnChild()
    {
        this.Blocks.ForEach(block =>
        {
            block.CanSwap = true;
            block.CanDecrease = true;
            block.CanPop = true;
            block.transform.SetParent(GameplayManagers.GameManager.BlockHolder);
        });

        GameplayManagers.SpawnManager.TetrominoeNormalSpawner.OnKill(this);
    }
}
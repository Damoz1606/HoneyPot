using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridTetromino _gridTetromino;
    [SerializeField] private GridTile _gridTile;
    [SerializeField] protected int _gridHeight = Constants.GRID_HEIGHT;
    [SerializeField] protected int _gridWidth = Constants.GRID_WIDTH;

    public GridTetromino GridTetromino { get { return this._gridTetromino; } }
    public GridTile GridTile { get { return this._gridTile; } }

    public int GridHeight { get { return this._gridHeight; } }
    public int GridWidth { get { return this._gridWidth; } }

    private void Start()
    {
        Transform[,] grid = new Transform[this._gridWidth, this._gridHeight];
        if (this._gridTetromino != null) this._gridTetromino.InitGrid(this._gridWidth, this._gridHeight);
        if (this._gridTile != null) this._gridTile.InitGrid(this._gridWidth, this._gridHeight);
    }
}

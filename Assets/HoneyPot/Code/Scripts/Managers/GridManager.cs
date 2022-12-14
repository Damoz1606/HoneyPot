using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // [SerializeField] private GridTetromino _gridTetromino;
    [SerializeField] private Board _board;
    [SerializeField] protected int _gridHeight = Constants.GRID_HEIGHT;
    [SerializeField] protected int _gridWidth = Constants.GRID_WIDTH;

    // public GridTetromino GridTetromino { get { return this._gridTetromino; } }
    public Board Board { get { return this._board; } }

    public int GridHeight { get { return this._gridHeight; } }
    public int GridWidth { get { return this._gridWidth; } }

    private void Start()
    {
        Transform[,] grid = new Transform[this._gridWidth, this._gridHeight];
        if (this._board != null) this._board.InitGrid(this._gridWidth, this._gridHeight);
    }
}

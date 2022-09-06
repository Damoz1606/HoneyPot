using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridTetromino))]
[RequireComponent(typeof(GridTile))]
public class GridManager : MonoBehaviour
{
    private GridTetromino _gridTetromino;
    private GridTile _gridTile;

    public GridTetromino GridTetromino { get { return this._gridTetromino; } }
    public GridTile GridTile { get { return this._gridTile; } }

    private void Awake()
    {
        this._gridTetromino = GetComponent<GridTetromino>();
        this._gridTile = GetComponent<GridTile>();
    }
}

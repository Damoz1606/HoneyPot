using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    [SerializeField] private Transform _rotationPivot;
    [SerializeField] private int _rotationDegree = 90;
    public void Rotate(bool isClockWise)
    {
        float rotation = (isClockWise) ? this._rotationDegree : -this._rotationDegree;
        this.transform.RotateAround(this._rotationPivot.position, Vector3.forward, rotation);
        if (GameplayManagers.GridManager.Board.IsValidPosition(this.GetComponent<Tetromino>()))
        {
            GameplayManagers.GridManager.Board.UpdateTetromino(this.GetComponent<Tetromino>());
        }
        else
        {
            this.transform.RotateAround(this._rotationPivot.position, Vector3.forward, -rotation);
        }
    }
}
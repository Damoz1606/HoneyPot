using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallComponent : MonoBehaviour
{
    [SerializeField] private float _transitionInterval = 0.8f;
    [SerializeField] private float _fastTransitionInterval = 0;
    private float _lastFall;

    public void FreeFall()
    {
        if (Time.time - this._lastFall >= this._transitionInterval)
        {
            this.transform.position += Vector3.down;
            if (GameplayManagers.GridManager.Board.IsValidPosition(this.GetComponent<Tetrominoe>()))
            {
                GameplayManagers.GridManager.Board.UpdateTetromino(this.GetComponent<Tetrominoe>());
            }
            else
            {
                this.transform.position += Vector3.up;
                GetComponent<FallComponent>().enabled = false;
                GetComponent<HorizontalMovement>().enabled = false;
                GetComponent<RotationComponent>().enabled = false;
                GetComponent<Tetrominoe>().enabled = false;
                GetComponent<Tetrominoe>().PlaceTilesOnGrid();

                GameplayManagers.GameManager.CurrentTetrominoe = null;

                GameplayManagers.GridManager.Board.PlaceNewTetromino();
            }

            this._lastFall = Time.time;
        }
    }

    public void InstantFall()
    {
        this._transitionInterval = this._fastTransitionInterval;
    }
}

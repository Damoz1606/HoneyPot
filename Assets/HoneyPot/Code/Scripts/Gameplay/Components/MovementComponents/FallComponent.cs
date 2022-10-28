using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FallComponent : MonoBehaviour
{
    [SerializeField] private float _transitionInterval = 0.8f;
    [SerializeField] private float _fastTransitionInterval = 0;
    private float _auxiliarTransitionInterval = 0;
    private float _lastFall;

    private void Start()
    {
        this._auxiliarTransitionInterval = this._transitionInterval;
    }

    public void FreeFall()
    {
        if (Time.time - this._lastFall >= this._transitionInterval)
        {
            this.transform.position += Vector3.down;
            if (GameplayManagers.GridManager.Board.IsValidPosition(this.GetComponent<ITetrominoe>()))
            {
                GameplayManagers.GridManager.Board.UpdateTetromino(this.GetComponent<ITetrominoe>());
            }
            else
            {
                this._transitionInterval = this._auxiliarTransitionInterval;
                this.transform.position += Vector3.up;
                GetComponent<ITetrominoe>().PlaceBlockOnChild();

                GameplayManagers.GameManager.CurrentTetrominoe = default;

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

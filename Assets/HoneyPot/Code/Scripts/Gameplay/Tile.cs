using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwipeComponent))]
public class Tile : MonoBehaviour
{
    [SerializeField] private float _scoreValue = 100;
    [SerializeField] private float _increaseFactor = 1;
    private SwipeComponent _movementController;
    public SwipeComponent MovementController { get { return this._movementController; } }

    public int IncreaseFactor { set { this._increaseFactor = value; } }

    private void Awake()
    {
        this._movementController = this.GetComponent<SwipeComponent>();
    }

    private void OnDestroy()
    {
        GameplayManagers.ScoreManager.OnScore(Mathf.RoundToInt(this._scoreValue * (this._increaseFactor)));
    }
}

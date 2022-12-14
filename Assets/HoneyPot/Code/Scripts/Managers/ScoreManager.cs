using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore = 0;
    [SerializeField] private int _maxScore = 1000;
    [SerializeField] private int _increment = 1;

    public int CurrentScore { get { return this._currentScore; } }
    public float MaxScore { get { return this._maxScore; } }
    public int Increment { set { this.Increment = value; } }
    public float[] ScoreReferences => new[] { _maxScore * 0.5f, _maxScore * 0.75f, _maxScore * 1 };

    void Start()
    {
        GameplayManagers.UIManager.InGameUI.UpdateUI();
    }

    public void OnScore(int scoreIncreaseAmount)
    {
        this._currentScore += scoreIncreaseAmount * this._increment;
        GameplayManagers.UIManager.InGameUI.UpdateUI();
    }
}

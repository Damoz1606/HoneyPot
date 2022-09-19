using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore = 0;
    [SerializeField] private int _maxScore = 1000;

    public int CurrentScore { get { return this._currentScore; } }
    public float MaxScore { get { return this._maxScore; } }
    public float[] ScoreReferences => new[] { _maxScore * 0.25f, _maxScore * 0.5f, _maxScore * 1 };

    void Start()
    {
        GameplayManagers.UIManager.InGameUI.UpdateScoreUI();
    }

    public void OnScore(int scoreIncreaseAmount)
    {
        this._currentScore += scoreIncreaseAmount;
        GameplayManagers.UIManager.InGameUI.UpdateScoreUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore = 0;
    private int _maxScore = 1000;

    public int CurrentScore { get { return this._currentScore; } }
    public float MaxScore { get { return this._maxScore; } }

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

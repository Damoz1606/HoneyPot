using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore = 0;
    [SerializeField] private int _maxScore = 1000;
    [SerializeField] private int _increment = 1;

    // [SerializeField] private ScoreChannel _scoreChannel;

    public int CurrentScore { get { return this._currentScore; } }
    public float MaxScore { get { return this._maxScore; } }
    public int Increment { set { this.Increment = value; } }
    public float[] ScoreReferences => new[] { _maxScore * 0.5f, _maxScore * 0.75f, _maxScore * 1 };

    private void OnEnable()
    {
        EventManager.StartListening(EventEnum.SCORE.ToString(), this.OnScore);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventEnum.SCORE.ToString(), this.OnScore);
    }

    void Start()
    {
        GameplayManagers.UIManager.InGameUI.UpdateUI();
    }

    public void OnScore(object message)
    {
        var amount = (int)message;
        this._currentScore += amount * this._increment;
        GameplayManagers.UIManager.InGameUI.UpdateUI();
    }
}

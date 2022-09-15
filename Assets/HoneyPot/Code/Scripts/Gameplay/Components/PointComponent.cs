using UnityEngine;

public class PointComponent : MonoBehaviour
{
    [SerializeField] private float _scoreValue = 100;
    [SerializeField] private float _increaseFactor = 1;
    public int IncreaseFactor { set { this._increaseFactor = value; } }
    
    private void OnDestroy()
    {
        GameplayManagers.ScoreManager.OnScore(Mathf.RoundToInt(this._scoreValue * (this._increaseFactor)));
    }
}
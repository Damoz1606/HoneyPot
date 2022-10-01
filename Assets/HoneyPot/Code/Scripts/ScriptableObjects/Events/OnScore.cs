using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OnScore", menuName = "Events/OnScore", order = 0)]
public class OnScore : ScriptableObject
{
    [SerializeField] private int _score = 100;

    public UnityAction<int> ListenScoreIncrease;

    public void OnActivate()
    {
        this.ListenScoreIncrease(this._score);
    }
}
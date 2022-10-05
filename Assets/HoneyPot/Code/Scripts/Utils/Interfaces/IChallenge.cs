using UnityEngine;

public interface IChallenge
{
    void Initialize();
    void OnGoalComplete();
    void CheckGoals();
    void CloseChallenge();
}
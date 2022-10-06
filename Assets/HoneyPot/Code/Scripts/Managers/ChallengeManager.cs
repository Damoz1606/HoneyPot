using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] private List<_AGoal> _goals;

    private void Start()
    {
        this.Initialize();
    }

    public void Initialize()
    {
        _goals.ForEach(goal => goal.Initialize());
    }

    private void OnEnable()
    {
        EventManager.StartListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, this.CheckGoals);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, this.CheckGoals);
    }

    public void CheckGoals(object message)
    {
        Debug.Log("Checking...");
        bool completed = _goals.All(goal => goal.Completed);
        if (completed) this.Complete();
    }

    public void Complete()
    {
        Debug.Log("Complete!!!");
        GameplayManagers.GameManager.SetState(GameStates.GAMEOVER);
    }
}
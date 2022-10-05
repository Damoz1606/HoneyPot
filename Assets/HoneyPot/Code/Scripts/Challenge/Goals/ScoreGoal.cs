using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Goal/Score", order = 0)]
public class ScoreGoal : _AGoal
{
    public override void Initialize()
    {
        base.Initialize();
        EventManager.StartListening(Channels.SCORE_CHANNEL, ScoreEvent.INCREASE, this.UpdateGoal);
    }

    protected override void Complete()
    {
        base.Complete();
        Debug.Log($"{this.CurrentAmount}/{this.RequireAmount} => Complete goal");
        EventManager.StopListening(Channels.SCORE_CHANNEL, ScoreEvent.INCREASE, this.UpdateGoal);
    }

    public override void UpdateGoal(object message)
    {
        int score = (int)message;
        this.CurrentAmount += score;
        Debug.Log($"{this.CurrentAmount}/{this.RequireAmount}");
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
        this.Evaluate();
    }
}
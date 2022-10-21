using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "Goal/Score", order = 0)]
public class ScoreGoal : _AGoal
{
    public override void Initialize()
    {
        base.Initialize();
        EventManager.StartListening(Channels.SCORE_CHANNEL, ScoreEvent.INCREASE, this.UpdateGoal);
        EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.START_SCORE_GUI, this);
    }

    protected override void Complete()
    {
        EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.END_SCORE_GUI, this);
        base.Complete();
        if (this.IsTutorial) EventManager.TriggerEvent(Channels.TUTORIAL_CHANNEL, TutorialEvent.TUTORIAL, null);
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
        EventManager.StopListening(Channels.SCORE_CHANNEL, ScoreEvent.INCREASE, this.UpdateGoal);
    }

    public override void UpdateGoal(object message)
    {
        int score = (int)message;
        this.CurrentAmount += score;
        this.Evaluate();
    }
}
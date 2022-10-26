using UnityEngine;

[CreateAssetMenu(fileName = "Swipe", menuName = "Tutorial/Swipe", order = 0)]
public class SwipeGoal : _AGoal
{
    public override void Initialize()
    {
        base.Initialize();
        EventManager.StartListening(Channels.TUTORIAL_CHANNEL, TutorialEvent.SWIPE, this.UpdateGoal);
    }

    public override void UpdateGoal(object message)
    {
        ITile tile = (ITile)message;
        CurrentAmount += 1;
        this.Evaluate();
    }

    protected override void Complete()
    {
        base.Complete();
        if (this.IsTutorial) EventManager.TriggerEvent(Channels.TUTORIAL_CHANNEL, TutorialEvent.TUTORIAL, null);
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
        EventManager.StopListening(Channels.TUTORIAL_CHANNEL, TutorialEvent.SWIPE, this.UpdateGoal);
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Tutorial/Move", order = 0)]
public class MoveGoal : _AGoal
{
    [SerializeField] private MoveButtonType _type;
    public override void Initialize()
    {
        base.Initialize();
        EventManager.StartListening(Channels.TUTORIAL_CHANNEL, TutorialEvent.MOVE, this.UpdateGoal);
    }

    public override void UpdateGoal(object message)
    {
        if (((MoveButtonType)message).Equals(this._type))
        {
            this.CurrentAmount += 1;
        }
        this.Evaluate();
    }

    protected override void Complete()
    {
        base.Complete();
        if (this.IsTutorial) EventManager.TriggerEvent(Channels.TUTORIAL_CHANNEL, TutorialEvent.TUTORIAL, null);
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
        EventManager.StopListening(Channels.TUTORIAL_CHANNEL, TutorialEvent.MOVE, this.UpdateGoal);
    }
}
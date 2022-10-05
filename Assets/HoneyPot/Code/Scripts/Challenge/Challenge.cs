using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Challenge
{
    protected ChallengeChannel _challengeChannel;
    public GoalState state;

    public virtual void Enable()
    {
        EventManager.StartListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.ACTIVE, this.ChallengeActiveEvent);
        EventManager.StartListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.COMPLETED, this.ChallengeCompleteEvent);

        if (this.state != GoalState.ACTIVE) this.state = GoalState.ACTIVE;
    }

    public virtual void Disable()
    {
        EventManager.StopListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.ACTIVE, this.ChallengeActiveEvent);
        EventManager.StopListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.COMPLETED, this.ChallengeCompleteEvent);
    }

    private void ChallengeActiveEvent(object message)
    {
        this.state = GoalState.ACTIVE;
        this.ChallengeActive();
    }

    private void ChallengeCompleteEvent(object message)
    {
        this.state = GoalState.COMPLETED;
        this.ChallengeCompleted();
    }

    protected void Complete()
    {
        this._challengeChannel.CompleteChallenge(this);
    }

    public abstract void ChallengeActive();
    public abstract void ChallengeCompleted();

}
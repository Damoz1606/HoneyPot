using UnityEngine;
using UnityEngine.Events;

public class ChallengeChannel
{
    public UnityAction<Challenge> ChallengeCompleteEvent;
    public UnityAction<Challenge> ChallengeActivateEvent;

    public void CompleteChallenge(Challenge completeChallenge)
    {
        this.ChallengeCompleteEvent?.Invoke(completeChallenge);
    }

    public void AssignChallenge(Challenge challengeToAssign)
    {
        this.ChallengeActivateEvent?.Invoke(challengeToAssign);
    }
}
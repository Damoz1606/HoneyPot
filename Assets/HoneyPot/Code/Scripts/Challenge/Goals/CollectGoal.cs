using UnityEngine;

[CreateAssetMenu(fileName = "Collect", menuName = "Goal/Collect", order = 0)]
public class CollectGoal : _AGoal
{
    [SerializeField] private TileNormalType _tileNormalType;
    [SerializeField] private TileComboType _tileComboType;

    public override void Initialize()
    {
        base.Initialize();
        EventManager.StartListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.COLLECT, this.UpdateGoal);

    }

    public override void UpdateGoal(object message)
    {
        ITile tile = (ITile)message;
        if ((tile.type.Equals(TileNormalType.COMBO) &&
        tile.type.Equals(this._tileComboType)) ||
        tile.type.Equals(this._tileNormalType))
        {
            CurrentAmount += 1;
            EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.UPDATE_COLLECT, this);
            EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
        }
        this.Evaluate();
    }

    protected override void Complete()
    {
        base.Complete();
        Debug.Log($"{this._tileNormalType}:{this.CurrentAmount}/{this.RequireAmount} => Complete goal: {this.Completed}");
        EventManager.StopListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.COLLECT, this.UpdateGoal);
    }
}
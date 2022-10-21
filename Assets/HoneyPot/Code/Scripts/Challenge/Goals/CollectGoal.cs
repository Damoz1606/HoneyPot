using UnityEngine;

[CreateAssetMenu(fileName = "Collect", menuName = "Goal/Collect", order = 0)]
public class CollectGoal : _AGoal
{
    [SerializeField] private TileNormalType _tileNormalType;
    [SerializeField] private TileComboType _tileComboType;
    [SerializeField] private Sprite _sprite;

    public Sprite Sprite => this._sprite;

    public override void Initialize()
    {
        base.Initialize();
        EventManager.StartListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.COLLECT, this.UpdateGoal);
        EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.START_COLLECT_GUI, this);
    }

    public override void UpdateGoal(object message)
    {
        ITile tile = (ITile)message;
        if ((tile.type.Equals(TileNormalType.COMBO) && ((TileCombo)tile).comboType.Equals(this._tileComboType)) ||
        (tile.type.Equals(this._tileNormalType) && !tile.type.Equals(TileNormalType.COMBO)))
        {
            CurrentAmount += 1;
            EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.UPDATE_COLLECT_GUI, this);
        }
        this.Evaluate();
    }

    protected override void Complete()
    {
        EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.END_COLLECT_GUI, this);
        base.Complete();
        if (this.IsTutorial) EventManager.TriggerEvent(Channels.TUTORIAL_CHANNEL, TutorialEvent.TUTORIAL, null);
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
        EventManager.StopListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.COLLECT, this.UpdateGoal);
    }
}
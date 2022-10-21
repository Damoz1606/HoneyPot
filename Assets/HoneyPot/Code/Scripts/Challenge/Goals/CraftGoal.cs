using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "Goal/Craft", order = 0)]
public class CraftGoal : _AGoal
{
    [SerializeField] private TileComboType _tileComboType;
    [SerializeField] private Sprite _sprite;

    public Sprite Sprite => this._sprite;

    public override void Initialize()
    {
        base.Initialize();
        EventManager.StartListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CRAFT, this.UpdateGoal);
        EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.START_CRAFT_GUI, this);
    }

    public override void UpdateGoal(object message)
    {
        ITile tile = (ITile)message;
        if ((tile.type.Equals(TileNormalType.COMBO) && ((TileCombo)tile).comboType.Equals(this._tileComboType)))
        {
            CurrentAmount += 1;
            EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.UPDATE_CRAFT_GUI, this);
        }
        this.Evaluate();
    }

    protected override void Complete()
    {
        EventManager.TriggerEvent(Channels.UI_CHANNEL, UIEvent.END_CRAFT_GUI, this);
        base.Complete();
        if (this.IsTutorial) EventManager.TriggerEvent(Channels.TUTORIAL_CHANNEL, TutorialEvent.TUTORIAL, null);
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CHECK_GOALS, null);
        EventManager.StopListening(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CRAFT, this.UpdateGoal);
    }
}
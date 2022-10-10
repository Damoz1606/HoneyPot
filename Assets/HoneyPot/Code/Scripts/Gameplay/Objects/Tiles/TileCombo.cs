using UnityEngine;

public class TileCombo : MonoBehaviour, ITile, IPoolObject
{
    [SerializeField] private TileComboModel data;
    public TileNormalType type => data.tileType;
    public TileComboType comboType => data.comboType;

    public void OnActivate()
    {
        this.transform.localScale = Vector3.one;
        this.data.hasEffectBeenActive = false;
    }

    public void OnDeactivate()
    {
        this.transform.localScale = Vector3.one;
        this.data.hasEffectBeenActive = false;
    }

    public void OnEffect(IBlock block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        if (block == null || this.data.hasEffectBeenActive) return;
        EventManager.TriggerEvent(Channels.SCORE_CHANNEL, ScoreEvent.INCREASE, this.data.score);
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.COLLECT, this);
        switch (this.comboType)
        {
            case TileComboType.BOMB:
                EventManager.TriggerEvent(Channels.POP_CHANNEL, PopEvent.POP_AROUND, block);
                break;
            case TileComboType.HONEYPOT:
                EventManager.TriggerEvent(Channels.POP_CHANNEL, PopEvent.POP_ALL, block);
                break;
            default: break;
        }
        this.data.hasEffectBeenActive = true;
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
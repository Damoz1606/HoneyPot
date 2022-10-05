using System.Collections.Generic;
using UnityEngine;

public class TileNormal : MonoBehaviour, ITile, IPoolObject
{
    [SerializeField] private TileNormalModel data;
    [SerializeField] public TileNormalType type => data.tileType;

    private bool hasEffectBeenActive = false;

    public void OnActivate()
    {
        this.transform.localScale = Vector3.one;
        this.hasEffectBeenActive = false;
    }

    public void OnDeactivate()
    {
        this.transform.localScale = Vector3.one;
        this.hasEffectBeenActive = false;
    }

    public void OnEffect(IBlock block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        if (this.hasEffectBeenActive) return;
        EventManager.TriggerEvent(Channels.SCORE_CHANNEL, ScoreEvent.INCREASE, this.data.score);
        EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.COLLECT, this);
        this.hasEffectBeenActive = true;
    }

    public void OnUpdate() { }
}
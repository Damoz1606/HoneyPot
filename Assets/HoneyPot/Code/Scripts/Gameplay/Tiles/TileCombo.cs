using UnityEngine;

public class TileCombo : MonoBehaviour, ITile, IPoolObject
{
    [SerializeField] private TileComboModel data;
    public TileTypes type => data.tileType;
    public ComboTypes comboType => data.comboType;

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
        GameplayManagers.GameManager.IsGameActive = false;
        if (block == null || this.data.hasEffectBeenActive) return;
        if (this.data._scoreChannel != null) this.data._scoreChannel.TriggerScore(this.data.score);
        if (this.data._challengeCollectChannel != null) this.data._challengeCollectChannel.TriggerCollectTile(this);
        switch (this.comboType)
        {
            case ComboTypes.BOMB:
                this.data._channelTileEvents.TriggerBomb(block);
                break;
            case ComboTypes.HONEYPOT:
                this.data._channelTileEvents.TriggerHoneypot();
                break;
            default: break;
        }
        GameplayManagers.GameManager.IsGameActive = true;
        this.data.hasEffectBeenActive = true;
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
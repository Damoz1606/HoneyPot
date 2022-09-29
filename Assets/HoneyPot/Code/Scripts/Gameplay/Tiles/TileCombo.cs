using UnityEngine;

public class TileCombo : MonoBehaviour, ITile, IPoolObject
{
    [SerializeField] private TileComboModel data;
    public TileTypes type => data.tileType;
    public ComboTypes comboType => data.comboType;

    public void OnActivate()
    {
        this.data.hasEffectBeenActive = false;
    }

    public void OnDeactivate()
    {
        this.data.hasEffectBeenActive = false;
    }

    public void OnEffect(IBlock block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        if (block == null || this.data.hasEffectBeenActive) return;
        if (this.data._scoreChannel != null) this.data._scoreChannel.TriggerScore(this.data.score);
        if (this.data._challengeCollectChannel != null) this.data._challengeCollectChannel.TriggerCollectTile(this);
        switch (this.comboType)
        {
            case ComboTypes.BOMB:
                Debug.Log("Explote");
                break;
            case ComboTypes.HONEYPOT:
                Debug.Log("Clear all");
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
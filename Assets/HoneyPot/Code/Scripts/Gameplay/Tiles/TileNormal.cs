using UnityEngine;

public class TileNormal : MonoBehaviour, ITile, IPoolObject
{
    [SerializeField] private TileNormalModel data;
    [SerializeField] public TileTypes type => data.tileType;

    public void OnActivate()
    {
        this.transform.localScale = Vector3.one;
    }

    public void OnDeactivate() { }

    public void OnEffect(IBlock block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        if (this.data._scoreChannel != null) this.data._scoreChannel.TriggerScore(this.data.score);
        if (this.data._challengeCollectChannel != null) this.data._challengeCollectChannel.TriggerCollectTile(this);
    }

    public void OnUpdate() { }
}
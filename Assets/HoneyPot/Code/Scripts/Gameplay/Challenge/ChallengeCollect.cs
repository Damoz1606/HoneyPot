using UnityEngine;

public class ChallengeCollect : MonoBehaviour, IChallenge
{
    [SerializeField] private int _require = 0;
    [SerializeField] private int _current = 0;
    [SerializeField] private ITile _tile;
    [SerializeField] private ChallengeCollectChannel _collectChannel;
    [SerializeField] private ChallengeChannel _challengeChannel;

    public void OnDisable()
    {
        this._collectChannel.ListenCollectTile -= OnEventTrigger;
    }

    public void OnEnable()
    {
        this._collectChannel.ListenCollectTile += OnEventTrigger;
    }

    public void DrawHUD()
    {
        // Debug.Log($"{this._tile.type.ToString()}: {this._current}/{this._require}");
    }

    public bool IsAchived()
    {
        return this._require <= this._current;
    }

    public void OnComplete()
    {
        Debug.Log($"Complete!!");
    }

    public void OnEventTrigger(ITile data)
    {
        if (this._tile == null) return;
        if (data.type.Equals(TileTypes.COMBO))
        {
            if (((TileCombo)this._tile).comboType.Equals(((TileCombo)data).comboType)) this._current++;
        }
        else
        {
            if (this._tile.type.Equals(data.type)) this._current++;
        }
        if (this._challengeChannel != null) this._challengeChannel.OnChallengeChangeTrigger();
    }
}
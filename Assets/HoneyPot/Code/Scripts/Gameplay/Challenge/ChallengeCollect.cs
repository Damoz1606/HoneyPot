using UnityEngine;

public class ChallengeCollect : MonoBehaviour, IChallenge
{
    [SerializeField] private int _require = 0;
    [SerializeField] private int _current = 0;
    [SerializeField] private Tile _tile;
    [SerializeField] private ChallengeCollectChannel _collectChannel;
    [SerializeField] private ChallengeChannel _challengeChannel;

    public void OnDisable()
    {
        this._collectChannel.OnTileCollectListener -= OnEventTrigger;
    }

    public void OnEnable()
    {
        this._collectChannel.OnTileCollectListener += OnEventTrigger;
    }

    public void DrawHUD()
    {
        Debug.Log($"{this._tile.Type.ToString()}: {this._current}/{this._require}");
    }

    public bool IsAchived()
    {
        return this._require <= this._current;
    }

    public void OnComplete()
    {
        Debug.Log($"Complete!!");
    }

    public void OnEventTrigger(Tile data)
    {
        if (this._tile == null) return;
        if (data.Type.Equals(TileTypes.COMBO))
        {
            if (((ComboTile)this._tile).ComboType.Equals(((ComboTile)data).ComboType)) this._current++;
        }
        else
        {
            if (this._tile.Type.Equals(data.Type)) this._current++;
        }
        if (this._challengeChannel != null) this._challengeChannel.OnChallengeChangeTrigger();
    }
}
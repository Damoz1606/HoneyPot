using UnityEngine;

public class NormalTile : Tile
{
    public override void OnEffect(Block block = null)
    {
        GameplayManagers.ScoreManager.OnScore(this._score);
    }
}
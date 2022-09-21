using UnityEngine;

public class NormalTile : Tile
{
    public override void OnEffect(Block block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        GameplayManagers.ScoreManager.OnScore(this._score);
    }
}
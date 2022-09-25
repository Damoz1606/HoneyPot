using UnityEngine;

public class NormalTile : Tile
{
    public override void OnActivate()
    {
        this.gameObject.SetActive(true);
        this.enabled = true;
    }

    public override void OnDeactivate()
    {
        GameplayManagers.SpawnManager.NormalTilePoolSpawner.SetOnPool(this.gameObject);
        this.gameObject.SetActive(false);
        this.enabled = false;
    }

    public override void OnEffect(Block block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        GameplayManagers.ScoreManager.OnScore(this._score);
        GameplayManagers.HistoryManager.UpdateTiles(this.Type.ToString());
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
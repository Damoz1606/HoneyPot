using UnityEngine;

public class NormalTile : Tile
{
    public override void OnActivate()
    {
        this.gameObject.SetActive(true);
        this.transform.position = Vector3.zero;
    }

    public override void OnDeactivate()
    {
        this.gameObject.SetActive(false);
        this.transform.position = Vector3.zero;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGoal : _GoalBase
{
    [SerializeField] private Tile _tile;
    public override void Complete()
    {
        // Debug.Log($"{((ComboTile)this._tile).ComboType.ToString()}: Completed");
    }

    public override void DrawHUD()
    {
        // GUILayout.Label(string.Format($"{((ComboTile)this._tile).ComboType.ToString()}: {GameplayManagers.HistoryManager.GetTiles(((ComboTile)this._tile).ComboType.ToString())}/{this._required}"));
    }

    public override bool IsAchived()
    {
        if (this._tile.Type == TileTypes.COMBO)
        {
            return GameplayManagers.HistoryManager.GetTiles(((ComboTile)this._tile).ComboType.ToString()) >= this._required;
        }
        else
        {
            return GameplayManagers.HistoryManager.GetTiles(this._tile.Type.ToString()) >= this._required;
        }
    }
}

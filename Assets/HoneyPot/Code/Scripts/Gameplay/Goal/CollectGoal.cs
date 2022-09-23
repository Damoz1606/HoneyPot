using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectGoal : _GoalBase
{
    [SerializeField] private Tile _tile;
    [SerializeField] private CollectionUIController _controller;

    private void Start()
    {
        this._controller.Initialize(_tile.GetComponentInChildren<Renderer>().sharedMaterial, (int)this._required);
    }
    public override void Complete()
    {
        this._controller.Completed();
    }

    public override void DrawHUD()
    {
        if (this._tile.Type.Equals(TileTypes.COMBO))
            this._controller.UpdateHUD(GameplayManagers.HistoryManager.GetTiles(((ComboTile)this._tile).ComboType.ToString()));
        else
            this._controller.UpdateHUD(GameplayManagers.HistoryManager.GetTiles((this._tile).Type.ToString()));
    }

    public override bool IsAchived()
    {
        if (this._tile.Type.Equals(TileTypes.COMBO))
            return GameplayManagers.HistoryManager.GetTiles(((ComboTile)this._tile).ComboType.ToString()) >= this._required;
        else
            return GameplayManagers.HistoryManager.GetTiles(this._tile.Type.ToString()) >= this._required;
    }
}

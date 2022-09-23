using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CollectHUD))]
public class CollectGoal : _GoalBase
{
    [SerializeField] private Tile _tile;
    private CollectHUD _hud;

    private void Start()
    {
        this.SetHUD(this.GetComponent<CollectHUD>());
    }

    public override void Complete()
    {
        this._hud.OnDeactiveHUD();
    }

    public override void DrawHUD()
    {
        try
        {
            if (this._tile.Type.Equals(TileTypes.COMBO))
                this._hud.Value = GameplayManagers.HistoryManager.GetTiles(((ComboTile)this._tile).ComboType.ToString());
            else
                this._hud.Value = GameplayManagers.HistoryManager.GetTiles((this._tile).Type.ToString());
            this._hud.OnUpdateHUD();
        }
        catch (System.Exception)
        {

        }
    }

    public override bool IsAchived()
    {
        if (this._tile.Type.Equals(TileTypes.COMBO))
            return GameplayManagers.HistoryManager.GetTiles(((ComboTile)this._tile).ComboType.ToString()) >= this._required;
        else
            return GameplayManagers.HistoryManager.GetTiles(this._tile.Type.ToString()) >= this._required;
    }

    public override void SetHUD(_HUDBase hud)
    {
        this._hud = (CollectHUD)hud;
        this._hud.Initialize(_tile.GetComponentInChildren<Renderer>().sharedMaterial, (int)this._required);
    }
}

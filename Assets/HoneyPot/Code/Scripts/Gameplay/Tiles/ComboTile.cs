using System.Collections.Generic;
using UnityEngine;

public class ComboTile : Tile
{
    [SerializeField] private ComboTypes _comboType;
    private bool _hasEffectBeenActive = false;

    public ComboTypes ComboType { get { return this._comboType; } private set { this._comboType = value; } }

    public override void OnActivate()
    {
        this.gameObject.SetActive(true);
        this.enabled = true;
    }

    public override void OnDeactivate()
    {
        GameplayManagers.SpawnManager.ComboTilePoolSpawner.SetOnPool(this.gameObject);
        this.enabled = false;
        this.gameObject.SetActive(false);
    }

    public override void OnEffect(Block block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        if (block == null || _hasEffectBeenActive) return;
        GameplayManagers.ScoreManager.OnScore(this._score);
        GameplayManagers.HistoryManager.UpdateTiles(this.ComboType.ToString());
        switch (_comboType)
        {
            case ComboTypes.BOMB:
                GameplayManagers.GridManager.Board.PopExplosion(block);
                _hasEffectBeenActive = true;
                break;
            case ComboTypes.HONEYPOT:
                GameplayManagers.GridManager.Board.PopAll();
                _hasEffectBeenActive = true;
                break;
            case ComboTypes.STAR:
                GameplayManagers.GridManager.Board.PopAll();
                _hasEffectBeenActive = true;
                break;
            default:
                break;
        }
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
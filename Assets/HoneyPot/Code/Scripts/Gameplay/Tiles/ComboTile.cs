using System.Collections.Generic;
using UnityEngine;

public class ComboTile : Tile
{
    [SerializeField] private ComboTypes _comboType;
    private bool _hasEffectBeenActive = false;

    public ComboTypes ComboTypes { get { return this._comboType; } private set { this._comboType = value; } }

    public override void OnEffect(Block block = null)
    {
        if (!GameplayManagers.GameManager.IsGameActive) return;
        if (block == null || _hasEffectBeenActive) return;
        GameplayManagers.ScoreManager.OnScore(this._score);
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
}
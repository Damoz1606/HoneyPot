using UnityEngine;

public class ComboTile : Tile
{

    [SerializeField] private ComboTypes _comboType;

    public ComboTypes ComboTypes { get { return this._comboType; } private set { this._comboType = value; } }

    public override void OnEffect()
    {
        throw new System.NotImplementedException();
    }
}
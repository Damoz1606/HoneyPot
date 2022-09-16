using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] private TileTypes _type;

    public TileTypes Type { get { return this._type; } }

    public abstract void OnEffect();
}

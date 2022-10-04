using System.Collections.Generic;
using UnityEngine;

public abstract class ATileModel
{
    [SerializeField] public TileNormalType tileType;
    [SerializeField] public int score = 100;
    [HideInInspector] public bool hasEffectBeenActive = false;

}
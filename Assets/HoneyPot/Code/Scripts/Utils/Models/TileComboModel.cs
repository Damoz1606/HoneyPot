using UnityEngine;

[System.Serializable]
public class TileComboModel : ATileModel
{
    [SerializeField] public ComboTypes comboType;
    [HideInInspector] public bool hasEffectBeenActive = false;
}
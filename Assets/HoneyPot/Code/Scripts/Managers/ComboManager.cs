using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private TileCombo _comboPrefabs;

    public TileCombo InstanceCombo(TileComboType combo)
    {
        switch (combo)
        {
            case TileComboType.BOMB:
                return (TileCombo)GameplayManagers.SpawnManager.BlockNormalSpawner.TileComboPoolDictionary[TileComboType.BOMB].OnSpawn();
            case TileComboType.HONEYPOT:
                return (TileCombo)GameplayManagers.SpawnManager.BlockNormalSpawner.TileComboPoolDictionary[TileComboType.HONEYPOT].OnSpawn();
            default:
                return null;
        }
    }
}

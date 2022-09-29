using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private TileCombo _comboPrefabs;

    public TileCombo InstanceCombo(ComboTypes combo)
    {
        switch (combo)
        {
            case ComboTypes.BOMB:
                return (TileCombo)GameplayManagers.SpawnManager.BlockNormalSpawner.TileComboPoolDictionary[ComboTypes.BOMB].OnSpawn();
            case ComboTypes.HONEYPOT:
                return (TileCombo)GameplayManagers.SpawnManager.BlockNormalSpawner.TileComboPoolDictionary[ComboTypes.BOMB].OnSpawn();
            default:
                return null;
        }
    }
}

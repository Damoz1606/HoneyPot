using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private ComboTile _comboPrefabs;

    public ComboTile InstanceCombo(ComboTypes combo)
    {
        switch (combo)
        {
            case ComboTypes.BOMB:
                return (ComboTile)GameplayManagers.SpawnManager.BlockSpawner.GetSpawner(ComboTypes.BOMB).OnSpawn();
            case ComboTypes.HONEYPOT:
                return (ComboTile)GameplayManagers.SpawnManager.BlockSpawner.GetSpawner(ComboTypes.HONEYPOT).OnSpawn();
            default:
                return null;
        }
    }
}

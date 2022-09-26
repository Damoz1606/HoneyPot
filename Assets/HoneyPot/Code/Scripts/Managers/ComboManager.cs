using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{

    public void InstanceCombo(ComboTypes combo, Block block)
    {
        // Destroy(block.Child.gameObject);
        block.Child.OnDeactivate();
        block.Child = null;
        switch (combo)
        {
            case ComboTypes.BOMB:
                GameplayManagers.SpawnManager.ComboTilePoolSpawner.ComboType = ComboTypes.BOMB;
                GameplayManagers.SpawnManager.ComboTilePoolSpawner.Spawn();
                break;
            case ComboTypes.HONEYPOT:
                GameplayManagers.SpawnManager.ComboTilePoolSpawner.ComboType = ComboTypes.HONEYPOT;
                GameplayManagers.SpawnManager.ComboTilePoolSpawner.Spawn();
                break;
        }
        block.Child = GameplayManagers.SpawnManager.ComboTilePoolSpawner.CurrentTile;
        block.Child.transform.SetParent(block.transform);
    }
}

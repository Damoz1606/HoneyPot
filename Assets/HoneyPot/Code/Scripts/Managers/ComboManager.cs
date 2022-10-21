using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private TileCombo _comboPrefabs;

    public TileCombo InstanceCombo(TileComboType combo)
    {
        TileCombo tileCombo;
        switch (combo)
        {
            case TileComboType.BOMB:
                tileCombo = (TileCombo)GameplayManagers.SpawnManager.BlockNormalSpawner.TileComboPoolDictionary[TileComboType.BOMB].OnSpawn();
                EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CRAFT, tileCombo);
                return tileCombo;
            case TileComboType.HONEYPOT:
                tileCombo = (TileCombo)GameplayManagers.SpawnManager.BlockNormalSpawner.TileComboPoolDictionary[TileComboType.HONEYPOT].OnSpawn();
                EventManager.TriggerEvent(Channels.CHALLENGE_CHANNEL, ChallengeEvent.CRAFT, tileCombo);
                return tileCombo;
            default:
                return null;
        }
    }
}

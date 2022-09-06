using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tiles;

    public void Spawn(Transform parent) {
        int randomIndex = Random.Range(0, this._tiles.Count);
        Vector3 spawnPosition = parent.position;
        spawnPosition.z = 0.25f;
        GameObject temp = Instantiate(_tiles[randomIndex], spawnPosition, Quaternion.identity);
        temp.transform.SetParent(parent);
    }
}

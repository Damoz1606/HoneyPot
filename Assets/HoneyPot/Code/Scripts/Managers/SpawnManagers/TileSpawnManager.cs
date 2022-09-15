using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tiles;

    public void Spawn(out GameObject tile)
    {
        int randomIndex = Random.Range(0, this._tiles.Count);
        tile = Instantiate(_tiles[randomIndex], Vector3.zero, Quaternion.identity);
    }
}

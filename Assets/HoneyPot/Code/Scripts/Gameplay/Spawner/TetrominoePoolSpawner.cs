using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoePoolSpawner : _SpawnerBase
{
    [SerializeField] private List<Pool> _tetrominoePool;

    public override void Spawn()
    {
        int randomIndex = Random.Range(0, this._tetrominoePool.Count);
        GameObject tmp = _tetrominoePool[randomIndex].GetPooledObject();
        tmp.transform.position = new Vector3(GameplayManagers.GridManager.GridWidth / 2, GameplayManagers.GridManager.GridHeight - Constants.GRID_GREACE_HEIGHT, 0);
        tmp.transform.rotation = Quaternion.identity;
        GameplayManagers.GameManager.CurrentTetrominoe = tmp.GetComponent<Tetrominoe>();
        // tmp.transform.SetParent(GameplayManagers.GameManager.BlockHolder);

        GameplayManagers.InputManager.IsInputActive = true;
        tmp.GetComponent<Tetrominoe>().OnActivate();
    }
}

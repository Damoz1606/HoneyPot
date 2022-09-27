using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTetrominoesSpawner : MonoBehaviour
{
    [SerializeField] private List<KeyPair<TetrominoeTypes, TetrominoeSpawner>> _tetrominoeSpawnerKeyPair;
    [SerializeField] private bool _usePool = false;
    private Dictionary<TetrominoeTypes, TetrominoeSpawner> _tetrominoeSpawnerDictionary = new Dictionary<TetrominoeTypes, TetrominoeSpawner>();

    public bool UsePool
    {
        set
        {
            this._usePool = value;
            _tetrominoeSpawnerKeyPair.ForEach(item => item.value.UsePool = this._usePool);
        }
    }

    private void Awake()
    {
        _tetrominoeSpawnerKeyPair.ForEach(item =>
        {
            item.value.UsePool = _usePool;
            _tetrominoeSpawnerDictionary.Add(item.key, item.value);
        });
    }
    public void OnSpawn()
    {
        int randomIndex = Random.Range(0, this._tetrominoeSpawnerDictionary.Count);
        Tetrominoe tmp = this.GetSpawner(randomIndex).OnSpawn();
        GameplayManagers.GameManager.CurrentTetrominoe = tmp.GetComponent<Tetrominoe>();
        GameplayManagers.InputManager.IsInputActive = true;
    }

    private TetrominoeSpawner GetSpawner(int index)
    {
        switch (index)
        {
            case 0:
                return this._tetrominoeSpawnerDictionary[TetrominoeTypes.I];
            case 1:
                return this._tetrominoeSpawnerDictionary[TetrominoeTypes.J];
            case 2:
                return this._tetrominoeSpawnerDictionary[TetrominoeTypes.L];
            case 3:
                return this._tetrominoeSpawnerDictionary[TetrominoeTypes.O];
            case 4:
                return this._tetrominoeSpawnerDictionary[TetrominoeTypes.S];
            case 5:
                return this._tetrominoeSpawnerDictionary[TetrominoeTypes.T];
            case 6:
                return this._tetrominoeSpawnerDictionary[TetrominoeTypes.Z];
            default:
                return null;
        }
    }

    public void OnKill(Tetrominoe tetrominoe) => this._tetrominoeSpawnerDictionary[tetrominoe.TetrominoeTypes].OnKill(tetrominoe);
}

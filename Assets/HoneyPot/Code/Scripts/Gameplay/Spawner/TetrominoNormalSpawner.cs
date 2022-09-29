using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TetrominoNormalSpawner : MonoBehaviour, ISpawn<TetrominoeNormal>
{
    [SerializeField] private List<KeyPair<TetrominoeTypes, TetrominoeNormalPool>> _tetrominoNormal;
    private Dictionary<TetrominoeTypes, TetrominoeNormalPool> _tetrominoNormalDictionary = new Dictionary<TetrominoeTypes, TetrominoeNormalPool>();
    public Dictionary<TetrominoeTypes, TetrominoeNormalPool> tetrominoNormalDictionary => this._tetrominoNormalDictionary;

    public bool UsePool { set { _tetrominoNormal.ForEach(item => item.value.UsePool = value); } }

    private void Awake()
    {
        _tetrominoNormal.ForEach(item => this.tetrominoNormalDictionary.Add(item.key, item.value));
    }

    public void OnKill(TetrominoeNormal shape)
    {
        _tetrominoNormalDictionary[shape.type].OnKill(shape);
    }

    public TetrominoeNormal OnSpawn()
    {
        int randomIndex = Random.Range(0, this._tetrominoNormalDictionary.Count);
        TetrominoeNormal tmp = this._tetrominoNormalDictionary[this.GetTypes(randomIndex)].OnSpawn();
        tmp.transform.rotation = Quaternion.identity;
        GameplayManagers.GameManager.CurrentTetrominoe = tmp.GetComponent<ITetrominoe>();
        GameplayManagers.InputManager.IsInputActive = true;
        return null;
    }

    private TetrominoeTypes GetTypes(int index)
    {
        switch (index)
        {
            case 0: return TetrominoeTypes.I;
            case 1: return TetrominoeTypes.J;
            case 2: return TetrominoeTypes.L;
            case 3: return TetrominoeTypes.O;
            case 4: return TetrominoeTypes.S;
            case 5: return TetrominoeTypes.T;
            case 6: return TetrominoeTypes.Z;
            default: return TetrominoeTypes.I;
        }
    }
}

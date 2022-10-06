using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private bool _useCollectGoal = false;
    [SerializeField] private List<CollectGoal> _collectGoals;
    [SerializeField] private bool _useScoreGoal = false;
    [SerializeField] private ScoreGoal _scoreGoal;

    public bool UseCollectGoal => this._useCollectGoal;
    public bool UseScoreGoal => this._useScoreGoal;
}
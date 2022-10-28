using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationManager : MonoBehaviour
{
    public static ConfigurationManager _instance;
    public static ConfigurationManager Instance => _instance;

    [SerializeField] private GoalStruct _goals;
    [SerializeField] private GridStruct _grid;
    [SerializeField] private ScoreStruct _score;

    public GoalStruct Goals { get => _goals; set => _goals = value; }
    public ScoreStruct Score { get => _score; set => _score = value; }
    public GridStruct Grid { get => _grid; set => _grid = value; }
    public int WorldID { get; set; }
    public int LevelID { get; set; }
    public int MusicAudioLevel { get; set; }
    public int SFXAudioLevel { get; set; }
    public int UIAudioLevel { get; set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ClearConfig()
    {
        this.Goals = default;
        this.Score = default;
        this.Grid = default;
    }
}

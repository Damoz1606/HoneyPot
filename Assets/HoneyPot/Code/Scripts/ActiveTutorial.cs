using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveTutorial : MonoBehaviour
{
    [System.Serializable]
    public struct ConfigurationStruct
    {
        [SerializeField] public GoalStruct _goals;
        [SerializeField] public GridStruct _grid;
        [SerializeField] public ScoreStruct _score;
    }
    [SerializeField] public ConfigurationStruct configuration;
    [SerializeField] private AUIBase _loadUI;
    [SerializeField] private bool _isArcadeMode = false;
    private List<GameStats> stats;
    public bool IsArcadeMode { set => this._isArcadeMode = value; get => this._isArcadeMode; }

    private void Start()
    {
        this.stats = Storage.Instance.Read<GameStats>($"{Storage.ROOT}{StorageConstants.GAME_STATS}");
    }

    public void CheckTutorial()
    {
        if (this.stats[0] == null || !this.stats[0].hasCompleteTutorial)
        {

            ConfigurationManager.Instance.Goals = configuration._goals;
            ConfigurationManager.Instance.Grid = configuration._grid;
            ConfigurationManager.Instance.Score = configuration._score;
            ConfigurationManager.Instance.LevelID = 0;
            this.stats[0].hasCompleteTutorial = true;
            StartCoroutine(StartLevelEventAsyncOperation(2));
            Storage.Instance.Store<GameStats>(this.stats[0], $"{Storage.ROOT}{StorageConstants.GAME_STATS}");
        }
        else
        {
            if (_isArcadeMode)
            {
                ConfigurationManager.Instance.Grid = configuration._grid;
                configuration._score.maxScore = 10000;
                ConfigurationManager.Instance.Score = configuration._score;
                ConfigurationManager.Instance.LevelID = 0;
                StartCoroutine(StartLevelEventAsyncOperation(2));
            }
            else
            {
                StartCoroutine(StartLevelEventAsyncOperation(1));
            }
        }
    }

    private IEnumerator StartLevelEventAsyncOperation(int sceneID)
    {
        _loadUI.StartUI();
        yield return new WaitForSecondsRealtime(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            // Debug.Log($"Loading... {operation.progress * 100}%");
            yield return null;
        }
    }
}
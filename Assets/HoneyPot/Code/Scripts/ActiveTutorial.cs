using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private async void Start()
    {
        await this.CheckFileAsync();
    }

    private async Task CheckFileAsync()
    {
        this.stats = await Storage.Instance.ReadAsync<GameStats>($"{StorageConstants.GAME_STATS}");
    }

    private async Task UpdateFileAsync()
    {
        await Storage.Instance.StoreAsync<GameStats>(this.stats[0], $"{StorageConstants.GAME_STATS}");
    }

    public async void CheckTutorial()
    {
        await this.CheckTutorialAsync();
    }

    public async Task CheckTutorialAsync()
    {
        if (this.stats[0] == null || (this.stats[0] != null && !this.stats[0].hasCompleteTutorial))
        {
            ConfigurationManager.Instance.Goals = configuration._goals;
            ConfigurationManager.Instance.Grid = configuration._grid;
            ConfigurationManager.Instance.Score = configuration._score;
            ConfigurationManager.Instance.LevelID = 0;
            this.stats[0].hasCompleteTutorial = true;
            StartCoroutine(StartLevelEventAsyncOperation(2));
            await this.UpdateFileAsync();
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
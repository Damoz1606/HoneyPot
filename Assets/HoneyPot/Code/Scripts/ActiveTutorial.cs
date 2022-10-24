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

    private List<GameStats> stats;

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

            StartCoroutine(StartLevelEventAsyncOperation(2));
        }
        else
        {
            SceneManager.LoadScene(1);
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
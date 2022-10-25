using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
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
    [SerializeField] private int _worldID = 0;
    [SerializeField] private int _levelID = 0;

    public void StartLevelEvent(int scene)
    {
        ConfigurationManager.Instance.Goals = configuration._goals;
        ConfigurationManager.Instance.Grid = configuration._grid;
        ConfigurationManager.Instance.Score = configuration._score;
        ConfigurationManager.Instance.LevelID = this._levelID;
        ConfigurationManager.Instance.WorldID = this._worldID;

        StartCoroutine(StartLevelEventAsyncOperation(scene));
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

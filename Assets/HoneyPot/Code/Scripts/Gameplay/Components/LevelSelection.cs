using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] ConfigurationStruct configuration;
    [SerializeField] private AUIBase _loadUI;

    public void StartLevelEvent(int scene)
    {
        ConfigurationManager.Instance.Goals = configuration._goals;
        ConfigurationManager.Instance.Grid = configuration._grid;
        ConfigurationManager.Instance.Score = configuration._score;

        StartCoroutine(StartLevelEventAsyncOperation(scene));
    }

    private IEnumerator StartLevelEventAsyncOperation(int sceneID)
    {
        _loadUI.StartUI();
        yield return new WaitForSecondsRealtime(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            Debug.Log($"Loading... {operation.progress * 100}%");
            yield return null;
        }
    }
}

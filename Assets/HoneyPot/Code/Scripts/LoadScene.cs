using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private AUIBase _loadUI;

    public void LoadSceneOperation(int sceneID)
    {
        StartCoroutine(this.LoadAsyncOperation(sceneID));
    }

    private IEnumerator LoadAsyncOperation(int sceneID)
    {
        if (_loadUI != null) this._loadUI.StartUI();
        yield return new WaitForSecondsRealtime(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
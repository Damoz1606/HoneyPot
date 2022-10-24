using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void LoadSceneOperation(int sceneID)
    {
        StartCoroutine(this.LoadAsyncOperation(sceneID));
    }

    private IEnumerator LoadAsyncOperation(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
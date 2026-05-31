using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneManager : MonoBehaviour
{
    protected void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadScenes(sceneIndex));
    }

    private IEnumerator LoadScenes(int _sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        GameManager.Instance.currentSceneIndex = _sceneIndex;
        PlayerManager.Instance.playerPause = false;
        yield break;
    }

    protected void UnloadScene(int sceneIndex)
    {
        StartCoroutine(UnloadScenes(sceneIndex));
    }

    private IEnumerator UnloadScenes(int _sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(_sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        GameManager.Instance.isSceneChangeable = true;

        yield break;
    }

    public static AsyncOperation ReloadScene()
    {
        return SceneManager.LoadSceneAsync(GameManager.Instance.currentSceneIndex);
    }
}
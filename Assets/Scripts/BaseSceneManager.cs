using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneManager : MonoBehaviour
{
    protected int nowSceneIndex = GameManager.currentSceneIndex;

    public void LoadScene(int _sceneIndex)
    {
        StartCoroutine(LoadScenes(_sceneIndex));
    }

    public IEnumerator LoadScenes(int sceneIndex)
    {
        PlayerManager.playerPause = true;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        GameManager.currentSceneIndex = sceneIndex;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        PlayerManager.playerPause = false;
    }

    public void UnloadScene(int _sceneIndex)
    {
        StartCoroutine(UnloadScenes(_sceneIndex));
    }

    public IEnumerator UnloadScenes(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        GameManager.isSceneChangeable = true;
    }

    public static AsyncOperation ReloadScene()
    {
        return SceneManager.LoadSceneAsync(GameManager.currentSceneIndex);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneManager : MonoBehaviour
{
    protected int nowSceneIndex;

    private void Start()
    {
        nowSceneIndex = GameManager.Instance.currentSceneIndex;
    }

    public void LoadScene(int _sceneIndex)
    {
        StartCoroutine(LoadScenes(_sceneIndex));
    }

    public IEnumerator LoadScenes(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        GameManager.Instance.currentSceneIndex = sceneIndex;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        PlayerManager.Instance.playerPause = false;
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

        GameManager.Instance.isSceneChangeable = true;
    }

    public static AsyncOperation ReloadScene()
    {
        return SceneManager.LoadSceneAsync(GameManager.Instance.currentSceneIndex);
    }
}
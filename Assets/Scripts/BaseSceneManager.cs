using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneManager : MonoBehaviour
{
    protected int nowSceneIndex = GameManager.currentSceneIndex;
    protected bool isFirst = true;

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
        SceneManager.UnloadSceneAsync(nowSceneIndex);
    }

    public static AsyncOperation ReloadScene()
    {
        return SceneManager.LoadSceneAsync(GameManager.currentSceneIndex);
    }
}
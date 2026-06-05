using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseSceneManager : MonoBehaviour
{
    private static BaseSceneManager instance;
    public static BaseSceneManager Instance { get { return instance; } }

    public int NowScene { get; private set; }
    [SerializeField] private float transitionTime = 0.2f;

    private Slider transitionUI;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        transitionUI = GetComponentInChildren<Slider>(true);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        NowScene = SceneManager.GetActiveScene().buildIndex;
        if (transitionUI.value == 1f)   StartCoroutine(TransitionExit());
    }


    public void LoadScene(int sceneIndex, bool transition = true)
    {
        if (transition)
        {
            StartCoroutine(TransitionEnter(sceneIndex));
        }
        else
        {
            StartCoroutine(LoadSceneDefault(sceneIndex));
        }
    }

    private IEnumerator LoadSceneDefault(int _sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield break;
    }

    private IEnumerator TransitionEnter(int _sceneIndex)
    {
        transitionUI.direction = Slider.Direction.RightToLeft;
        for (float timeCount = 0; timeCount <= transitionTime; timeCount += Time.deltaTime)
        {
            transitionUI.value = timeCount / transitionTime;
            yield return null;
        }
        transitionUI.value = 1;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneIndex);
        if (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield break;
    }

    private IEnumerator TransitionExit()
    {
        transitionUI.direction = Slider.Direction.LeftToRight;
        for (float timeCount = transitionTime; timeCount >= 0; timeCount -= Time.deltaTime)
        {
            transitionUI.value = timeCount / transitionTime;
            yield return null;
        }
        transitionUI.value = 0;

        yield break;
    }
}
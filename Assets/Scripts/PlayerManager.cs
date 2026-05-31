using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } }

    private Rigidbody2D _rigid;
    private CapsuleCollider2D _collider2D;

    public Vector2 spawnPoint = new Vector2(-12f, -5.13f);
    public bool isPlayerTouchSpawnPoint = false;
    public bool playerPause = false;

    public bool debug_setInvincibilityMode = false;

    private void Awake()
    {
        if (instance.Equals(null))
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GetComponent<PlayerInput>().enabled = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerVFX.Instance.PlayerVFXReset();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<CapsuleCollider2D>();
    }

    public void PlayerPauseOn()
    {
        playerPause = true;
        _rigid.bodyType = RigidbodyType2D.Static;
        _collider2D.enabled = false;
    }

    public void PlayerPauseOff()
    {
        playerPause = false;
        _rigid.bodyType = RigidbodyType2D.Dynamic;
        _collider2D.enabled = true;
    }

    public void Respawn()
    {
        StartCoroutine(Restart());
    }

    public IEnumerator Restart()
    {
        AsyncOperation asyncLoad = BaseSceneManager.ReloadScene();

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        transform.position = spawnPoint;
        PlayerPauseOff();

        yield break;
    }
}

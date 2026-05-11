using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    private Rigidbody2D _rigid;
    private CapsuleCollider2D _collider2D;

    public static Vector2 spawnPoint = new Vector2(-12f, -5.13f);
    public static bool isPlayerTouchSpawnPoint = false;
    public static bool playerPause = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        GetComponent<PlayerInput>().enabled = true;
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
    }
}

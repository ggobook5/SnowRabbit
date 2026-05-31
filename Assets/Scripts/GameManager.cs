using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int currentSceneIndex;
    public Vector2 playerSpawnPoint = Vector2.zero;
    public bool isSceneChangeable = true;

    private void Awake()
    {
        if (instance.Equals(null))
        {
            instance = this;
            currentSceneIndex = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

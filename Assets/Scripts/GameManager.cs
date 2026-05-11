using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static int currentSceneIndex = 0;
    public static Vector2 playerSpawnPoint = Vector2.zero;
    public static bool isSceneChangeable = true;

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
    }
}

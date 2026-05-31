using UnityEngine;

public class NextLevelManager : BaseSceneManager
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.isSceneChangeable && collision.CompareTag("Player"))
        {
            GameManager.Instance.isSceneChangeable = false;
            LoadScene(GameManager.Instance.currentSceneIndex + 1);
        }
    }
}

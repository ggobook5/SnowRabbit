using System.Collections;
using UnityEngine;

public class NextLevelManager : BaseSceneManager
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.isSceneChangeable && collision.CompareTag("Player"))
        {
            GameManager.isSceneChangeable = false;
            LoadScene(nowSceneIndex + 1);
        }
    }
}

using System.Collections;
using UnityEngine;

public class PreviousManager : BaseSceneManager
{
    private void Start()
    {
        StartCoroutine(EnableCollider());
    }

    private IEnumerator EnableCollider()
    {
        while (!PlayerManager.isPlayerTouchSpawnPoint)
        {
            yield return null;
        }

        GetComponent<BoxCollider2D>().enabled = true;

        yield return null;

        PlayerManager.isPlayerTouchSpawnPoint = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFirst && collision.CompareTag("Player"))
        {
            LoadScene(nowSceneIndex - 1);
            isFirst = false;
        }
    }
}

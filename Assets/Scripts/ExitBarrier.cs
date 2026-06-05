using UnityEngine;

public class ExitBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BaseSceneManager.Instance.LoadScene(BaseSceneManager.Instance.NowScene + 1);
        }
    }
}

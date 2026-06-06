using UnityEngine;

public class ExitBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.LoadScene(GameManager.Instance.NowScene + 1);
        }
    }
}

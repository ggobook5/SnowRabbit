using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        Debug.Log("스크립트 실행됨");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("트리거 충돌!");

        if (collision.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
}
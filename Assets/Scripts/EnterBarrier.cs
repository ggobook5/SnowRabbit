using UnityEngine;

public class EnterBarrier : BaseSceneManager
{
    private BoxCollider2D _box;

    private void Start()
    {
        _box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _box.isTrigger = false;
            UnloadScene(nowSceneIndex - 1);
        }
    }
}

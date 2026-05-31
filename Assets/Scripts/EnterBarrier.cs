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
            UnloadScene(GameManager.Instance.currentSceneIndex - 1);
            _box.isTrigger = false;
        }
    }
}

using UnityEngine;

public class EnterBarrier : MonoBehaviour
{
    private BoxCollider2D _box;
    
    [Tooltip("Position the character when the character first enter the scene")]
    [SerializeField] private Vector2 movePoint;

    private void Awake()
    {
        _box = GetComponent<BoxCollider2D>();
        Invoke("TurnOffTrigger", 0.2f);
    }

    private void TurnOffTrigger()
    {
        _box.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _box.isTrigger = false;
            PlayerManager.Instance.PlayerPause = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D _pRigid))
        {
            _pRigid.MovePosition(movePoint);
        }
    }
}

using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 13f;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private Vector2 searchOffset = new Vector2(0f, -3f);
    [SerializeField] private Vector2 searchScope = new Vector2(2f, 6f);
    private Vector2 searchPosition;
    private Vector2 direction;
    private Collider2D target;

    private Rigidbody2D _rigid;
    private Animator _anim;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        searchPosition = new Vector2(transform.position.x + searchOffset.x, transform.position.y + searchOffset.y);
    }

    private void Update()
    {
        if (target == null)
        {
            target = Physics2D.OverlapBox(searchPosition, searchScope, 0, layerPlayer);
            if (target)
            {
                _anim.SetTrigger("PlayerDetected");
                direction = (target.transform.position - transform.position).normalized;
            }
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            _rigid.linearVelocity = direction * moveSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier") || collision.CompareTag("DeadBarrier"))
        {
            gameObject.SetActive(false);
        }
    }
}

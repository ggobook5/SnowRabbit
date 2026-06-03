using UnityEngine;

public class FlyingMonster : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 13f;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private Vector2 searchOffset = new Vector2(0f, -3f);
    [SerializeField] private Vector2 searchScope = new Vector2(2f, 6f);
    private Vector2 searchPosition;
    private Vector2 targetPosition;
    private Collider2D target;

    private void Start()
    {
        searchPosition = new Vector2(transform.position.x + searchOffset.x, transform.position.y + searchOffset.y);
    }

    private void Update()
    {
        if (target == null)
        {
            target = Physics2D.OverlapBox(searchPosition, searchScope, 0, layerPlayer);
            if (target) targetPosition = target.transform.position;
            else return;
        }
        else    
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}

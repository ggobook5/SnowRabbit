using UnityEngine;

public class MonsterAI_FlyingMonster : MonoBehaviour
{
    public float moveSpeed = 13f;
    public LayerMask layerPlayer;
    public Vector2 searchOffset = new Vector2(0f, -3f);
    public Vector2 searchScope = new Vector2(2f, 6f);
    private Vector2 searchPosition;
    private Vector2 targetPosition;

    private void Start()
    {
        searchPosition = new Vector2(transform.position.x + searchOffset.x, transform.position.y + searchOffset.y);
    }

    void Update()
    {
        if (targetPosition != null)
        {
            Collider2D target = Physics2D.OverlapBox(searchPosition, searchScope, layerPlayer);
            if (target != null)
            {
                targetPosition = target.transform.position;
            }

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position.y <= targetPosition.y)
            {
                enabled = false;
            }
        }
    }
}

using UnityEngine;

public class MonsterAI_FlyingMonster : MonoBehaviour
{
    public float moveSpeed = 13f;
    public LayerMask layerPlayer;
    public Vector2 findOffset = new Vector2(0f, -3f);
    public Vector2 findSize = new Vector2(2f, 6f);
    private Vector2 findPosition;
    private Collider2D target;
    private Vector2 targetPoint;

    private void Start()
    {
        findPosition = new Vector2(transform.position.x + findOffset.x, transform.position.y + findOffset.y);
    }

    void Update()
    {
        if (target == null)
        {
            target = Physics2D.OverlapBox(findPosition, findSize, 0f, layerPlayer);

            if (target != null)
            {
                targetPoint = target.transform.position;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        }

        if (transform.position.y <= targetPoint.y)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(findPosition, findSize);
    }
}

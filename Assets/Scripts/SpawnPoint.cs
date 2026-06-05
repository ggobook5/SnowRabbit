using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector2 SpawnPos { get; private set; }
    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        if (PlayerManager.Instance == null)
        {
            Instantiate(playerPrefab);
        }
    }

    void Start()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, groundLayer);

        // 0.87f: Y position gap between position of the player character and position of the actual collision with the ground
        SpawnPos = new Vector2(hit.point.x, hit.point.y + 0.87f);
    }
}

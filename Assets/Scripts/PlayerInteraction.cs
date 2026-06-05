using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerMovement _pMove;
    private PlayerAnimation _pAnim;
    private Rigidbody2D _rigid;

    private void Awake()
    {
        _pMove = GetComponent<PlayerMovement>();
        _pAnim = GetComponent<PlayerAnimation>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("DeadBarrier"))
        {
            _pAnim.Melt();
        }
        else if (collision.gameObject.CompareTag("Wall") && _pMove.IsWall)
        {
            _pMove.IsWallStop = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            _pAnim.Melt();
            collision.enabled = false;
        }

        if (collision.TryGetComponent<SpawnPoint>(out SpawnPoint _spawn))
        {
            if (DataManager.Instance.NowPlayData.lastSpawnPoint != _spawn.SpawnPos)
            {
                DataManager.Instance.NowPlayData.lastSpawnPoint = _spawn.SpawnPos;
                DataManager.Instance.NowPlayData.lastSceneIndex = GameObject.Find("BaseSceneManager").GetComponent<BaseSceneManager>().NowScene;
                DataManager.Instance.SaveData();
                collision.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}

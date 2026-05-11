using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerMovement _pMove;
    private PlayerAnimation _pAnim;

    private void Start()
    {
        _pMove = GetComponent<PlayerMovement>();
        _pAnim = GetComponent<PlayerAnimation>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("DeadBarrier"))
        {
            _pAnim.Melt();
        }
        else if (collision.gameObject.CompareTag("Wall") && _pMove.isWall)
        {
            _pMove.isWallStop = true;
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
            _pMove.spawnPoint = _spawn.spawnPos;
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

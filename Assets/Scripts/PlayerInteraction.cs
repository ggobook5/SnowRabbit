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
            collision.gameObject.SetActive(false);
            _pAnim.Melt();
        }
        
        if (collision.TryGetComponent<SpawnPoint>(out SpawnPoint _spawn))
        {
            PlayerManager.spawnPoint = _spawn.spawnPos;
            PlayerManager.isPlayerTouchSpawnPoint = true;
            collision.enabled = false;
        }
    }
}

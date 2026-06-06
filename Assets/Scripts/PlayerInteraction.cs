using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerMovement _pMove;
    private PlayerAnimation _pAnim;

    private void Awake()
    {
        _pMove = GetComponent<PlayerMovement>();
        _pAnim = GetComponent<PlayerAnimation>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("DeadBarrier"))
        {
            PlayerManager.Instance.PlayerPauseOn();
            _pAnim.Die();
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
            PlayerManager.Instance.PlayerPauseOn();
            _pAnim.Die();
            collision.enabled = false;
        }

        if (collision.TryGetComponent<CheckPoint>(out CheckPoint checkPoint))
        {
            checkPoint.SetActivated();
        }

        if (collision.TryGetComponent<Item>(out Item item))
        {
            item.SetActivated();
        }
    }
}

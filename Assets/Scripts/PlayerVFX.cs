using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    public GameObject BodyParticleVFX;
    public GameObject ChargeJumpVFX;
    public GameObject DieVFX;
    public GameObject JumpVFX;
    public GameObject RespawnVFX;

    private PlayerMovement _pMove;

    private void Start()
    {
        _pMove = GetComponent<PlayerMovement>();
    }

    public void Effect()
    {

    }
}

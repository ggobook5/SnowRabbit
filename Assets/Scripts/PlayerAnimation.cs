using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rigid;
    private PlayerMovement _pMove;

    private int animIDSpeedX;
    private int animIDSpeedY;
    private int animIDGrounded;
    private int animIDWalled;
    private int animIDCharge;
    private int animIDMelt;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _pMove = GetComponent<PlayerMovement>();

        animIDSpeedX = Animator.StringToHash("SpeedX");
        animIDSpeedY = Animator.StringToHash("SpeedY");
        animIDGrounded = Animator.StringToHash("Grounded");
        animIDWalled = Animator.StringToHash("Walled");
        animIDCharge = Animator.StringToHash("Charge");
        animIDMelt = Animator.StringToHash("Melt");
    }

    private void Update()
    {
        _anim.SetFloat(animIDSpeedX, _rigid.linearVelocityX);
        _anim.SetFloat(animIDSpeedY, _rigid.linearVelocityY);
        _anim.SetBool(animIDGrounded, _pMove.IsGround);
        _anim.SetBool(animIDWalled, _pMove.IsWall);
        _anim.SetBool(animIDCharge, _pMove.IsJumpCharging);
    }

    public void Melt()
    {
        if (PlayerManager.Instance.Debug_setInvincibilityMode)    return;

        _anim.SetTrigger(animIDMelt);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement: MonoBehaviour
{
    // Management
    private float _deltaTime;

    // Character
    private Rigidbody2D _rigid;
    private SpriteRenderer _render;
    private PlayerAnimation _pAnim;



    // Movement
    [Header("Movement")]
    [SerializeField]
    [Tooltip("Move speed of the character")]
    [Min(0f)] private float moveSpeed = 10f;

    [SerializeField]
    [Tooltip("Magnification of move speed when the character is charging the jump")]
    [Range(0f, 1f)] private float chargingModifyX = 0.4f;

    [SerializeField]
    [Tooltip("Magnification of falling speed when the character is attached to the climbing wall")]
    [Range(0f, 1f)] private float gripModifyY = 0.4f;

    [Space(7)]
    [SerializeField]
    [Tooltip("Time when the character can be stopped (in seconds)")]
    [Min(0f)] private float maxStopTime = 5f;

    private float stopTime = 0f;
    private Vector2 inputDirection;


    // Jump
    [Header("Jump")]
    [SerializeField]
    [Tooltip("Default jump force of the character")]
    [Min(0f)] private float defaultJumpForce = 13f;

    [SerializeField]
    [Tooltip("Magnification of jump force at maximum charge")]
    [Min(0f)] private float maxChargeMultiplier = 2f;

    [SerializeField]
    [Tooltip("Charge time required to reach maximum jump force (in seconds)")]
    [Min(0f)] private float maxChargeTime = 1.5f;

    [SerializeField]
    [Tooltip("Time the character was charging (in seconds) (Maximum value: maxChargeTime)")]
    private float jumpChargeTime = 0f;

    [SerializeField]
    [Tooltip("Whether the character is charging the jump or not")]
    private bool isJumpCharging = false;
    public bool IsJumpCharging { get { return isJumpCharging; } }

    [Space(7)]
    [SerializeField]
    [Tooltip("Wall jump force of the character")]
    [Min(0f)] private float wallJumpForce = 30f;

    [SerializeField]
    [Tooltip("Whether the character does wall jump or not")]
    private bool isWallJump = false;
    public bool IsWallJump { get { return isWallJump; } }

    [Space(7)]
    [SerializeField]
    [Tooltip("Jump force of the character, when character is on the mushroom")]
    [Min(0f)] private float mushroomJumpForce = 30f;

    private InputAction _jumpAction;


    // Ground Check
    [Header("Ground Check")]
    [SerializeField]
    [Tooltip("Y position gab from position of the character to the center point of the ground check")]
    private float groundCheckOffset = -0.7f;

    [Tooltip("Radius of the ground check.")]
    [SerializeField]
    [Min(0.00001f)] private float groundCheckRadius = 0.3f;

    [Space(7)]
    [SerializeField]
    [Tooltip("Layer the character uses as the ground")]
    private LayerMask layerGround;

    [SerializeField]
    [Tooltip("Whether the character is on the ground or not")]
    private bool isGround;
    public bool IsGround { get { return isGround; } }

    [Space(7)]
    [SerializeField]
    [Tooltip("Layer the character uses as the mushroom")]
    private LayerMask layerMushroom;

    [SerializeField]
    [Tooltip("Whether the character is on the mushroom or not")]
    private bool isMushroom;
    public bool IsMushroom { get { return isMushroom; } }


    // Wall Check
    [Header("Wall Check")]
    [SerializeField]
    [Tooltip("X position gab from position of the character to the start point of the climbing wall check")]
    private float wallCheckOffsetX = 0.6f;

    [SerializeField]
    [Tooltip("Y position gab from position of the character to the start point of the climbing wall check")]
    private float wallCheckOffsetY = -0.265f;

    [SerializeField]
    [Tooltip("Y position gab from the center point to the top point of the character")]
    private float wallCheckPointOffset = 0.6f;

    [SerializeField]
    [Tooltip("Distance to detect the climbing wall")]
    [Min(0f)] private float wallCheckDistance = 0.1f;

    [SerializeField]
    [Tooltip("Layer the character uses as the climbing wall")]
    private LayerMask layerWall;

    [SerializeField]
    [Tooltip("Whether the character is on the climbing wall or not")]
    private bool isWall;
    public bool IsWall { get { return isWall; } }

    [Space(7)]
    [SerializeField]
    [Tooltip("Time when the character can be stopped on the climbing wall (in seconds)")]
    [Min(0f)] private float maxWallStopTime = 0.5f;

    [SerializeField]
    [Tooltip("Time the character stopped on the climbing wall (in seconds) (Maximum value: maxWallStopTime)")]
    private float wallStopTime = 0f;

    [SerializeField]
    [Tooltip("Whether the character is stopped on the climbing wall or not")]
    private bool isWallStop;
    public bool IsWallStop { set { isWallStop = value; } }



    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _render = GetComponent<SpriteRenderer>();
        _pAnim = GetComponent<PlayerAnimation>();
        _jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        if (PlayerManager.Instance.playerPause)     return;

        _deltaTime = Time.deltaTime;

        if (_rigid.linearVelocity == Vector2.zero)
        {
            stopTime += _deltaTime;
            if (stopTime >= maxStopTime)
            {
                _pAnim.Melt();
                stopTime = 0f;
            }
        }
        else
        {
            stopTime = 0f;
        }

        GroundCheck();
        WallCheck();
        Jump();

        if (isWallStop)
        {
            WallStop();
        }
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.playerPause)     return;

        _render.flipX = (inputDirection.x == 0) ? _render.flipX : (inputDirection.x < 0f);

        if (isWallJump)
        {
            PlayerVFX.Instance.KickVFX();
            _rigid.gravityScale = 1f;
            _rigid.linearVelocity = Vector2.zero;
            _rigid.linearVelocity = new Vector2(-inputDirection.x * wallJumpForce, wallJumpForce);

            isWallJump = false;
        }
        else
        {
            float moveModifyX = isJumpCharging ? (moveSpeed * chargingModifyX) : moveSpeed;
            float moveModifyY = isWall ? gripModifyY : 1f;

            _rigid.linearVelocity = new Vector2(inputDirection.x * moveModifyX, _rigid.linearVelocityY * moveModifyY);
        }
    }

    private void GroundCheck()
    {
        Vector2 groundCheckPoint = new Vector2(transform.position.x, transform.position.y + groundCheckOffset);
        isMushroom = Physics2D.OverlapCircle(groundCheckPoint, groundCheckRadius, layerMushroom);
        isGround = !isMushroom && Physics2D.OverlapCircle(groundCheckPoint, groundCheckRadius, layerGround);
    }

    private void WallCheck()
    {
        if (isGround) 
        {
            isWall = false;
            return;
        }

        Vector2 wallCheckDirection = Vector2.right * inputDirection.x;

        Vector2 wallCheckPoint = new Vector2(transform.position.x + (wallCheckOffsetX * inputDirection.x), transform.position.y + wallCheckOffsetY);
        bool wallCheckCenter = Physics2D.Raycast(wallCheckPoint, wallCheckDirection, wallCheckDistance, layerWall);
        wallCheckPoint = new Vector2(wallCheckPoint.x, wallCheckPoint.y + wallCheckPointOffset);
        bool wallCheckTop = Physics2D.Raycast(wallCheckPoint, wallCheckDirection, wallCheckDistance, layerWall);

        isWall = wallCheckTop && wallCheckCenter;
    }

    private void WallStop()
    {
        wallStopTime += _deltaTime;
        _rigid.gravityScale = 0f;

        if (wallStopTime >= maxWallStopTime || !isWall)
        {
            isWallStop = false;
            _rigid.gravityScale = 1f;
            wallStopTime = 0f;
        }
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    private void Jump()
    {
        if (isGround)
        {
            if (_jumpAction.IsPressed())
            {
                isJumpCharging = true;

                jumpChargeTime += _deltaTime;
                jumpChargeTime = Mathf.Clamp(jumpChargeTime, 0f, maxChargeTime);
            }
            if (_jumpAction.WasReleasedThisFrame())
            {
                PlayerVFX.Instance.JumpVFX();
                float chargeRatio = jumpChargeTime / maxChargeTime;
                float finalJumpForce = defaultJumpForce * Mathf.Lerp(1f, maxChargeMultiplier, chargeRatio);
                _rigid.linearVelocityY = finalJumpForce;

                isJumpCharging = false;
                jumpChargeTime = 0f;
            }
        }
        else
        {
            isJumpCharging = false;
            jumpChargeTime = 0f;
        }

        if (isMushroom)
        {
            _rigid.linearVelocityY = mushroomJumpForce;
        }
        
        if (isWall)
        {
            if (_jumpAction.WasReleasedThisFrame())
            {
                isWallJump = true;
            }
        }
    }

    public void OnInvincibilityMode()
    {
        PlayerManager.Instance.debug_setInvincibilityMode = !PlayerManager.Instance.debug_setInvincibilityMode;
    }
}

using UnityEngine;
using Debug = UnityEngine.Debug; // Debug 충돌 해결

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 inputVec;
    public float speed = 5f;

    [Header("Charge Jump")]
    public float jumpForce = 10f;
    public float maxChargeTime = 1.5f;
    public float maxChargeMultiplier = 2.5f;

    [Header("Direction")]
    public float minVerticalAim = 0.35f;
    public int facingDir = 1;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Effects")]
    public GameObject jumpSlashEffectPrefab;
    public GameObject landingEffectPrefab;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;

    private bool isGround = false;
    private bool wasGrounded = false;
    private bool isChargingJump = false;
    private float jumpChargeTime = 0f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1) Ground Check
        if (groundCheck != null)
        {
            isGround = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }
        else
        {
            isGround = false;
        }

        // 2) 착지 이펙트
        if (!wasGrounded && isGround)
        {
            SpawnLandingEffect();
        }

        wasGrounded = isGround;

        // 3) 입력 받기
        inputVec.x = Input.GetKey(KeyCode.D) ? 1f :
                     Input.GetKey(KeyCode.A) ? -1f : 0f;

        inputVec.y = Input.GetKey(KeyCode.W) ? 1f :
                     Input.GetKey(KeyCode.S) ? -1f : 0f;

        // 방향 저장
        if (inputVec.x > 0)
            facingDir = 1;
        else if (inputVec.x < 0)
            facingDir = -1;

        // 4) 차징
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            isChargingJump = true;
            jumpChargeTime += Time.deltaTime;
            jumpChargeTime = Mathf.Clamp(jumpChargeTime, 0f, maxChargeTime);
        }

        // 5) 점프 실행
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log($"KeyUp 감지! jumpChargeTime: {jumpChargeTime}");

            if (jumpChargeTime > 0f)
            {
                float chargeRatio = jumpChargeTime / maxChargeTime;
                float finalJumpForce = jumpForce * Mathf.Lerp(1f, maxChargeMultiplier, chargeRatio);

                Vector2 jumpDir = GetJumpDirection();

                Debug.Log($"점프 실행! force: {finalJumpForce}, dir: {jumpDir}");

                rigid.linearVelocity = Vector2.zero;
                rigid.AddForce(jumpDir * finalJumpForce, ForceMode2D.Impulse);

                SpawnJumpSlashEffect(jumpDir);
            }

            isChargingJump = false;
            jumpChargeTime = 0f;
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = isChargingJump ? speed * 0.4f : speed;

        rigid.linearVelocity = new Vector2(
            inputVec.x * currentSpeed,
            rigid.linearVelocity.y
        );
    }

    void LateUpdate()
    {
        if (spriter != null && inputVec.x != 0)
            spriter.flipX = inputVec.x < 0;
    }

    Vector2 GetJumpDirection()
    {
        Vector2 dir = inputVec;

        if (dir.sqrMagnitude < 0.001f)
        {
            dir = new Vector2(facingDir * 0.35f, 1f);
        }

        if (dir.y < minVerticalAim)
        {
            dir.y = minVerticalAim;
        }

        dir.Normalize();
        return dir;
    }

    void SpawnJumpSlashEffect(Vector2 dir)
    {
        if (jumpSlashEffectPrefab == null) return;

        GameObject fx = Instantiate(
            jumpSlashEffectPrefab,
            transform.position,
            Quaternion.identity
        );

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        fx.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void SpawnLandingEffect()
    {
        if (landingEffectPrefab == null) return;

        Vector3 spawnPos = groundCheck != null
            ? groundCheck.position
            : transform.position;

        Instantiate(
            landingEffectPrefab,
            spawnPos,
            Quaternion.identity
        );
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
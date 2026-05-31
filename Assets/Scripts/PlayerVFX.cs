using System.Collections;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    private static PlayerVFX instance;
    public static PlayerVFX Instance {  get { return instance; } }

    private PlayerMovement _pMove;

    [Header("Body Particle VFX")]
    [SerializeField] private GameObject bodyParticleVFXPrefab;
    [SerializeField] private Vector3 bodyParticleVFXPosition = Vector3.zero;
    private GameObject bodyParticleVFX;

    [Header("Die VFX")]
    [SerializeField] private GameObject dieVFXPrefab;
    [SerializeField] private Vector3 dieVFXPosition = Vector3.zero;

    [Header("Jump VFX")]
    [SerializeField] private GameObject jumpVFXPrefab;
    [SerializeField] private Vector3 jumpVFXPosition = Vector3.zero;
    private GameObject jumpVFX;

    [Header("Kick VFX")]
    [SerializeField] private GameObject kickVFXPrefab;
    [SerializeField] private Vector3 kickVFXPosition = Vector3.zero;
    private GameObject kickVFX;
    private Transform kickVFX_C;

    [Header("Melt VFX")]
    [SerializeField] private GameObject meltVFXPrefab;
    [SerializeField] private Vector3 meltVFXPosition = Vector3.zero;

    [Header("Mushroom Jump VFX")]
    [SerializeField] private GameObject mushroomJumpVFXPrefab;
    [SerializeField] private Vector3 mushroomJumpVFXPosition = Vector3.zero;
    private GameObject mushroomJumpVFX;

    [Header("Respawn VFX")]
    [SerializeField] private GameObject respawnVFXPrefab;
    [SerializeField] private Vector3 respawnVFXPosition = Vector3.zero;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _pMove = GetComponent<PlayerMovement>();

        bodyParticleVFX = Instantiate(bodyParticleVFXPrefab, (transform.position + bodyParticleVFXPosition), Quaternion.identity, transform);

        mushroomJumpVFX = Instantiate(mushroomJumpVFX, transform);
        mushroomJumpVFX.transform.localPosition = mushroomJumpVFXPosition;
        StartCoroutine(MushroomJumpVFX());
    }

    public void PlayerVFXReset()
    {
        jumpVFX = null;
        jumpVFX = Instantiate(jumpVFXPrefab, (transform.position + jumpVFXPosition), Quaternion.identity);
        jumpVFX.SetActive(false);

        kickVFX = null;
        kickVFX = Instantiate(kickVFXPrefab, (transform.position + kickVFXPosition), Quaternion.identity);
        kickVFX_C = kickVFX.transform.GetChild(1);
        kickVFX.SetActive(false);
    }

    public void DieVFX()
    {
        Instantiate(dieVFXPrefab, (transform.position + dieVFXPosition), Quaternion.identity);
    }

    public void JumpVFX()
    {
        if (jumpVFX.Equals(null))    jumpVFX = Instantiate(jumpVFXPrefab, (transform.position + jumpVFXPosition), Quaternion.identity);
        else
        {
            jumpVFX.transform.position = transform.position + jumpVFXPosition;
            jumpVFX.SetActive(true);
        }
    }

    public void KickVFX()
    {
        if (kickVFX.Equals(null))
        {
            kickVFX = Instantiate(kickVFXPrefab, (transform.position + kickVFXPosition), Quaternion.identity);
            kickVFX_C = kickVFX.transform.GetChild(1);
        }
        else
        {

            kickVFX.transform.position = transform.position + kickVFXPosition;
            kickVFX_C.gameObject.SetActive(true);
        }
    }

    public void MeltVFX()
    {
        Instantiate(meltVFXPrefab, (transform.position + meltVFXPosition), Quaternion.identity);
    }

    private IEnumerator MushroomJumpVFX()
    {
        while (true)
        {
            yield return new WaitUntil(() => (_pMove.IsGround || _pMove.IsWall));
            mushroomJumpVFX.SetActive(false);

            yield return new WaitUntil(() => _pMove.IsMushroom);
            mushroomJumpVFX.SetActive(true);
        }
    }

    public void RespawnVFX()
    {
        Instantiate(respawnVFXPrefab, (transform.position + respawnVFXPosition), Quaternion.identity);
    }
}

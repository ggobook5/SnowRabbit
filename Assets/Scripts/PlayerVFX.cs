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
    private GameObject dieVFX;

    [Header("Jump VFX")]
    [SerializeField] private GameObject jumpVFXPrefab;
    [SerializeField] private Vector3 jumpVFXPosition = Vector3.zero;
    private GameObject jumpVFX;

    [Header("Kick VFX")]
    [SerializeField] private GameObject kickVFXPrefab;
    [SerializeField] private Vector3 kickVFXPosition = Vector3.zero;
    private GameObject kickVFX;

    [Header("Melt VFX")]
    [SerializeField] private GameObject meltVFXPrefab;
    [SerializeField] private Vector3 meltVFXPosition = Vector3.zero;
    private GameObject meltVFX;

    [Header("Mushroom Jump VFX")]
    [SerializeField] private GameObject mushroomJumpVFXPrefab;
    [SerializeField] private Vector3 mushroomJumpVFXPosition = Vector3.zero;
    private GameObject mushroomJumpVFX;

    [Header("Respawn VFX")]
    [SerializeField] private GameObject respawnVFXPrefab;
    [SerializeField] private Vector3 respawnVFXPosition = Vector3.zero;
    private GameObject respawnVFX;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _pMove = GetComponent<PlayerMovement>();

        bodyParticleVFX = Instantiate(bodyParticleVFXPrefab, (transform.position + bodyParticleVFXPosition), Quaternion.identity, transform);

        mushroomJumpVFX = Instantiate(mushroomJumpVFXPrefab, transform);
        mushroomJumpVFX.transform.localPosition = mushroomJumpVFXPosition;
        StartCoroutine(MushroomJumpVFX());
    }

    public void DieVFX()
    {
        dieVFX = ObjectPool.Instance.GetObject(dieVFXPrefab);
        dieVFX.transform.SetPositionAndRotation((transform.position + dieVFXPosition), Quaternion.identity);
    }

    public void JumpVFX()
    {
        jumpVFX = ObjectPool.Instance.GetObject(jumpVFXPrefab);
        jumpVFX.transform.SetPositionAndRotation((transform.position + jumpVFXPosition), Quaternion.identity);
    }

    public void KickVFX()
    {
        kickVFX = ObjectPool.Instance.GetObject(kickVFXPrefab);
        kickVFX.transform.SetPositionAndRotation((transform.position + kickVFXPosition), Quaternion.Euler(0, 0, 90));
        ObjectPool.Instance.ReturnObject(kickVFX, 2f);
    }

    public void MeltVFX()
    {
        meltVFX = ObjectPool.Instance.GetObject(meltVFXPrefab);
        meltVFX.transform.SetPositionAndRotation((transform.position + meltVFXPosition), Quaternion.identity);
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
        respawnVFX = ObjectPool.Instance.GetObject(respawnVFXPrefab);
        respawnVFX.transform.SetPositionAndRotation((transform.position + respawnVFXPosition), Quaternion.identity);
    }
}

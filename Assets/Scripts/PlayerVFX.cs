using System.Collections;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    private PlayerMovement _pMove;
    private SpriteRenderer _render;

    [Header("Body Particle VFX")]
    [SerializeField] private GameObject bodyParticleVFXPrefab;
    [SerializeField] private Vector3 bodyParticleVFXOffset = new Vector3(0, -0.87f, 0);
    private GameObject bodyParticleVFX;

    [Header("Die VFX")]
    [SerializeField] private GameObject dieVFXPrefab;
    [SerializeField] private Vector3 dieVFXOffset = new Vector3(0, -0.2f, 0);
    private GameObject dieVFX;

    [Header("Jump VFX")]
    [SerializeField] private GameObject jumpVFXPrefab;
    [SerializeField] private Vector3 jumpVFXOffset = new Vector3(0, -0.7f, 0);
    private GameObject jumpVFX;

    [Header("Kick VFX")]
    [SerializeField] private GameObject kickVFXPrefab;
    [SerializeField] private Vector3 kickVFXOffset = new Vector3(0, -0.6f, 0);
    private GameObject kickVFX;

    [Header("Melt VFX")]
    [SerializeField] private GameObject meltVFXPrefab;
    [SerializeField] private Vector3 meltVFXOffset = new Vector3(0, -0.82f, 0);
    private GameObject meltVFX;

    [Header("Mushroom Jump VFX")]
    [SerializeField] private GameObject mushroomJumpVFXPrefab;
    [SerializeField] private Vector3 mushroomJumpVFXOffset = new Vector3(0, -0.3f, 0);
    private GameObject mushroomJumpVFX;

    [Header("Respawn VFX")]
    [SerializeField] private GameObject respawnVFXPrefab;
    [SerializeField] private Vector3 respawnVFXOffset = new Vector3(0, -0.2f, 0);
    private GameObject respawnVFX;

    private void Awake()
    {
        _pMove = GetComponent<PlayerMovement>();
        _render = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        bodyParticleVFX = Instantiate(bodyParticleVFXPrefab, (transform.position + bodyParticleVFXOffset), Quaternion.Euler(-90, 0, 0), transform);

        dieVFX = Instantiate(dieVFXPrefab, (transform.position + dieVFXOffset), Quaternion.Euler(0, 0, 90), transform);
        dieVFX.SetActive(false);

        meltVFX = Instantiate(meltVFXPrefab, (transform.position + meltVFXOffset), Quaternion.identity, transform);
        meltVFX.SetActive(false);

        mushroomJumpVFX = Instantiate(mushroomJumpVFXPrefab, (transform.position + mushroomJumpVFXOffset), Quaternion.identity, transform);
        StartCoroutine(MushroomJumpVFX());

        respawnVFX = Instantiate(respawnVFXPrefab, (transform.position + respawnVFXOffset), Quaternion.identity, transform);
        respawnVFX.SetActive(false);
    }

    public void DieVFX()
    {
        dieVFX.SetActive(true);
    }

    public void JumpVFX()
    {
        jumpVFX = ObjectPool.Instance.GetObject(jumpVFXPrefab);
        jumpVFX.transform.SetPositionAndRotation((transform.position + jumpVFXOffset), Quaternion.Euler(0, 0, -1.477f));
    }

    public void KickVFX()
    {
        int flip = (_render.flipX ? -1 : 1);
        kickVFX = ObjectPool.Instance.GetObject(kickVFXPrefab);
        kickVFX.transform.SetPositionAndRotation((transform.position + kickVFXOffset), Quaternion.Euler(0, 0, 90 * flip));
        ObjectPool.Instance.ReturnObject(kickVFX, 2f);
    }

    public void MeltVFX()
    {
        meltVFX.SetActive(true);
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
        respawnVFX.SetActive(true);
    }
}

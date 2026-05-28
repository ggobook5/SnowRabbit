using UnityEngine;

public class SmokeEmitter2D : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] smokePrefabs;

    [Header("Burst Settings")]
    public int burstCount = 40;

    [Header("Spawn Spread")]
    public float spreadX = 0.2f;
    public float spreadY = 0.2f;

    [Header("Force")]
    public float minForce = 0.5f;
    public float maxForce = 2.5f;

    [Header("Random Size")]
    public float minScale = 0.3f;
    public float maxScale = 1.6f;

    [Header("Random Lifetime")]
    public float minLifetime = 1.5f;
    public float maxLifetime = 3.5f;

    [Header("Direction")]
    public float directionAngle = 90f;
    public float angleSpread = 360f;

    [Header("Rotation")]
    public bool randomRotation = true;

    void Start()
    {
        Burst();
    }

    public void Burst()
    {
        // ÇÁ¸®ÆÕ ¾øÀ¸¸é Á¾·á
        if (smokePrefabs == null || smokePrefabs.Length == 0)
            return;

        for (int i = 0; i < burstCount; i++)
        {
            // ·£´ý ÇÁ¸®ÆÕ ¼±ÅÃ
            GameObject prefab =
                smokePrefabs[
                    UnityEngine.Random.Range(0, smokePrefabs.Length)
                ];

            // »ý¼º À§Ä¡ ·£´ý
            Vector3 spawnPos = transform.position;

            spawnPos.x += Random.Range(-spreadX, spreadX);
            spawnPos.y += Random.Range(-spreadY, spreadY);

            // »ý¼º
            GameObject smoke = Instantiate(
                prefab,
                spawnPos,
                Quaternion.identity
            );

            // ·£´ý Å©±â
            float scale =
                UnityEngine.Random.Range(minScale, maxScale);

            smoke.transform.localScale =
                Vector3.one * scale;

            // ·£´ý È¸Àü
            if (randomRotation)
            {
                smoke.transform.rotation =
                    Quaternion.Euler(
                        0,
                        0,
                        UnityEngine.Random.Range(0f, 360f)
                    );
            }

            // ¹æÇâ °è»ê
            float angle =
                directionAngle +
                UnityEngine.Random.Range(
                    -angleSpread * 0.5f,
                    angleSpread * 0.5f
                );

            Vector2 dir =
                new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                ).normalized;

            // ·£´ý Èû
            float force =
                UnityEngine.Random.Range(minForce, maxForce);

            Rigidbody2D rb =
                smoke.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = dir * force;
            }

            // ·£´ý lifetime
            SmokeParticle2D particle =
                smoke.GetComponent<SmokeParticle2D>();

            if (particle != null)
            {
                particle.lifetime =
                    UnityEngine.Random.Range(
                        minLifetime,
                        maxLifetime
                    );
            }
        }
    }
}
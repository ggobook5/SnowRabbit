using UnityEngine;

public class SmokeParticle2D : MonoBehaviour
{
    public float lifetime = 6f;
    public float growSpeed = 0.15f;
    public float noiseForce = 0.05f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    float timer;
    Vector3 startScale;
    float startAlpha;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        startScale = transform.localScale;
        startAlpha = sr.color.a;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float life01 = Mathf.Clamp01(timer / lifetime);

        // 천천히 커짐
        transform.localScale = startScale * (1f + life01 * growSpeed);

        // 마지막 35% 구간부터만 서서히 사라짐
        float fade01 = Mathf.InverseLerp(0.65f, 1f, life01);

        Color c = sr.color;
        c.a = Mathf.Lerp(startAlpha, 0f, fade01);
        sr.color = c;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 noise = new Vector2(
            Mathf.PerlinNoise(Time.time * 0.7f, transform.position.y) - 0.5f,
            Mathf.PerlinNoise(transform.position.x, Time.time * 0.7f) - 0.5f
        );

        rb.AddForce(noise * noiseForce, ForceMode2D.Force);
    }
}
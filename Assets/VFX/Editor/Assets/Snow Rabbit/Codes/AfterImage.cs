using UnityEngine;

public class LaunchSlashEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float lifeTime = 0.12f;
    [SerializeField] private Vector3 startScale = new Vector3(0.7f, 0.7f, 1f);
    [SerializeField] private Vector3 endScale = new Vector3(1.2f, 1.2f, 1f);
    [SerializeField] private float startAlpha = 1f;
    [SerializeField] private float endAlpha = 0f;

    private float timer;
    private Color color;

    private void Awake()
    {
        color = sr.color;
    }

    public void Play(Vector3 pos, Vector2 dir)
    {
        transform.position = pos - (Vector3)(dir.normalized * 0.15f);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.localScale = startScale;

        timer = 0f;
        color.a = startAlpha;
        sr.color = color;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifeTime;

        if (t >= 1f)
        {
            gameObject.SetActive(false);
            return;
        }

        float ease = 1f - Mathf.Pow(1f - t, 3f);
        transform.localScale = Vector3.Lerp(startScale, endScale, ease);

        Color c = sr.color;
        c.a = Mathf.Lerp(startAlpha, endAlpha, ease);
        sr.color = c;
    }
}
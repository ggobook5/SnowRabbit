using System.Collections;
using UnityEngine;

public class UIPanelSwingAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform target;

    [Header("In Animation")]
    [SerializeField] private float inDuration = 0.45f;
    [SerializeField] private float startY = 120f;

    [Header("Out Animation")]
    [SerializeField] private float outDuration = 0.22f;
    [SerializeField] private float exitY = 180f;

    private Vector2 originalPos;
    private Vector3 originalScale;
    private Coroutine currentRoutine;

    private void Awake()
    {
        if (target == null)
            target = GetComponent<RectTransform>();

        originalPos = target.anchoredPosition;
        originalScale = target.localScale;
    }

    public void PlayIn()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(PlayInRoutine());
    }

    public void PlayOut(System.Action onComplete)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(PlayOutRoutine(onComplete));
    }

    private IEnumerator PlayInRoutine()
    {
        float time = 0f;

        Vector2 fromPos = originalPos + new Vector2(0f, startY);
        Vector2 toPos = originalPos;

        while (time < inDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / inDuration);

            float ease = EaseOutBack(t);
            target.anchoredPosition = Vector2.LerpUnclamped(fromPos, toPos, ease);

            float swing = Mathf.Sin(t * Mathf.PI * 4f) * (1f - t) * 6f;
            target.localRotation = Quaternion.Euler(0f, 0f, swing);

            target.localScale = Vector3.LerpUnclamped(originalScale * 0.95f, originalScale, ease);

            yield return null;
        }

        target.anchoredPosition = originalPos;
        target.localRotation = Quaternion.identity;
        target.localScale = originalScale;
    }

    private IEnumerator PlayOutRoutine(System.Action onComplete)
    {
        float time = 0f;

        Vector2 fromPos = target.anchoredPosition;
        Vector2 toPos = originalPos + new Vector2(0f, exitY);

        while (time < outDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / outDuration);

            float ease = EaseInBack(t);
            target.anchoredPosition = Vector2.LerpUnclamped(fromPos, toPos, ease);

            float swing = Mathf.Sin(t * Mathf.PI * 2f) * 4f;
            target.localRotation = Quaternion.Euler(0f, 0f, swing);

            yield return null;
        }

        target.localRotation = Quaternion.identity;
        onComplete?.Invoke();
    }

    private float EaseOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }

    private float EaseInBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return c3 * t * t * t - c1 * t * t;
    }
}
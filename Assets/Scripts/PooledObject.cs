using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private GameObject originalPrefab;
    public GameObject OriginalPrefab
    {
        get { return originalPrefab; }
        set { originalPrefab = value; }
    }

    private Transform originalTransform;

    private void Awake()
    {
        originalTransform = transform;
    }

    private void OnDisable()
    {
        transform.SetLocalPositionAndRotation(originalTransform.localPosition, originalTransform.localRotation);
        transform.localScale = originalTransform.localScale;

        if (gameObject.transform != ObjectPool.Instance.transform)      ObjectPool.Instance.ReturnObject(gameObject);
    }
}

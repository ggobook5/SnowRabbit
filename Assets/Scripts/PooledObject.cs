using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private GameObject originalPrefab;
    public GameObject OriginalPrefab
    {
        get { return originalPrefab; }
        set { if (!originalPrefab.Equals(null))     originalPrefab = value; }
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
    }
}

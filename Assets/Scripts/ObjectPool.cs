using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance {  get { return instance; } }

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary;
    [SerializeField] private int defaultPoolSize = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))    InitializeNewPool(prefab);

        if (poolDictionary[prefab].Count == 0)     CreateObject(prefab);

        GameObject spawnObject = poolDictionary[prefab].Dequeue();
        spawnObject.SetActive(true);

        return spawnObject;
    }

    private void InitializeNewPool(GameObject prefab)
    {
        poolDictionary[prefab] = new Queue<GameObject>();

        for (int i = 0; i < defaultPoolSize; i++) CreateObject(prefab);
    }

    public void InitializeNewPool(GameObject prefab, int poolSize)
    {
        poolDictionary[prefab] = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++) CreateObject(prefab);
    }

    private void CreateObject(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab);

        if (!newObject.TryGetComponent<PooledObject>(out PooledObject pooledComponent))
            pooledComponent = newObject.AddComponent<PooledObject>();
        pooledComponent.OriginalPrefab = prefab;

        newObject.SetActive(false);
        poolDictionary[prefab].Enqueue(newObject);
    }

    public void ReturnObject(GameObject returnObject, float delay = 0.001f)
    {
        StartCoroutine(WaitReturn(returnObject, delay));
    }

    private IEnumerator WaitReturn(GameObject _returnObject, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        ReturnPool(_returnObject);

        yield break;
    }

    private void ReturnPool(GameObject returnObject)
    {
        if (returnObject.TryGetComponent<PooledObject>(out PooledObject pooledComponent).Equals(null))   return;
        GameObject originalPrefab = pooledComponent.OriginalPrefab;

        returnObject.SetActive(false);
        returnObject.transform.parent = transform;

        poolDictionary[originalPrefab].Enqueue(returnObject);
    }

    public void ClearPool(GameObject prefab)
    {
        if (poolDictionary.ContainsKey(prefab))
        {
            while (poolDictionary[prefab].Count > 0)
            {
                DestroyImmediate(poolDictionary[prefab].Dequeue());
            }
            poolDictionary.Remove(prefab);
        }
    }
}

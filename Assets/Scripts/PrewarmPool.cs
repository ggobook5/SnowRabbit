using System;
using UnityEngine;

[System.Serializable]
public class PoolInfo
{
    public GameObject prefab;
    public int poolSize;
}

public class PrewarmPool : MonoBehaviour
{
    [SerializeField] private PoolInfo[] playerVFXPools;

    private void Start()
    {
        foreach(PoolInfo poolInfo in playerVFXPools)
        {
            ObjectPool.Instance.InitializeNewPool(poolInfo.prefab, poolInfo.poolSize);
        }
    }
}

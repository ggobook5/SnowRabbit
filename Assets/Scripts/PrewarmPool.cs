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

    [Serializable]
    public class PoolsIndex
    {
        
    }

    private void Prewarm(PoolInfo[] createPool)
    {
        foreach(PoolInfo poolInfo in createPool)
        {
            ObjectPool.Instance.InitializeNewPool(poolInfo.prefab, poolInfo.poolSize);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Root.Pool
{
    public class PoolManager : MonoBehaviour,IPoolManager
    {
        [SerializeField] private List<PoolUse> pools;

        public PoolUse GetPool(PoolEnum poolEnum)
        {
            foreach (var pool in pools)
            {
                if (pool.poolEnum == poolEnum)
                {
                    return pool;
                }
            }

            return null;
        }
    }
}
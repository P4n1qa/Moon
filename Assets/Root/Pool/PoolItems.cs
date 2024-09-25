using Root.Pool.PoolBase;
using UnityEngine;

namespace Root.Pool
{
    public class PoolItems : PoolUse
    {
        [SerializeField] private PoolObjectBase _poolObjectBase;
        
        private PoolMono<PoolObjectBase> _pool;

        public PoolObjectBase CreateObject(Vector3 spawnPosition)
        {
            var poolObjectBase = _pool.GetFreeElement();
            poolObjectBase.InitializeSystems(transform,spawnPosition,poolEnum);
            return poolObjectBase;
        }
        
        protected override void CreatePool()
        {
            _pool = new PoolMono<PoolObjectBase>(_poolObjectBase, PoolCount, transform)
            {
                AutoExpand = AutoExpand
            };
        }
    }
}
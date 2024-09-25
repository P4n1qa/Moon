using UnityEngine;

namespace Root.Pool.PoolBase
{
    public abstract class PoolObjectBase : MonoBehaviour
    {
        protected Transform poolParentTransform;

        public virtual void InitializeSystems(Transform poolTransform, Vector3 spawnPosition, PoolEnum poolType)
        {
            poolParentTransform = poolTransform;
        }

        protected void ReturnToParentPool(Transform poolTransform)
        {
            transform.SetParent(poolTransform,true);
        }
    }
}
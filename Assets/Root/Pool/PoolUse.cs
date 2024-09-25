using UnityEngine;

namespace Root.Pool
{
    public abstract class PoolUse : MonoBehaviour
    {
        public PoolEnum poolEnum;
        public int PoolCount;
        public bool AutoExpand;

        private void Awake()
        {
            CreatePool();
        }

        protected abstract void CreatePool();
    }
}
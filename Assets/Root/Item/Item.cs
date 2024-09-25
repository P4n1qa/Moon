using Root.Pool;
using Root.Pool.PoolBase;
using UnityEngine;

namespace Root.Item
{
    public class Item : PoolObjectBase,IItem
    {
        [SerializeField] private ItemAnimation.ItemAnimation itemAnimation;
        public PoolEnum ItemType { get; private set; }

        public override void InitializeSystems(Transform poolTransform,Vector3 spawnPosition,PoolEnum poolType)
        {
            base.InitializeSystems(poolTransform, spawnPosition,poolType);
            transform.position = spawnPosition;
            ItemType = poolType;
        }
        
        public void ItemDropped(Vector3 endPositionForAnimation)
        {
            ReturnToParentPool(poolParentTransform);
            itemAnimation.AnimationDropObject(endPositionForAnimation);
        }

        public void ChangeParentForPlayerCollectItem(Transform playerTransform)
        {
            transform.SetParent(playerTransform);
        }
        
        public void ItemTacked(Vector3 endPositionForAnimation)
        {
            itemAnimation.AnimationStackItem(endPositionForAnimation);
        }
    }
}
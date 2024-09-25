using Root.Pool;
using UnityEngine;

namespace Root.Item
{
    public interface IItem
    {
        public PoolEnum ItemType { get; }

        public void ItemDropped(Vector3 endPositionForAnimation);
        public void ItemTacked(Vector3 endPositionForAnimation);
        public void ChangeParentForPlayerCollectItem(Transform playerTransform);
    }
}
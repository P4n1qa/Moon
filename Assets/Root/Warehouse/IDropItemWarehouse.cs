using System;
using Root.Item;
using Root.Item.ItemData;
using Root.Pool;

namespace Root.Warehouse
{
    public interface IDropItemWarehouse
    {
        public event Action ItemDropped;
        public bool CanDropItem(IItem item);
        public void DropItemToWarehouse(IItem item);
        public void TakeResources();
        public bool HaveResourcesForItem(ItemData itemData, PoolEnum itemType);
    }
}
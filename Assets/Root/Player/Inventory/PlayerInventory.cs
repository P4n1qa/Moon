using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Root.Item;
using Root.Warehouse;
using UnityEngine;

namespace Root.Player.Inventory
{
    public class PlayerInventory : MonoBehaviour,IPlayerInventory
    {
        [SerializeField] private int maxInventorySize;
        [SerializeField] private float itemHeightOffset;
        [SerializeField] private float pickupDelay;
        [SerializeField] private Transform stackStartPosition;
        
        public bool IsPickingItem { get; private set; }
        
        private List<IItem> _items = new();
        private Vector3 _stackPosition;
        
        public void Initialize()
        {
            _stackPosition = stackStartPosition.position;
            IsPickingItem = false;
        }

        public async void StackItem(IWarehouseReadyProducts warehouse)
        {
            if (IsPickingItem) return;
            if (_items.Count >= maxInventorySize)return;
            
            var item = warehouse.FindItem();
            
            if (item == null)return;
            
            IsPickingItem = true;
            
            item.ChangeParentForPlayerCollectItem(transform);
            warehouse.TakeItemFromCell(_stackPosition);
            _items.Add(item);
            UpdateStackPositionItemAdd();
            
            await UniTask.Delay((int)(pickupDelay * 1000));
            IsPickingItem = false;
        }

        public async void DropItem(IDropItemWarehouse warehouse)
        {
            if (IsPickingItem) return;
            if (_items.Count <= 0)return;
            
            var item = FindItemFromEnd(warehouse);
            if (item == null)return;
            
            IsPickingItem = true;
            warehouse.DropItemToWarehouse(item);
            _items.Remove(item);
            UpdateStackPositionItemLeft();
            
            await UniTask.Delay((int)(pickupDelay * 1000));
            IsPickingItem = false;
        }
        
        private IItem FindItemFromEnd(IDropItemWarehouse warehouse)
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                if (warehouse.CanDropItem(_items[i]))
                {
                    return _items[i];
                }
            }

            return null;
        }
        
        private void UpdateStackPositionItemAdd()
        {
            _stackPosition += Vector3.up * itemHeightOffset;
        }

        private void UpdateStackPositionItemLeft()
        {
            _stackPosition -= Vector3.up * itemHeightOffset;
        }
    }
}

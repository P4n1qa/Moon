using Root.Item.ItemData;
using Root.Pool;
using Root.Warehouse.WarehouseUI;
using UnityEngine;

namespace Root.Warehouse.Factory
{
    public class Factory : MonoBehaviour,IFactory
    {
        [SerializeField] private WarehouseReadyProducts warehouseReadyProducts;
        [SerializeField] private WarehouseNotReadyProducts warehouseNotReadyProducts;
        [SerializeField] private WarehouseUI.WarehouseUI warehouseUI;
        [SerializeField] private ItemData itemData;
        [SerializeField] private PoolEnum itemType;
        
        public ItemData ItemData => itemData;

        public PoolEnum ItemType => itemType;
        public IDropItemWarehouse DropItemWarehouse => warehouseNotReadyProducts;
        public IWarehouseUI WarehouseUI => warehouseUI;
        
        public void Initialize()
        {
            if (warehouseNotReadyProducts != null)
            {
                warehouseNotReadyProducts.Initialize(this);
            }
            
            warehouseReadyProducts.Initialize(this);
        }
    }
}
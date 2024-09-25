using Root.Warehouse;

namespace Root.Player.Inventory
{
    public interface IPlayerInventory
    {
        public bool IsPickingItem{get;}
        public void Initialize();

        public void StackItem(IWarehouseReadyProducts warehouse);
        public void DropItem(IDropItemWarehouse warehouse);
    }
}
using Root.Item.ItemData;
using Root.Pool;
using Root.Warehouse.WarehouseUI;

namespace Root.Warehouse.Factory
{
    public interface IFactory
    {
        ItemData ItemData { get; }
        PoolEnum ItemType { get; }
        IDropItemWarehouse DropItemWarehouse { get; }
        IWarehouseUI WarehouseUI { get; }
    }
}
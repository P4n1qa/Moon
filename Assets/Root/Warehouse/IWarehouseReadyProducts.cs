using Root.Item;
using UnityEngine;

namespace Root.Warehouse
{
    public interface IWarehouseReadyProducts
    {
        public IItem FindItem();
        public IItem TakeItemFromCell(Vector3 position);
    }
}
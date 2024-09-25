using System;
using Root.Item;
using UnityEngine;

namespace Root.Warehouse.WarehouseBase
{
    [Serializable]
    public class Cell
    {
        public Vector3 Position;
        public bool IsFull;
        public IItem Item;

        public IItem ReturnCellItem()
        {
            return Item;
        }
        
        public void FillCell(IItem item)
        {
            Item = item;
            Item.ItemDropped(Position);
            IsFull = true;
        }
        
        public IItem FreeCell(Vector3 position)
        {
            var item = Item;
            item.ItemTacked(position);
            Item = null;
            IsFull = false;
            return item;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Root.Item;
using Root.Item.ItemData;
using Root.Pool;
using Root.Warehouse.WarehouseBase;
using UnityEngine;

namespace Root.Warehouse
{
    public class WarehouseNotReadyProducts : WarehouseBase.WarehouseBase, IDropItemWarehouse
    {
        [SerializeField] private List<PoolEnum> itemsCanTake;
        
        public event Action ItemDropped;
        
        private ItemData _itemData;

        public bool CanDropItem(IItem item)
        {
            return !IsWarehouseFull() && IsItemFitsWarehouse(item);
        }
        
        public void DropItemToWarehouse(IItem item)
        {
            Cell cell = GiveFreeCell();
            if (cell != null)
            {
                cell.FillCell(item);
                ItemDropped?.Invoke();
            }
        }

        public void TakeResources()
        {
            List<Cell> cells = FindCellsWithItems(factory.ItemData, factory.ItemType);
            if (cells != null)
            {
                foreach (var cell in cells)
                {
                    cell.FreeCell(houseSpawnPosition.position);
                }
            }
        }

        public bool HaveResourcesForItem(ItemData itemData, PoolEnum itemType)
        {
            var itemCharacteristics = FindItemCharacteristics(itemData, itemType);
            return itemCharacteristics != null && AreResourcesAvailable(itemCharacteristics);
        }
        
        private bool IsWarehouseFull()
        {
            return GiveFreeCell() == null;
        }

        private bool IsItemFitsWarehouse(IItem item)
        {
            return itemsCanTake.Contains(item.ItemType);
        }
        
        private Cell GiveFreeCell()
        {
            return cells.FirstOrDefault(cell => !cell.IsFull);
        }
        
        private ItemCharacteristics FindItemCharacteristics(ItemData itemData, PoolEnum itemType)
        {
            var itemCharacteristics = itemData.Items.FirstOrDefault(data => data.ItemType == itemType);
            return itemCharacteristics;
        }

        private bool AreResourcesAvailable(ItemCharacteristics itemCharacteristics)
        {
            foreach (var neededItem in itemCharacteristics.ItemNeedToCreate)
            {
                if (!cells.Any(cell => cell.Item != null && cell.Item.ItemType == neededItem))
                {
                    return false;
                }
            }
            return true;
        }

        private List<Cell> FindCellsWithItems(ItemData itemData, PoolEnum itemType)
        {
            var itemCharacteristics = FindItemCharacteristics(itemData, itemType);
            if (itemCharacteristics == null || !AreResourcesAvailable(itemCharacteristics))
            {
                return null;
            }

            HashSet<PoolEnum> foundItemTypes = new HashSet<PoolEnum>();
            List<Cell> resultCells = new List<Cell>();

            foreach (var cell in cells)
            {
                if (cell.Item != null && itemCharacteristics.ItemNeedToCreate.Contains(cell.Item.ItemType))
                {
                    if (!foundItemTypes.Contains(cell.Item.ItemType))
                    {
                        resultCells.Add(cell);
                        foundItemTypes.Add(cell.Item.ItemType);
                    }
                }
            }

            return resultCells;
        }
    }
}

using System.Threading;
using Cysharp.Threading.Tasks;
using Root.Item;
using Root.Pool;
using Root.Warehouse.Factory;
using Root.Warehouse.WarehouseBase;
using Root.Warehouse.WarehouseUI;
using UnityEngine;

namespace Root.Warehouse
{
    public class WarehouseReadyProducts : WarehouseBase.WarehouseBase,IWarehouseReadyProducts
    {
        [SerializeField] private int spawnInterval;
        
        private PoolItems _poolItems;
        private CancellationTokenSource _cancellationTokenSource;
        
        private bool _isSpawning;
        

        public override void Initialize(IFactory factory)
        {
            base.Initialize(factory);
            _poolItems = Root.Root.Instance.PoolManager.GetPool(factory.ItemType) as PoolItems;
            Subscribe();
            StartSpawn();
        }

        public IItem FindItem()
        {
            if (!IsWarehouseHasItem())return null;
            var cell = GiveFullCell();
            return cell.ReturnCellItem();
        }
        
        public IItem TakeItemFromCell(Vector3 position)
        {
            var cell = GiveFullCell();
            StartSpawn();
            return cell.FreeCell(position);
        }
        
        private bool IsWarehouseHasItem()
        {
            var cell = GiveFullCell();
            return cell != null;
        }
        
        private Cell GiveFullCell()
        {
            foreach (var cell in cells)
            {
                if (!cell.IsFull) continue;
                return cell;
            }
            return null;
        }
        
        private void Subscribe()
        {
            if (factory.DropItemWarehouse != null)
            {
                factory.DropItemWarehouse.ItemDropped += StartSpawn;
            }
        }
        private void StartSpawn()
        {
            if (_isSpawning) return;
            
            factory.WarehouseUI.HideUI();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            SpawnObjectsUntilFull(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask SpawnObjectsUntilFull(CancellationToken cancellationToken)
        {
            _isSpawning = true; 

            while (!cancellationToken.IsCancellationRequested)
            {
                CheckFreeCells();

                if (emptyCells.Count == 0) 
                {
                    factory.WarehouseUI.ShowUI(WarehouseUIEnum.WarehouseFull);
                    StopSpawning(); 
                    return;
                }

                for (int i = 0; i < emptyCells.Count; i++)
                {
                    if (cancellationToken.IsCancellationRequested) return;

                    if (factory.DropItemWarehouse != null)
                    {
                        IDropItemWarehouse droppedFactory = factory.DropItemWarehouse;           
                        if (droppedFactory.HaveResourcesForItem(factory.ItemData,factory.ItemType))
                        {
                            droppedFactory.TakeResources();
                            
                            await UniTask.Delay(spawnInterval * 1000, cancellationToken: cancellationToken);
                            emptyCells[i].FillCell(_poolItems.CreateObject(houseSpawnPosition.position) as IItem);
                        }
                        else
                        {
                            factory.WarehouseUI.ShowUI(WarehouseUIEnum.WarehouseDontHaveResources);
                            StopSpawning();
                        }
                    }
                    else
                    {
                        await UniTask.Delay(spawnInterval * 1000, cancellationToken: cancellationToken);
                        emptyCells[i].FillCell(_poolItems.CreateObject(houseSpawnPosition.position) as IItem);
                    }
                    
                }
            }
        }
        
        private void CheckFreeCells()
        {
            emptyCells.Clear();
            
            foreach (var cell in cells)
            {
                if (!cell.IsFull)
                {
                    emptyCells.Add(cell);
                }
            }
        }

        private void StopSpawning()
        {
            _isSpawning = false; 
            _cancellationTokenSource?.Cancel(); 
        }

        private void UnSubscribe()
        {
            if (factory.DropItemWarehouse != null)
            {
                factory.DropItemWarehouse.ItemDropped -= StartSpawn;
            }
        }
        
        private void OnDestroy()
        {
            UnSubscribe();
            StopSpawning(); 
            _cancellationTokenSource?.Dispose();
        }
    }
}

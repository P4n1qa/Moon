using Root.Player.Inventory;
using Root.Warehouse;
using UnityEngine;

namespace Root.Player.Body
{
    public class PlayerBody: MonoBehaviour
    {
        private IPlayerInventory _playerInventory;
        public void Initialize(IPlayerController playerController)
        {
            _playerInventory = playerController.PlayerInventory;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<IWarehouseReadyProducts>() != null)
            {
                if (_playerInventory.IsPickingItem)return;
                var warehouse = other.gameObject.GetComponent<IWarehouseReadyProducts>();
                _playerInventory.StackItem(warehouse);
            }

            if (other.gameObject.GetComponent<IDropItemWarehouse>() != null)
            {
                if (_playerInventory.IsPickingItem)return;
                var warehouse = other.gameObject.GetComponent<IDropItemWarehouse>();
                _playerInventory.DropItem(warehouse);
            }
        }
    }
}
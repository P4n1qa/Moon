using System;
using TMPro;
using UnityEngine;

namespace Root.Warehouse.WarehouseUI
{
    public class WarehouseUI : MonoBehaviour,IWarehouseUI
    {
        [SerializeField] private GameObject _warehouseUICanvas;
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        public void ShowUI(WarehouseUIEnum warehouseUI)
        {
            ChangeText(warehouseUI);
            _warehouseUICanvas.SetActive(true);
        }

        public void HideUI()
        {
            _warehouseUICanvas.SetActive(false);
        }

        private void ChangeText(WarehouseUIEnum warehouseUI)
        {
            _textMeshPro.text = warehouseUI switch
            {
                WarehouseUIEnum.WarehouseFull => "WarehouseFull",
                WarehouseUIEnum.WarehouseDontHaveResources => "WarehouseDontHaveResources",
                _ => throw new ArgumentOutOfRangeException(nameof(warehouseUI), warehouseUI, null)
            };
        }
    }
}
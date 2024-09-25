using System.Collections.Generic;
using UnityEngine;

namespace Root.Item.ItemData
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private List<ItemCharacteristics> items;
        
        public IReadOnlyList<ItemCharacteristics> Items => items;
    }
}
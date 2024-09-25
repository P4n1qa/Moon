using System;
using System.Collections.Generic;
using Root.Pool;

namespace Root.Item.ItemData
{
    [Serializable]
    public class ItemCharacteristics
    {
        public PoolEnum ItemType;
        public List<PoolEnum> ItemNeedToCreate;
    }
}
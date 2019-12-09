using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Items
{
    public class ItemCollection : DiscMappedCollection<ItemObject>, IItemCollection
    {
        public const long ItemCollectionOffset = 0x6ADAC;
        public const int ItemCount = 255;
        public Dictionary<int, ItemObject> ItemData { get; }
        public IReadOnlyList<ItemObject> Items => MappedObjectReadOnly;
        public ItemCollection() : base(ItemCollectionOffset, ItemObject.ObjectSize, ItemObject.ObjectSize, ItemCount)
        {
        }
        internal void SetItemName(int itemId, string itemName)
        {
            MappedObjectData[itemId].Name = itemName;
        }
        public void SetItemNames()
        {
            var itemNames = Properties.Resources.ItemNames.Split('\n');
            for (var i = 0; i < itemNames.Length; i++)
            {
                MappedObjectData[i].Name = itemNames[i].Replace("\r", "").Trim();
            }
        }
    }
}

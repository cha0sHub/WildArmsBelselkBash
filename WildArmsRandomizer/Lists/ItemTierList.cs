using System;
using System.Collections.Generic;

namespace WildArmsRandomizer.Lists
{
    public class ItemTierList : IItemTierList
    {
        public List<List<int>> TieredItemList { get; }
        private Dictionary<int, int> ItemToTier { get; }
        public ItemTierList()
        {
            TieredItemList = new List<List<int>>();
            ItemToTier = new Dictionary<int, int>();
        }
        public void LoadData(string tierListData)
        {
            string[] tierDataSplit = tierListData.Split('\n');
            var currentList = new List<int>();
            foreach (var data in tierDataSplit)
            {
                if (string.IsNullOrEmpty(data))
                    continue;
                var newData = data.Trim();
                newData = newData.Replace("\r", "");
                if (newData[0].Equals('['))
                {
                    if (currentList.Count > 0)
                        TieredItemList.Add(currentList);
                    currentList = new List<int>();
                }
                else
                {
                    var dashIndex = newData.IndexOf('-');
                    var itemId = Convert.ToInt32(newData.Substring(0, dashIndex));
                    currentList.Add(itemId);
                    ItemToTier.Add(itemId, TieredItemList.Count);
                }
            }
            if (currentList.Count > 0)
                TieredItemList.Add(currentList);
        }
        public int GetItemTier(int itemId)
        {
            if (!ItemToTier.ContainsKey(itemId))
                return -1;
            return ItemToTier[itemId];
        }
    }
}

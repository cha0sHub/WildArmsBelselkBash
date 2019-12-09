using System.Collections.Generic;

namespace WildArmsRandomizer.Lists
{
    public interface IItemTierList
    {
        List<List<int>> TieredItemList { get; }

        int GetItemTier(int itemId);
        void LoadData(string tierListData);
    }
}
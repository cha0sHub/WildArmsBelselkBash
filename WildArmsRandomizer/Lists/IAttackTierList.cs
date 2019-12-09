using System.Collections.Generic;

namespace WildArmsRandomizer.Lists
{
    internal interface IAttackTierList
    {
        List<List<int>> TieredAtpIds { get; }
        List<List<int>> TieredEffIds { get; }
        List<List<int>> TieredSorIds { get; }

        byte GetAttackTier(int attackId);
        string GetAttackType(int attackId);
        List<int> GetTierList(string type, int tier);
        void LoadData(string tierListData);
    }
}
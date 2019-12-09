using System.Collections.Generic;

namespace WildArmsRandomizer.Lists
{
    internal interface IBossOrbTierList
    {
        Dictionary<int, string> IdToAreaName { get; }
        List<List<int>> TieredBossIds { get; }

        void LoadData(string tierListData);
    }
}
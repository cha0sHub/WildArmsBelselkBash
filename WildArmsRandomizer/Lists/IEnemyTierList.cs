using System.Collections.Generic;

namespace WildArmsRandomizer.Lists
{
    internal interface IEnemyTierList
    {
        List<int> BossIds { get; }
        List<List<int>> TieredBossIds { get; }
        List<double> TierScales { get; }

        byte GetBossTier(int bossId);
        void LoadData(string tierListData);
        void SwapTiers(int oldBossId, int newBossId);
    }
}
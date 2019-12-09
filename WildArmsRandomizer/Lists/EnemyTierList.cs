using System;
using System.Collections.Generic;

namespace WildArmsRandomizer.Lists
{
    internal class EnemyTierList : IEnemyTierList
    {
        public List<double> TierScales { get; }
        public List<List<int>> TieredBossIds { get; }
        public List<int> BossIds { get; }
        private Dictionary<int, int> BossToTier { get; }

        public EnemyTierList()
        {
            TierScales = new List<double>();
            TieredBossIds = new List<List<int>>();
            BossToTier = new Dictionary<int, int>();
            BossIds = new List<int>();
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
                    var commaIndex = newData.IndexOf(',');
                    var scaleData = newData.Substring(commaIndex + 1, newData.Length - commaIndex - 2);
                    var scale = Convert.ToDouble(scaleData);
                    TierScales.Add(scale);
                    if (currentList.Count > 0)
                        TieredBossIds.Add(currentList);
                    currentList = new List<int>();
                }
                else
                {
                    var dashIndex = newData.IndexOf('-');
                    var bossId = Convert.ToInt32(newData.Substring(0, dashIndex));
                    currentList.Add(bossId);
                    BossToTier.Add(bossId, TieredBossIds.Count);
                    BossIds.Add(bossId);
                }
            }
            if (currentList.Count > 0)
                TieredBossIds.Add(currentList);
        }
        public byte GetBossTier(int bossId)
        {
            if (BossToTier.ContainsKey(bossId))
                return (byte)BossToTier[bossId];
            return 255;
        }

        public void SwapTiers(int oldBossId, int newBossId)
        {
            var oldTier = GetBossTier(oldBossId);
            var newTier = GetBossTier(newBossId);
            BossToTier.Remove(oldBossId);
            BossToTier.Add(oldBossId, newTier);
            BossToTier.Remove(newBossId);
            BossToTier.Add(newBossId, oldTier);
            TieredBossIds[oldTier].Remove(oldBossId);
            TieredBossIds[oldTier].Add(newBossId);
            TieredBossIds[newTier].Remove(newBossId);
            TieredBossIds[newTier].Add(oldBossId);
        }
    }
}

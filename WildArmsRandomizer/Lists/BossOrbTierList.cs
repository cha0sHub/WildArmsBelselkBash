using System;
using System.Collections.Generic;

namespace WildArmsRandomizer.Lists
{
    internal class BossOrbTierList : IBossOrbTierList
    {
        public List<List<int>> TieredBossIds { get; }
        public Dictionary<int, string> IdToAreaName { get; }

        public BossOrbTierList()
        {
            TieredBossIds = new List<List<int>>();
            IdToAreaName = new Dictionary<int, string>();
        }

        public void LoadData(string tierListData)
        {
            var tierDataSplit = tierListData.Split('\n');

            var currentList = new List<int>();

            foreach (var data in tierDataSplit)
            {
                if (string.IsNullOrEmpty(data))
                {
                    continue;
                }
                var newData = data.Trim();
                newData = newData.Replace("\r", "");
                if (newData[0].Equals('['))
                {
                    if (currentList.Count > 0)
                    {
                        TieredBossIds.Add(currentList);
                    }
                }
                else
                {
                    var dashIndex = newData.IndexOf('-');
                    var bossId = Convert.ToInt32(newData.Substring(0, dashIndex));
                    var areaName = newData.Substring(dashIndex + 1, newData.Length - (dashIndex + 1));
                    IdToAreaName.Add(bossId, areaName);
                    currentList.Add(bossId);
                }
            }
            if (currentList.Count > 0)
            {
                TieredBossIds.Add(currentList);
            }
        }
    }
}

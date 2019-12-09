using System;
using System.Collections.Generic;

namespace WildArmsRandomizer.Lists
{
    internal class AttackTierList : IAttackTierList
    {
        private const string AtpType = "|ATP|";
        private const string SorType = "|SOR|";
        private const string EffType = "|EFF|";
        public List<List<int>> TieredAtpIds { get; }
        public List<List<int>> TieredSorIds { get; }
        public List<List<int>> TieredEffIds { get; }
        private Dictionary<int, string> AttackToType { get; }
        private Dictionary<int, int> AttackToTier { get; }
        public AttackTierList()
        {
            TieredAtpIds = new List<List<int>>();
            TieredSorIds = new List<List<int>>();
            TieredEffIds = new List<List<int>>();
            AttackToType = new Dictionary<int, string>();
            AttackToTier = new Dictionary<int, int>();
        }
        public void LoadData(string tierListData)
        {
            string[] tierDataSplit = tierListData.Split('\n');
            var currentList = new List<int>();
            var currentType = "";
            List<List<int>> currentTypeList = null;
            foreach (var data in tierDataSplit)
            {

                if (string.IsNullOrEmpty(data))
                    continue;
                var newData = data.Trim();
                newData = newData.Replace("\r", "");
                if (newData[0].Equals('['))
                {
                }
                else if (newData[0].Equals('|'))
                {
                    if (currentTypeList != null)
                        currentTypeList.Add(currentList);
                    if (newData.Equals("|ATP|"))
                        currentTypeList = TieredAtpIds;
                    if (newData.Equals("|SOR|"))
                        currentTypeList = TieredSorIds;
                    if (newData.Equals("|EFF|"))
                        currentTypeList = TieredEffIds;
                    currentType = newData;
                    currentList = new List<int>();
                }
                else
                {
                    var dashIndex = newData.IndexOf('-');
                    var attackId = Convert.ToInt32(newData.Substring(0, dashIndex));
                    currentList.Add(attackId);
                    AttackToType.Add(attackId, currentType);
                    AttackToTier.Add(attackId, currentTypeList.Count);
                }
            }
            if (currentTypeList != null)
                currentTypeList.Add(currentList);
        }
        public byte GetAttackTier(int attackId)
        {
            if (!AttackToTier.ContainsKey(attackId))
                return 255;
            return (byte)AttackToTier[attackId];
        }
        public string GetAttackType(int attackId)
        {
            if (!AttackToType.ContainsKey(attackId))
                return string.Empty;
            return AttackToType[attackId];
        }

        public List<int> GetTierList(string type, int tier)
        {
            switch (type)
            {
                case AtpType:
                    return TieredAtpIds[tier];
                case SorType:
                    return TieredSorIds[tier];
                case EffType:
                    return TieredEffIds[tier];
            }
            return null;
        }
    }
}

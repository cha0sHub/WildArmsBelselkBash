using WildArmsModel.Model.Items;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Lists;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class ItemRandomizer : IItemRandomizer
    {

        private IRandomizerAgent Agent { get; }
        private IItemCollection ItemCollection { get; }
        private IItemTierList ItemTierList { get; }

        public ItemRandomizer(IRandomizerAgent agent, IItemCollection itemCollection, IItemTierList itemTierList)
        {
            Agent = agent;
            ItemCollection = itemCollection;
            ItemTierList = itemTierList;
        }

        public void RandomizeEquipment()
        {
            foreach (var item in ItemCollection.MappedObjectReadOnly)
            {
                RandomizeEquipmentObject(item);
            }
            ItemCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void RandomizeEquipmentObject(ItemObject item)
        {
            if (item.EquipSlot == ItemMasks.EquipSlot.None)
                return;
            RandomizeUsers(item);
            RandomizeEquipmentStats(item);
            //don't randomize rune slots, may change
            if (item.EquipSlot == ItemMasks.EquipSlot.Rune)
                return;
            RandomizeEquipmentSlot(item);
        }

        private void RandomizeUsers(ItemObject item)
        {
            var randUsers = Agent.Rng.NextDouble();
            if (randUsers > Agent.Probabilities.RandomizeItemUsageRate)
                return;
            var randUser = Agent.Rng.Next(0, 7);
            switch (randUser)
            {
                case 0:
                    item.Usage = ItemMasks.UsageMask.EquipAsRudy;
                    break;
                case 1:
                    item.Usage = ItemMasks.UsageMask.EquipAsJack;
                    break;
                case 2:
                    item.Usage = ItemMasks.UsageMask.EquipAsCecilia;
                    break;
                case 3:
                    item.Usage = ItemMasks.UsageMask.EquipAsRudy | ItemMasks.UsageMask.EquipAsJack;
                    break;
                case 4:
                    item.Usage = ItemMasks.UsageMask.EquipAsRudy | ItemMasks.UsageMask.EquipAsCecilia;
                    break;
                case 5:
                    item.Usage = ItemMasks.UsageMask.EquipAsJack | ItemMasks.UsageMask.EquipAsCecilia;
                    break;
                case 6:
                default:
                    item.Usage = ItemMasks.UsageMask.EquipAsRudy | ItemMasks.UsageMask.EquipAsJack | ItemMasks.UsageMask.EquipAsCecilia;
                    break;
            }
        }

        //For now only assign one potential slot
        private void RandomizeEquipmentSlot(ItemObject item)
        {
            var mutateSlot = Agent.Rng.NextDouble();
            if (mutateSlot > Agent.Probabilities.RandomizeItemSlotRate)
                return;
            var randSlot = Agent.Rng.Next(0, 4);
            switch (randSlot)
            {
                case 0:
                    item.EquipSlot = ItemMasks.EquipSlot.Head;
                    break;
                case 1:
                    item.EquipSlot = ItemMasks.EquipSlot.Chest;
                    break;
                case 2:
                    item.EquipSlot = ItemMasks.EquipSlot.Accessory;
                    break;
                case 3:
                default:
                    item.EquipSlot = ItemMasks.EquipSlot.Weapon;
                    break;
            }
        }

        private void RandomizeEquipmentStats(ItemObject item)
        {
            //this function is pretty dumb, should be improved
            var if0ThenRoll = Agent.Rng.NextDouble();
            item.HpMpIncrease = (item.HpMpIncrease > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianByte(Agent.Rng, item.HpMpIncrease, item.HpMpIncrease / 4 + 5, 99) : item.HpMpIncrease;
            if0ThenRoll = Agent.Rng.NextDouble();
            item.Strength = (item.Strength > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Strength, item.Strength / 4 + 5, 999) : item.Strength;
            if0ThenRoll = Agent.Rng.NextDouble();
            item.Vitality = (item.Vitality > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Vitality, item.Vitality / 4 + 5, 999) : item.Vitality;
            if0ThenRoll = Agent.Rng.NextDouble();
            item.Sorcery = (item.Sorcery > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Sorcery, item.Sorcery / 4 + 5, 999) : item.Sorcery;
            if0ThenRoll = Agent.Rng.NextDouble();
            item.Agility = (item.Agility > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Agility, item.Agility / 4 + 5, 999) : item.Agility;

            if0ThenRoll = Agent.Rng.NextDouble();
            item.Atp = (item.Atp > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Atp, item.Atp / 4 + 5, 999) : item.Atp;
            if0ThenRoll = Agent.Rng.NextDouble();
            item.Dfp = (item.Dfp > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Dfp, item.Dfp / 4 + 5, 999) : item.Dfp;
            if0ThenRoll = Agent.Rng.NextDouble();
            item.Mgr = (item.Mgr > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Mgr, item.Mgr / 4 + 5, 999) : item.Mgr;
            if0ThenRoll = Agent.Rng.NextDouble();
            item.Pry = (item.Pry > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Pry, item.Pry / 4 + 5, 99) : item.Pry;
            if0ThenRoll = Agent.Rng.NextDouble();
            item.Price = (item.Price > 0 || if0ThenRoll < 0.2) ? RandomFunctions.GenerateGaussianShort(Agent.Rng, item.Price, item.Price / 4 + 5, 50000) : item.Price;
        }

        public byte GetMutatedItemId(byte oldItemId, double mutationRate, double standardDeviation)
        {
            var isItemMutated = Agent.Rng.NextDouble();
            if (isItemMutated > mutationRate)
                return oldItemId;
            var itemTier = ItemTierList.GetItemTier(oldItemId);
            itemTier = RandomFunctions.GenerateGaussianByte(Agent.Rng, (byte)itemTier, standardDeviation, (byte)(ItemTierList.TieredItemList.Count - 1));
            var randItem = Agent.Rng.Next(0, ItemTierList.TieredItemList[itemTier].Count);
            return (byte)ItemTierList.TieredItemList[itemTier][randItem];
        }
    }
}

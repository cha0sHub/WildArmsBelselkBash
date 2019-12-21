using WildArmsModel.Model.Areas;
using WildArmsModel.Model.Enemies;
using WildArmsModel.Model.Events;
using WildArmsModel.Model.Items;
using WildArmsModel.Model.ShoppingLists;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Lists;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Modes
{
    internal class SparsityMode : ISparsityMode
    {
        private const byte HealBerryId = 0x0;
        private const byte PotionBerryId = 0x1;
        private const byte MegaBerryId = 0x2;
        private const byte NectarId = 0xF;
        private const byte AmbrosiaId = 0x10;
        private const byte MagicCarrotId = 0x03;

        private IRandomizerAgent Agent { get; }
        private IAreaCollection AreaCollection { get; }
        private IEnemyCollection EnemyCollection { get; }
        private IItemCollection ItemCollection { get; }
        private IItemTierList ItemTierList { get; }

        public SparsityMode(IRandomizerAgent agent, IAreaCollection areaCollection, IEnemyCollection enemyCollection, IItemCollection itemCollection, IItemTierList itemTierList)
        {
            Agent = agent;
            AreaCollection = areaCollection;
            EnemyCollection = enemyCollection;
            ItemCollection = itemCollection;
            ItemTierList = itemTierList;
        }

        public void ApplySparsityMode()
        {
            //Modify Shopping Lists to have no healing items
            RemoveHealingItemsFromShoppingLists();
            //Repopulate Found Items with more healing items
            PopulateEventsWithNewItems();
            //Increase rate of berry drops for enemies
            IncreaseEnemiesHealDropRate();

            //sets ambrosia and nectar to fully heal MP as well as HP
            var nectar = ItemCollection.GetMappedObject(NectarId);
            nectar.Vitality = 999;
            var ambrosia = ItemCollection.GetMappedObject(AmbrosiaId);
            ambrosia.Vitality = 999;

            //commit all changes
            ItemCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
            AreaCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
            EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void IncreaseEnemiesHealDropRate()
        {
            foreach (var enemy in EnemyCollection.MappedObjectReadOnly)
            {
                IncreaseEnemyHealDropRate(enemy);
            }
        }

        private void IncreaseEnemyHealDropRate(EnemyObject enemy)
        {
            //base tier on enemy lvl
            var itemTier = enemy.Level / 3;
            if (IsHealingItem(enemy.DropId))
            {
                var newItemId = GetRandomBerry(itemTier);
                enemy.DropId = newItemId;
                if (enemy.DropRate == 0)
                {
                    enemy.DropRate = GetBerryDropRate(newItemId);
                }
            }
            if (IsHealingItem(enemy.StealId))
            {
                var newItemId = GetRandomBerry(itemTier);
                enemy.StealId = newItemId;
                if (enemy.StealRate == 0)
                {
                    enemy.StealRate = GetBerryDropRate(newItemId);
                }
            }
        }
        private byte GetBerryDropRate(byte itemId)
        {
            switch (itemId)
            {
                case HealBerryId:
                    return RandomFunctions.GenerateGaussianByte(Agent.Rng, 50, 20, 100);
                case PotionBerryId:
                    return RandomFunctions.GenerateGaussianByte(Agent.Rng, 30, 10, 100);
                case MegaBerryId:
                    return RandomFunctions.GenerateGaussianByte(Agent.Rng, 10, 5, 100);
                case NectarId:
                    return RandomFunctions.GenerateGaussianByte(Agent.Rng, 5, 5, 100);
                case AmbrosiaId:
                default:
                    return RandomFunctions.GenerateGaussianByte(Agent.Rng, 5, 5, 100);
            }
        }
        private void PopulateEventsWithNewItems()
        {
            foreach (var area in AreaCollection.MappedObjectReadOnly)
            {
                foreach (var nEvent in area.EventData.MappedObjectReadOnly)
                {
                    PopulateEventWithNewItem(nEvent);
                }
            }
        }
        //Also replaces gella with berries
        private void PopulateEventWithNewItem(EventObject nEvent)
        {
            if (nEvent.Type == EventObject.EventType.Unknown)
                return;
            //TODO: correct Gella so that it can be tiered as well
            if (nEvent.IsGella)
            {
                return;
            }
            var itemTier = ItemTierList.GetItemTier(nEvent.ItemId);
            if (itemTier < 0)
                return;
            //if already a healing item, auto progress to random berry
            if (IsHealingItem(nEvent.ItemId))
                nEvent.ItemId = GetRandomBerry(itemTier);
            else
            {
                var mutateItemOdds = Agent.Rng.NextDouble();
                if (mutateItemOdds > Agent.Probabilities.SparsityModeItemMutationRate)
                    return;
                nEvent.ItemId = GetRandomBerry(itemTier);
            }
        }
        private static bool IsHealingItem(ushort itemId)
        {
            return (itemId == HealBerryId || itemId == PotionBerryId
                || itemId == MegaBerryId || itemId == NectarId
                || itemId == AmbrosiaId);
        }
        private byte GetRandomBerry(int tier)
        {
            var randBerry = Agent.Rng.NextDouble();
            if (tier < 4)
            {
                return GetBerryByOdds(randBerry,
                    0.6, 0.85, 0.9, 0.96, 0.98);
            }
            else if (tier < 8)
            {
                return GetBerryByOdds(randBerry,
                    0.4, 0.8, 0.9, 0.95, 0.98);
            }
            return GetBerryByOdds(randBerry,
                0.2, 0.6, 0.8, 0.9, 0.96);
        }

        //sloppy
        private static byte GetBerryByOdds(double odds, double healBerryOdds, double potionBerryOdds,
                                           double megaBerryOdds, double magicCarrotOdds, double nectarOdds)
        {
            if (odds < healBerryOdds)
                return HealBerryId;
            if (odds < potionBerryOdds)
                return PotionBerryId;
            if (odds < megaBerryOdds)
                return MegaBerryId;
            if (odds < magicCarrotOdds)
                return MagicCarrotId;
            if (odds < nectarOdds)
                return NectarId;
            return AmbrosiaId;
        }

        private void RemoveHealingItemsFromShoppingLists()
        {
            foreach (var area in AreaCollection.MappedObjectReadOnly)
            {
                if (area.Id == 3 || area.Id == 19 || area.Id == 20)
                {
                    continue;
                }
                if (area.Id > 34)
                {
                    break;
                }
                foreach (var list in area.ShoppingListData.MappedObjectReadOnly)
                {
                    RemoveHealingItemsFromList(list);
                }
            }
        }

        private static void RemoveHealingItemsFromList(ShoppingListObject shoppingList)
        {
            //Heal Berry to Wind Vane
            ReplaceItems(shoppingList, 0, 95);
            //Potion Berry to Diary
            ReplaceItems(shoppingList, 1, 17);
            //Goat Doll to Spirit Ring
            ReplaceItems(shoppingList, 89, 86);
        }

        //brute force way of replacing list items, prob better way
        private static void ReplaceItems(ShoppingListObject shoppingList, byte toReplaceId, byte replaceWithId)
        {
            if (shoppingList.ItemId1 == toReplaceId)
                shoppingList.ItemId1 = replaceWithId;
            if (shoppingList.ItemId2 == toReplaceId)
                shoppingList.ItemId2 = replaceWithId;
            if (shoppingList.ItemId3 == toReplaceId)
                shoppingList.ItemId3 = replaceWithId;
            if (shoppingList.ItemId4 == toReplaceId)
                shoppingList.ItemId4 = replaceWithId;
            if (shoppingList.ItemId5 == toReplaceId)
                shoppingList.ItemId5 = replaceWithId;
            if (shoppingList.ItemId6 == toReplaceId)
                shoppingList.ItemId6 = replaceWithId;
            if (shoppingList.ItemId7 == toReplaceId)
                shoppingList.ItemId7 = replaceWithId;
            if (shoppingList.ItemId8 == toReplaceId)
                shoppingList.ItemId8 = replaceWithId;
            if (shoppingList.ItemId9 == toReplaceId)
                shoppingList.ItemId9 = replaceWithId;
            if (shoppingList.ItemId10 == toReplaceId)
                shoppingList.ItemId10 = replaceWithId;
        }
    }
}

using WildArmsModel.Model.Items;
using WildArmsModel.Model.ShoppingLists;
using WildArmsRandomizer.Lists;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class ShoppingListRandomizer : IShoppingListRandomizer
    {

        private IRandomizerAgent Agent { get; }
        private IItemCollection ItemCollection { get; }
        private IItemTierList ItemTierList { get; }
        private IItemRandomizer ItemRandomizer { get; }

        public ShoppingListRandomizer(IRandomizerAgent agent, IItemCollection itemCollection, IItemTierList itemTierList,
                                        IItemRandomizer itemRandomizer)
        {
            Agent = agent;
            ItemCollection = itemCollection;
            ItemTierList = ItemTierList;
            ItemRandomizer = itemRandomizer;
        }

        public void RandomizeShoppingLists(IShoppingListCollection shoppingListCollection)
        {
            foreach (var shoppingList in shoppingListCollection.MappedObjectReadOnly)
            {
                RandomizeList(shoppingList);
            }
        }
        private void RandomizeList(ShoppingListObject shoppingList)
        {
            shoppingList.ItemId1 = RandomizeListEntry(shoppingList.ItemId1);
            shoppingList.ItemId2 = RandomizeListEntry(shoppingList.ItemId2);
            shoppingList.ItemId3 = RandomizeListEntry(shoppingList.ItemId3);
            shoppingList.ItemId4 = RandomizeListEntry(shoppingList.ItemId4);
            shoppingList.ItemId5 = RandomizeListEntry(shoppingList.ItemId5);
            shoppingList.ItemId6 = RandomizeListEntry(shoppingList.ItemId6);
            shoppingList.ItemId7 = RandomizeListEntry(shoppingList.ItemId7);
            shoppingList.ItemId8 = RandomizeListEntry(shoppingList.ItemId8);
            shoppingList.ItemId9 = RandomizeListEntry(shoppingList.ItemId9);
            shoppingList.ItemId10 = RandomizeListEntry(shoppingList.ItemId10);
        }
        private byte RandomizeListEntry(byte oldItemId)
        {
            //value returned when the entry does not exist
            if (oldItemId == 255)
                return oldItemId;
            var item = ItemCollection.GetMappedObject(oldItemId);
            if (item.EquipSlot == ItemMasks.EquipSlot.None)
                return oldItemId;
            byte newItemId = 0;
            do
            {
                newItemId = ItemRandomizer.GetMutatedItemId(oldItemId, Agent.Probabilities.DefaultItemMutationRate, Agent.Probabilities.DefaultItemStandardDeviation);
            }
            //TODO: lazy way to ensure new item is equipment
            while (ItemCollection.GetMappedObject(newItemId).EquipSlot == ItemMasks.EquipSlot.None);
            return newItemId;
        }
    }
}

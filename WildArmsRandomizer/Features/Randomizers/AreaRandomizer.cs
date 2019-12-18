using WildArmsModel.Model.Areas;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class AreaRandomizer : IAreaRandomizer
    {
        private IRandomizerAgent Agent { get; }
        private IAreaCollection AreaCollection { get; }
        private IEventRandomizer EventRandomizer { get; }
        private IShoppingListRandomizer ShoppingListRandomizer { get; }

        public AreaRandomizer(IRandomizerAgent agent, IAreaCollection areaCollection, IEventRandomizer eventRandomizer, IShoppingListRandomizer shoppingListRandomizer)
        {
            Agent = agent;
            AreaCollection = areaCollection;
            EventRandomizer = eventRandomizer;
            ShoppingListRandomizer = shoppingListRandomizer;
        }

        public void RandomizeFoundItems()
        {
            foreach (var area in AreaCollection.MappedObjectReadOnly)
            {
                EventRandomizer.RandomizeEventCollection(area.EventData);
            }
            AreaCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        public void RandomizeShopLists()
        {
            foreach (var area in AreaCollection.MappedObjectReadOnly)
            {
                if (area.Id == 3)
                {
                    continue;
                }
                ShoppingListRandomizer.RandomizeShoppingLists(area.ShoppingListData);
            }
            AreaCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

    }
}

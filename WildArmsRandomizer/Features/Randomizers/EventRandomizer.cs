using WildArmsModel.Model.Events;
using WildArmsModel.Model.Items;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Lists;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class EventRandomizer : IEventRandomizer
    {
        private IRandomizerAgent Agent { get; }
        private IItemCollection ItemCollection { get; }
        private IItemTierList ItemTierList { get; }

        public EventRandomizer(IRandomizerAgent agent, IItemCollection itemCollection, IItemTierList itemTierList)
        {
            Agent = agent;
            ItemCollection = itemCollection;
            ItemTierList = itemTierList;
        }

        public void RandomizeEventCollection(IEventCollection eventCollection)
        {
            foreach (var nEvent in eventCollection.MappedObjectReadOnly)
            {
                RandomizeEvent(nEvent);
            }
        }
        private void RandomizeEvent(EventObject nEvent)
        {
            var doMutation = Agent.Rng.NextDouble();
            if (doMutation > Agent.Probabilities.EventItemMutationRate)
                return;
            if (nEvent.Type == EventObject.EventType.Unknown)
                return;
            if (nEvent.ItemId < 200)
            {
                var item = ItemCollection.GetMappedObject(nEvent.ItemId);
                if (item == null)
                    return;
                var itemTier = ItemTierList.GetItemTier(item.Id);
                //don't randomize items not contained in the tier list
                if (itemTier == -1)
                    return;
                itemTier = RandomFunctions.GenerateGaussianByte(Agent.Rng, (byte)itemTier, Agent.Probabilities.EventItemStandardDeviation, 14);
                var randItem = Agent.Rng.Next(0, ItemTierList.TieredItemList[itemTier].Count);
                var newItem = ItemCollection.GetMappedObject(ItemTierList.TieredItemList[itemTier][randItem]);
                nEvent.ItemId = (byte)newItem.Id;
            }
        }
    }
}

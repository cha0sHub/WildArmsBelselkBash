using WildArmsModel.Model.Items;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    internal class ItemPriceCorrectionOption : IItemPriceCorrectionOption
    {
        private IRandomizerAgent Agent { get; }
        private IItemCollection ItemCollection { get; }

        public ItemPriceCorrectionOption(IRandomizerAgent agent, IItemCollection itemCollection)
        {
            Agent = agent;
            ItemCollection = itemCollection;
        }

        public void ApplyCorrections()
        {
            CorrectItemPrices();
            ItemCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        //Sets prices of items not normally sold that have a value meant to discourage selling.
        //Makes prices match the price curve, since items could potentially appear in stores.
        private void CorrectItemPrices()
        {
            //Diary
            CorrectItemPrice(17, 100);
            //Gimel Coin
            CorrectItemPrice(21, 500);
            //Juggernaut
            CorrectItemPrice(59, 45000);
            //Spirit Ring
            CorrectItemPrice(86, 4000);
            //Sheriff Star
            CorrectItemPrice(102, 60000);
            //Reverse Edge
            CorrectItemPrice(119, 40000);
            //Necronomicon
            CorrectItemPrice(124, 50000);
        }
        private void CorrectItemPrice(int itemId, ushort newPrice)
        {
            var item = ItemCollection.GetMappedObject(itemId);
            item.Price = newPrice;
        }
    }
}

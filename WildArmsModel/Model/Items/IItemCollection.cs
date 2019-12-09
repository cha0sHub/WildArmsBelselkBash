using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Items
{
    public interface IItemCollection : IDiscMappedCollection<ItemObject>
    {
        Dictionary<int, ItemObject> ItemData { get; }
        IReadOnlyList<ItemObject> Items { get; }

        void SetItemNames();
    }
}
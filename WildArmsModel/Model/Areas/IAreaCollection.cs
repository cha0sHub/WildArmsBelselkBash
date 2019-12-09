using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Areas
{
    public interface IAreaCollection : IDiscMappedCollection<AreaObject>
    {
        IReadOnlyList<AreaObject> Areas { get; }

        void SetAreaNames();
    }
}
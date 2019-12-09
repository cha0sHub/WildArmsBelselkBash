using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Events
{
    public interface IEventCollection : IParentMappedCollection<EventObject>
    {
        IReadOnlyList<EventObject> Events { get; }
    }
}
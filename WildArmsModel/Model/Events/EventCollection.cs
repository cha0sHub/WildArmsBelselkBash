using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Events
{
    public class EventCollection : ParentMappedCollection<EventObject>, IEventCollection
    {
        public IReadOnlyList<EventObject> Events => MappedObjectReadOnly;
    }
}

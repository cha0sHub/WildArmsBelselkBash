using DiscDataManipulation.Model;
using System.Collections.Generic;

namespace WildArmsModel.Model.Events
{
    public class EventDetailCollection : ParentMappedCollection<EventDetailObject>, IEventDetailCollection
    {
        public IReadOnlyList<EventDetailObject> EventDetails => MappedObjectReadOnly;
    }
}

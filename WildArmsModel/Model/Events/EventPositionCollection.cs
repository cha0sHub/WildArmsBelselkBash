using DiscDataManipulation.Model;
using System.Collections.Generic;

namespace WildArmsModel.Model.Events
{
    public class EventPositionCollection : ParentMappedCollection<EventPositionObject>, IEventPositionCollection
    {
        public IReadOnlyList<EventPositionObject> EventPositions => MappedObjectReadOnly;
    }
}

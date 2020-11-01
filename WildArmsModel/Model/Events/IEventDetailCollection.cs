using System.Collections.Generic;

namespace WildArmsModel.Model.Events
{
    public interface IEventDetailCollection
    {
        IReadOnlyList<EventDetailObject> EventDetails { get; }
    }
}

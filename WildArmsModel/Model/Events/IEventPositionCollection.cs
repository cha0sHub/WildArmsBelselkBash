using System.Collections.Generic;

namespace WildArmsModel.Model.Events
{
    public interface IEventPositionCollection
    {
        IReadOnlyList<EventPositionObject> EventPositions { get; }
    }
}

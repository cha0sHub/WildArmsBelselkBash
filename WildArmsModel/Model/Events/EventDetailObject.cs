using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Events
{
    public class EventDetailObject : ParentMappedObject
    {
        public int Size { get; }
        public EventDetailObject(DiscMappedObject parent, int offset, int id, int size) : base(parent, offset)
        {
            Id = id;
            Size = size;
        }

    }
}

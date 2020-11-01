using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Events
{
    public class EventPositionObject : ParentMappedObject
    {
        public ushort Offset
        {
            get
            {
                return ReadUnsignedShort(EventPositionOffsets.Offset);
            }
        }

        public EventPositionObject(DiscMappedObject parent, int offset, int id) : base(parent, offset)
        {
            Id = id;
        }
    }
}

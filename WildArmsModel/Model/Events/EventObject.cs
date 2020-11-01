using DiscDataManipulation.Model;
using System.Collections.Generic;

namespace WildArmsModel.Model.Events
{
    public class EventObject : ParentMappedObject
    {
        public const int ObjectSize = 0x12;
        public enum EventType
        {
            Chest,
            Barrel,
            Box,
            Bottle,
            Unknown
        }
        public EventType Type { get; set; }

        public byte TypeIndicator1
        {
            get
            {
                return ReadByte(EventOffsets.TypeIndicator1);
            }
        }
        public byte TypeIndicator2
        {
            get
            {
                return ReadByte(EventOffsets.TypeIndicator2);
            }
        }
        public byte TypeIndicator3
        {
            get
            {
                return ReadByte(EventOffsets.TypeIndicator3);
            }
        }

        public ushort ItemId
        {
            get
            {
                return ReadUnsignedShort(EventOffsets.ItemId);
            }
            set
            {
                if (value == ItemId)
                    return;
                WriteUnsignedShort(value, EventOffsets.ItemId);
            }
        }


        public int Gella { get; set; }
        public bool IsGella
        {
            get
            {
                if (ItemId > 255)
                    return true;
                return false;
            }
        }

        public EventObject(DiscMappedObject parent, int offset) : base(parent, offset)
        {
            Id = ReadByte(EventOffsets.Id);
            EvaluateEventType();
            if (IsGella)
                Gella = ushort.MaxValue - ItemId + 1;
        }
        private void EvaluateEventType()
        {
            //TODO: Simplify this, likely a bit in the byte sequence is common between all these. For now it works to pick the item events out though
            if ((TypeIndicator1 == 17 && TypeIndicator2 == 18)
                || (TypeIndicator1 == 7 && TypeIndicator2 == 8)
                || (TypeIndicator1 == 6 && TypeIndicator2 == 6)
                || (TypeIndicator1 == 7 && TypeIndicator2 == 7)
                || (TypeIndicator1 == 4 && TypeIndicator2 == 5)
                || (TypeIndicator1 == 213 && TypeIndicator2 == 214))
            {
                Type = EventType.Chest;
                return;
            }
            //Type indicators aren't quite perfect.
            if (TypeIndicator1 == 6)
            {
                Type = EventType.Box;
                return;
            }
            if (TypeIndicator3 == 136)
            {
                Type = EventType.Barrel;
                return;
            }
            if (TypeIndicator3 == 128)
            {
                Type = EventType.Bottle;
                return;
            }
            Type = EventType.Unknown;
        }
    }
}

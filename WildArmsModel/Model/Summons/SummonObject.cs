using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Summons
{
    public class SummonObject : DiscMappedObject
    {
        public const int SummonSize = 10;

        public byte Multiplier
        {
            get
            {
                return ReadByte(SummonOffsets.Multiplier);
            }
            set
            {
                if (Multiplier == value)
                    return;
                WriteByte(value, SummonOffsets.Multiplier);
            }
        }

        public byte RawElementFlag
        {
            get
            {
                return ReadByte(SummonOffsets.ElementFlag);
            }
            set
            {
                if (RawElementFlag == value)
                    return;
                WriteByte(value, SummonOffsets.ElementFlag);
            }
        }

        public byte HitRate
        {
            get
            {
                return ReadByte(SummonOffsets.HitRate);
            }
            set
            {
                if (HitRate == value)
                    return;
                WriteByte(value, SummonOffsets.HitRate);
            }
        }

        public byte TargetParty
        {
            get
            {
                return ReadByte(SummonOffsets.TargetParty);
            }
            set
            {
                if (TargetParty == value)
                    return;
                WriteByte(value, SummonOffsets.TargetParty);
            }
        }

        public SummonObject(byte[] rawData, long discOffset) : base(rawData, discOffset)
        {

        }
    }
}

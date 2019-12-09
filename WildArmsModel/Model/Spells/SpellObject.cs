using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Spells
{
    public class SpellObject : DiscMappedObject
    {
        public const int SpellSize = 14;

        public byte IsAdvanced
        {
            get
            {
                return ReadByte(SpellOffsets.IsAdvanced);
            }
            set
            {
                if (IsAdvanced == value)
                    return;
                WriteByte(value, SpellOffsets.IsAdvanced);
            }
        }

        public byte MpCost
        {
            get
            {
                return ReadByte(SpellOffsets.MpCost);
            }
            set
            {
                if (MpCost == value)
                    return;
                WriteByte(value, SpellOffsets.MpCost);
            }
        }

        public byte BindRow
        {
            get
            {
                return ReadByte(SpellOffsets.BindRow);
            }
            set
            {
                if (BindRow == value)
                    return;
                WriteByte(value, SpellOffsets.BindRow);
            }
        }

        public byte BindCol
        {
            get
            {
                return ReadByte(SpellOffsets.BindCol);
            }
            set
            {
                if (BindCol == value)
                    return;
                WriteByte(value, SpellOffsets.BindCol);
            }
        }

        public ushort SpellMultiplier
        {
            get
            {
                return ReadUnsignedShort(SpellOffsets.SpellMultiplier);
            }
            set
            {
                if (SpellMultiplier == value)
                    return;
                WriteUnsignedShort(value, SpellOffsets.SpellMultiplier);
            }
        }

        public byte UsageFlag
        {
            get
            {
                return ReadByte(SpellOffsets.UsageFlag);
            }
            set
            {
                if (UsageFlag == value)
                    return;
                WriteByte(value, SpellOffsets.UsageFlag);
            }
        }

        public byte ElementFlag
        {
            get
            {
                return ReadByte(SpellOffsets.ElementFlag);
            }
            set
            {
                if (ElementFlag == value)
                    return;
                WriteByte(value, SpellOffsets.ElementFlag);
            }
        }

        public byte StatusFlag
        {
            get
            {
                return ReadByte(SpellOffsets.StatusFlag);
            }
            set
            {
                if (StatusFlag == value)
                    return;
                WriteByte(value, SpellOffsets.StatusFlag);
            }
        }

        public byte HitRate
        {
            get
            {
                return ReadByte(SpellOffsets.HitRate);
            }
            set
            {
                if (HitRate == value)
                    return;
                WriteByte(value, SpellOffsets.HitRate);
            }
        }

        public byte ReviveFlag
        {
            get
            {
                return ReadByte(SpellOffsets.ReviveFlag);
            }
            set
            {
                if (ReviveFlag == value)
                    return;
                WriteByte(value, SpellOffsets.ReviveFlag);
            }
        }

        public byte IsBlackMagic
        {
            get
            {
                return ReadByte(SpellOffsets.IsBlackMagic);
            }
            set
            {
                if (IsBlackMagic == value)
                    return;
                WriteByte(value, SpellOffsets.IsBlackMagic);
            }
        }

        public SpellObject(byte[] rawData, long discOffset) : base(rawData, discOffset)
        {
        }
    }
}

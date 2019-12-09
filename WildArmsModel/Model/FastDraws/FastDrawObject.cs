using DiscDataManipulation.Model;

namespace WildArmsModel.Model.FastDraws
{
    public class FastDrawObject : DiscMappedObject
    {
        public const int FastDrawSize = 12;

        public byte Difficulty
        {
            get
            {
                return ReadByte(FastDrawOffsets.Difficulty);
            }
            set
            {
                if (Difficulty == value)
                    return;
                WriteByte(value, FastDrawOffsets.Difficulty);
            }
        }

        public byte MpUsage
        {
            get
            {
                return ReadByte(FastDrawOffsets.MpUsage);
            }
            set
            {
                if (MpUsage == value)
                    return;
                WriteByte(value, FastDrawOffsets.MpUsage);
            }
        }

        public ushort AttackMultiplier
        {
            get
            {
                return ReadUnsignedShort(FastDrawOffsets.AttackMultiplier);
            }
            set
            {
                if (AttackMultiplier == value)
                    return;
                WriteUnsignedShort(value, FastDrawOffsets.AttackMultiplier);
            }
        }

        public byte RawTargetFlag
        {
            get
            {
                return ReadByte(FastDrawOffsets.TargetFlag);
            }
            set
            {
                if (RawTargetFlag == value)
                    return;
                WriteByte(value, FastDrawOffsets.TargetFlag);
            }
        }

        public byte EffectHitRate
        {
            get
            {
                return ReadByte(FastDrawOffsets.EffectHitRate);
            }
            set
            {
                if (EffectHitRate == value)
                    return;
                WriteByte(value, FastDrawOffsets.EffectHitRate);
            }
        }

        public byte HitRate
        {
            get
            {
                return ReadByte(FastDrawOffsets.Hitrate);
            }
            set
            {
                if (HitRate == value)
                    return;
                WriteByte(value, FastDrawOffsets.Hitrate);
            }
        }

        public FastDrawObject(byte[] rawData, long discOffset) : base(rawData, discOffset)
        {
        }
    }
}

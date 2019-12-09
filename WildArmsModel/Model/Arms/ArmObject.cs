using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Arms
{
    public class ArmObject : DiscMappedObject
    {
        public const int ArmSize = 12;

        public byte InitialAccuracy
        {
            get
            {
                return ReadByte(ArmOffsets.InitAccuracy);
            }
            set
            {
                if (InitialAccuracy == value)
                    return;
                WriteByte(value, ArmOffsets.InitAccuracy);
            }
        }

        public byte InitialBullets
        {
            get
            {
                return ReadByte(ArmOffsets.InitBullets);
            }
            set
            {
                if (InitialBullets == value)
                    return;
                WriteByte(value, ArmOffsets.InitBullets);
            }
        }

        public ushort InitialAtp
        {
            get
            {
                return ReadUnsignedShort(ArmOffsets.InitAtp);
            }
            set
            {
                if (InitialAtp == value)
                    return;
                WriteUnsignedShort(value, ArmOffsets.InitAtp);
            }
        }

        public byte RawTargetFlag
        {
            get
            {
                return ReadByte(ArmOffsets.TargetFlag);
            }
            set
            {
                if (RawTargetFlag == value)
                    return;
                WriteByte(value, ArmOffsets.TargetFlag);
            }
        }

        public byte RawElementFlag
        {
            get
            {
                return ReadByte(ArmOffsets.ElementFlag);
            }
            set
            {
                if (RawElementFlag == value)
                    return;
                WriteByte(value, ArmOffsets.ElementFlag);
            }
        }

        public ArmObject(byte[] rawData, long discOffset) : base(rawData, discOffset)
        {
        }
    }
}

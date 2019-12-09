using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Attacks
{
    public class AttackObject : DiscMappedObject
    {
        public const int AttackSize = 10;
        public ushort DamageModifier
        {
            get
            {
                return ReadUnsignedShort(AttackOffsets.DmgModifier);
            }
            set
            {
                if (DamageModifier == value)
                    return;
                WriteUnsignedShort(value, AttackOffsets.DmgModifier);
            }
        }

        public byte RawTargetFlag
        {
            get
            {
                return ReadByte(AttackOffsets.TargetFlag);
            }
            set
            {
                if (RawTargetFlag == value)
                    return;
                WriteByte(value, AttackOffsets.TargetFlag);
            }
        }

        public byte RawElementFlag
        {
            get
            {
                return ReadByte(AttackOffsets.ElementFlag);
            }
            set
            {
                if (RawElementFlag == value)
                    return;
                WriteByte(value, AttackOffsets.ElementFlag);
            }
        }

        public byte EffectChance
        {
            get
            {
                return ReadByte(AttackOffsets.EffectChance);
            }
            set
            {
                if (EffectChance == value)
                    return;
                WriteByte(value, AttackOffsets.EffectChance);
            }
        }

        public AttackObject(byte[] rawData, long discOffset) : base(rawData, discOffset)
        {
        }
    }
}

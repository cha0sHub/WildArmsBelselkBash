using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Items
{
    public class ItemObject : DiscMappedObject
    {
        public const int ObjectSize = 0x24;

        public ItemMasks.UsageMask Usage
        {
            get
            {
                return (ItemMasks.UsageMask)RawUsage;
            }
            set
            {
                if (value == Usage)
                    return;
                RawUsage = (byte)value;
            }
        }
        public ItemMasks.EquipSlot EquipSlot
        {
            get
            {
                return (ItemMasks.EquipSlot)RawEquipSlot;
            }
            set
            {
                if (value == EquipSlot)
                    return;
                RawEquipSlot = (byte)value;
            }
        }

        public ItemMasks.UsageConditionMask UsageCondition
        {
            get
            {
                return (ItemMasks.UsageConditionMask)RawUsageCondition;
            }
            set
            {
                if (value == UsageCondition)
                    return;
                RawUsageCondition = (byte)value;
            }
        }

        public ItemMasks.CureStatusMask CureStatus
        {
            get
            {
                return (ItemMasks.CureStatusMask)RawCureStatusAndAttackElementFlag;
            }
            set
            {
                if (value == CureStatus)
                    return;
                RawCureStatusAndAttackElementFlag = (byte)value;
            }
        }
        public ItemMasks.ElementMask AttackElement
        {
            get
            {
                return (ItemMasks.ElementMask)RawCureStatusAndAttackElementFlag;
            }
            set
            {
                if (value == AttackElement)
                    return;
                RawCureStatusAndAttackElementFlag = (byte)value;
            }
        }

        public ItemMasks.ElementMask WeaknessElement
        {
            get
            {
                return (ItemMasks.ElementMask)RawCureDeathAndWeaknessElementFlag;
            }
            set
            {
                if (value == WeaknessElement)
                    return;
                RawCureDeathAndWeaknessElementFlag = (byte)value;
            }
        }

        public ItemMasks.ElementMask AbsorbElement
        {
            get
            {
                return (ItemMasks.ElementMask)RawAbsorbElementFlag;
            }
            set
            {
                if (value == AbsorbElement)
                    return;
                RawAbsorbElementFlag = (byte)value;
            }
        }

        public ItemMasks.ElementMask NullifyElement
        {
            get
            {
                return (ItemMasks.ElementMask)RawNullifyElementFlag;
            }
            set
            {
                if (value == NullifyElement)
                    return;
                RawNullifyElementFlag = (byte)value;
            }
        }

        public byte RawUsage
        {
            get
            {
                return ReadByte(ItemOffsets.UsageFlag);
            }
            set
            {
                if (value == RawUsage)
                    return;
                WriteByte(value, ItemOffsets.UsageFlag);
                NotifyPropertyChanged();
            }
        }

        public byte RawEquipSlot
        {
            get
            {
                return ReadByte(ItemOffsets.EquipSlot);
            }
            set
            {
                if (value == RawEquipSlot)
                    return;
                WriteByte(value, ItemOffsets.EquipSlot);
                NotifyPropertyChanged();
            }
        }


        public byte RawUsageCondition
        {
            get
            {
                return ReadByte(ItemOffsets.UsageConditionFlag);
            }
            set
            {
                if (value == RawUsageCondition)
                    return;
                WriteByte(value, ItemOffsets.UsageConditionFlag);
                NotifyPropertyChanged();
            }
        }

        public byte RawCureStatusAndAttackElementFlag
        {
            get
            {
                return ReadByte(ItemOffsets.CureStatusAndAttackElementFlag);
            }
            set
            {
                if (value == RawCureStatusAndAttackElementFlag)
                    return;
                WriteByte(value, ItemOffsets.CureStatusAndAttackElementFlag);
                NotifyPropertyChanged();
            }
        }

        public byte RawCureDeathAndWeaknessElementFlag
        {
            get
            {
                return ReadByte(ItemOffsets.CureDeathAndWeaknessElementFlag);
            }
            set
            {
                if (value == RawCureDeathAndWeaknessElementFlag)
                    return;
                WriteByte(value, ItemOffsets.CureDeathAndWeaknessElementFlag);
                NotifyPropertyChanged();
            }
        }

        public byte RawAbsorbElementFlag
        {
            get
            {
                return ReadByte(ItemOffsets.AbsorbElementFlag);
            }
            set
            {
                if (value == RawAbsorbElementFlag)
                    return;
                WriteByte(value, ItemOffsets.AbsorbElementFlag);
                NotifyPropertyChanged();
            }
        }

        public byte RawNullifyElementFlag
        {
            get
            {
                return ReadByte(ItemOffsets.NullifyElementFlag);
            }
            set
            {
                if (value == RawNullifyElementFlag)
                    return;
                WriteByte(value, ItemOffsets.NullifyElementFlag);
                NotifyPropertyChanged();
            }
        }

        public byte HpMpIncrease
        {
            get
            {
                return ReadByte(ItemOffsets.HpMpIncrease);
            }
            set
            {
                if (value == HpMpIncrease)
                    return;
                WriteByte(value, ItemOffsets.HpMpIncrease);
                NotifyPropertyChanged();
            }
        }
        public ushort Strength
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Strength);
            }
            set
            {
                if (value == Strength)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Strength);
                NotifyPropertyChanged();
            }
        }
        public ushort Vitality
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Vitality);
            }
            set
            {
                if (value == Vitality)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Vitality);
                NotifyPropertyChanged();
            }
        }
        public ushort Sorcery
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Sorcery);
            }
            set
            {
                if (value == Sorcery)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Sorcery);
                NotifyPropertyChanged();
            }
        }
        public ushort Agility
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Agility);
            }
            set
            {
                if (value == Agility)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Agility);
                NotifyPropertyChanged();
            }
        }

        public ushort Atp
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Atp);
            }
            set
            {
                if (value == Atp)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Atp);
                NotifyPropertyChanged();
            }
        }
        public ushort Dfp
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Dfp);
            }
            set
            {
                if (value == Dfp)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Dfp);
                NotifyPropertyChanged();
            }
        }
        public ushort Mgr
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Mgr);
            }
            set
            {
                if (value == Mgr)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Mgr);
                NotifyPropertyChanged();
            }
        }
        public ushort Pry
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Pry);
            }
            set
            {
                if (value == Pry)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Pry);
                NotifyPropertyChanged();
            }
        }
        public ushort Luck
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Luck);
            }
            set
            {
                if (value == Luck)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Luck);
                NotifyPropertyChanged();
            }
        }
        public ushort Price
        {
            get
            {
                return ReadUnsignedShort(ItemOffsets.Price);
            }
            set
            {
                if (value == Price)
                    return;
                WriteUnsignedShort(value, ItemOffsets.Price);
                NotifyPropertyChanged();
            }
        }

        public byte MysticEffectType
        {
            get
            {
                return ReadByte(ItemOffsets.MysticEffectType);
            }
            set
            {
                if (value == MysticEffectType)
                    return;
                WriteByte(value, ItemOffsets.MysticEffectType);
                NotifyPropertyChanged();
            }
        }

        public byte SortId
        {
            get
            {
                return ReadByte(ItemOffsets.SortId);
            }
            set
            {
                if (value == SortId)
                    return;
                WriteByte(value, ItemOffsets.SortId);
                NotifyPropertyChanged();
            }
        }

        public ItemObject(byte[] rawData, long discOffset) : base(rawData, discOffset)
        {
        }
    }
}

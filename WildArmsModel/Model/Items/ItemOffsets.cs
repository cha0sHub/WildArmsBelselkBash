namespace WildArmsModel.Model.Items
{
    internal static class ItemOffsets
    {
        public const int UsageFlag = 0x0; //byte
        public const int EquipSlot = 0x1; //byte
        public const int UsageConditionFlag = 0x2;
        public const int CureStatusAndAttackElementFlag = 0x3;
        public const int CureDeathAndWeaknessElementFlag = 0x4;
        public const int AbsorbElementFlag = 0x5;
        public const int NullifyElementFlag = 0x6;
        public const int HpMpIncrease = 0x1C; //byte
        public const int Strength = 0x8;
        public const int Vitality = 0xA;
        public const int Sorcery = 0xC;
        public const int Agility = 0xE;
        public const int Atp = 0x10;          //ushort
        public const int Dfp = 0x12;
        public const int Mgr = 0x14;
        public const int Pry = 0x16;
        public const int Luck = 0x18;
        public const int Price = 0x1A;        //ushort
        public const int MysticEffectType = 0x1D;
        public const int SortId = 0x1F;
    }
}
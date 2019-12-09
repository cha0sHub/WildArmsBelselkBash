using System;

namespace WildArmsModel.Model.Items
{
    public class ItemMasks
    {
        [Flags]
        public enum UsageMask
        {
            EquipAsRudy = 0b00000001,
            EquipAsJack = 0b00000010,
            EquipAsCecilia = 0b00000100,
            UseOnARM = 0b00010000,
            UseOnFastDraw = 0b00100000,
            UseOnCharacter = 0b01110000
        }
        [Flags]
        public enum EquipSlot
        {
            None = 0b00000000,
            Rune = 0b00000001,
            Head = 0b00000010,
            Chest = 0b00000100,
            Accessory = 0b00001000,
            Weapon = 0b00010000
        }
        [Flags]
        public enum UsageConditionMask
        {
            Consumable = 0b00000001,
            UseInBattle = 0b00000010,
            UseInField = 0b00000100,
            GroupTarget = 0b01000000,
            SingleTarget = 0b10000000
        }
        [Flags]
        public enum CureStatusMask
        {
            None = 0b11111111,
            Poison = 0b01111111,
            Disease = 0b10111111,
            Silence = 0b11011111,
            Paralysis = 0b11101111,
            Confusion = 0b11110111,
            Forgetful = 0b11111011,
            Curse = 0b11111101,
            Flash = 0b11111110
        }
        [Flags]
        public enum CureDeathMask
        {
            None = 0b11111111,
            Revive = 0b10111111
        }
        [Flags]
        public enum ElementMask
        {
            Evil = 0b00000010,
            Holy = 0b00000100,
            Thunder = 0b00001000,
            Wind = 0b00010000,
            Fire = 0b00100000,
            Water = 0b01000000,
            Geo = 0b10000000
        }
        [Flags]
        public enum StatusImmunityMask
        {
            All = 0b11111111,
            Flash = 0b00000001,
            Curse = 0b00000010,
            Forgetful = 0b00000100,
            Confusion = 0b00001000,
            Paralysis = 0b00010000,
            Silence = 0b00100000,
            Disease = 0b01000000,
            Poison = 0b10000000
        }
    }
}
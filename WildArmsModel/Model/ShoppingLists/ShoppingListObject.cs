using DiscDataManipulation.Model;

namespace WildArmsModel.Model.ShoppingLists
{
    public class ShoppingListObject : ParentMappedObject
    {
        public byte ItemId1
        {
            get
            {
                if (ItemCount < 1)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item1);
            }
            set
            {
                if (value == ItemId1)
                    return;
                WriteByte(value, ShoppingListOffsets.Item1);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId2
        {
            get
            {
                if (ItemCount < 2)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item2);
            }
            set
            {
                if (value == ItemId2)
                    return;
                WriteByte(value, ShoppingListOffsets.Item2);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId3
        {
            get
            {
                if (ItemCount < 3)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item3);
            }
            set
            {
                if (value == ItemId3)
                    return;
                WriteByte(value, ShoppingListOffsets.Item3);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId4
        {
            get
            {
                if (ItemCount < 4)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item4);
            }
            set
            {
                if (value == ItemId4)
                    return;
                WriteByte(value, ShoppingListOffsets.Item4);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId5
        {
            get
            {
                if (ItemCount < 5)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item5);
            }
            set
            {
                if (value == ItemId5)
                    return;
                WriteByte(value, ShoppingListOffsets.Item5);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId6
        {
            get
            {
                if (ItemCount < 6)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item6);
            }
            set
            {

                if (value == ItemId6)
                    return;
                WriteByte(value, ShoppingListOffsets.Item6);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId7
        {
            get
            {
                if (ItemCount < 7)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item7);
            }
            set
            {

                if (value == ItemId7)
                    return;
                WriteByte(value, ShoppingListOffsets.Item7);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId8
        {
            get
            {
                if (ItemCount < 8)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item8);
            }
            set
            {

                if (value == ItemId8)
                    return;
                WriteByte(value, ShoppingListOffsets.Item8);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId9
        {
            get
            {
                if (ItemCount < 9)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item9);
            }
            set
            {
                if (value == ItemId9)
                    return;
                WriteByte(value, ShoppingListOffsets.Item9);
                NotifyPropertyChanged();
            }
        }

        public byte ItemId10
        {
            get
            {
                if (ItemCount < 10)
                    return 255;
                return ReadByte(ShoppingListOffsets.Item10);
            }
            set
            {
                if (value == ItemId10)
                    return;
                WriteByte(value, ShoppingListOffsets.Item10);
                NotifyPropertyChanged();
            }
        }
        private int ItemCount { get; }
        public ShoppingListObject(DiscMappedObject parent, int offset, int id, int count) : base(parent, offset)
        {
            Id = id;
            ItemCount = count;
        }
    }
}

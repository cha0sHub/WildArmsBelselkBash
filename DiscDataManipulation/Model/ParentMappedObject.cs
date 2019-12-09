using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DiscDataManipulation.Model
{
    //represents an object contained within a disc mapped object.
    public abstract class ParentMappedObject : INotifyPropertyChanged, IParentMappedObject
    {
        private DiscMappedObject Parent { get; }
        private int ParentOffset { get; }
        public int Id { get; set; }
        public ParentMappedObject(DiscMappedObject parent, int parentOffset)
        {
            Parent = parent;
            ParentOffset = parentOffset;
        }

        public void WriteByte(byte data, int offset)
        {
            Parent.WriteByte(data, ParentOffset + offset);
        }

        public void WriteUnsignedShort(ushort data, int offset)
        {
            Parent.WriteUnsignedShort(data, ParentOffset + offset);
        }

        public void WriteUnsignedInt(uint data, int offset)
        {
            Parent.WriteUnsignedInt(data, ParentOffset + offset);
        }

        public byte ReadByte(int offset)
        {
            return Parent.ReadByte(ParentOffset + offset);
        }
        public ushort ReadUnsignedShort(int offset)
        {
            return Parent.ReadUnsignedShort(ParentOffset + offset);
        }
        public uint ReadUnsignedInt(int offset)
        {
            return Parent.ReadUnsignedInt(ParentOffset + offset);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

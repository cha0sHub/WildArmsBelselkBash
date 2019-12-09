using System.ComponentModel;

namespace DiscDataManipulation.Model
{
    public interface IParentMappedObject
    {
        int Id { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        byte ReadByte(int offset);
        uint ReadUnsignedInt(int offset);
        ushort ReadUnsignedShort(int offset);
        void WriteByte(byte data, int offset);
        void WriteUnsignedInt(uint data, int offset);
        void WriteUnsignedShort(ushort data, int offset);
    }
}
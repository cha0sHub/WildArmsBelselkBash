using System.Collections.Generic;
using System.ComponentModel;

namespace DiscDataManipulation.Model
{
    public interface IDiscMappedObject
    {
        IReadOnlyList<long> DiscOffsetsReadOnly { get; }
        int Id { get; set; }
        string Name { get; set; }
        bool PendingChange { get; }

        event PropertyChangedEventHandler PropertyChanged;

        void AddOffset(long offset, byte[] rawData);
        byte ReadByte(int offset, int offsetIndex = 0);
        uint ReadUnsignedInt(int offset);
        ushort ReadUnsignedShort(int offset);
        string ToString();
        void WriteByte(byte data, int offset);
        void WriteUnsignedInt(uint data, int offset);
        void WriteUnsignedShort(ushort data, int offset);
    }
}
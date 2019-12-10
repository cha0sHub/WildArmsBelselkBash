using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DiscDataManipulation.DataAccessor;

namespace DiscDataManipulation.Model
{
    //represents an individual object that corresponds to an object on disc.
    public abstract class DiscMappedObject : INotifyPropertyChanged, IDiscMappedObject
    {
        private byte[] RawData { get; }
        private List<byte[]> RawDataCollection { get; }
        private List<long> DiscOffsets { get; }
        public IReadOnlyList<long> DiscOffsetsReadOnly => DiscOffsets;
        public bool PendingChange { get; private set; }
        public int Id { get; set; }

        // Not strictly necessary here, but pretty much all children will need this.
        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value.Trim();
            }
        }

        public DiscMappedObject(byte[] rawData, long discOffset)
        {
            RawData = rawData;
            DiscOffsets = new List<long>();
            RawDataCollection = new List<byte[]>();
            RawDataCollection.Add(RawData);
            DiscOffsets.Add(discOffset);
            Id = -1; //If left as -1, will be auto incremented by collection
        }

        public void OverwriteObject(DiscMappedObject overwritter)
        {
            for(var i = 0; i < overwritter.GetRawDataSize();i++)
            {
                WriteByte(overwritter.ReadByte(i), i);
            }
        }

        public void AddOffset(long offset, byte[] rawData)
        {
            DiscOffsets.Add(offset);
            RawDataCollection.Add(rawData);
        }

        public void WriteByte(byte data, int offset)
        {
            RawData[offset] = data;
            foreach (var rawDatas in RawDataCollection)
            {
                rawDatas[offset] = data;
            }
            PendingChange = true;
        }
        public void WriteUnsignedShort(ushort data, int offset)
        {
            var dataBytes = BitConverter.GetBytes(data);
            RawData[offset] = dataBytes[0];
            RawData[offset + 1] = dataBytes[1];
            foreach (var rawDatas in RawDataCollection)
            {
                rawDatas[offset] = dataBytes[0];
                RawData[offset + 1] = dataBytes[1];
            }
            PendingChange = true;
        }
        public void WriteUnsignedInt(uint data, int offset)
        {
            var dataBytes = BitConverter.GetBytes(data);
            RawData[offset] = dataBytes[0];
            RawData[offset + 1] = dataBytes[1];
            RawData[offset + 2] = dataBytes[2];
            RawData[offset + 3] = dataBytes[3];
            foreach (var rawDatas in RawDataCollection)
            {
                rawDatas[offset] = dataBytes[0];
                RawData[offset + 1] = dataBytes[1];
                RawData[offset + 2] = dataBytes[2];
                RawData[offset + 3] = dataBytes[3];
            }
            PendingChange = true;
        }
        public byte ReadByte(int offset, int offsetIndex = 0)
        {
            if (offsetIndex != 0)
            {
                return RawDataCollection[offsetIndex][offset];
            }
            return RawData[offset];
        }
        public ushort ReadUnsignedShort(int offset)
        {
            return BitConverter.ToUInt16(RawData, offset);
        }
        public uint ReadUnsignedInt(int offset)
        {
            return BitConverter.ToUInt32(RawData, offset);
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }

        internal int GetRawDataSize()
        {
            return RawData.Length;
        }

        protected List<int> FindByteSequence(List<byte> byteSequence)
        {
            var data = new DiscData();
            data.LoadData(RawData);
            return data.FindByteSequence(byteSequence);
        }
        protected List<int> FindBytesInVicinity(List<Tuple<byte, byte>> bytePairs, int range)
        {
            var data = new DiscData();
            data.LoadData(RawData);
            var indices = data.FindTwoByteValue(bytePairs[0].Item2, bytePairs[0].Item1);
            var matchingIndices = new List<int>();
            foreach (var index in indices)
            {
                bool match = true;
                foreach (var bytePair in bytePairs)
                {
                    var matches = data.SearchVicinityForTwoBytes(index, range, bytePair.Item2, bytePair.Item1);
                    if (matches.Count == 0)
                    {
                        match = false;
                        break;
                    }
                }
                if (!match)
                    continue;
                matchingIndices.Add(index);
            }
            return matchingIndices;
        }

        internal void ClearOffsets()
        {
            DiscOffsets.Clear();
            RawDataCollection.Clear();
        }

        protected List<int> FindTwoByteValue(byte a, byte b)
        {
            var data = new DiscData();
            data.LoadData(RawData);
            return data.FindTwoByteValue(a, b);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

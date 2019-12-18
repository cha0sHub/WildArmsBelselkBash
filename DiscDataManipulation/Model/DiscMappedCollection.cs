using System;
using System.Collections.Generic;
using System.IO;

namespace DiscDataManipulation.Model
{
    //Class for repeating structures on a disc. extracts and rewrites disc based objects to the disc file.
    public abstract class DiscMappedCollection<T> : IDiscMappedCollection<T> where T : DiscMappedObject
    {
        protected Dictionary<int, T> MappedObjectData { get; }
        protected List<T> MappedObjects { get; }
        public IReadOnlyList<T> MappedObjectReadOnly => MappedObjects;
        private long Offset { get; }
        private int Size { get; }
        private int Count { get; }
        private int DataLength { get; }
        public DiscMappedCollection(long offset, int size, int dataLength, int count)
        {
            MappedObjectData = new Dictionary<int, T>();
            MappedObjects = new List<T>();
            Offset = offset;
            Size = size;
            DataLength = dataLength;
            Count = count;
        }
        public void ReadObjects(string fileName)
        {
            MappedObjectData.Clear();
            MappedObjects.Clear();
            for (var i = 0; i < Count; i++)
            {
                var dataBytes = new byte[DataLength];
                var objectOffset = Offset + (i * Size);
                using (var fs = File.OpenRead(fileName))
                {
                    fs.Seek(objectOffset, SeekOrigin.Begin);
                    fs.Read(dataBytes, 0, DataLength);
                }
                Type type = typeof(T);
                var ctor = type.GetConstructor(new[] { typeof(byte[]), typeof(long) });
                var mappedObject = (T)ctor.Invoke(new object[] { dataBytes, objectOffset });
                if (mappedObject.Id == -1)
                    mappedObject.Id = i;
                if (MappedObjectData.ContainsKey(mappedObject.Id))
                {
                    MappedObjectData[mappedObject.Id].AddOffset(objectOffset, dataBytes);
                }
                else
                {
                    MappedObjectData.Add(mappedObject.Id, mappedObject);
                    MappedObjects.Add(mappedObject);
                }
            }
        }
        public void WriteObjects(string fileName)
        {
            foreach (var mappedObject in MappedObjects)
            {
                if (!mappedObject.PendingChange)
                {
                    continue;
                }
                int index = 0;
                foreach (var offset in mappedObject.DiscOffsetsReadOnly)
                {
                    WriteObjectToFile(fileName, offset, mappedObject, index);
                    index++;
                }
            }
        }
        private void WriteObjectToFile(string fileName, long offset, T mappedObject, int offsetIndex = 0)
        {
            using (var fs = File.OpenWrite(fileName))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                for (int i = 0; i < DataLength; i++)
                {
                    fs.WriteByte(mappedObject.ReadByte(i, offsetIndex));
                }
            }
        }
        private byte[] GetFullObjectData(string fileName, T mappedObject)
        {
            var fullData = new byte[Size];

            using (var fs = File.OpenRead(fileName))
            {
                fs.Seek(mappedObject.DiscOffsetsReadOnly[0], SeekOrigin.Begin);
                fs.Read(fullData, 0, Size);
            }
            return fullData;
        }
        private void WriteFullObjectToFile(string fileName, IReadOnlyList<long> targetOffsets, byte[] objectData)
        {
            using (var fs = File.OpenWrite(fileName))
            {
                foreach (var offset in targetOffsets)
                {
                    fs.Seek(offset, SeekOrigin.Begin);
                    fs.Write(objectData, 0, Size);
                }
            }
        }
        //completely swaps the data of two mapped objects, preserving the offsets so that objects can be swapped multiple times.
        public void SwapMappedObjects(string fileName, T mappedObjectA, T mappedObjectB)
        {
            var fullDataA = GetFullObjectData(fileName, mappedObjectA);
            var fullDataB = GetFullObjectData(fileName, mappedObjectB);
            var offsetsA = new List<long>();
            var offsetsB = new List<long>();
            foreach (var offset in mappedObjectA.DiscOffsetsReadOnly)
                offsetsA.Add(offset);
            foreach (var offset in mappedObjectB.DiscOffsetsReadOnly)
                offsetsB.Add(offset);
            WriteFullObjectToFile(fileName, mappedObjectB.DiscOffsetsReadOnly, fullDataA);
            WriteFullObjectToFile(fileName, mappedObjectA.DiscOffsetsReadOnly, fullDataB);
            mappedObjectA.ClearOffsets();
            mappedObjectB.ClearOffsets();
            foreach (var offset in offsetsB)
                mappedObjectA.AddOffset(offset, fullDataA);
            foreach (var offset in offsetsA)
                mappedObjectB.AddOffset(offset, fullDataB);

            MappedObjectData[mappedObjectA.Id] = mappedObjectB;
            MappedObjectData[mappedObjectB.Id] = mappedObjectA;

            var mappedObjectAId = mappedObjectA.Id;
            mappedObjectA.Id = mappedObjectB.Id;
            mappedObjectB.Id = mappedObjectAId;
        }

        public void OverwriteMappedObjects(string fileName, T mappedObjectA, T mappedObjectB)
        {
            var fullDataA = GetFullObjectData(fileName, mappedObjectA);
            var offsetsA = new List<long>();
            var offsetsB = new List<long>();
            foreach (var offset in mappedObjectA.DiscOffsetsReadOnly)
                offsetsA.Add(offset);
            foreach (var offset in mappedObjectB.DiscOffsetsReadOnly)
                offsetsB.Add(offset);
            WriteFullObjectToFile(fileName, mappedObjectB.DiscOffsetsReadOnly, fullDataA);
            WriteFullObjectToFile(fileName, mappedObjectA.DiscOffsetsReadOnly, fullDataA);
            mappedObjectA.ClearOffsets();
            mappedObjectB.ClearOffsets();
            foreach (var offset in offsetsB)
                mappedObjectA.AddOffset(offset, fullDataA);
            foreach (var offset in offsetsA)
                mappedObjectB.AddOffset(offset, fullDataA);
        }

        public T GetMappedObject(int id)
        {
            if (MappedObjectData.ContainsKey(id))
                return MappedObjectData[id];
            return null;
        }
    }
}

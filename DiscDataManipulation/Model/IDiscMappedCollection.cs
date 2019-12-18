using System.Collections.Generic;

namespace DiscDataManipulation.Model
{
    public interface IDiscMappedCollection<T> where T : DiscMappedObject
    {
        IReadOnlyList<T> MappedObjectReadOnly { get; }

        T GetMappedObject(int id);
        void ReadObjects(string fileName);
        void SwapMappedObjects(string fileName, T mappedObjectA, T mappedObjectB);
        void OverwriteMappedObjects(string fileName, T mappedObjectA, T mappedObjectB);
        void WriteObjects(string fileName);
    }
}
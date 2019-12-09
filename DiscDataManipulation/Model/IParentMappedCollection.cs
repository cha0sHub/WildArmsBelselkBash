using System.Collections.Generic;

namespace DiscDataManipulation.Model
{
    public interface IParentMappedCollection<T> where T : ParentMappedObject
    {
        IReadOnlyList<T> MappedObjectReadOnly { get; }

        void AddMappedObject(T mappedObject);
        T GetMappedObject(int id);
    }
}
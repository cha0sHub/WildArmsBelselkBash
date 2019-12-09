using System.Collections.Generic;

namespace DiscDataManipulation.Model
{
    //represents collections contained within a disc mapped object. Cannot be extracted from disc as directly as disc mapped objects.
    public abstract class ParentMappedCollection<T> : IParentMappedCollection<T> where T : ParentMappedObject
    {
        protected Dictionary<int, T> MappedObjectData { get; }
        protected List<T> MappedObjects { get; }
        public IReadOnlyList<T> MappedObjectReadOnly => MappedObjects;

        public ParentMappedCollection()
        {
            MappedObjectData = new Dictionary<int, T>();
            MappedObjects = new List<T>();
        }
        public void AddMappedObject(T mappedObject)
        {
            MappedObjectData.Add(mappedObject.Id, mappedObject);
            MappedObjects.Add(mappedObject);
        }
        public T GetMappedObject(int id)
        {
            if (MappedObjectData.ContainsKey(id))
                return MappedObjectData[id];
            return null;
        }
    }
}

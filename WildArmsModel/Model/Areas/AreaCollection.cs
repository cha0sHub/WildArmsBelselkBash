using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Areas
{
    public class AreaCollection : DiscMappedCollection<AreaObject>, IAreaCollection
    {
        public const long AreaCollectionOffset = 0x002B383C;
        public const int AreaCount = 128;

        public IReadOnlyList<AreaObject> Areas => MappedObjectReadOnly;
        public AreaCollection() : base(AreaCollectionOffset, AreaObject.ObjectSize, AreaObject.ObjectSize, AreaCount)
        {
        }

        //Only call this after ReadObjects has been called
        public void SetAreaNames()
        {
            if (Areas.Count == 0)
                return;
            var areaNames = Properties.Resources.AreaNames.Split('\n');
            for (int i = 0; i < areaNames.Length; i++)
            {
                GetMappedObject(i).Name = areaNames[i].Replace("\r", "").Trim();
            }
        }
    }
}

using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Summons
{
    public class SummonCollection : DiscMappedCollection<SummonObject>, ISummonCollection
    {
        public const long SummonCollectionOffset = 0x74E10;
        public const int SummonCount = 42;

        public SummonCollection() : base(SummonCollectionOffset, SummonObject.SummonSize, SummonObject.SummonSize, SummonCount)
        {
        }

        internal void SetSummonNames()
        {
            var summons = Properties.Resources.SummonNames.Split('\n');
            for (int i = 0; i < summons.Length; i++)
            {
                var summon = GetMappedObject(i);
                summon.Name = summons[i];
            }
        }
    }
}

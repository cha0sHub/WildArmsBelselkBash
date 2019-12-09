using DiscDataManipulation.Model;

namespace WildArmsModel.Model.FastDraws
{
    public class FastDrawCollection : DiscMappedCollection<FastDrawObject>, IFastDrawCollection
    {
        public const long FastDrawCollectionOffset = 0x6D840;
        public const int FastDrawCount = 18;

        public FastDrawCollection() : base(FastDrawCollectionOffset, FastDrawObject.FastDrawSize, FastDrawObject.FastDrawSize, FastDrawCount)
        {

        }

        internal void SetFastDrawNames()
        {
            var fastDraws = Properties.Resources.FastDrawNames.Split('\n');
            for (int i = 0; i < fastDraws.Length; i++)
            {
                var fastDraw = GetMappedObject(i);
                fastDraw.Name = fastDraws[i];
            }
        }
    }
}

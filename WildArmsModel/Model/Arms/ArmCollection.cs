using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Arms
{
    public class ArmCollection : DiscMappedCollection<ArmObject>, IArmCollection
    {
        public const long ArmCollectionOffset = 0x6D30C;
        public const int ArmCount = 8;

        public ArmCollection() : base(ArmCollectionOffset, ArmObject.ArmSize, ArmObject.ArmSize, ArmCount)
        {

        }

        internal void SetArmNames()
        {
            var arms = Properties.Resources.ArmNames.Split('\n');
            for (var i = 0; i < arms.Length; i++)
            {
                var arm = GetMappedObject(i);
                arm.Name = arms[i];
            }
        }
    }
}

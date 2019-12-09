using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Attacks
{
    public class AttackCollection : DiscMappedCollection<AttackObject>, IAttackCollection
    {
        public const long AttackCollectionOffset = 0x74064;
        public const int AttackCount = 228;
        public AttackCollection() : base(AttackCollectionOffset, AttackObject.AttackSize, AttackObject.AttackSize, AttackCount)
        {
        }

        internal void SetAttackNames()
        {
            var attacks = Properties.Resources.AttackNames.Split('\n');
            for (int i = 0; i < attacks.Length; i++)
            {
                var attack = GetMappedObject(i);
                attack.Name = attacks[i];
            }
        }
    }
}

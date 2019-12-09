using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Spells
{
    public class SpellCollection : DiscMappedCollection<SpellObject>, ISpellCollection
    {
        public const long SpellCollectionOffset = 0x6E7C4;
        public const int SpellCount = 109;

        public SpellCollection() : base(SpellCollectionOffset, SpellObject.SpellSize, SpellObject.SpellSize, SpellCount)
        {

        }

        internal void SetSpellNames()
        {
            var spells = Properties.Resources.SpellNames.Split('\n');
            for (int i = 0; i < spells.Length; i++)
            {
                var spell = GetMappedObject(i);
                spell.Name = spells[i];
            }
        }
    }
}

using WildArmsModel.Model.Spells;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class SpellRandomizer : ISpellRandomizer
    {
        private IRandomizerAgent Agent { get; }
        private ISpellCollection SpellCollection { get; }

        public SpellRandomizer(IRandomizerAgent agent, ISpellCollection spellCollection)
        {
            Agent = agent;
            SpellCollection = spellCollection;
        }

        public void RandomizeSpellCollection()
        {
            foreach (var spell in SpellCollection.MappedObjectReadOnly)
            {
                RandomizeSpellStats(spell);
            }
            SpellCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void RandomizeSpellStats(SpellObject spell)
        {
            spell.MpCost = RandomFunctions.GenerateGaussianByte(Agent.Rng, spell.MpCost, spell.MpCost / Agent.Probabilities.SpellStatStandardDivisor);
            spell.SpellMultiplier = RandomFunctions.GenerateGaussianShort(Agent.Rng, spell.SpellMultiplier, spell.SpellMultiplier / Agent.Probabilities.SpellStatStandardDivisor);
        }
    }
}

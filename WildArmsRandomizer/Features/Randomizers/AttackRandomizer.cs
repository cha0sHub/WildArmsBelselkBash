using WildArmsModel.Model.Attacks;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class AttackRandomizer : IAttackRandomizer
    {
        private IRandomizerAgent Agent { get; }
        private IAttackCollection AttackCollection { get; }

        public AttackRandomizer(IRandomizerAgent agent, IAttackCollection attackCollection)
        {
            Agent = agent;
            AttackCollection = attackCollection;
        }

        public void RandomizeAttackCollection()
        {
            foreach (var attack in AttackCollection.MappedObjectReadOnly)
            {
                RandomizeAttackStats(attack);
            }
            AttackCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void RandomizeAttackStats(AttackObject attack)
        {
            attack.DamageModifier = RandomFunctions.GenerateGaussianShort(Agent.Rng, attack.DamageModifier, attack.DamageModifier / Agent.Probabilities.AttackStatStandardDivisor);
        }
    }
}

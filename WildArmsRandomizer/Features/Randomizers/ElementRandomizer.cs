using System.Collections;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class ElementRandomizer : IElementRandomizer
    {
        private IRandomizerAgent Agent { get; }

        public ElementRandomizer(IRandomizerAgent agent)
        {
            Agent = agent;
        }

        public byte MutateElementalAttribute(byte originalAttribute)
        {
            byte newElementalAttribute = 0x0;
            var mutationRoll = Agent.Rng.NextDouble();
            if (mutationRoll > Agent.Probabilities.EnemyElementalMutationRate)
                return originalAttribute;
            var bits = new BitArray(new byte[] { originalAttribute });
            for (var i = 0; i < bits.Length; i++)
            {
                var mutateBitRoll = Agent.Rng.NextDouble();
                if (mutateBitRoll > Agent.Probabilities.EnemyElementalBitMutationRate)
                    continue;
                bits[i] = !bits[i];
            }
            return newElementalAttribute;
        }
    }
}

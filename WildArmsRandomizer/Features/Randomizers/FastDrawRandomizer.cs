using System;
using System.Collections.Generic;
using WildArmsModel.Model.FastDraws;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class FastDrawRandomizer : IFastDrawRandomizer
    {
        private IRandomizerAgent Agent { get; }
        private IFastDrawCollection FastDrawCollection { get; }

        public FastDrawRandomizer(IRandomizerAgent agent, IFastDrawCollection fastDrawCollection)
        {
            Agent = agent;
            FastDrawCollection = fastDrawCollection;
        }

        public void RandomizeFastDrawCollection()
        {
            foreach (var fastDraw in FastDrawCollection.MappedObjectReadOnly)
            {
                RandomizeFastDrawStats(fastDraw);
            }
            FastDrawCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        public void ShuffleFastDrawOrder()
        {
            var toSwap = new List<Tuple<int, int>>();
            var swapped = new HashSet<int>();
            foreach (var fastDraw in FastDrawCollection.MappedObjectReadOnly)
            {
                var fastDrawId = fastDraw.Id;
                var swapFastDrawId = fastDraw.Id;
                while (swapFastDrawId == fastDrawId)
                {
                    var randNum = Agent.Rng.Next(0, FastDrawCollection.MappedObjectReadOnly.Count);
                    swapFastDrawId = FastDrawCollection.MappedObjectReadOnly[randNum].Id;
                }
                swapped.Add(fastDrawId);
                swapped.Add(swapFastDrawId);
                toSwap.Add(new Tuple<int, int>(fastDrawId, swapFastDrawId));
            }
            foreach (var t in toSwap)
            {
                FastDrawCollection.SwapMappedObjects(Agent.GeneralConfiguration.TempFile, FastDrawCollection.GetMappedObject(t.Item1), FastDrawCollection.GetMappedObject(t.Item2));
            }
            FastDrawCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void RandomizeFastDrawStats(FastDrawObject fastDraw)
        {
            fastDraw.AttackMultiplier = RandomFunctions.GenerateGaussianShort(Agent.Rng, fastDraw.AttackMultiplier, fastDraw.AttackMultiplier / Agent.Probabilities.FastDrawStatStandardDivisor);
            fastDraw.MpUsage = RandomFunctions.GenerateGaussianByte(Agent.Rng, fastDraw.MpUsage, fastDraw.MpUsage / Agent.Probabilities.FastDrawStatStandardDivisor);
        }
    }
}

using System;
using System.Collections.Generic;
using WildArmsModel.Model.Summons;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class SummonRandomizer : ISummonRandomizer
    {
        private IRandomizerAgent Agent { get; }
        private ISummonCollection SummonCollection { get; }

        public SummonRandomizer(IRandomizerAgent agent, ISummonCollection summonCollection)
        {
            Agent = agent;
            SummonCollection = summonCollection;
        }

        public void RandomizeSummonCollection()
        {
            foreach (var summon in SummonCollection.MappedObjectReadOnly)
            {
                RandomizeSummonStats(summon);
            }
        }

        public void ShuffleSummonOrder()
        {
            var toSwap = new List<Tuple<int, int>>();
            var swapped = new HashSet<int>();
            foreach (var summon in SummonCollection.MappedObjectReadOnly)
            {
                var summonId = summon.Id;
                var swapSummonId = summon.Id;
                while (swapSummonId == summonId)
                {
                    var randNum = Agent.Rng.Next(0, SummonCollection.MappedObjectReadOnly.Count);
                    swapSummonId = SummonCollection.MappedObjectReadOnly[randNum].Id;
                }
                swapped.Add(summonId);
                swapped.Add(swapSummonId);
                toSwap.Add(new Tuple<int, int>(summonId, swapSummonId));
            }
            foreach (var t in toSwap)
            {
                SummonCollection.SwapMappedObjects(Agent.GeneralConfiguration.TempFile, SummonCollection.GetMappedObject(t.Item1), SummonCollection.GetMappedObject(t.Item2));
            }
            SummonCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void RandomizeSummonStats(SummonObject summon)
        {
            summon.Multiplier = RandomFunctions.GenerateGaussianByte(Agent.Rng, summon.Multiplier, summon.Multiplier / Agent.Probabilities.FastDrawStatStandardDivisor);
        }
    }
}

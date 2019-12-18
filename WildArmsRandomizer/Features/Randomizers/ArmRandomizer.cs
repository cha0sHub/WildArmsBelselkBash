using System;
using System.Collections.Generic;
using WildArmsModel.Model.Arms;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class ArmRandomizer : IArmRandomizer
    {
        private IRandomizerAgent Agent { get; }
        private IArmCollection ArmCollection { get; }

        public ArmRandomizer(IRandomizerAgent agent, IArmCollection armCollection)
        {
            Agent = agent;
            ArmCollection = armCollection;
        }

        public void RandomizeArmCollection()
        {
            foreach (var arm in ArmCollection.MappedObjectReadOnly)
            {
                RandomizeArmStats(arm);
            }
        }

        public void ShuffleArmOrder()
        {
            var toSwap = new List<Tuple<int, int>>();
            var swapped = new HashSet<int>();
            foreach (var arm in ArmCollection.MappedObjectReadOnly)
            {
                var armId = arm.Id;
                var swapArmId = arm.Id;
                while (swapArmId == armId)
                {
                    var randNum = Agent.Rng.Next(0, ArmCollection.MappedObjectReadOnly.Count);
                    swapArmId = ArmCollection.MappedObjectReadOnly[randNum].Id;
                }
                swapped.Add(armId);
                swapped.Add(swapArmId);
                toSwap.Add(new Tuple<int, int>(armId, swapArmId));
            }
            foreach (var t in toSwap)
            {
                ArmCollection.SwapMappedObjects(Agent.GeneralConfiguration.TempFile, ArmCollection.GetMappedObject(t.Item1), ArmCollection.GetMappedObject(t.Item2));
            }
            ArmCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void RandomizeArmStats(ArmObject arm)
        {
            arm.InitialAccuracy = RandomFunctions.GenerateGaussianByte(Agent.Rng, arm.InitialAccuracy, arm.InitialAccuracy / Agent.Probabilities.ArmStatStandardDivisor);
            arm.InitialAtp = RandomFunctions.GenerateGaussianShort(Agent.Rng, arm.InitialAtp, arm.InitialAtp / Agent.Probabilities.ArmStatStandardDivisor);
            arm.InitialBullets = RandomFunctions.GenerateGaussianByte(Agent.Rng, arm.InitialBullets, arm.InitialBullets / Agent.Probabilities.ArmStatStandardDivisor);
        }

    }
}

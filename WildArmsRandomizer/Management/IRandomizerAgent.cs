using System;
using WildArmsRandomizer.Configuration;

namespace WildArmsRandomizer.Management
{
    public interface IRandomizerAgent
    {
        FeatureSet Features { get; }
        GeneralConfiguration GeneralConfiguration { get; }
        ProbabilitySet Probabilities { get; }
        Random Rng { get; }
        void InitializeRng();
    }
}
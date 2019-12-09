using System;
using WildArmsRandomizer.Configuration;

namespace WildArmsRandomizer.Management
{
    /// <summary>
    /// Passed in from UI or console to supply randomizer with configuration it needs
    /// </summary>
    public class RandomizerAgent : IRandomizerAgent
    {
        public Random Rng { get; private set; }
        public GeneralConfiguration GeneralConfiguration { get; }
        public FeatureSet Features { get; }
        public ProbabilitySet Probabilities { get; }

        public RandomizerAgent()
        {
            Features = new FeatureSet();
            Probabilities = new ProbabilitySet();
            GeneralConfiguration = new GeneralConfiguration();
        }
        public void InitializeRng()
        {
            Rng = new Random(GeneralConfiguration.Seed.GetHashCode());
        }
    }
    
    
}

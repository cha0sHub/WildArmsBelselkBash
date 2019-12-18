using Microsoft.Extensions.DependencyInjection;
using System;
using WildArmsModel.DependencyInjection;
using WildArmsRandomizer.DependencyInjection;
using WildArmsRandomizer.Management;

namespace RandomizerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddWildArmsModelServices();
            serviceCollection.AddWildArmsRandomizerServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var randomizerAgent = serviceProvider.GetRequiredService<IRandomizerAgent>();
            randomizerAgent.GeneralConfiguration.InputFile = "wild_arms.bin";
            randomizerAgent.GeneralConfiguration.TempFile = "wa1.bin";
            randomizerAgent.GeneralConfiguration.Seed = DateTime.UtcNow.Ticks.ToString();
            randomizerAgent.GeneralConfiguration.OutputFile = $"wildarms{randomizerAgent.GeneralConfiguration.Seed}.bin";
            var randomizerManager = serviceProvider.GetRequiredService<IRandomizerManager>();
            randomizerManager.RunRandomizer();
        }
    }
}

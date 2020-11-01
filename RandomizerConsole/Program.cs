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
            randomizerAgent.GeneralConfiguration.InputFile = "wild_arms1.bin";
            randomizerAgent.GeneralConfiguration.TempFile = "wa1.bin";
            randomizerAgent.GeneralConfiguration.Seed = DateTime.UtcNow.Ticks.ToString();
            randomizerAgent.GeneralConfiguration.OutputFile = $"wild_arms.bin";
            randomizerAgent.GeneralConfiguration.RunAnalysis = true;
            if (args.Length > 0 && args.Length != 3)
            {
                Console.WriteLine("Usage: [input_file] [output_file] [seed]");
                return;
            }
            if (args.Length == 3)
            {
                randomizerAgent.GeneralConfiguration.InputFile = args[0];
                randomizerAgent.GeneralConfiguration.OutputFile = args[1];
                randomizerAgent.GeneralConfiguration.Seed = args[2];
            }
            var randomizerManager = serviceProvider.GetRequiredService<IRandomizerManager>();
            randomizerManager.RunRandomizer();
        }
    }
}

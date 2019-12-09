using System;
using System.Collections.Generic;
using System.Text;
using Utilities;
using WildArmsRandomizer.Features.Modes;
using WildArmsRandomizer.Features.Options;
using WildArmsRandomizer.Features.Randomizers;

namespace WildArmsRandomizer.Management
{
    internal class RandomizerManager
    {
        private ISparsityMode SparsityMode { get; }
        private IThreeOrbMode ThreeOrbMode { get; }

        private IAlwaysRunOption AlwaysRunOption { get; }
        private ICeciliaSpellsOption CeciliaSpellsOption { get; }
        private IItemPriceCorrectionOption ItemPriceCorrectionOption { get; }
        private ISwitchAnywhereOption SwitchAnywhereOption { get; }
        private IVehiclesAvailableOption VehiclesAvailableOption { get; }
        private IEventReducerOption EventReducerOption { get; }

        private IAreaRandomizer AreaRandomizer { get; }
        private IEnemyRandomizer EnemyRandomizer { get; }
        private IItemRandomizer ItemRandomizer { get; }

        private IRandomizerAgent Agent { get; }

        public RandomizerManager(ISparsityMode sparsityMode, IThreeOrbMode threeOrbMode, IAlwaysRunOption alwaysRunOption,
                                 ICeciliaSpellsOption ceciliaSpellsOption, IItemPriceCorrectionOption itemPriceCorrectionOption,
                                 ISwitchAnywhereOption switchAnywhereOption, IVehiclesAvailableOption vehiclesAvailableOption,
                                 IAreaRandomizer areaRandomizer, IEnemyRandomizer enemyRandomizer, IItemRandomizer itemRandomizer,
                                 IEventReducerOption eventReducerOption, IRandomizerAgent agent)
        {
            SparsityMode = sparsityMode;
            ThreeOrbMode = threeOrbMode;
            AlwaysRunOption = alwaysRunOption;
            CeciliaSpellsOption = ceciliaSpellsOption;
            ItemPriceCorrectionOption = itemPriceCorrectionOption;
            SwitchAnywhereOption = switchAnywhereOption;
            VehiclesAvailableOption = vehiclesAvailableOption;
            EventReducerOption = eventReducerOption;
            AreaRandomizer = areaRandomizer;
            EnemyRandomizer = enemyRandomizer;
            ItemRandomizer = itemRandomizer;
            Agent = agent;
        }

        public void RunRandomizer()
        {
            Agent.InitializeRng();
            Uniso.RemoveSectorMetadata(Agent.GeneralConfiguration.InputFile, Agent.GeneralConfiguration.TempFile);
            ApplyOptions();
            ApplyRandomizers();
            ApplyModes();
            Uniso.InjectLogicalSectors(Agent.GeneralConfiguration.TempFile, Agent.GeneralConfiguration.InputFile, Agent.GeneralConfiguration.OutputFile);
        }

        private void ApplyOptions()
        { 
            
        }

        private void ApplyRandomizers()
        { 
        
        }

        private void ApplyModes()
        { 
        
        }
    }
}

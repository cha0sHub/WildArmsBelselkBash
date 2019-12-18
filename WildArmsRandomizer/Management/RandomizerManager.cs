using System.IO;
using Utilities;
using WildArmsModel.Model.Areas;
using WildArmsModel.Model.Arms;
using WildArmsModel.Model.Attacks;
using WildArmsModel.Model.Enemies;
using WildArmsModel.Model.FastDraws;
using WildArmsModel.Model.Items;
using WildArmsModel.Model.Spells;
using WildArmsModel.Model.Summons;
using WildArmsRandomizer.Features.Modes;
using WildArmsRandomizer.Features.Options;
using WildArmsRandomizer.Features.Randomizers;

namespace WildArmsRandomizer.Management
{
    internal class RandomizerManager : IRandomizerManager
    {
        private IAttackCollection AttackCollection { get; }
        private IArmCollection ArmCollection { get; }
        private IFastDrawCollection FastDrawCollection { get; }
        private ISpellCollection SpellCollection { get; }
        private ISummonCollection SummonCollection { get; }
        private IEnemyCollection EnemyCollection { get; }
        private IItemCollection ItemCollection { get; }
        private IAreaCollection AreaCollection { get; }

        private ISparsityMode SparsityMode { get; }
        private IThreeOrbMode ThreeOrbMode { get; }

        private IAlwaysRunOption AlwaysRunOption { get; }
        private IBossRebalancerOption BossRebalancerOption { get; }
        private ICeciliaSpellsOption CeciliaSpellsOption { get; }
        private IEventReducerOption EventReducerOption { get; }
        private IExperienceFlattenerOption ExperienceFlattenerOption { get; }
        private IItemPriceCorrectionOption ItemPriceCorrectionOption { get; }
        private ISwitchAnywhereOption SwitchAnywhereOption { get; }
        private IUberBelselkOption UberBelselkOption { get; }
        private IVehiclesAvailableOption VehiclesAvailableOption { get; }
        private ISoloFightBufferOption SoloFightBufferOption { get; }


        private IAreaRandomizer AreaRandomizer { get; }
        private IArmRandomizer ArmRandomizer { get; }
        private IAttackRandomizer AttackRandomizer { get; }
        private IEnemyRandomizer EnemyRandomizer { get; }
        private IFastDrawRandomizer FastDrawRandomizer { get; }
        private IItemRandomizer ItemRandomizer { get; }
        private ISpellRandomizer SpellRandomizer { get; }
        private ISummonRandomizer SummonRandomizer { get; }

        private IRandomizerAgent Agent { get; }

        public RandomizerManager(IAttackCollection attackCollection,
                                 IArmCollection armCollection,
                                 IFastDrawCollection fastDrawCollection,
                                 ISpellCollection spellCollection,
                                 ISummonCollection summonCollection,
                                 IEnemyCollection enemyCollection,
                                 IItemCollection itemCollection,
                                 IAreaCollection areaCollection,
                                 ISparsityMode sparsityMode,
                                 IThreeOrbMode threeOrbMode,
                                 IAlwaysRunOption alwaysRunOption,
                                 IBossRebalancerOption bossRebalancerOption,
                                 ICeciliaSpellsOption ceciliaSpellsOption,
                                 IEventReducerOption eventReducerOption,
                                 IExperienceFlattenerOption experienceFlattenerOption,
                                 IItemPriceCorrectionOption itemPriceCorrectionOption,
                                 ISwitchAnywhereOption switchAnywhereOption,
                                 IUberBelselkOption uberBelselkOption,
                                 IVehiclesAvailableOption vehiclesAvailableOption,
                                 ISoloFightBufferOption soloFightBufferOption,
                                 IAreaRandomizer areaRandomizer,
                                 IArmRandomizer armRandomizer,
                                 IAttackRandomizer attackRandomizer,
                                 IEnemyRandomizer enemyRandomizer,
                                 IFastDrawRandomizer fastDrawRandomizer,
                                 IItemRandomizer itemRandomizer,
                                 ISpellRandomizer spellRandomizer,
                                 ISummonRandomizer summonRandomizer,
                                 IRandomizerAgent agent)
        {
            AttackCollection = attackCollection;
            ArmCollection = armCollection;
            FastDrawCollection = fastDrawCollection;
            SpellCollection = spellCollection;
            SummonCollection = summonCollection;
            EnemyCollection = enemyCollection;
            ItemCollection = itemCollection;
            AreaCollection = areaCollection;
            SparsityMode = sparsityMode;
            ThreeOrbMode = threeOrbMode;
            AlwaysRunOption = alwaysRunOption;
            BossRebalancerOption = bossRebalancerOption;
            CeciliaSpellsOption = ceciliaSpellsOption;
            ItemPriceCorrectionOption = itemPriceCorrectionOption;
            ExperienceFlattenerOption = experienceFlattenerOption;
            SwitchAnywhereOption = switchAnywhereOption;
            VehiclesAvailableOption = vehiclesAvailableOption;
            EventReducerOption = eventReducerOption;
            UberBelselkOption = uberBelselkOption;
            SoloFightBufferOption = soloFightBufferOption;
            AreaRandomizer = areaRandomizer;
            ArmRandomizer = armRandomizer;
            AttackRandomizer = attackRandomizer;
            EnemyRandomizer = enemyRandomizer;
            FastDrawRandomizer = fastDrawRandomizer;
            ItemRandomizer = itemRandomizer;
            SpellRandomizer = spellRandomizer;
            SummonRandomizer = summonRandomizer;
            Agent = agent;
        }

        public void RunRandomizer()
        {
            if (File.Exists(Agent.GeneralConfiguration.TempFile))
            {
                File.Delete(Agent.GeneralConfiguration.TempFile);
            }
            Agent.InitializeRng();
            Uniso.RemoveSectorMetadata(Agent.GeneralConfiguration.InputFile, Agent.GeneralConfiguration.TempFile);
            LoadData();
            ApplyOptions();
            ApplyRandomizers();
            ApplyModes();
            Uniso.InjectLogicalSectors(Agent.GeneralConfiguration.TempFile, Agent.GeneralConfiguration.InputFile, Agent.GeneralConfiguration.OutputFile);
        }

        private void LoadData()
        {
            AttackCollection.ReadObjects(Agent.GeneralConfiguration.TempFile);
            ArmCollection.ReadObjects(Agent.GeneralConfiguration.TempFile);
            FastDrawCollection.ReadObjects(Agent.GeneralConfiguration.TempFile);
            SpellCollection.ReadObjects(Agent.GeneralConfiguration.TempFile);
            SummonCollection.ReadObjects(Agent.GeneralConfiguration.TempFile);
            EnemyCollection.ReadObjects(Agent.GeneralConfiguration.TempFile);
            ItemCollection.ReadObjects(Agent.GeneralConfiguration.TempFile);
            AreaCollection.ReadObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void ApplyOptions()
        {
            if (Agent.Features.AlwaysRunOption)
            {
                AlwaysRunOption.ApplyAlwaysRunOption();
            }
            if (Agent.Features.RebalanceBossesOption)
            {
                BossRebalancerOption.RebalanceBosses();
            }
            if (Agent.Features.CeciliaSpellsOption)
            {
                CeciliaSpellsOption.ChangeCeciliaStartingSpells();
            }
            if (Agent.Features.EventReducerOption)
            {
                EventReducerOption.ReduceEvents();
            }
            if (Agent.Features.ExperienceFlattenerOption)
            {
                ExperienceFlattenerOption.FlattenExperience();
            }
            if (Agent.Features.ItemPriceCorrectionOption)
            {
                ItemPriceCorrectionOption.ApplyCorrections();
            }
            if (Agent.Features.SwitchAnywhereOption)
            {
                SwitchAnywhereOption.ApplySwitchAnywhereOption();
            }
            if (Agent.Features.UberBelselkOption)
            {
                //UberBelselkOption.ApplyUberBelselkOption();
            }
            if (Agent.Features.VehiclesAvailableOption)
            {
                VehiclesAvailableOption.ApplyVehiclesAvailableOption();
            }
            if (Agent.Features.SoloFightBufferOption)
            {
                SoloFightBufferOption.ApplySoloFightBuffer();
            }
        }

        private void ApplyRandomizers()
        {
            
            if (Agent.Features.RandomizeEnemyData)
            {
                EnemyRandomizer.RandomizeEnemyCollection();
            }
            
            if (Agent.Features.RandomizeEquipmentData)
            {
                ItemRandomizer.RandomizeEquipment();
            }
            
            if (Agent.Features.RandomizeShopLists)
            {
                AreaRandomizer.RandomizeShopLists();
            }
            
            if (Agent.Features.RandomizeFoundItems)
            {
                AreaRandomizer.RandomizeFoundItems();
            }
            if (Agent.Features.ShuffleBossEncounters)
            {
                EnemyRandomizer.ShuffleBossOrder();
            }
            if (Agent.Features.RandomizeArmData)
            {
                ArmRandomizer.RandomizeArmCollection();
            }
            
            if (Agent.Features.ShuffleArmOrder)
            {
                //ArmRandomizer.ShuffleArmOrder();
            }
            if (Agent.Features.RandomizeFastDrawData)
            {
                FastDrawRandomizer.RandomizeFastDrawCollection();
            }
            if (Agent.Features.RandomizeFastDrawData)
            {
                //FastDrawRandomizer.ShuffleFastDrawOrder();
            }
            if (Agent.Features.RandomizeSpellData)
            {
                SpellRandomizer.RandomizeSpellCollection();
            }
            if (Agent.Features.RandomizeSummonData)
            {
                SummonRandomizer.RandomizeSummonCollection();
            }
            if (Agent.Features.ShuffleSummonOrder)
            {
                //SummonRandomizer.ShuffleSummonOrder();
            }
            if (Agent.Features.RandomizeAttackData)
            {
                AttackRandomizer.RandomizeAttackCollection();
            }
        }

        private void ApplyModes()
        {
            if (Agent.Features.SparsityMode)
            {
                SparsityMode.ApplySparsityMode();
            }
            if (Agent.Features.ThreeOrbMode)
            {
                ThreeOrbMode.ApplyThreeOrbMode();
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using WildArmsRandomizer.Features.Modes;
using WildArmsRandomizer.Features.Options;
using WildArmsRandomizer.Features.Randomizers;
using WildArmsRandomizer.Lists;
using WildArmsRandomizer.Management;
using WildArmsRandomizer.Properties;

namespace WildArmsRandomizer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddWildArmsRandomizerServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IRandomizerAgent, RandomizerAgent>();
            serviceCollection.AddTransient<IRandomizerManager, RandomizerManager>();

            serviceCollection.AddTransient<ISparsityMode, SparsityMode>();
            serviceCollection.AddTransient<IThreeOrbMode, ThreeOrbMode>();

            serviceCollection.AddTransient<IAlwaysRunOption, AlwaysRunOption>();
            serviceCollection.AddTransient<IBossRebalancerOption, BossRebalancerOption>();
            serviceCollection.AddTransient<ICeciliaSpellsOption, CeciliaSpellsOption>();
            serviceCollection.AddTransient<IEventReducerOption, EventReducerOption>();
            serviceCollection.AddTransient<IExperienceFlattenerOption, ExperienceFlattenerOption>();
            serviceCollection.AddTransient<IItemPriceCorrectionOption, ItemPriceCorrectionOption>();
            serviceCollection.AddTransient<ISwitchAnywhereOption, SwitchAnywhereOption>();
            serviceCollection.AddTransient<IUberBelselkOption, UberBelselkOption>();
            serviceCollection.AddTransient<IVehiclesAvailableOption, VehiclesAvailableOption>();
            serviceCollection.AddTransient<ISoloFightBufferOption, SoloFightBufferOption>();

            serviceCollection.AddTransient<IAreaRandomizer, AreaRandomizer>();
            serviceCollection.AddTransient<IArmRandomizer, ArmRandomizer>();
            serviceCollection.AddTransient<IAttackRandomizer, AttackRandomizer>();
            serviceCollection.AddTransient<IElementRandomizer, ElementRandomizer>();
            serviceCollection.AddTransient<IEnemyRandomizer, EnemyRandomizer>();
            serviceCollection.AddTransient<IEventRandomizer, EventRandomizer>();
            serviceCollection.AddTransient<IFastDrawRandomizer, FastDrawRandomizer>();
            serviceCollection.AddTransient<IItemRandomizer, ItemRandomizer>();
            serviceCollection.AddTransient<IShoppingListRandomizer, ShoppingListRandomizer>();
            serviceCollection.AddTransient<ISpellRandomizer, SpellRandomizer>();
            serviceCollection.AddTransient<ISummonRandomizer, SummonRandomizer>();

            var attackTierList = new AttackTierList();
            attackTierList.LoadData(Resources.AttackTierList);
            serviceCollection.AddSingleton<IAttackTierList>(attackTierList);

            var bossOrbTierList = new BossOrbTierList();
            bossOrbTierList.LoadData(Resources.BossOrbGroups);
            serviceCollection.AddSingleton<IBossOrbTierList>(bossOrbTierList);

            var enemyTierList = new EnemyTierList();
            enemyTierList.LoadData(Resources.BossTierList);
            serviceCollection.AddSingleton<IEnemyTierList>(enemyTierList);

            var itemTierList = new ItemTierList();
            itemTierList.LoadData(Resources.ItemTierList);
            serviceCollection.AddSingleton<IItemTierList>(itemTierList);

            return serviceCollection;
        }

    }
}

using Microsoft.Extensions.DependencyInjection;
using WildArmsModel.EventParsing;
using WildArmsModel.Model.Areas;
using WildArmsModel.Model.Arms;
using WildArmsModel.Model.Attacks;
using WildArmsModel.Model.Enemies;
using WildArmsModel.Model.FastDraws;
using WildArmsModel.Model.Items;
using WildArmsModel.Model.Spells;
using WildArmsModel.Model.Summons;

namespace WildArmsModel.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddWildArmsModelServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAttackCollection, AttackCollection>();
            serviceCollection.AddSingleton<IArmCollection, ArmCollection>();
            serviceCollection.AddSingleton<IFastDrawCollection, FastDrawCollection>();
            serviceCollection.AddSingleton<ISpellCollection, SpellCollection>();
            serviceCollection.AddSingleton<ISummonCollection, SummonCollection>();
            serviceCollection.AddSingleton<IEnemyCollection, EnemyCollection>();
            serviceCollection.AddSingleton<IItemCollection, ItemCollection>();
            serviceCollection.AddSingleton<IAreaCollection, AreaCollection>();
            serviceCollection.AddTransient<IEventParser, EventParser>();

            return serviceCollection;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
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

        public static IServiceCollection AddWildArmsModelServices(this IServiceCollection serviceCollection, string fileName)
        {
            var attackCollection = new AttackCollection();
            attackCollection.ReadObjects(fileName);
            attackCollection.SetAttackNames();
            serviceCollection.AddSingleton<IAttackCollection>(attackCollection);

            var armCollection = new ArmCollection();
            armCollection.ReadObjects(fileName);
            armCollection.SetArmNames();
            serviceCollection.AddSingleton<IArmCollection>(armCollection);

            var fastDrawCollection = new FastDrawCollection();
            fastDrawCollection.ReadObjects(fileName);
            fastDrawCollection.SetFastDrawNames();
            serviceCollection.AddSingleton<IFastDrawCollection>(fastDrawCollection);

            var spellCollection = new SpellCollection();
            spellCollection.ReadObjects(fileName);
            spellCollection.SetSpellNames();
            serviceCollection.AddSingleton<ISpellCollection>(spellCollection);

            var summonCollection = new SummonCollection();
            summonCollection.ReadObjects(fileName);
            summonCollection.SetSummonNames();
            serviceCollection.AddSingleton<ISummonCollection>(summonCollection);

            var enemyCollection = new EnemyCollection();
            enemyCollection.ReadObjects(fileName);
            enemyCollection.SetEnemyNames();
            serviceCollection.AddSingleton<IEnemyCollection>(enemyCollection);

            var itemCollection = new ItemCollection();
            itemCollection.ReadObjects(fileName);
            itemCollection.SetItemNames();
            serviceCollection.AddSingleton<IItemCollection>(itemCollection);

            var areaCollection = new AreaCollection();
            areaCollection.ReadObjects(fileName);
            areaCollection.SetAreaNames();
            serviceCollection.AddSingleton<IAreaCollection, AreaCollection>();


            return serviceCollection;
        }
    }
}

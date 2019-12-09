using WildArmsModel.Model.Enemies;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal interface IEnemyRandomizer
    {
        void MutateEnemyDrops(EnemyObject enemy);
        void RandomizeEnemyCollection();
        void ShuffleBossOrder();
    }
}
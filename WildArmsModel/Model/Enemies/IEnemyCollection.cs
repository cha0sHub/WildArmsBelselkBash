using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Enemies
{
    public interface IEnemyCollection : IDiscMappedCollection<EnemyObject>
    {
        IReadOnlyList<EnemyObject> Enemies { get; }

        void AddEnemyAttackData(EnemyAttack attack);
        EnemyAttack GetAttack(int attackId);
    }
}
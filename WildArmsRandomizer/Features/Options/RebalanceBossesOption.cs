using System;
using WildArmsModel.Model.Enemies;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    //Rebalances bosses up to a certain point to compensate for player's party being more powerful than they would be in standard game.
    internal class BossRebalancer : IBossRebalancer
    {
        private IRandomizerAgent Agent { get; }
        private IEnemyCollection EnemyCollection { get; }

        public BossRebalancer(IRandomizerAgent agent, IEnemyCollection enemyCollection)
        {
            Agent = agent;
            EnemyCollection = enemyCollection;
        }

        public void RebalanceBosses()
        {
            foreach (var enemy in EnemyCollection.MappedObjectReadOnly)
            {
                RebalanceBoss(enemy);
            }
            EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }
        private void RebalanceBoss(EnemyObject enemy)
        {
            enemy.Hp = (ushort)Math.Min(enemy.Hp * Agent.Probabilities.RebalanceScale, ushort.MaxValue);
            enemy.Atp = (ushort)Math.Min(enemy.Atp * Agent.Probabilities.RebalanceScale, 999);
            enemy.Sor = (ushort)Math.Min(enemy.Sor * Agent.Probabilities.RebalanceScale, 999);
            enemy.Dfp = (ushort)Math.Min(enemy.Dfp * Agent.Probabilities.RebalanceScale, 999);
            enemy.Mgr = (ushort)Math.Min(enemy.Mgr * Agent.Probabilities.RebalanceScale, 999);
            enemy.Res = (ushort)Math.Min(enemy.Res * Agent.Probabilities.RebalanceScale, 999);
            enemy.Pry = (ushort)Math.Min(enemy.Pry * Agent.Probabilities.RebalanceScale, 99);
        }
    }
}

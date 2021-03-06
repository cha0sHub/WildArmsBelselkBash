﻿using System;
using System.Collections.Generic;
using WildArmsModel.Model.Enemies;
using WildArmsRandomizer.Lists;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    //Rebalances bosses up to a certain point to compensate for player's party being more powerful than they would be in standard game.
    internal class BossRebalancerOption : IBossRebalancerOption
    {
        private static HashSet<int> RebalanceTiers = new HashSet<int>() { 0, 1, 2, 3, 4, 5, 6 };

        private IRandomizerAgent Agent { get; }
        private IEnemyCollection EnemyCollection { get; }
        private IEnemyTierList EnemyTierList { get; }

        public BossRebalancerOption(IRandomizerAgent agent, IEnemyCollection enemyCollection, IEnemyTierList enemyTierList)
        {
            Agent = agent;
            EnemyCollection = enemyCollection;
            EnemyTierList = enemyTierList;
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
            if (!RebalanceTiers.Contains(EnemyTierList.GetBossTier(enemy.Id)))
                return;
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

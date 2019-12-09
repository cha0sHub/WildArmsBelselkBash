using System;
using System.Collections.Generic;
using WildArmsModel.Model.Enemies;
using WildArmsModel.Model.Items;
using WildArmsRandomizer.Helper;
using WildArmsRandomizer.Lists;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Randomizers
{
    internal class EnemyRandomizer : IEnemyRandomizer
    {
        private const string AtpType = "|ATP|";
        private const string SorType = "|SOR|";
        private const string EffType = "|EFF|";

        private IRandomizerAgent Agent { get; }
        private IEnemyCollection EnemyCollection { get; }
        private IItemCollection ItemCollection { get; }
        private IAttackTierList AttackTierList { get; }
        private IItemTierList ItemTierList { get; }
        private IEnemyTierList EnemyTierList { get; }
        private IElementRandomizer ElementRandomizer { get; }
        private IItemRandomizer ItemRandomizer { get; }

        public EnemyRandomizer(IRandomizerAgent agent, IEnemyCollection enemyCollection, IItemCollection itemCollection, IAttackTierList attackTierList,
                                IItemTierList itemTierList, IEnemyTierList enemyTierList, IElementRandomizer elementRandomizer, IItemRandomizer itemRandomizer)
        {
            Agent = agent;
            EnemyCollection = enemyCollection;
            ItemCollection = itemCollection;
            AttackTierList = attackTierList;
            ItemTierList = itemTierList;
            EnemyTierList = enemyTierList;
            ElementRandomizer = elementRandomizer;
            ItemRandomizer = itemRandomizer;
        }

        public void RandomizeEnemyCollection()
        {
            foreach (var enemy in EnemyCollection.MappedObjectReadOnly)
            {
                MutateEnemyAttacks(enemy);
                RandomizeEnemyStats(enemy);
                MutateEnemyElementalAttributes(enemy);
                MutateEnemyDrops(enemy);
            }
            EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        public void MutateEnemyDrops(EnemyObject enemy)
        {
            enemy.DropId = ItemRandomizer.GetMutatedItemId(enemy.DropId, Agent.Probabilities.EnemyDropMutationRate, Agent.Probabilities.DefaultItemStandardDeviation);
            enemy.DropRate = RandomFunctions.GenerateGaussianByte(Agent.Rng, enemy.DropRate, Agent.Probabilities.EnemyDropPercentageStandardDeviation, 100);
            enemy.StealId = ItemRandomizer.GetMutatedItemId(enemy.StealId, Agent.Probabilities.EnemyDropMutationRate, Agent.Probabilities.DefaultItemStandardDeviation);
            enemy.StealRate = RandomFunctions.GenerateGaussianByte(Agent.Rng, enemy.StealRate, Agent.Probabilities.EnemyDropPercentageStandardDeviation, 100);
        }

        public void ShuffleBossOrder()
        {
            var toSwap = new List<Tuple<int, int>>();
            var swapped = new HashSet<int>();
            foreach (var bossId in EnemyTierList.BossIds)
            {
                //determine new tier rank
                var swapBossId = bossId;
                byte newTier = 0;
                byte originalTier = 0;
                while (swapBossId == bossId)
                {
                    originalTier = EnemyTierList.GetBossTier(bossId);
                    newTier = RandomFunctions.GenerateGaussianByte(Agent.Rng, originalTier, Agent.Probabilities.BossShuffleStandardDeviation, (byte)(EnemyTierList.TieredBossIds.Count - 1));
                    if (newTier < 0)
                        newTier = 0;
                    //determine what boss to swap with based on new rank
                    var randNum = Agent.Rng.Next(0, EnemyTierList.TieredBossIds[newTier].Count);
                    swapBossId = EnemyTierList.TieredBossIds[newTier][randNum];

                }
                swapped.Add(bossId);
                swapped.Add(swapBossId);
                ScaleEnemies(originalTier, newTier, bossId, swapBossId);
                toSwap.Add(new Tuple<int, int>(bossId, swapBossId));
            }
            EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
            foreach (var t in toSwap)
            {
                EnemyCollection.SwapMappedObjects(Agent.GeneralConfiguration.TempFile, EnemyCollection.GetMappedObject(t.Item1), EnemyCollection.GetMappedObject(t.Item2));
            }
        }

        private void ScaleEnemies(int oldTier, int newTier, int oldBossId, int newBossId)
        {
            if (newTier == oldTier)
                return;
            var oldBoss = EnemyCollection.GetMappedObject(oldBossId);
            var newBoss = EnemyCollection.GetMappedObject(newBossId);
            if (oldTier > newTier)
            {
                ScaleDownEnemy(oldBoss, newTier, oldTier);
                ScaleUpEnemy(newBoss, newTier, oldTier);
            }
            else
            {
                ScaleUpEnemy(oldBoss, oldTier, newTier);
                ScaleDownEnemy(newBoss, oldTier, newTier);
            }
            EnemyTierList.SwapTiers(oldBossId, newBossId);
        }

        private void ScaleDownEnemy(EnemyObject enemy, int newTier, int oldTier)
        {
            for (var i = oldTier - 1; i >= newTier; i--)
            {
                var scale = 1 - EnemyTierList.TierScales[i];
                ScaleEnemyOneStep(enemy, scale);
            }
        }

        private void ScaleUpEnemy(EnemyObject enemy, int oldTier, int newTier)
        {
            for (var i = oldTier; i < newTier; i++)
            {
                var scale = GetInvertedScale(EnemyTierList.TierScales[i]);
                ScaleEnemyOneStep(enemy, scale);
            }
        }

        private static double GetInvertedScale(double scale)
        {
            return 1 / (1 - scale);
        }

        private static void ScaleEnemyOneStep(EnemyObject enemy, double scale)
        {
            enemy.Hp = (ushort)Math.Min(enemy.Hp * scale, ushort.MaxValue);
            enemy.Atp = (ushort)Math.Min(enemy.Atp * scale, 999);
            //TODO: this is too messed up most likely, since SOR doesn't really scale well
            enemy.Sor = (ushort)Math.Min(enemy.Sor * scale, 999);
            enemy.Dfp = (ushort)Math.Min(enemy.Dfp * scale, 999);
            enemy.Mgr = (ushort)Math.Min(enemy.Mgr * scale, 999);
            enemy.Res = (ushort)Math.Min(enemy.Res * scale, 999);
            enemy.Pry = (ushort)Math.Min(enemy.Pry * scale, 99);
            enemy.Gella = (ushort)Math.Min(enemy.Gella * scale, ushort.MaxValue);
            enemy.Xp = (ushort)Math.Min(enemy.Xp * scale, ushort.MaxValue);
        }

        //Simple Gaussian variance for base stats
        private void RandomizeEnemyStats(EnemyObject enemy)
        {
            enemy.Hp = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Hp, enemy.Hp / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Mp = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Mp, enemy.Mp / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Atp = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Atp, enemy.Atp / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Sor = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Sor, enemy.Sor / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Dfp = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Dfp, enemy.Dfp / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Mgr = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Mgr, enemy.Mgr / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Res = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Res, enemy.Res / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Pry = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Pry, enemy.Pry / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Gella = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Gella, enemy.Gella / Agent.Probabilities.EnemyStatStandardDivisor);
            enemy.Xp = RandomFunctions.GenerateGaussianShort(Agent.Rng, enemy.Xp, enemy.Xp / Agent.Probabilities.EnemyStatStandardDivisor);
        }

        //Low mutation rate for enemy elemental attributes
        private void MutateEnemyElementalAttributes(EnemyObject enemy)
        {
            enemy.RawAttackElement = ElementRandomizer.MutateElementalAttribute(enemy.RawAttackElement);
            enemy.RawWeakness = ElementRandomizer.MutateElementalAttribute(enemy.RawWeakness);
            enemy.RawResistance = ElementRandomizer.MutateElementalAttribute(enemy.RawResistance);
            enemy.RawAbsorb = ElementRandomizer.MutateElementalAttribute(enemy.RawAbsorb);
            enemy.RawImmunity = ElementRandomizer.MutateElementalAttribute(enemy.RawImmunity);
        }

        private void MutateEnemyAttacks(EnemyObject enemy)
        {
            enemy.Attack1Id = GetMutatedAttack(enemy.Attack1Id, enemy);
            enemy.Attack2Id = GetMutatedAttack(enemy.Attack2Id, enemy);
            enemy.Attack3Id = GetMutatedAttack(enemy.Attack3Id, enemy);
            enemy.Attack4Id = GetMutatedAttack(enemy.Attack4Id, enemy);
            enemy.Attack5Id = GetMutatedAttack(enemy.Attack5Id, enemy);
            enemy.Attack6Id = GetMutatedAttack(enemy.Attack6Id, enemy);
            enemy.Attack7Id = GetMutatedAttack(enemy.Attack7Id, enemy);

            var hasMagic = EnemyHasMagicAttack(enemy);
            if (hasMagic && enemy.Sor == 0)
            {
                enemy.Sor = Convert.ToUInt16(Math.Round((double)enemy.Atp / 8));
                enemy.Mp = Math.Max((ushort)100, enemy.Mp);
            }
        }

        private bool EnemyHasMagicAttack(EnemyObject enemy)
        {
            if (AttackTierList.GetAttackType(enemy.Attack1Id).Equals(SorType)
                || AttackTierList.GetAttackType(enemy.Attack1Id).Equals(SorType)
                || AttackTierList.GetAttackType(enemy.Attack2Id).Equals(SorType)
                || AttackTierList.GetAttackType(enemy.Attack3Id).Equals(SorType)
                || AttackTierList.GetAttackType(enemy.Attack4Id).Equals(SorType)
                || AttackTierList.GetAttackType(enemy.Attack5Id).Equals(SorType)
                || AttackTierList.GetAttackType(enemy.Attack6Id).Equals(SorType)
                || AttackTierList.GetAttackType(enemy.Attack7Id).Equals(SorType))
                return true;
            return false;
        }

        //treat 0 as none even though its technically shield.
        private byte GetMutatedAttack(byte oldAttackId, EnemyObject enemy)
        {
            //likely that an attack with no associated animation will soft lock the game
            if (oldAttackId == 0 || oldAttackId == 255)
                return oldAttackId;
            var doMutation = Agent.Rng.NextDouble();
            if (doMutation > Agent.Probabilities.EnemyAttackMutationRate)
                return oldAttackId;
            var attackTier = AttackTierList.GetAttackTier(oldAttackId);
            var newTier = RandomFunctions.GenerateGaussianByte(Agent.Rng, attackTier, 1, (byte)(AttackTierList.TieredAtpIds.Count - 1));
            var changeType = Agent.Rng.NextDouble();
            var oldType = AttackTierList.GetAttackType(oldAttackId);
            if (oldType == "")
                return oldAttackId;
            var newType = GetNewType(oldType, Agent.Probabilities.EnemyAttackTypeMutationRate, newTier);

            List<int> attackList = AttackTierList.GetTierList(newType, newTier);

            var attackIndex = Agent.Rng.Next(0, attackList.Count);
            return (byte)attackList[attackIndex];
        }

        private string GetNewType(string oldType, double mutationRate, int tier)
        {
            var changeType = Agent.Rng.NextDouble();
            if (changeType > mutationRate && AttackTierList.GetTierList(oldType, tier).Count > 0)
                return oldType;
            double coinFlip = Agent.Rng.NextDouble();
            var type = FlipType(oldType, coinFlip > 0.5);
            if (AttackTierList.GetTierList(type, tier).Count > 0)
                return type;
            type = FlipType(oldType, coinFlip <= 0.5);
            if (AttackTierList.GetTierList(type, tier).Count > 0)
                return type;
            return oldType;
        }

        private static string FlipType(string type, bool flipDirection)
        {
            if (flipDirection)
            {
                switch (type)
                {
                    case AtpType:
                        return SorType;
                    case SorType:
                    case EffType:
                        return AtpType;
                }
            }
            else
            {
                switch (type)
                {
                    case SorType:
                    case AtpType:
                        return EffType;
                    case EffType:
                        return SorType;
                }
            }
            return AtpType;
        }
    }
}

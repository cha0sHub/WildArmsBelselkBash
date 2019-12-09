using System.Collections.Generic;
using DiscDataManipulation.Model;

namespace WildArmsModel.Model.Enemies
{
    public class EnemyCollection : DiscMappedCollection<EnemyObject>, IEnemyCollection
    {
        //don't mind the offset calculation
        public const long EnemyCollectionOffset = 0x05ACB130 - 0x34 - 252;
        public const int EnemyCount = 420; // =O
        public IReadOnlyList<EnemyObject> Enemies => MappedObjectReadOnly;
        private Dictionary<int, EnemyAttack> EnemyAttackData { get; }
        private List<EnemyAttack> EnemyAttackList { get; }
        public EnemyCollection() : base(EnemyCollectionOffset, EnemyObject.ObjectSize, EnemyObject.DataLength, EnemyCount)
        {
            EnemyAttackData = new Dictionary<int, EnemyAttack>();
            EnemyAttackList = new List<EnemyAttack>();
            SetEnemyAttackNames();
        }

        public void AddEnemyAttackData(EnemyAttack attack)
        {
            if (EnemyAttackData.ContainsKey(attack.Id))
                return;
            EnemyAttackData.Add(attack.Id, attack);
            EnemyAttackList.Add(attack);
        }

        internal void SetEnemyNames()
        {
            var enemyNames = Properties.Resources.EnemyNames.Split('\n');

            for (int i = 0; i < enemyNames.Length; i++)
            {
                if (MappedObjectData.ContainsKey(i))
                {
                    MappedObjectData[i].Name = enemyNames[i].Replace("\r", "").Trim();
                }
            }
        }
        public EnemyAttack GetAttack(int attackId)
        {
            if (EnemyAttackData.ContainsKey(attackId))
                return EnemyAttackData[attackId];
            else return null;
        }
        internal void SetEnemyAttackNames()
        {
            var attacks = Properties.Resources.AttackNames.Split('\n');
            for (int i = 0; i < attacks.Length; i++)
            {
                var attack = new EnemyAttack();
                attack.Id = i;
                attack.Name = attacks[i].Replace("\r", "").Trim();
                AddEnemyAttackData(attack);
            }
            var defaultAttack = new EnemyAttack();
            defaultAttack.Id = 255;
            defaultAttack.Name = "Regular Attack";
            AddEnemyAttackData(defaultAttack);
        }

    }
}

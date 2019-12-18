namespace WildArmsRandomizer.Configuration
{
    public class ProbabilitySet
    {
        public int AttackStatStandardDivisor { get; set; } = 8;
        public int SpellStatStandardDivisor { get; set; } = 4;
        public int ArmStatStandardDivisor { get; set; } = 4;
        public int FastDrawStatStandardDivisor { get; set; } = 4;
        public int SummonStatStandardDivisor { get; set; } = 4;
        public int EnemyStatStandardDivisor { get; set; } = 4;
        public int BossShuffleStandardDeviation { get; set; } = 3;
        public int EnemyAttackStandardDeviation { get; set; } = 1;
        public double EnemyDropPercentageStandardDeviation { get; set; } = 20;

        public double EnemyDropMutationRate { get; set; } = 0.3;
        public double EnemyElementalMutationRate { get; set; } = 0.1;
        public double EnemyElementalBitMutationRate { get; set; } = 0.3;
        public double EnemyAttackMutationRate { get; set; } = 0.4;
        public double EnemyAttackTypeMutationRate { get; set; } = 0.33;

        public int EventItemStandardDeviation { get; set; } = 2;

        public double EventItemMutationRate { get; set; } = 0.9;

        public double DefaultItemMutationRate { get; set; } = 0.5;
        public int DefaultItemStandardDeviation { get; set; } = 2;

        public double RandomizeItemUsageRate { get; set; } = 0.5;
        public double RandomizeItemSlotRate { get; set; } = 0.3;

        public double SparsityModeItemMutationRate { get; set; } = 0.3;

        public double RebalanceScale { get; set; } = 1.2;
    }
}

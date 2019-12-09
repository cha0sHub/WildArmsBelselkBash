namespace WildArmsRandomizer.Configuration
{
    public class FeatureSet
    {
        public bool AlwaysRunOption { get; set; } = true;
        public bool CeciliaSpellsOption { get; set; } = true;
        public bool ItemPriceCorrectionOption { get; set; } = true;
        public bool SwitchAnywhereOption { get; set; } = true;
        public bool VehiclesAvailableOption { get; set; } = true;

        public bool RandomizeEnemyData { get; set; } = true;
        public bool RandomizeEquipmentData { get; set; } = true;
        public bool RandomizeShopLists { get; set; } = true;
        public bool RandomizeFoundItems { get; set; } = true;
        public bool ShuffleBossEncounters { get; set; } = true;
        public bool SparsityMode { get; set; } = true;
        public bool ThreeOrbMode { get; set; } = true;
        public bool RebalanceBosses { get; set; } = true;
    }
}

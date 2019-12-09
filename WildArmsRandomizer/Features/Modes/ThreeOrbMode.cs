using WildArmsModel.Model.Enemies;
using WildArmsRandomizer.Lists;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Modes
{
    internal class ThreeOrbMode : IThreeOrbMode
    {
        private const int RealEye = 245;
        private const int Wings = 246;
        private const int Arms = 247;
        private const int FakeEye = 248;

        private const int ArmsMaldukeArea = 71;
        private const int ArmsEventId = 35;
        private const int WingsMaldukeArea = 72;
        private const int WingsEventId = 15;
        private const int EyeMaldukeArea = 69;
        private const int EyeEventId = 52;

        private const int BoomerangI = 208;
        private const int BoomerangII = 209;
        private const int BoomerangIII = 210;

        private const int LuceidI = 212;
        private const int LuceidII = 213;
        private const int LuceidIII = 214;

        private IRandomizerAgent RandomizerAgent { get; }
        private IBossOrbTierList BossOrbTierList { get; }
        private IEnemyCollection EnemyCollection { get; }

        private string RealEyeArea { get; set; } = string.Empty;
        private string FakeEyeArea { get; set; } = string.Empty;
        private string WingsArea { get; set; } = string.Empty;
        private string ArmsArea { get; set; } = string.Empty;

        public ThreeOrbMode(IRandomizerAgent randomizerAgent, IBossOrbTierList bossOrbTierList, IEnemyCollection enemyCollection)
        {
            RandomizerAgent = randomizerAgent;
            BossOrbTierList = bossOrbTierList;
            EnemyCollection = enemyCollection;
        }

        public void ApplyThreeOrbMode()
        {
            AssignThreeOrbs();
            BuffBoomerangs();

            EnemyCollection.WriteObjects(RandomizerAgent.GeneralConfiguration.TempFile);
        }

        private void BuffBoomerangs()
        {
            SetBossValues(BoomerangI, 20000, 20000);
            SetBossValues(BoomerangII, 40000, 40000);
            SetBossValues(BoomerangIII, 60000, 60000);
        }

        private void SetBossValues(ushort bossId, ushort experience, ushort gella)
        {
            var boss = EnemyCollection.GetMappedObject(bossId);
            boss.Xp = experience;
            boss.Gella = gella;
        }

        private void AssignThreeOrbs()
        {
            var wingsBossId = BossOrbTierList.TieredBossIds[0][RandomizerAgent.Rng.Next(0, BossOrbTierList.TieredBossIds[0].Count)];
            WingsArea = BossOrbTierList.IdToAreaName[wingsBossId];
            var armsBossId = BossOrbTierList.TieredBossIds[1][RandomizerAgent.Rng.Next(0, BossOrbTierList.TieredBossIds[1].Count)];
            ArmsArea = BossOrbTierList.IdToAreaName[armsBossId];
            var fakeEyeBossId = BossOrbTierList.TieredBossIds[2][RandomizerAgent.Rng.Next(0, BossOrbTierList.TieredBossIds[2].Count)];
            FakeEyeArea = BossOrbTierList.IdToAreaName[fakeEyeBossId];
            RealEyeArea = FakeEyeArea;

            var realEyeBossId = 0;
            while (RealEyeArea.Equals(FakeEyeArea))
            {
                realEyeBossId = BossOrbTierList.TieredBossIds[2][RandomizerAgent.Rng.Next(0, BossOrbTierList.TieredBossIds[2].Count)];
                RealEyeArea = BossOrbTierList.IdToAreaName[realEyeBossId];
            }

            var wingsBoss = EnemyCollection.GetMappedObject(wingsBossId);
            wingsBoss.DropId = Wings;
            wingsBoss.DropRate = 100;

            var armsBoss = EnemyCollection.GetMappedObject(armsBossId);
            armsBoss.DropId = Arms;
            armsBoss.DropRate = 100;

            var fakeEyeBoss = EnemyCollection.GetMappedObject(fakeEyeBossId);
            fakeEyeBoss.DropId = FakeEye;
            fakeEyeBoss.DropRate = 100;

            var realEyeBoss = EnemyCollection.GetMappedObject(realEyeBossId);
            realEyeBoss.DropId = RealEye;
            realEyeBoss.DropRate = 100;
        }

    }
}

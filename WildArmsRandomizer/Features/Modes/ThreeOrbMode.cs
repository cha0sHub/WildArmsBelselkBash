using System.Collections.Generic;
using System.IO;
using WildArmsModel.Model.Areas;
using WildArmsModel.Model.Enemies;
using WildArmsModel.Model.Items;
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

        private const int CeciliaSoloFightId = 192;
        private const int JackSoloFightId = 207;

        private IRandomizerAgent Agent { get; }
        private IBossOrbTierList BossOrbTierList { get; }
        private IEnemyCollection EnemyCollection { get; }
        private IItemTierList ItemTierList { get; }
        private IItemCollection ItemCollection { get; }
        private IAreaCollection AreaCollection { get; }

        private string RealEyeArea { get; set; } = string.Empty;
        private string FakeEyeArea { get; set; } = string.Empty;
        private string WingsArea { get; set; } = string.Empty;
        private string ArmsArea { get; set; } = string.Empty;

        public ThreeOrbMode(IRandomizerAgent randomizerAgent, IBossOrbTierList bossOrbTierList, IEnemyCollection enemyCollection, IItemTierList itemTierList, IItemCollection itemCollection, IAreaCollection areaCollection)
        {
            Agent = randomizerAgent;
            BossOrbTierList = bossOrbTierList;
            EnemyCollection = enemyCollection;
            ItemTierList = itemTierList;
            ItemCollection = itemCollection;
            AreaCollection = areaCollection;
        }

        public void ApplyThreeOrbMode()
        {
            AssignThreeOrbs();
            BuffBoomerangs();
            AssignItemDrops();
            SetFakeEyeSortId();
            RemoveMaldukeOrbs();
            DebuffSoloFights();
            AreaCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
            ItemCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
            EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
            CreateThreeOrbTextFile();
        }

        private void DebuffSoloFights()
        {
            var ceciliaSoloFight = EnemyCollection.GetMappedObject(CeciliaSoloFightId);
            ceciliaSoloFight.Atp = (ushort)(ceciliaSoloFight.Atp / 2);
            ceciliaSoloFight.Sor = (ushort)(ceciliaSoloFight.Sor / 2);
            ceciliaSoloFight.Xp = 20000;
            ceciliaSoloFight.Gella = 20000;
            ceciliaSoloFight.DropId = 124; //Necronomicon
            ceciliaSoloFight.DropRate = 100;

            var jackSoloFight = EnemyCollection.GetMappedObject(JackSoloFightId);
            jackSoloFight.Atp = (ushort)(jackSoloFight.Atp / 2);
            jackSoloFight.Sor = (ushort)(jackSoloFight.Sor / 2);
            jackSoloFight.Xp = 20000;
            jackSoloFight.Gella = 20000;
            jackSoloFight.DropId = 59; //Juggernaut
            jackSoloFight.DropRate = 100;
        }

        private void RemoveMaldukeOrbs()
        {
            var armArea = AreaCollection.GetMappedObject(ArmsMaldukeArea);
            armArea.EventData.GetMappedObject(ArmsEventId).ItemId = 0;

            var wingArea = AreaCollection.GetMappedObject(WingsMaldukeArea);
            wingArea.EventData.GetMappedObject(WingsEventId).ItemId = 0;

            var eyeArea = AreaCollection.GetMappedObject(EyeMaldukeArea);
            eyeArea.EventData.GetMappedObject(EyeEventId).ItemId = 0;
        }

        private void CreateThreeOrbTextFile()
        {
            var fakeEyeFirst = Agent.Rng.Next(0, 2);
            using (var sw = File.CreateText("threeorbs.txt"))
            {
                sw.WriteLine($"Wings are in {WingsArea}");
                sw.WriteLine($"Arms are in {ArmsArea}");
                if (fakeEyeFirst == 1)
                {
                    sw.WriteLine($"Eyes are in {FakeEyeArea} and {RealEyeArea}");
                }
                else
                {
                    sw.WriteLine($"Eyes are in {RealEyeArea} and {FakeEyeArea}");
                }
            }
        }

        private void SetFakeEyeSortId()
        {
            var fakeEye = ItemCollection.GetMappedObject(FakeEye);
            fakeEye.SortId = 26;
        }

        private void AssignItemDrops()
        {
            foreach (var id in BossOrbTierList.TieredBossIds[0])
            {
                var boss = EnemyCollection.GetMappedObject(id);
                if (boss.DropId == Wings)
                {
                    continue;
                }
                var potentialItemIds = new List<int>();
                potentialItemIds.AddRange(ItemTierList.TieredItemList[5]);
                potentialItemIds.AddRange(ItemTierList.TieredItemList[6]);
                potentialItemIds.AddRange(ItemTierList.TieredItemList[7]);
                var item = ItemCollection.GetMappedObject(0);
                while (item.EquipSlot == ItemMasks.EquipSlot.None)
                {
                    item = ItemCollection.GetMappedObject(potentialItemIds[Agent.Rng.Next(0, potentialItemIds.Count)]);
                }
                boss.DropId = (byte)item.Id;
                boss.DropRate = 100;
            }
            foreach (var id in BossOrbTierList.TieredBossIds[1])
            {
                var boss = EnemyCollection.GetMappedObject(id);
                if (boss.DropId == Arms)
                {
                    continue;
                }
                var potentialItemIds = new List<int>();
                potentialItemIds.AddRange(ItemTierList.TieredItemList[8]);
                potentialItemIds.AddRange(ItemTierList.TieredItemList[9]);
                potentialItemIds.AddRange(ItemTierList.TieredItemList[10]);
                var item = ItemCollection.GetMappedObject(0);
                while (item.EquipSlot == ItemMasks.EquipSlot.None)
                {
                    item = ItemCollection.GetMappedObject(potentialItemIds[Agent.Rng.Next(0, potentialItemIds.Count)]);
                }
                boss.DropId = (byte)item.Id;
                boss.DropRate = 100;
            }
            foreach (var id in BossOrbTierList.TieredBossIds[2])
            {
                var boss = EnemyCollection.GetMappedObject(id);
                if (boss.DropId == RealEye || boss.DropId == FakeEye)
                {
                    continue;
                }
                var potentialItemIds = new List<int>();
                potentialItemIds.AddRange(ItemTierList.TieredItemList[11]);
                potentialItemIds.AddRange(ItemTierList.TieredItemList[12]);
                potentialItemIds.AddRange(ItemTierList.TieredItemList[13]);
                potentialItemIds.AddRange(ItemTierList.TieredItemList[14]);
                var item = ItemCollection.GetMappedObject(0);
                while (item.EquipSlot == ItemMasks.EquipSlot.None)
                {
                    item = ItemCollection.GetMappedObject(potentialItemIds[Agent.Rng.Next(0, potentialItemIds.Count)]);
                }
                boss.DropId = (byte)item.Id;
                boss.DropRate = 100;
            }
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
            var wingsBossId = BossOrbTierList.TieredBossIds[0][Agent.Rng.Next(0, BossOrbTierList.TieredBossIds[0].Count)];
            WingsArea = BossOrbTierList.IdToAreaName[wingsBossId];
            var armsBossId = BossOrbTierList.TieredBossIds[1][Agent.Rng.Next(0, BossOrbTierList.TieredBossIds[1].Count)];
            ArmsArea = BossOrbTierList.IdToAreaName[armsBossId];
            var fakeEyeBossId = BossOrbTierList.TieredBossIds[2][Agent.Rng.Next(0, BossOrbTierList.TieredBossIds[2].Count)];
            FakeEyeArea = BossOrbTierList.IdToAreaName[fakeEyeBossId];
            RealEyeArea = FakeEyeArea;

            var realEyeBossId = 0;
            while (RealEyeArea.Equals(FakeEyeArea))
            {
                realEyeBossId = BossOrbTierList.TieredBossIds[2][Agent.Rng.Next(0, BossOrbTierList.TieredBossIds[2].Count)];
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

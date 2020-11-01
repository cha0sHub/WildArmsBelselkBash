using WildArmsModel.Model.Enemies;
using WildArmsRandomizer.Management;

namespace WildArmsRandomizer.Features.Options
{
    internal class SoloFightBufferOption : ISoloFightBufferOption
    {
        private const int ZombieId = 188;
        private const int NelgaulId = 189;
        private const int ElizabethId = 192;
        private const int Harken3Id = 207;
        private const int ZeikTuvaiId = 202;

        //New Nelgaul Attacks
        private const int DangerousObject666Id = 72;
        //New Zombie Attacks
        private const int OxygenDestroyerId = 87;
        private const int PowerfulBlowId = 2;
        //New Elizabeth Attacks
        private const int DarkRayId = 70;
        private const int ForbiddenSpellId = 52;
        private const int PrisonId = 23;
        private const int ValkyrieId = 28;
        private const int HiReviveId = 36;
        private const int HiHealId = 37;
        //New Tuvai Attacks
        private const int ZeroCountExecutionId = 151;
        private const int GramZamberNemesisId = 156;
        private const int NegativeRainbowId = 153;
        private const int RageAttackId = 43;

        private IRandomizerAgent Agent { get; }
        private IEnemyCollection EnemyCollection { get; }

        public SoloFightBufferOption(IRandomizerAgent agent, IEnemyCollection enemyCollection)
        {
            Agent = agent;
            EnemyCollection = enemyCollection;
        }

        public void ApplySoloFightBuffer()
        {
            BuffNelgaul();
            BuffZombie();
            BuffElizabeth();
            BuffHarken3();
            BuffTuvai();
            EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
        }

        private void BuffNelgaul()
        {
            var nelgaul = EnemyCollection.GetMappedObject(NelgaulId);
            nelgaul.Hp = 2000;
            nelgaul.Atp = 40;
            nelgaul.Dfp = 25;
            nelgaul.Xp = 400;
            nelgaul.Gella = 1000;
            nelgaul.Attack2Id = DangerousObject666Id;
            nelgaul.Attack2Probability = 25;
        }

        private void BuffZombie()
        {
            var zombie = EnemyCollection.GetMappedObject(ZombieId);
            zombie.Hp = 2000;
            zombie.Atp = 40;
            zombie.Dfp = 30;
            zombie.Res = 12;
            zombie.Xp = 400;
            zombie.Gella = 1000;
            zombie.Attack1Id = OxygenDestroyerId;
            zombie.Attack2Id = PowerfulBlowId;
            zombie.Attack2Probability = 50;
        }

        private void BuffElizabeth()
        {
            var elizabeth = EnemyCollection.GetMappedObject(ElizabethId);
            elizabeth.Hp = 30000;
            elizabeth.Dfp = 999;
            elizabeth.Res = 100;
            elizabeth.Sor = 60;
            elizabeth.Xp = 18000;
            elizabeth.Gella = 7000;
            elizabeth.Attack1Id = DarkRayId;
            elizabeth.Attack1Probability = 50;
            elizabeth.Attack2Id = ForbiddenSpellId;
            elizabeth.Attack2Probability = 25;
            elizabeth.Attack3Id = ValkyrieId;
            elizabeth.Attack3Probability = 25;
            elizabeth.Attack4Id = PrisonId;
            elizabeth.Attack4Probability = 0;
        }

        private void BuffHarken3()
        {
            var harken = EnemyCollection.GetMappedObject(Harken3Id);
            harken.Atp = 500;
            harken.Dfp = 700;
            harken.Res = 200;
            harken.Mgr = 999;
            harken.Xp = 18000;
            harken.Gella = 38000;
            harken.RawAbsorb = 254;
            harken.Attack1Probability = 50;
            harken.Attack2Probability = 50;
        }

        private void BuffTuvai()
        {
            var tuvai = EnemyCollection.GetMappedObject(ZeikTuvaiId);
            tuvai.Atp = 800;
            tuvai.Sor = 70;
            tuvai.Res = 250;
            tuvai.Attack1Id = ZeroCountExecutionId;
            tuvai.Attack4Id = NegativeRainbowId;
            tuvai.Attack5Id = RageAttackId;
            tuvai.Attack7Id = GramZamberNemesisId;
            tuvai.Attack7Probability = 100;
            tuvai.Xp = 49200;
            tuvai.Gella = 49200;
        }
    }
}

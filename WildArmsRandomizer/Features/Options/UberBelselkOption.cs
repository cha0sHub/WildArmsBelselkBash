using WildArmsModel.Model.Enemies;
using WildArmsRandomizer.Management;

internal class UberBelselkOption : IUberBelselkOption
{

    private IRandomizerAgent Agent { get; }
    private IEnemyCollection EnemyCollection { get; }

    public UberBelselkOption(IRandomizerAgent randomizerAgent, IEnemyCollection enemyCollection)
    {
        Agent = randomizerAgent;
        EnemyCollection = enemyCollection;
    }

    public void ApplyUberBelselkOption()
    {
        var belselk2 = EnemyCollection.GetMappedObject(218);
        var berial = EnemyCollection.GetMappedObject(197);
        EnemyCollection.OverwriteMappedObjects(Agent.GeneralConfiguration.TempFile, belselk2, berial);
        berial.Id = 197;
        var uberBelselk = berial;
        uberBelselk.Hp = 37500;
        uberBelselk.Mp = 300;
        uberBelselk.Atp = 700;
        uberBelselk.Dfp = 999;
        uberBelselk.Res = 500;
        uberBelselk.Mgr = 999;

        uberBelselk.Attack2Id = 41;
        uberBelselk.Xp = 21900;
        uberBelselk.Gella = 8500;
        EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
    }

}
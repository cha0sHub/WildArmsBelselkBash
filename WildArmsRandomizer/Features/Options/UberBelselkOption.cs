


using WildArmsModel.Model.Enemies;
using WildArmsRandomizer.Management;

internal class UberBelselkOption
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
        berial.OverwriteObject(belselk2);
        var uberBelselk = berial;
        uberBelselk.Hp = 37500;
        uberBelselk.Atp = 700;
        uberBelselk.Dfp = 999;
        uberBelselk.Res = 500;
        uberBelselk.Mgr = 999;

        EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
    }

}
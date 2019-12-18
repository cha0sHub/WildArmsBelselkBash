using System;
using WildArmsModel.Model.Enemies;
using WildArmsRandomizer.Management;

internal class ExperienceFlattenerOption : IExperienceFlattenerOption
{

    private const int BossStartingIndex = 175;

    private IRandomizerAgent Agent { get; }
    private IEnemyCollection EnemyCollection { get; }

    public ExperienceFlattenerOption(IRandomizerAgent agent, IEnemyCollection enemyCollection)
    {
        Agent = agent;
        EnemyCollection = enemyCollection;
    }

    public void FlattenExperience()
    {
        foreach (var enemy in EnemyCollection.MappedObjectReadOnly)
        {
            if (enemy.Id < BossStartingIndex)
            {
                enemy.Xp = Math.Min(enemy.Xp, (ushort)1000);
            }
            else
            {
                enemy.Xp = (ushort)Math.Min(enemy.Xp * 2, ushort.MaxValue);
            }
        }
        EnemyCollection.WriteObjects(Agent.GeneralConfiguration.TempFile);
    }

}
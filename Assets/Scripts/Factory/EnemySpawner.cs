using UnityEngine;

public class EnemySpawner
{
    private readonly EnemiesFactory enemiesFactory;

    public EnemySpawner(EnemiesFactory enemiesFactory)
    {
        this.enemiesFactory = enemiesFactory;
    }
        
    // Logic

    public void SpawnEnemy(string id, GameObject[] listOfPointsToMove)
    {
        var enemy = enemiesFactory.Create(id);
        enemy.Config(listOfPointsToMove);
    }
}
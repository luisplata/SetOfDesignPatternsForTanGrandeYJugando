using UnityEngine;

public class EnemiesFactory
{
    private readonly EnemiesConfiguration _enemiesConfiguration;

    public EnemiesFactory(EnemiesConfiguration EnemiesConfiguration)
    {
        this._enemiesConfiguration = EnemiesConfiguration;
    }
        
    public EnemyTemplate Create(string id)
    {
        var prefab = _enemiesConfiguration.GetEnemyPrefabById(id);

        return Object.Instantiate(prefab);
    }

    public string GetEnemyRandom()
    {
        var enemies = _enemiesConfiguration.Enemies;
        var random = Random.Range(0, enemies.Count);
        var enemyTemplate = enemies[random];
        Debug.Log($"enemyTemplate {enemyTemplate.Id}");
        return enemyTemplate.Id;
    }
}
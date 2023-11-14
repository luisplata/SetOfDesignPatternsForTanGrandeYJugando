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
}
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Enemy configuration")]
public class EnemiesConfiguration : ScriptableObject
{
    [SerializeField] private EnemyTemplate[] enemies;
    private Dictionary<string, EnemyTemplate> idToEnemy;

    private void Awake()
    {
        idToEnemy = new Dictionary<string, EnemyTemplate>(enemies.Length);
        foreach (var enemy in enemies)
        {
            idToEnemy.Add(enemy.Id, enemy);
        }
    }

    public EnemyTemplate GetEnemyPrefabById(string id)
    {
        if (!idToEnemy.TryGetValue(id, out var enemy))
        {
            throw new Exception($"EnemyTemplate with id {id} does not exit");
        }
        return enemy;
    }
    
    public List<EnemyTemplate> Enemies => new(enemies); 
}
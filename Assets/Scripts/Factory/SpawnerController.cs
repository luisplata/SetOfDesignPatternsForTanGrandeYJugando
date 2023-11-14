using System;
using SL;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour, ISpawnController
{
    [SerializeField] private EnemiesConfiguration enemiesConfiguration;
    [SerializeField] private float intervalToSpawn, timeLocalToSpawn;
    [SerializeField] private GameObject[] begging, middle, end;
    private EnemiesFactory _enemiesFactory;
    private EnemySpawner _enemySpawner;
    private bool _canSpawn;

    private void Start()
    {
        _enemiesFactory = new EnemiesFactory(Instantiate(enemiesConfiguration));
        _enemySpawner = new EnemySpawner(_enemiesFactory);
        ServiceLocator.Instance.RegisterService<ISpawnController>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<ISpawnController>();
    }

    public void StartSpawn()
    {
        _canSpawn = true;
    }

    private void Update()
    {
        if(!_canSpawn) return;
        timeLocalToSpawn += Time.deltaTime;
        if(timeLocalToSpawn > intervalToSpawn)
        {
            timeLocalToSpawn = 0;
            //get element from begging, middle, end
            var beggingPoint = begging[Random.Range(0, begging.Length)];
            var middlePoint = middle[Random.Range(0, middle.Length)];
            var endPoint = end[Random.Range(0, end.Length)];
            var listOfPointsToMove = new[] { beggingPoint, middlePoint, endPoint };
            //calc to what is the next enemy
            var nameOfNextEnemy = _enemiesFactory.GetEnemyRandom();
            _enemySpawner.SpawnEnemy(nameOfNextEnemy, listOfPointsToMove);
        }
    }

    public void EndSpawn()
    {
        _canSpawn = false;
    }
}
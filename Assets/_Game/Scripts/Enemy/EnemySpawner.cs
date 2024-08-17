using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private Vector2 _spawnOffest = new Vector2(2f, 2f);
    [SerializeField] private ObjectPool<Enemy> _pool;
    [SerializeField] private EnemyProjectilePool _enemyProjectilePool;

    private List<SpawnPoint> _unarySpawnPoints;

    private void Awake()
    {
        _unarySpawnPoints = new();

        foreach (var spawnPoint in _spawnPoints)
            _unarySpawnPoints.Add(new SpawnPoint(spawnPoint.position));
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        var wait = new WaitForSeconds(_cooldown);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        if (TryGetRandomSpawnPoint(out SpawnPoint spawnPoint) == false)
            return;

        var enemy = _pool.Get(out bool isInstantiated);

        spawnPoint.SetEnemy(enemy);

        if (isInstantiated)
            AddListeners(enemy);

        Vector3 movePosition = spawnPoint.Position;
        float randomSpawnYPoint = Random.Range(-_spawnOffest.y, _spawnOffest.y);
        Vector3 spawnPosition = movePosition + Vector3.right * _spawnOffest.x + Vector3.up * randomSpawnYPoint;
        Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);

        enemy.Init(spawnPosition, movePosition, spawnRotation, _enemyProjectilePool);
    }

    private bool TryGetRandomSpawnPoint(out SpawnPoint spawnPoint)
    {
        var freeSpawnPoints = _unarySpawnPoints.Where(spawnPoint => spawnPoint.IsFree).ToList();

        print("Free spawn points coint = " + freeSpawnPoints.Count);

        if (freeSpawnPoints.Count == 0)
        {
            spawnPoint = null;
            return false;
        }

        spawnPoint = freeSpawnPoints[Random.Range(0, freeSpawnPoints.Count)];
        return true;
    }

    private void AddListeners(Enemy enemy)
    {
        //enemy.Dying += ClearSpawnPoint;
        enemy.Dying += _pool.Put;
        //enemy.Dying += PrintSpawnPointCount;
        //enemy.Died += RemoveListeners;
    }

    private void RemoveListeners(Enemy enemy)
    {
        //enemy.Dying -= ClearSpawnPoint;
        enemy.Dying -= _pool.Put;
        //enemy.Dying -= PrintSpawnPointCount;
        enemy.Died -= RemoveListeners;
    }

    //private void PrintSpawnPointCount(Enemy _)
    //{
    //    print(_unarySpawnPoints.Where(spawnPoint => spawnPoint.IsFree).ToList().Count);
    //}

    //private void ClearSpawnPoint(Enemy enemy)
    //{
    //    _unarySpawnPoints.FirstOrDefault(spawnPoint => spawnPoint.Enemy == enemy).Clear();
    //}
}

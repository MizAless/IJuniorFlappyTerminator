using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private Vector2 _spawnOffest = new Vector2(2f, 2f);
    [SerializeField] private Spawner<Projectile<Enemy>> _projectileSpawner;

    private List<SpawnPoint> _unarySpawnPoints;

    private void Awake()
    {
        _unarySpawnPoints = new();

        foreach (var spawnPoint in _spawnPoints)
            _unarySpawnPoints.Add(new SpawnPoint(spawnPoint.position));
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    public override Enemy Spawn()
    {
        if (TryGetRandomSpawnPoint(out SpawnPoint spawnPoint) == false)
            return null;

        var enemy = base.Spawn();

        spawnPoint.SetEnemy(enemy);

        Vector3 targetPosition = spawnPoint.Position;
        float randomSpawnYPoint = UnityEngine.Random.Range(-_spawnOffest.y, _spawnOffest.y);
        Vector3 spawnPosition = targetPosition + Vector3.right * _spawnOffest.x + Vector3.up * randomSpawnYPoint;
        Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);

        enemy.Init(spawnPosition, targetPosition, spawnRotation, _projectileSpawner);

        return enemy;
    }

    protected override void AddListeners(Enemy enemy)
    {
        enemy.Died += Pool.Put;
        enemy.Destroyed += RemoveListeners;
    }

    protected override void RemoveListeners(IDestroyable destroyableObject)
    {
        var enemy = destroyableObject as Enemy;
        enemy.Died -= Pool.Put;
        enemy.Destroyed -= RemoveListeners;
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

    private bool TryGetRandomSpawnPoint(out SpawnPoint spawnPoint)
    {
        var freeSpawnPoints = _unarySpawnPoints.Where(spawnPoint => spawnPoint.IsFree).ToList();

        if (freeSpawnPoints.Count == 0)
        {
            spawnPoint = null;
            return false;
        }

        spawnPoint = freeSpawnPoints[UnityEngine.Random.Range(0, freeSpawnPoints.Count)];
        return true;
    }
}

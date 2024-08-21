using System;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyShooter))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour, IDamagable, IDestroyable
{
    private Health _health;
    private EnemyShooter _enemyShooter;
    private EnemyMover _enemyMover;

    public event Action<Enemy> Died;
    public event Action<IDestroyable> Destroyed;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _enemyShooter = GetComponent<EnemyShooter>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }
    

    public void Init(Vector3 spawnPosition, Vector3 targetPosition, Quaternion rotation, Spawner<Projectile<Enemy>> projectileSpawner)
    {
        transform.position = spawnPosition;
        transform.rotation = rotation;
        _enemyShooter.Init(projectileSpawner);
        _health.Init();
        _enemyShooter.StartShooting();
        _enemyMover.MoveTowards(targetPosition);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnDied()
    {
        Died?.Invoke(this);
    }
}

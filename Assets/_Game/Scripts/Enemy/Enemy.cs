using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyShooter))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour, IDamagable
{
    private Health _health;
    private EnemyShooter _enemyShooter;
    private EnemyMover _enemyMover;

    public event Action<Enemy> Dying;
    public event Action<Enemy> Died;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _enemyShooter = GetComponent<EnemyShooter>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void Start()
    {
        StartCoroutine(_enemyShooter.Shooting());
    }

    private void OnEnable()
    {
        _health.Dying += OnDying;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Dying -= OnDying;
        _health.Died -= OnDied;
    }

    public void Init(Vector3 spawnPosition, Vector3 movePosition, Quaternion rotation, EnemyProjectilePool enemyProjectilePool)
    {
        transform.position = spawnPosition;
        transform.rotation = rotation;
        _enemyShooter.Init(enemyProjectilePool);
        _health.Init();
        StartCoroutine(_enemyShooter.Shooting());
        StartCoroutine(_enemyMover.MoveTowards(movePosition));
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnDying()
    {
        Dying?.Invoke(this);
    }

    private void OnDied()
    {
        Died?.Invoke(this);
    }
}

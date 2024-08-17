using System;
using UnityEngine;

public class KilledEnemy : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private int counter = 0;

    public event Action<int> Changed;

    private void OnEnable()
    {
        _enemySpawner.InstatiatedEnemy += OnInstatiated;
    }

    private void OnDisable()
    {
        _enemySpawner.InstatiatedEnemy -= OnInstatiated;
    }

    private void OnInstatiated(Enemy enemy)
    {
        AddListeners(enemy);
    }

    private void UpdateCounter(Enemy _)
    {
        counter++;
        Changed?.Invoke(counter);
    }

    private void AddListeners(Enemy enemy)
    {
        enemy.Dying += UpdateCounter;
        enemy.Destroyed += RemoveListeners;
    }

    private void RemoveListeners(Enemy enemy)
    {
        enemy.Dying -= UpdateCounter;
        enemy.Destroyed -= RemoveListeners;
    }
}

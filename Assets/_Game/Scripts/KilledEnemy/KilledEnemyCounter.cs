using System;
using UnityEngine;

public class KilledEnemyCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private int counter = 0;

    public event Action<int> Changed;

    private void OnEnable()
    {
        _enemySpawner.Instatiated += OnInstatiated;
    }

    private void OnDisable()
    {
        _enemySpawner.Instatiated -= OnInstatiated;
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
        enemy.Died += UpdateCounter;
        enemy.Destroyed += RemoveListeners;
    }

    private void RemoveListeners(IDestroyable destroyableObject)
    {
        Enemy enemy = destroyableObject as Enemy;
        enemy.Died -= UpdateCounter;
        enemy.Destroyed -= RemoveListeners;
    }
}

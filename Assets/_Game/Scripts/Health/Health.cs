using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _maxHealth;

    private int _health;

    public event Action<int> Changed;
    public event Action Died;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _health = _maxHealth;
        Changed?.Invoke(_health);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        _health -= damage;

        _health = Mathf.Clamp(_health, 0, _maxHealth);

        Changed?.Invoke(_health);

        if (_health == 0)
            Die();
    }

    public void Die()
    {
        Died?.Invoke();
    }
}
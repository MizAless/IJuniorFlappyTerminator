using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _maxHealth;

    private int _health;

    public event Action Dying;
    public event Action Died;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        //print(_health);

        if (damage < 0)
            return;

        _health -= damage;

        _health = Mathf.Clamp(_health, 0, _maxHealth);

        if (_health == 0)
            Die();
    }

    private void Die()
    {
        Dying?.Invoke();
        Died?.Invoke();
    }
}
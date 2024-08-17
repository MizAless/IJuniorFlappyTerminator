using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, IDamagable
{
    private Mover _mover;
    private InputReader _inputReader;
    private PlayerShooter _shooter;
    private Health _health;

    public event Action Died;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _shooter = GetComponent<PlayerShooter>();
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        StartCoroutine(_mover.AutomaticFlying());
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void FixedUpdate() => _mover.Rotate();

    public void StartFlying()
    {
        _mover.OffAutomaticFlying();
        AddListeners();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Die()
    {
        _health.Die();
    }

    private void AddListeners()
    {
        _inputReader.Jumped += _mover.Jump;
        _inputReader.ShootFired += _shooter.Shoot;
        _health.Died += OnDied;
    }

    private void RemoveListeners()
    {
        _inputReader.Jumped -= _mover.Jump;
        _inputReader.ShootFired -= _shooter.Shoot;
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        Died?.Invoke();
    }
}

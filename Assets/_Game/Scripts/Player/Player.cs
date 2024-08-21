using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CollisionDetector))] 
[RequireComponent(typeof(PlayerStates))] 
public class Player : MonoBehaviour, IDamagable
{
    private Mover _mover;
    private InputReader _inputReader;
    private PlayerShooter _shooter;
    private Health _health;
    private CollisionDetector _collisionDetector;
    private PlayerStates _playerStates;

    public event Action Died;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _shooter = GetComponent<PlayerShooter>();
        _health = GetComponent<Health>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _playerStates = GetComponent<PlayerStates>();
    }

    private void Start()
    {
        _playerStates.StartAutomaticFlying();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void FixedUpdate() => _mover.Rotate();

    public void StartFlying()
    {
        _playerStates.StartManualFlying();
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
        _collisionDetector.DeadlyWallCollide += Die;
    }

    private void RemoveListeners()
    {
        _inputReader.Jumped -= _mover.Jump;
        _inputReader.ShootFired -= _shooter.Shoot;
        _health.Died -= OnDied;
        _collisionDetector.DeadlyWallCollide -= Die;
    }

    private void OnDied()
    {
        Died?.Invoke();
    }
}

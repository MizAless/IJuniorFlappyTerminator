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

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _shooter = GetComponent<PlayerShooter>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _inputReader.Jumped += _mover.Jump;
        _inputReader.ShootFired += _shooter.Shoot;
    }

    private void OnDisable()
    {
        _inputReader.Jumped -= _mover.Jump;
        _inputReader.ShootFired -= _shooter.Shoot;
    }

    private void FixedUpdate() => _mover.Rotate();

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}

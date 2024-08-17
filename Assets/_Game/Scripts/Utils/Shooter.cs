using UnityEngine;

public abstract class Shooter<EnemyType> : MonoBehaviour
    where EnemyType : IDamagable
{
    [SerializeField] private float _shootCooldown;
    [SerializeField] private ObjectPool<Projectile<EnemyType>> _pool;
    [SerializeField] private Transform _shootPoint;

    private bool _canShoot = true;

    protected float ShootCooldown => _shootCooldown;

    public void Init(ObjectPool<Projectile<EnemyType>> pool)
    {
        _pool = pool;
    }

    public void Shoot()
    {
        if (_canShoot == false)
            return;

        _canShoot = false;

        var projectile = _pool.Get(out bool isInstantiated);

        if (isInstantiated)
            AddListeners(projectile);

        projectile.Init(_shootPoint.position, _shootPoint.rotation);

        Reload();
    }

    private void OnProjectileHit(Projectile<EnemyType> projectile, EnemyType enemy)
    {
        enemy.TakeDamage(projectile.Damage);
        projectile.Destroy();
    }

    private void Reload()
    {
        Invoke(nameof(AllowShooting), _shootCooldown);
    }

    private void AddListeners(Projectile<EnemyType> projectile)
    {
        projectile.Desactivating += _pool.Put;
        projectile.Collide += OnProjectileHit;
        projectile.Destroyed += RemoveListeners;
    }

    private void RemoveListeners(Projectile<EnemyType> projectile)
    {
        projectile.Desactivating -= _pool.Put;
        projectile.Collide -= OnProjectileHit;
        projectile.Destroyed -= RemoveListeners;
    }

    private void AllowShooting() => _canShoot = true;
}

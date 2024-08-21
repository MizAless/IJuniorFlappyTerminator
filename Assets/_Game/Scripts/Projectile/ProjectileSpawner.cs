using UnityEngine;

public class ProjectileSpawner<OwnerType> : Spawner<Projectile<OwnerType>>
    where OwnerType : MonoBehaviour, IDamagable
{
    public override Projectile<OwnerType> Spawn()
    {
        return base.Spawn();
    }

    protected override void AddListeners(Projectile<OwnerType> projectile)
    {
        projectile.Desactivated += Pool.Put;
        projectile.Collide += OnProjectileHit;
        projectile.Destroyed += RemoveListeners;
    }

    protected override void RemoveListeners(IDestroyable destroyableObject)
    {
        Projectile<OwnerType> projectile = destroyableObject as Projectile<OwnerType>;

        projectile.Desactivated -= Pool.Put;
        projectile.Collide -= OnProjectileHit;
        projectile.Destroyed -= RemoveListeners;
    }

    private void OnProjectileHit(Projectile<OwnerType> projectile, IDamagable enemy)
    {
        enemy.TakeDamage(projectile.Damage);
        projectile.Destroy();
    }
}

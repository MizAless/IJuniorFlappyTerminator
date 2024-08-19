using UnityEngine;

public class ProjectileSpawner<OwnerType> : Spawner<Projectile<OwnerType>>
    where OwnerType : MonoBehaviour, IDamagable
{
    public override Projectile<OwnerType> Spawn()
    {
        var projectile = base.Spawn();
        return projectile;
    }

    protected override void AddListeners(Projectile<OwnerType> projectile)
    {
        projectile.Desactivating += Pool.Put;
        projectile.Collide += OnProjectileHit;
        projectile.Destroyed += RemoveListeners;
    }

    protected override void RemoveListeners(IDestroyable destroyableObject)
    {
        var projectile = destroyableObject as Projectile<OwnerType>;

        projectile.Desactivating += Pool.Put;
        projectile.Collide += OnProjectileHit;
        projectile.Destroyed += RemoveListeners;
    }

    private void OnProjectileHit(Projectile<OwnerType> projectile, IDamagable enemy)
    {
        enemy.TakeDamage(projectile.Damage);
        projectile.Destroy();
    }
}

using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile<EnemyType> : MonoBehaviour
    where EnemyType : IDamagable
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _flyForce = 10;
    [SerializeField] private float _destroyDelay = 2;

    private Rigidbody2D _rigidbody2D;

    public int Damage => _damage;

    public event Action<Projectile<EnemyType>, EnemyType> Collide;
    public event Action<Projectile<EnemyType>> Desactivating;
    public event Action<Projectile<EnemyType>> Desactivated;
    public event Action<Projectile<EnemyType>> Destroyed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyType enemy))
        {
            Collide?.Invoke(this, enemy);
        }
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }

    public void Init(Vector3 position, Quaternion quaternion)
    {
        _rigidbody2D.velocity = Vector3.zero;
        transform.position = position;
        transform.rotation = quaternion;
        _rigidbody2D.AddForce(transform.right * _flyForce, ForceMode2D.Impulse);
        StartCoroutine(DestroyWithDelay());
    }

    public void Destroy()
    {
        Desactivating?.Invoke(this);
        Desactivated?.Invoke(this);
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(_destroyDelay);
        Destroy();
    }
}

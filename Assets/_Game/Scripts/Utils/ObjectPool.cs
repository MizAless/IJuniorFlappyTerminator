using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour
    where T : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private T _prefab;

    private Queue<T> _pool;

    private void Awake()
    {
        _pool = new();
    }

    public T Get(out bool isInstantiated)
    {
        T newObject;

        if (_pool.Count == 0)
        {
            newObject = Instantiate(_prefab, _container);
            isInstantiated = true;
            return newObject;
        }

        newObject = _pool.Dequeue();
        newObject.gameObject.SetActive(true);

        if (newObject is Enemy)
            print("Pool count = " + _pool.Count);

        isInstantiated = false;
        return newObject;
    }

    public void Put(T projectile)
    {
        _pool.Enqueue(projectile);
        projectile.gameObject.SetActive(false);
    }
}

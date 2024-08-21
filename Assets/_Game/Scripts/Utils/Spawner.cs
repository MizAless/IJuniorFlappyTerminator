using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
    where T : MonoBehaviour, IDestroyable
{
    [SerializeField] protected ObjectPool<T> Pool;

    public event Action<T> Instatiated;

    public virtual T Spawn()
    {
        T newObject = Pool.Get(out bool isInstantiated);

        if (isInstantiated)
        {
            Instatiated?.Invoke(newObject);
            AddListeners(newObject);
        }

        return newObject;
    }

    protected abstract void AddListeners(T instantiatedObject);

    protected abstract void RemoveListeners(IDestroyable destroyableObject);
}

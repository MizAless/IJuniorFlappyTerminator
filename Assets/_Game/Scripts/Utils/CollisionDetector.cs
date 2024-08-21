using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionDetector : MonoBehaviour
{
    public event Action DeadlyWallCollide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<DeadlyWall>(out _))
            DeadlyWallCollide?.Invoke();
    }
}

using System;
using UnityEngine;

public class DeadlyWall : MonoBehaviour
{
    public event Action Collide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
            Collide?.Invoke();
    }
}

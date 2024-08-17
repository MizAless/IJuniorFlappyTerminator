using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveTime = 1;
    [SerializeField] private float _inaccuracy = 0.01f;

    public IEnumerator MoveTowards(Vector3 position)
    {
        var wait = new WaitForFixedUpdate();

        float speed = (transform.position - position).magnitude / _moveTime;
        float moveDelta = speed * Time.fixedDeltaTime;

        while ((transform.position - position).magnitude > _inaccuracy)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, moveDelta);
            yield return wait;
        }

        transform.position = position;
    }
}

using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveTime = 1;
    [SerializeField] private float _inaccuracy = 0.01f;

    public void MoveTowards(Vector3 position)
    {
        StartCoroutine(MovingTowards(position));
    }

    private IEnumerator MovingTowards(Vector3 position)
    {
        var wait = new WaitForFixedUpdate();

        float speed = (transform.position - position).magnitude / _moveTime;
        float moveDelta = speed * Time.fixedDeltaTime;
        float sqrInaccuracy = _inaccuracy * _inaccuracy;

        while ((transform.position - position).sqrMagnitude > sqrInaccuracy)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, moveDelta);
            yield return wait;
        }

        transform.position = position;
    }
}

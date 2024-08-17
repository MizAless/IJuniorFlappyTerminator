using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private float _automaticFlyingDelay;

    private Rigidbody2D _rigidbody2D;

    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private bool _isAutomaticFlying;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotateSpeed * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        _rigidbody2D.velocity = new Vector2(0, _jumpForce);
        transform.rotation = _maxRotation;
    }

    public IEnumerator AutomaticFlying()
    {
        _isAutomaticFlying = true;

        while (_isAutomaticFlying)
        {
            Jump();
            yield return new WaitForSeconds(_automaticFlyingDelay);
        }
    }

    public void OffAutomaticFlying()
    {
        _isAutomaticFlying = false;
    }
}

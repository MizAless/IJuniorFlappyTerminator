using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PlayerStates : MonoBehaviour
{
    [SerializeField] private float _automaticFlyingDelay = 1;

    private Mover _mover;

    private bool _isAutomaticFlying;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public void StartManualFlying()
    {
        _isAutomaticFlying = false;

    }

    public void StartAutomaticFlying()
    {
        StartCoroutine(AutomaticFlying());
    }

    private IEnumerator AutomaticFlying()
    {
        _isAutomaticFlying = true;

        while (_isAutomaticFlying)
        {
            _mover.Jump();
            yield return new WaitForSeconds(_automaticFlyingDelay);
        }
    }
}

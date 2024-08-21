using UnityEngine;
using UnityEngine.UI;

public class KilledEnemyCounterView : MonoBehaviour
{
    [SerializeField] private KilledEnemyCounter _killedEnemyCounter;
    [SerializeField] private Text _killedEnemyCounterText;

    private int _startValue = 0;

    private void Start()
    {
        OnChanged(_startValue);
    }

    private void OnEnable()
    {
        _killedEnemyCounter.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _killedEnemyCounter.Changed -= OnChanged;
    }

    public void OnChanged(int value)
    {
        _killedEnemyCounterText.text = value.ToString();
    }
}

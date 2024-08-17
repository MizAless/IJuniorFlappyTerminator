using UnityEngine;
using UnityEngine.UI;

public class KilledEnemyView : MonoBehaviour
{
    [SerializeField] private KilledEnemy _killedEnemy;
    [SerializeField] private Text _killedEnemyText;

    private int _startValue = 0;

    private void Start()
    {
        OnChanged(_startValue);
    }

    private void OnEnable()
    {
        _killedEnemy.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _killedEnemy.Changed -= OnChanged;
    }

    public void OnChanged(int value)
    {
        _killedEnemyText.text = value.ToString();
    }
}

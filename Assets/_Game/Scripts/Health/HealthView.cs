using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Text _healthText;

    private void OnEnable()
    {
        _health.Changed += OnChange;
    }

    private void OnDisable()
    {
        _health.Changed -= OnChange;
    }

    public void OnChange(int healthValue)
    {
        _healthText.text = healthValue.ToString();   
    }
}

// Hero/Scripts/UI/HealthBar.cs
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HeroState _heroState;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        if (_heroState != null)
            _heroState.OnHealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        if (_heroState != null)
            _heroState.OnHealthChanged -= UpdateBar;
    }

    private void Start()
    {
        // начальное обновление
        if (_heroState != null)
            UpdateBar(_heroState.CurrentHealth, _heroState.MaxHealth);
    }

    private void UpdateBar(int current, int max)
    {
        _slider.value = (float)current / max;
    }
}
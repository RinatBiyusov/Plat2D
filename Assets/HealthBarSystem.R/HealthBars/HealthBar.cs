using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected Health Health => _health;

    protected virtual void Awake()
    {
        _health.Changed += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
    }

    protected abstract void OnHealthChanged();
}
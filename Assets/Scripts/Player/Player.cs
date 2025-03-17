using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _invincibilityTime;

    private Health _health;
    private int _coinBag;
    private float _invincibilityEndTime;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += OnDying;
    }

    private void OnDisable()
    {
        _health.Died -= OnDying;
    }
    
    private void OnDying() => gameObject.SetActive(false);
    
    public void PickUpCoin() => _coinBag++;

    public void TakeDamage(float amount)
    {
        if (Time.time < _invincibilityEndTime)
            return;

        _health.TakeDamage(amount);
        _invincibilityEndTime = Time.time + _invincibilityTime;
    }

    public bool TryHeal(float amount)
    {
        if (_health.CurrentPoints + amount > _health.MaxPoints)
            return false;
        
        _health.TakeHeal(amount);
        return true;
    }
}
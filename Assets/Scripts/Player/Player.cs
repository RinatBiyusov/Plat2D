using UnityEngine;

[RequireComponent(typeof(Collectable))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;

    private int _coinBag;
    private int _currentHealthPoints;

    private void Start()
    {
        _currentHealthPoints = _maxHealth;
    }

    public void TakeDamage(int receivedDamage)
    {
        if (receivedDamage >= _currentHealthPoints)
        {
            _currentHealthPoints = 0;
            gameObject.SetActive(false);
        }
        else
            _currentHealthPoints -= receivedDamage;
    }

    public void TryHeal(int healthCount)
    {
        if (_maxHealth > _currentHealthPoints)
            _currentHealthPoints += healthCount;
    }

    public void PickUpCoin() => _coinBag++;
}
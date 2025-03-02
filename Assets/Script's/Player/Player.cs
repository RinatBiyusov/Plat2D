using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;

    private int _coinBag;
    private int _currentHealthPoints;

    private void Start()
    {
        _currentHealthPoints = _maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _coinBag++;
            coin.Dispose();
        }
        else if(collision.TryGetComponent(out Apple apple))
        {
            if (_maxHealth > _currentHealthPoints)
            {
                _currentHealthPoints += apple.AmountHealing;
                apple.Dispose();
            }
        }
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
}
using UnityEngine;
//Сделать CollectablesChecker, так чтобы он отвечал за подъём предметов.
[RequireComponent(typeof(Collectable))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;

    private Collectable _collectable;
    private int _coinBag;
    private int _currentHealthPoints;

    private void OnEnable()
    {
        _collectable.WasCollect += PickUpCollectable;
    }

    private void OnDisable()
    {
        _collectable.WasCollect -= PickUpCollectable;
    }

    private void Awake()
    {
        _collectable = GetComponent<Collectable>();
    }

    private void Start()
    {
        _currentHealthPoints = _maxHealth;
    }

    private void PickUpCollectable(Collectable collectable)
    {
        if (collectable.TryGetComponent(out Coin coin))
        {
            _coinBag++;
            coin.Dispose();
        }
        else if(collectable.TryGetComponent(out Apple apple))
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
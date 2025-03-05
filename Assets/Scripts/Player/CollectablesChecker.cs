using UnityEngine;

[RequireComponent(typeof(Collectable))]
public class CollectablesChecker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Collectable _colletables;

    private void Awake()
    {
        _colletables = GetComponent<Collectable>();
    }

    private void OnEnable()
    {
        _colletables.WasCollect += PickUpCollectable;
    }

    private void OnDisable()
    {
        _colletables.WasCollect -= PickUpCollectable;
    }

    private void PickUpCollectable(Collectable collectable)
    {
        if (collectable.TryGetComponent(out Coin coin))
        {
            _player.PickUpCoin();
            coin.Dispose();
        }
        else if (collectable.TryGetComponent(out Apple apple))
        {
            _player.TryHeal(apple.AmountHealing);
            apple.Dispose();
        }
    }
}

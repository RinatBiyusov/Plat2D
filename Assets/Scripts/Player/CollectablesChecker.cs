using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CollectablesChecker : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _player.PickUpCoin();
            coin.Dispose();
        }
        else if (collision.TryGetComponent(out Apple apple))
        {
            if (_player.TryHeal(apple.AmountHealing))
                apple.Dispose();
        }
    }
}
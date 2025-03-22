using System;
using UnityEngine;

public class ItemInteractor : MonoBehaviour, IVisitor
{
    [SerializeField] private Player _player;  
    [SerializeField] private Collider2D _collider;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITakeable takeable))
            takeable.Accept(this);
    }
    
    public void VisitCoin(Coin coin)
    {
        _player.PickUpCoin();
        coin.Dispose();
    }

    public void VisitApple(Apple apple)
    {
        _player.TryHeal(apple.AmountHealing);
        apple.Dispose();
    }
}
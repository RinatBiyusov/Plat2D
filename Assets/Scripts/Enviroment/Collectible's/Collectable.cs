using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public event Action<Collectable> WasCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Collectable collectable))
        {
            if (collectable.TryGetComponent(out Apple apple))
                WasCollect?.Invoke(apple);
            else if (collectable.TryGetComponent(out Coin coin))
                WasCollect?.Invoke(coin);
        }
    }
}

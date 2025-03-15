using System;
using UnityEngine;

public class PlayerDectection : MonoBehaviour
{
    [SerializeField] private Collider2D _detectionCollider;

    public bool IsPlayerDetected { get; private set; } = false;
    public Vector2 PlayerPosition { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            IsPlayerDetected = true;
            PlayerPosition = player.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            PlayerPosition = player.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            IsPlayerDetected = false;
    }
}
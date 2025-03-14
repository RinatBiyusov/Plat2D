using System;
using UnityEngine;

public class PlayerDectection : MonoBehaviour
{
    [SerializeField] private Collider2D _detectionCollider;

    public bool IsPlayerDetected { get; private set; } = false;
    public Vector2 PlayerPosition { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerDetected = true;
            PlayerPosition = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PlayerPosition = collision.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            IsPlayerDetected = false;
    }
}
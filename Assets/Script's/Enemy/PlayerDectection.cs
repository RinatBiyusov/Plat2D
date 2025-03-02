using System;
using System.Collections;
using UnityEngine;

public class PlayerDectection : MonoBehaviour
{
    [SerializeField] private float _detectionDistance = 5f;
    [SerializeField] private float _checkRate = 0.1f;

    private WaitForSeconds _rate;

    public bool IsPlayerDetected { get; private set; } = false;
    public Vector2 PlayerPosition { get; private set; }

    private void Awake()
    {
        _rate = new WaitForSeconds(_checkRate);
    }

    private void Update()
    {
        StartCoroutine(SeachPlayer());
    }

    private bool IsPlayerInSight()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _detectionDistance);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Player player))
            {
                PlayerPosition = player.transform.position;
                return true;
            }
        }

        return false;
    }

    private IEnumerator SeachPlayer()
    {
        while (enabled)
        {
            IsPlayerDetected = IsPlayerInSight();

            yield return _rate;
        }
    }
}

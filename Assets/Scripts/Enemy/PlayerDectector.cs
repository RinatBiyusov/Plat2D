using System;
using UnityEngine;

public class PlayerDectector : MonoBehaviour
{
    [SerializeField] private float _radiusDetection = 10f;
    [SerializeField] private LayerMask _detectionLayer;

    private Collider2D[] _detectionColliders = new Collider2D[10];
    private int _detectedTargets;

    public bool IsPlayerDetected { get; private set; } = false;
    public Vector2 PlayerPosition { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusDetection);
    }
    
    public void Detect()
    {
        _detectedTargets = Physics2D.OverlapCircleNonAlloc(transform.position, _radiusDetection, _detectionColliders,
            _detectionLayer);

        if (_detectedTargets >= 1)
        {
            IsPlayerDetected = true;
            PlayerPosition = _detectionColliders[_detectedTargets - 1].transform.position;
        }
        else
            IsPlayerDetected = false;
    }
}
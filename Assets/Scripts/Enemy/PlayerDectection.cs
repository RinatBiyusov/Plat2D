using System.Collections;
using UnityEngine;

public class PlayerDectection : MonoBehaviour
{
    [SerializeField] private float _detectionDistance = 5f;
    [SerializeField] private float _checkRate = 0.1f;
    [SerializeField] private LayerMask _obstacleLayer;

    public bool IsPlayerDetected { get; private set; } = false;
    public Vector2 PlayerPosition { get; private set; }


    private void Start()
    {
        StartCoroutine(SeachPlayer());
    }

    private bool IsPlayerInSight()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _detectionDistance);
        float sqrDetectionDistance = _detectionDistance * _detectionDistance;

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Player player))
            {
                Vector2 differenceVectors = (player.transform.position - transform.position);

                if (differenceVectors.sqrMagnitude <= sqrDetectionDistance)
                {
                    Vector2 direction = differenceVectors.normalized;
                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, _detectionDistance, _obstacleLayer);

                    if (hitInfo.collider == null || hitInfo.collider.GetComponent<Player>() != null)
                    {
                        PlayerPosition = player.transform.position;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator SeachPlayer()
    {
        while (enabled)
        {
            IsPlayerDetected = IsPlayerInSight();

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionDistance);
    }
}

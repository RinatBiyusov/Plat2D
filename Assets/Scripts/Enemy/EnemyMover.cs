using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDectection))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _waypoints;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _chasingSpeed = 5f;

    private readonly Quaternion _lookRight = Quaternion.identity;
    private readonly Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

    private PlayerDectection _detector;
    private int _currentWaypoint = 0;
    private float _distanceInaccuracy = 0.6f;
    private bool _isChasing = false;

    private void Awake()
    {
        _detector = GetComponent<PlayerDectection>();
    }

    private void Update()
    {
        if (_detector.IsPlayerDetected)
            StartChasing();
        else
            StopChasing();

        if (_isChasing)
            Chase();
        else
            Patrol();
    }

    private void StartChasing() => _isChasing = true;

    private void StopChasing() => _isChasing = false;

    private void Chase()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = new Vector2(_detector.PlayerPosition.x, currentPosition.y);

        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, _chasingSpeed * Time.deltaTime);

        Flip(_detector.PlayerPosition);
    }

    private void Patrol()
    {
        if (Vector2.SqrMagnitude(_waypoints[_currentWaypoint].Position - transform.position) < _distanceInaccuracy * _distanceInaccuracy)
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Count;

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].Position, Time.deltaTime * _speed);

        Flip(_waypoints[_currentWaypoint].Position);
    }

    private void Flip(Vector2 position)
    {
        if (position.x - transform.position.x > 0)
            transform.rotation = _lookRight;
        else
            transform.rotation = _lookLeft;
    }
}
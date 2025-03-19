using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyPatroller _patroller;
    [SerializeField]  private EnemyChaser _chaser;
    [SerializeField] private PlayerDectection _detector;
    [SerializeField] private EnemyFlipper _flipper;
    
    private readonly Quaternion _lookRight = Quaternion.identity;
    private readonly Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

    private bool _isChasing = false;

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
        _chaser.Chase(_detector.PlayerPosition);
        _flipper.Flip(_detector.PlayerPosition);
    }

    private void Patrol()
    {
        _patroller.Patrol();
        _flipper.Flip(_patroller.CurrentWaypointPosition);
    }
    
    private void Flip(Vector2 position)
    {
        if (position.x - transform.position.x > 0)
            transform.rotation = _lookRight;
        else
            transform.rotation = _lookLeft;
    }
}
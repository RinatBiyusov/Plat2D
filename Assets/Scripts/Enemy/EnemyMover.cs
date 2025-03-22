using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyPatroller _patroller;
    [SerializeField]  private EnemyChaser _chaser;
    [SerializeField] private PlayerDectector _detector;
    [SerializeField] private Rotator _rotator;
    
    private readonly Quaternion _lookRight = Quaternion.identity;
    private readonly Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

    private bool _isChasing = false;

    private void Update()
    {
        _detector.Detect();
        
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
        _rotator.Flip(_detector.PlayerPosition);
    }

    private void Patrol()
    {
        _patroller.Patrol();
        _rotator.Flip(_patroller.CurrentWaypointPosition);
    }
}
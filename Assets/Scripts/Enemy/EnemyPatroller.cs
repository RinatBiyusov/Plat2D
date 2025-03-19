using UnityEngine;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyPatroller :  MonoBehaviour
    {
        [SerializeField] private List<Waypoint> _waypoints;
        [SerializeField] private float _speed = 3f;
        
        private int _currentWaypoint = 0;
        private readonly float _distanceInaccuracy = 0.6f;
        
        public Vector2 CurrentWaypointPosition { get; private set; }
        
        public void Patrol()
        {
            if (Vector2.SqrMagnitude(_waypoints[_currentWaypoint].Position - transform.position) < _distanceInaccuracy * _distanceInaccuracy)
            {
                _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Count;
                CurrentWaypointPosition =  _waypoints[_currentWaypoint].Position;
            }

            transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].Position, Time.deltaTime * _speed);
        }
    }
}
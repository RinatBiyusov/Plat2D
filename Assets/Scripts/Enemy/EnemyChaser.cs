using UnityEngine;

namespace Enemy
{
    public class EnemyChaser : MonoBehaviour
    {
        [SerializeField] private float _chasingSpeed = 5f;
        
        public void Chase(Vector2 target)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = new Vector2(target.x, currentPosition.y);

            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, _chasingSpeed * Time.deltaTime);
        }
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private Collider2D _collider;

    private EnemyMover _enemyMover;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(FollowPlayer(collision.GetComponent<Transform>()));
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            _enemyMover.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StopAllCoroutines();
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            _enemyMover.enabled = true;
        }
    }

    private IEnumerator FollowPlayer(Transform position)
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, position.position, Time.deltaTime * _speed);
            yield return null;
        }
    }
}

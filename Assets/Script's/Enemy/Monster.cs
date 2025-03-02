using UnityEngine;

public class Monster : MonoBehaviour
{
    [Range(1, 3)][SerializeField] private int _damage;
    [Range(1, 5)][SerializeField] private int _healthPoints;
    [SerializeField] private float _strengthKnockback = 10f;

    private Collider2D _hitBoxBody;

    public void TakeDamage(int receivedDamage)
    {
        if (receivedDamage >= _healthPoints)
        {
            _healthPoints = 0;
            gameObject.SetActive(false);
        }
        else
            _healthPoints -= receivedDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            ApplyKnock(player.transform);
        }
    }

    private void ApplyKnock(Transform player)
    {
        if (player == null)
            return;

        Rigidbody2D rigidbodyPlayer = player.GetComponent<Rigidbody2D>();

        Vector2 directionKnockback = player.transform.position - transform.position;
        directionKnockback.y = 0;
        directionKnockback.Normalize();

        Debug.Log(directionKnockback);

        rigidbodyPlayer.AddForce(directionKnockback * _strengthKnockback, ForceMode2D.Impulse);
    }
}

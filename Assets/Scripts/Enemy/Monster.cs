using Enemy;
using UnityEngine;


public class Monster : MonoBehaviour
{
    [Range(1, 3)] [SerializeField] private int _damage;
    [SerializeField] private float _strengthKnockback = 10f;
    
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += OnDying;
    }

    private void OnDisable()
    {
        _health.Died -= OnDying;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            ApplyKnock(player);
        }
    }
    
    private void OnDying() => gameObject.SetActive(false);

    private void ApplyKnock(Player player)
    {
        if (player == null)
            return;

        Rigidbody2D rigidbodyPlayer = player.GetComponent<Rigidbody2D>();

        Vector2 directionKnockback = player.transform.position - transform.position;
        directionKnockback.y = 0;
        directionKnockback.Normalize();

        rigidbodyPlayer.AddForce(directionKnockback * _strengthKnockback, ForceMode2D.Impulse);
    }
    
    public void TakeDamage(float receivedDamage) => _health.TakeDamage(receivedDamage);
}
using UnityEngine;

[RequireComponent(typeof(InputPlayerReader), typeof(PlayerAnimator))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _cooldownAttack = 0.5f;
    [SerializeField] private float _attackRange = 0.5f;
    [Range(1, 3)][SerializeField] private float _damage = 1;
    [SerializeField] private float _strengthKnockback = 2f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;
    
    private InputPlayerReader _controller;
    private PlayerAnimator _animator;
    private float _lastAttackTime = 0f;

    private void Awake()
    {
        _controller = GetComponent<InputPlayerReader>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _controller.AttackPressed += Attack;
    }

    private void OnDisable()
    {
        _controller.AttackPressed -= Attack;
    }

    private void Attack()
    {
        if (Time.time < _lastAttackTime + _cooldownAttack)
            return;

        _animator.TriggerAttack();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        DealDamage(hitEnemies);
        _lastAttackTime = Time.time;
    }

    private void DealDamage(Collider2D[] hitEnemies)
    {
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out Monster monster))
                monster.TakeDamage(_damage);

            Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
            ApplyKnockback(enemyRigidbody);
        }
    }

    private void ApplyKnockback(Rigidbody2D enemy)
    {
        if (enemy == null)
            return;

        Vector2 directionKnockback = enemy.transform.position - transform.position;
        directionKnockback.y = 0;
        directionKnockback.Normalize();

        enemy.AddForce(directionKnockback * _strengthKnockback, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        if (_attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}

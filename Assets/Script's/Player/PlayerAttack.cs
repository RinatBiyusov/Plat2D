using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(InputPlayerReader), typeof(PlayerAnimationController))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Параметры атаки игрока:")]
    [SerializeField] private float _cooldownAttack = 0.5f;
    [SerializeField] private float _attackRange = 0.5f;
    [Range(1, 3)][SerializeField] private int _damage = 1;
    [SerializeField] private float _strengthKnockback = 2f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;

    private InputPlayerReader _controller;
    private PlayerAnimationController _animationController;
    private float lastAttackTime = 0f;
    private float _attackDuration = 1f;
    private bool _isAttacking = false;
    private WaitForSeconds _waiting;

    private void Awake()
    {
        _controller = GetComponent<InputPlayerReader>();
        _animationController = GetComponent<PlayerAnimationController>();
        _waiting = new WaitForSeconds(_attackDuration);
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
        if (Time.time < lastAttackTime + _cooldownAttack || _isAttacking)
            return;

        _animationController.TriggerAttack();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        DealDamage(hitEnemies);
        lastAttackTime = Time.time;

        StartCoroutine(AttackRoutine());
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

        Debug.Log(directionKnockback);

        enemy.AddForce(directionKnockback * _strengthKnockback, ForceMode2D.Impulse);
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        yield return _waiting;
        _isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (_attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}

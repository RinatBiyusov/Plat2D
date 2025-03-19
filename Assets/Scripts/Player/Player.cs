using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _invincibilityTime;
    [SerializeField] private PlayerJumper _jumper;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private InputPlayerReader _inputPlayer;
    [SerializeField] private GroundedChecker _groundedChecker;
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Rotator _rotator;

    [FormerlySerializedAs("_animation")] [FormerlySerializedAs("_animationController")] [SerializeField]
    private PlayerAnimator _animator;

    private Health _health;
    private Rigidbody2D _rigidbody;
    private int _coinBag;
    private float _invincibilityEndTime;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _health.Died += OnDying;
        _inputPlayer.JumpPressed += Jump;
        _inputPlayer.AbilityPressed += ActivateAbility;
    }

    private void OnDisable()
    {
        _health.Died -= OnDying;
        _inputPlayer.JumpPressed -= Jump;
        _inputPlayer.AbilityPressed -= ActivateAbility;
    }

    private void Update()
    {
        _rotator.RotateLogic();
        _mover.Move();
    }

    private void OnDying() => gameObject.SetActive(false);

    private void Jump()
    {
        if (_groundedChecker.IsGrounded)
            _jumper.Jump(_rigidbody);
    }

    private void ActivateAbility() => _vampirism.ActivateAbility(this);

    public void PickUpCoin() => _coinBag++;

    public void TakeDamage(float amount)
    {
        if (Time.time < _invincibilityEndTime)
            return;

        _health.TakeDamage(amount);
        _invincibilityEndTime = Time.time + _invincibilityTime;
    }

    public bool TryHeal(float amount)
    {
        if (_health.CurrentPoints + amount > _health.MaxPoints)
            return false;

        _health.TakeHeal(amount);
        return true;
    }
}
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _run = Animator.StringToHash("Run");
    private readonly int _attack = Animator.StringToHash("Attack");

    public float DurationAttackAnimation { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TriggerRun(float value)
    {
        if (Mathf.Abs(value) > 0)
            _animator.SetBool(_run, true);
        else
            _animator.SetBool(_run, false);
    }

    public void TriggerAttack() => _animator.SetTrigger(_attack);
}

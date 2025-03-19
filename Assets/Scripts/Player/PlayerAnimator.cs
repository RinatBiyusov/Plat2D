using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _run = Animator.StringToHash("Run");
    private readonly int _attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TriggerRun(float value) => _animator.SetBool(_run, Mathf.Abs(value) > 0);

    public void TriggerAttack() => _animator.SetTrigger(_attack);
}

using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxPoints;

    private readonly float _deathPoints = 0;

    public event Action Changed;
    public event Action Died;

    public float CurrentPoints { get; private set; }
    public float MaxPoints { get; private set; }

    private void Awake()
    {
        CurrentPoints = _maxPoints;
        MaxPoints = _maxPoints;
    }

    public void TakeDamage(float damage)
    {
        if (CurrentPoints <= damage)
        {
            CurrentPoints = _deathPoints;
            Died?.Invoke();
        }
        else
        {
            CurrentPoints -= damage;
            Changed?.Invoke();
        }
    }

    public void TakeHeal(float heal)
    {
        if (CurrentPoints + heal >= _maxPoints)
            CurrentPoints = _maxPoints;
        else
            CurrentPoints += heal;

        Changed?.Invoke();
    }
}
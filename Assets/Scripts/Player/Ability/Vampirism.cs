using System;
using UnityEngine;
using System.Collections;

public class Vampirism : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _ratioRegenHealthPerTick = 1f;

    [SerializeField] private float _durationAbility = 6f;
    [SerializeField] private float _coolDownAbility = 4f;

    [SerializeField] private float _radiusAbility = 5f;
    [SerializeField] private float _timeTick = 0.5f;
    [SerializeField] private float _damagePerTick = 0.5f;
    
    private float _regenAmountPerTick;
    private WaitForSeconds _waitForSeconds;

    public bool IsOnCooldown { get; private set; } = false;
    public bool IsActivated { get; private set; } = false;
    public float TimeLastUse { get; private set; }
    public float DurationAbility => _durationAbility;

    public event Action AbilityStatusChanged;
    
    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_timeTick);
    }

    private void Start() => _regenAmountPerTick = _damagePerTick * _ratioRegenHealthPerTick;
    
    public void ActivateAbility(Player player)
    {
        if (TimeLastUse == 0)
        {
            TimeLastUse = Time.time;
            StartCoroutine(DealDamagePerTick(player));
        }
        else
        {
            if (CanBeActivated())
                return;

            StartCoroutine(DealDamagePerTick(player));
        }
    }

    private bool CanBeActivated() => IsActivated || Time.time<TimeLastUse + _coolDownAbility;

    private IEnumerator DealDamagePerTick(Player player)
    {
        float elapsedTime = 0f;
        IsActivated = true;
        AbilityStatusChanged?.Invoke();
        
        while (elapsedTime < _durationAbility)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radiusAbility);

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out Monster monster))
                {
                    monster.TakeDamage(_damagePerTick);
                    player.TryHeal(_regenAmountPerTick);
                }
            }

            yield return _waitForSeconds;

            elapsedTime += _timeTick;
        }

        IsOnCooldown = true;
        IsActivated = false;
        TimeLastUse = Time.time;

        AbilityStatusChanged?.Invoke();
        
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusAbility);
    }
}
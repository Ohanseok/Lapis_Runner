using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isPlayerTargetting;

    [ReadOnly] public Damageable currentTarget;

    [SerializeField] private VoidEventChannelSO _hitEvent = default;
    [SerializeField] private EffectManager _effectManager;

    [SerializeField] private VoidEventChannelSO _attackEvent = default;

    private AttackConfigSO attackConfig;

    private void Awake()
    {
        _effectManager = GetComponentInChildren<EffectManager>();
        attackConfig = GetComponent<Attack>().AttackConfig;
    }

    private void OnEnable()
    {
        _hitEvent.OnEventRaised += OnHit;
        _attackEvent.OnEventRaised += OnAttack;
    }

    private void OnDisable()
    {
        _hitEvent.OnEventRaised -= OnHit;
        _attackEvent.OnEventRaised -= OnAttack;
    }

    public void OnAlertTriggerChange(bool entered, GameObject who)
    {
        if (entered && who.TryGetComponent(out Damageable d))
        {
            currentTarget = d;
            currentTarget.OnDie += OnTargetDead;
        }
        else
        {
            currentTarget = null;
        }
    }

    private void OnHit()
    {
        _effectManager.StartEffect("Hit");
    }

    public void OnSetTarget(GameObject who)
    {
        if (who.TryGetComponent(out Damageable d))
        {
            isPlayerTargetting = true;

            currentTarget = d;
            currentTarget.OnDie += OnTargetDead;
        }
        else
        {
            isPlayerTargetting = false;
            currentTarget = null;
        }
    }

    private void OnTargetDead()
    {
        currentTarget = null;
        isPlayerTargetting = false;
    }

    public void OnAttack()
    {
        // 이거 Effect 매니저에서 알아서 처리?
        //_effectManager.StartEffect("Attack");

        if (currentTarget != null)
            currentTarget.ReceiveAnAttack(attackConfig.AttackStrength);
    }

    public void OnDead()
    {
        Destroy(gameObject);
    }
}

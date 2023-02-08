using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isPlayerInAlertZone;
    [HideInInspector] public bool isPlayerInAttackZone;

    [HideInInspector] public bool isPlayerTargetting;

    [ReadOnly] public Damageable currentTarget;

    [SerializeField] private VoidEventChannelSO _hitEvent = default;
    [SerializeField] private EffectManager _effectManager;

    [SerializeField] private VoidEventChannelSO _attackEvent = default;

    [SerializeField] private GameStateSO _gameStateSO = default;

    private AttackConfigSO attackConfig;

    private void Awake()
    {
        _effectManager = GetComponentInChildren<EffectManager>();
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
        isPlayerInAlertZone = entered;

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

    public void OnAttackTriggerChange(bool entered, GameObject who)
    {
        isPlayerInAttackZone = entered;

        //No need to set the target. If we did, we would get currentTarget to null even if
        //a target exited the Attack zone (inner) but stayed in the Alert zone (outer).
    }

    private void OnHit()
    {
        //_effectManager.StartEffect("Hit");
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
        isPlayerInAlertZone = false;
        isPlayerInAttackZone = false;
    }

    public void OnAttack()
    {
        // 이거 Effect 매니저에서 알아서 처리?
        //_effectManager.StartEffect("Attack");

        /*
        if (currentTarget != null)
            currentTarget.ReceiveAnAttack(attackConfig.AttackStrength);
        */
    }

    public void OnDead()
    {
        _gameStateSO.RemoveTargetEnemy(transform);
    }
}

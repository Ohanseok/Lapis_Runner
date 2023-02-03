using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool isMonsterInAlertZone;
    [HideInInspector] public bool isMonsterInAttackZone;

    public enum TYPE_ACTION
    {
        IDLE,
        WALKING,
        RUN,
        FIGHTING
    }

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;
    [SerializeField] private VoidEventChannelSO _alertEnemyEvent = default;
    [SerializeField] private VoidEventChannelSO _fightEnemyEvent = default;
    [SerializeField] private VoidEventChannelSO _attackEvent = default;

    [SerializeField] private EffectManager _effectManager;

    private Animator _controller;

    private TYPE_ACTION _currentAction = TYPE_ACTION.IDLE;
    public TYPE_ACTION CURRENT_ACTION => _currentAction;

    [ReadOnly] public Damageable currentTarget;
    private AttackConfigSO attackConfig;
    [SerializeField] private RunEffect runEffect;

    private void Awake()
    {
        _effectManager = GetComponentInChildren<EffectManager>();
        attackConfig = GetComponent<Attack>().AttackConfig;
    }

    private void OnEnable()
    {
        _currentAction = TYPE_ACTION.IDLE;

        _startStageEvent.OnEventRaised += OnStartStage;
        _alertEnemyEvent.OnEventRaised += OnAlertEnemy;
        _fightEnemyEvent.OnEventRaised += OnFightEnemy;
        _attackEvent.OnEventRaised += OnAttack;
    }

    private void OnDisable()
    {
        _startStageEvent.OnEventRaised -= OnStartStage;
        _alertEnemyEvent.OnEventRaised -= OnAlertEnemy;
        _fightEnemyEvent.OnEventRaised -= OnFightEnemy;
        _attackEvent.OnEventRaised -= OnAttack;
    }

    private void OnStartStage()
    {
        _effectManager.StartEffect("RunDust");

        // 여기서 달리기?
        _currentAction = TYPE_ACTION.WALKING;

        StartCoroutine(RunPlayer());
    }

    IEnumerator RunPlayer()
    {
        yield return new WaitForSeconds(1.0f);

        _currentAction = TYPE_ACTION.RUN;
    }

    private void OnAlertEnemy()
    {
        StopCoroutine(RunPlayer());

        _effectManager.StopEffect("RunDust");

        _currentAction = TYPE_ACTION.WALKING;
    }

    private void OnFightEnemy()
    {
        _currentAction = TYPE_ACTION.FIGHTING;
    }

    /*
    public void OnSetTarget(GameObject who)
    {
        if(who.TryGetComponent(out Damageable d))
        {
            currentTarget = d;
            currentTarget.OnDie += OnTargetDead;
        }
        else
        {
            currentTarget = null;
        }
    }
    */

    

    public void OnAttack()
    {
        // 이거 Effect 매니저에서 알아서 처리?
        _effectManager.StopEffect("RunDust");

        _effectManager.StartEffect("Attack");

        
        if (currentTarget != null)
            currentTarget.ReceiveAnAttack(attackConfig.AttackStrength);
        
    }

    public void OnAlertTriggerChange(bool entered, GameObject who)
    {
        isMonsterInAlertZone = entered;

        if(entered && who.TryGetComponent(out Damageable d))
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
        isMonsterInAttackZone = entered;
    }

    private void OnTargetDead()
    {
        currentTarget = null;

        isMonsterInAlertZone = false;
        isMonsterInAttackZone = false;

        //OnStartStage();
    }
}

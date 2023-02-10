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

    [SerializeField] private GameStateSO _gameStateSO;

    [SerializeField] private VoidEventChannelSO _searchEnemy = default;

    private Animator _controller;

    private TYPE_ACTION _currentAction = TYPE_ACTION.IDLE;
    public TYPE_ACTION CURRENT_ACTION => _currentAction;

    [ReadOnly] public Damageable currentTarget;
    [SerializeField] private RunEffect runEffect;

    public BoxCollider2D SearchCollider;

    [SerializeField] private VoidEventChannelSO _stopScrollingEvent = default;
    [SerializeField] private VoidEventChannelSO _startScrollingEvent = default;

    [SerializeField] private VoidEventChannelSO _infantry_normal_skill = default;

    [SerializeField] private AttackableAreaSO _attackableAreaSO = default;

    private BoxCollider2D AttackArea;

    private void Awake()
    {
        _effectManager = GetComponentInChildren<EffectManager>();
    }

    private void OnEnable()
    {
        AttackArea = _attackableAreaSO.GetArea();

        _currentAction = TYPE_ACTION.IDLE;

        _startStageEvent.OnEventRaised += OnStartStage;
        _alertEnemyEvent.OnEventRaised += OnAlertEnemy;
        _fightEnemyEvent.OnEventRaised += OnFightEnemy;
        _attackEvent.OnEventRaised += OnAttack;
        _searchEnemy.OnEventRaised += OnSearchEnemy;
        _startScrollingEvent.OnEventRaised += OnStartScrolling;
        _stopScrollingEvent.OnEventRaised += OnStopScrolling;

        StartCoroutine(normal_skill());
    }

    private void OnDisable()
    {
        _startStageEvent.OnEventRaised -= OnStartStage;
        _alertEnemyEvent.OnEventRaised -= OnAlertEnemy;
        _fightEnemyEvent.OnEventRaised -= OnFightEnemy;
        _attackEvent.OnEventRaised -= OnAttack;
        _searchEnemy.OnEventRaised -= OnSearchEnemy;
        _startScrollingEvent.OnEventRaised -= OnStartScrolling;
        _stopScrollingEvent.OnEventRaised -= OnStopScrolling;
    }

    IEnumerator normal_skill()
    {
        while (true)
        {
            if (currentTarget != null)
            {
                if (_infantry_normal_skill != null)
                    _infantry_normal_skill.RaiseEvent();
            }

            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        }
    }

    private void OnStartScrolling()
    {
        _effectManager.StartEffect("Move");
    }

    private void OnStopScrolling()
    {
        _effectManager.StopEffect("Move");
    }

    public void OnAttackEffect()
    {
        _effectManager.StartEffect("Normal_Attack");

        /*
        if(_infantry_normal_skill != null)
            _infantry_normal_skill.RaiseEvent();
        */
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(SearchCollider.transform.position, SearchCollider.size);
        //Gizmos.DrawWireCube(AttackArea.transform.position, AttackArea.size);
        
    }


    private void OnSearchEnemy()
    {
        //SearchCollider.enabled = true;

        //Collider2D[] hit = Physics2D.OverlapBoxAll(SearchCollider.transform.position, SearchCollider.size, 0, _layers);
        Collider2D[] hit = Physics2D.OverlapBoxAll(AttackArea.transform.position, AttackArea.size, 0, _layers);

        if (hit.Length != 0)
        {
            if (currentTarget == null)
            {
                if (hit[0].gameObject.TryGetComponent(out Damageable d))
                {
                    if (!d.IsDead)
                    {
                        Debug.Log("타겟 설정됨");
                        currentTarget = d;
                        currentTarget.OnDie += OnTargetDead;
                    }
                }
            }
        }
    }

    private void OnStartStage()
    {
        _effectManager.StartEffect("Move");

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

        _effectManager.StopEffect("Move");

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
        //_effectManager.StopEffect("RunDust");
        _effectManager.StopEffect("Move");

        //_effectManager.StartEffect("Attack");

        //_effectSystem.StartEffect("Hit", currentTarget.transform, attackConfig.AttackStrength);
        //_effectSystem.StartEffect("Hit", _gameStateSO.GetFirstTargetEnemy(), attackConfig.AttackStrength);

        /*
        if (currentTarget != null)
            currentTarget.ReceiveAnAttack(attackConfig.AttackStrength);
        */
    }

    [SerializeField] LayerMask _layers;

    public bool GetTarget()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(SearchCollider.transform.position, SearchCollider.size, 0, _layers);

        for(int i = 0; i < hit.Length; i++)
        {
            Debug.Log("GetTarget : " + hit[i].name);
        }

        return hit.Length != 0;

        //return true;
    }

    public void OnAlertTriggerChange(bool entered, GameObject who)
    {
        /*
        if(entered)
            Debug.Log(who.name + " 타겟에 들어옴 : " + who.transform.GetInstanceID());
        else
            Debug.Log(who.name + " 타겟에 나감 : " + who.transform.GetInstanceID());

        if (SearchCollider.enabled)
        {
            isMonsterInAlertZone = entered;

            if (entered && who.TryGetComponent(out Damageable d))
            {
                if (currentTarget == null)
                {
                    currentTarget = d;
                    currentTarget.OnDie += OnTargetDead;
                }
            }
            else
            {
                currentTarget = null;
            }
        }
        */
    }

    public void OnAttackTriggerChange(bool entered, GameObject who)
    {
        /*
        if (SearchCollider.enabled)
        {
            isMonsterInAttackZone = entered;
        }
        */

        /*
        Debug.Log(who.name + " 검출");

        isMonsterInAttackZone = entered;

        if(entered && who.TryGetComponent(out Damageable d))
        {
            _gameStateSO.AddTargetEnemy(who.transform);
        }
        */
    }

    private void OnTargetDead()
    {
        currentTarget = null;

        isMonsterInAlertZone = false;
        isMonsterInAttackZone = false;

        //SearchCollider.enabled = false;
        //OnStartStage();
    }
}

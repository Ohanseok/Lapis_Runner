using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Enemy Attack Action", menuName = "State Machines/Actions/Enemy Attack")]
public class EnemyAttackSO : StateActionSO
{
    public VoidEventChannelSO enemyAttackEvent;

    protected override StateAction CreateAction() => new EnemyAttack();
}

public class EnemyAttack : StateAction
{
    VoidEventChannelSO _attackEvent;

    public override void Awake(StateMachine stateMachine)
    {
        _attackEvent = ((EnemyAttackSO)OriginSO).enemyAttackEvent;
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {
        if (_attackEvent != null)
            _attackEvent.RaiseEvent();
    }
}
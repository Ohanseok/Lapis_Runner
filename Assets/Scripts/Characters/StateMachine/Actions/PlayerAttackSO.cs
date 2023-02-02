using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Player Attack Action", menuName = "State Machines/Actions/Player Attack")]
public class PlayerAttackSO : StateActionSO
{
    public VoidEventChannelSO playerAttackEvent;

    protected override StateAction CreateAction() => new PlayerAttack();
}

public class PlayerAttack : StateAction
{
    VoidEventChannelSO _attackEvent;

    public override void Awake(StateMachine stateMachine)
    {
        _attackEvent = ((PlayerAttackSO)OriginSO).playerAttackEvent;
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnStateEnter()
    {
        if(_attackEvent != null)
            _attackEvent.RaiseEvent();
    }
}
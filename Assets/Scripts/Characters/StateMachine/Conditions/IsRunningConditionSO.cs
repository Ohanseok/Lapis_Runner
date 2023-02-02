using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Check Running")]
public class IsRunningConditionSO : StateConditionSO<IsRunningCondition> { }

public class IsRunningCondition : Condition
{
    private Player _playerScript;

    private IsRunningConditionSO _originSO => (IsRunningConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _playerScript = stateMachine.GetComponent<Player>();
    }

    protected override bool Statement()
    {
        return _playerScript.CURRENT_ACTION == Player.TYPE_ACTION.RUN;
    }
}
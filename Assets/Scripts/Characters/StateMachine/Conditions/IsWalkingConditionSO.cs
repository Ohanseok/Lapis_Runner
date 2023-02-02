using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Check Walking")]
public class IsWalkingConditionSO : StateConditionSO<IsWalkingCondition> { }

public class IsWalkingCondition : Condition
{
    private Player _playerScript;

    private IsWalkingConditionSO _originSO => (IsWalkingConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _playerScript = stateMachine.GetComponent<Player>();
    }

    protected override bool Statement()
    {
        return _playerScript.CURRENT_ACTION == Player.TYPE_ACTION.WALKING;
    }
}
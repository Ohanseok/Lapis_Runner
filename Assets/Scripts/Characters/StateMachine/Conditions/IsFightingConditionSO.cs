using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Check Fighting")]
public class IsFightingConditionSO : StateConditionSO<IsFightingCondition> { }

public class IsFightingCondition : Condition
{
    private Player _playerScript;

    private IsFightingConditionSO _originSO => (IsFightingConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _playerScript = stateMachine.GetComponent<Player>();
    }

    protected override bool Statement()
    {
        return _playerScript.CURRENT_ACTION == Player.TYPE_ACTION.FIGHTING;
    }
}
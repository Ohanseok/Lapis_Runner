using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsAttackReloadedCondition", menuName = "State Machines/Conditions/Is Attack Reloaded")]
public class IsAttackReloadedConditionSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsAttackReloadedCondition();
}

public class IsAttackReloadedCondition : Condition
{
	private float _startTime;
	private float _reloadDuration;

	public override void Awake(StateMachine stateMachine)
	{
		//TODO: Remove this. We don't need to rely on a timer hidden in the attack config of the weapon,
		//since our attacks depend on the lenght of the animation anyway
		_reloadDuration = stateMachine.gameObject.GetComponent<Attack>().AttackConfig.AttackReloadDuration;
	}

	public override void OnStateEnter()
	{
		_startTime = Time.time;
	}

	protected override bool Statement()
	{
		return Time.time >= _startTime + _reloadDuration;
	}
}
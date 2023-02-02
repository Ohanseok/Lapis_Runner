using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsTargetDeadConditionForEnemy", menuName = "State Machines/Conditions/Is Target Dead Condition For Enemy")]
public class IsTargetDeadConditionForEnemySO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsTargetDeadConditionForEnemy();
}

public class IsTargetDeadConditionForEnemy : Condition
{
	private Enemy _enemyScript;

	public override void Awake(StateMachine stateMachine)
	{
		_enemyScript = stateMachine.GetComponent<Enemy>();
	}

	protected override bool Statement()
	{
		return _enemyScript.currentTarget == null || _enemyScript.currentTarget.IsDead;
	}
}

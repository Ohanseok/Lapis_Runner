using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;


[CreateAssetMenu(fileName = "PlayerIsInZone", menuName = "State Machines/Conditions/Player Is In Zone")]
public class PlayerIsInZoneSO : StateConditionSO
{
	[Tooltip("Target transform anchor.")]
	[SerializeField] private TransformAnchor _targetTransform = default;

	//public Vector3 TargetPosition => _targetTransform.Value.position;

	public ZoneType zone;

	protected override Condition CreateCondition() => new PlayerIsInZone();
}

public class PlayerIsInZone : Condition
{

	private Enemy _enemy;
	private Vector3 _targetPos;

	public override void Awake(StateMachine stateMachine)
	{
		_enemy = stateMachine.GetComponent<Enemy>();
		//_targetPos = ((PlayerIsInZoneSO)OriginSO).TargetPosition;
	}

	protected override bool Statement()
	{
		bool result = false;
		if (_enemy != null)
		{
			switch (((PlayerIsInZoneSO)OriginSO).zone)
			{
				case ZoneType.Alert:
					result = _enemy.isPlayerInAlertZone;
					break;
				case ZoneType.Attack:
					result = _enemy.isPlayerInAttackZone;
					break;
				default:
					break;
			}
		}
		return result;
	}
}

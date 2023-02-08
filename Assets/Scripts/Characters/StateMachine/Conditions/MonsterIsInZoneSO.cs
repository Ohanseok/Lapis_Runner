using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

public enum ZoneType
{
    Alert,
    Attack
}

[CreateAssetMenu(fileName = "MonsterIsInZone", menuName = "State Machines/Conditions/Monster Is In Zone")]
public class MonsterIsInZoneSO : StateConditionSO
{
	public ZoneType zone;

	protected override Condition CreateCondition() => new MonsterIsInZone();
}

public class MonsterIsInZone : Condition
{
	private Player _player;

	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		bool result = false;
		if (_player != null)
		{
			switch (((MonsterIsInZoneSO)OriginSO).zone)
			{
				case ZoneType.Alert:
					result = _player.isMonsterInAlertZone;
					break;
				case ZoneType.Attack:
					result = _player.isMonsterInAttackZone;
					break;
				default:
					break;
			}
		}
		return result;
	}
}
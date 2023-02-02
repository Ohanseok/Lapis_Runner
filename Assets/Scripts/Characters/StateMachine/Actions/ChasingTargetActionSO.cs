using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ChasingTargetAction", menuName = "State Machines/Actions/Chasing Target Action")]
public class ChasingTargetActionSO : StateActionSO
{
	[Tooltip("Target transform anchor.")]
	[SerializeField] private TransformAnchor _targetTransform = default;

	[Tooltip("NPC chasing speed")]
	[SerializeField] private float _chasingSpeed = default;

	public Vector3 TargetPosition => _targetTransform.Value.position;
	public float ChasingSpeed => _chasingSpeed;

	protected override StateAction CreateAction() => new ChasingTargetAction();
}

public class ChasingTargetAction : StateAction
{
	private Enemy _enemy;
	private ChasingTargetActionSO _config;

	private Vector3 _startPos;
	private Vector3 _targetPos;

	private float startTime;
	private float distanceLength;

	public override void Awake(StateMachine stateMachine)
	{
		_config = (ChasingTargetActionSO)OriginSO;
		_enemy = stateMachine.GetComponent<Enemy>();
		_startPos = stateMachine.GetComponent<Enemy>().transform.position;
		_targetPos = _config.TargetPosition;
	}

	public override void OnUpdate()
	{
		float distCovered = (Time.time - startTime) * ((ChasingTargetActionSO)OriginSO).ChasingSpeed;
		float franJourney = distCovered / distanceLength;
		_enemy.gameObject.transform.position = Vector3.Lerp(_startPos, _targetPos, franJourney);
	}

	public override void OnStateEnter()
	{
		startTime = Time.time;
		distanceLength = Vector3.Distance(_startPos, _targetPos);
	}
}

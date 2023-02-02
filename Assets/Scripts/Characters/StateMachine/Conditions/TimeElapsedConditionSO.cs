using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Time elapsed")]
public class TimeElapsedConditionSO : StateConditionSO<TimeElapsedCondition>
{
    public float timerLength = .5f;
}

public class TimeElapsedCondition : Condition
{
    private float _startTime;

    private TimeElapsedConditionSO _originSO => (TimeElapsedConditionSO)base.OriginSO;

    public override void OnStateEnter()
    {
        _startTime = Time.time;
    }

    protected override bool Statement() => Time.time >= _startTime + _originSO.timerLength;
}
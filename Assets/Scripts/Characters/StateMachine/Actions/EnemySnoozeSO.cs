using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Enemy Snooze", menuName = "State Machines/Actions/Enemy Snooze")]
public class EnemySnoozeSO : StateActionSO
{
    protected override StateAction CreateAction() => new EnemySnooze();
}

public class EnemySnooze : StateAction
{
    Enemy _enemyScript;

    public override void Awake(StateMachine stateMachine)
    {
        _enemyScript = stateMachine.GetComponent<Enemy>();
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {
        _enemyScript.OnDead();
    }
}
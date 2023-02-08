using UnityEngine;
using Bono.StateMachine;
using Bono.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsCombatGameState", menuName = "State Machines/Conditions/Is Combat GameState Condition")]
public class IsCombatGameStateConditionSO : StateConditionSO
{
    [SerializeField] private GameStateSO _gameState = default;

    protected override Condition CreateCondition() => new IsCombatGameStateCondition(_gameState);
}

public class IsCombatGameStateCondition : Condition
{
    private GameStateSO _gameStateSO = default;

    public IsCombatGameStateCondition(GameStateSO gameStateSO)
    {
        _gameStateSO = gameStateSO;
    }

    protected override bool Statement()
    {
        return _gameStateSO.CurrentGameState == GameState.Combat;
    }
}

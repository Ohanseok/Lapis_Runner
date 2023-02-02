using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Gameplay,
    Pause, // ���� ���� ����
    LocationTransition // �ó����� �̵� �� ȭ�� ��ȯ ����
}

[CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState", order = 51)]
public class GameStateSO : DescriptionBaseSO
{
    public GameState CurrentGameState => _currentGameState;

    [Header("Game states")]
    [SerializeField] [ReadOnly] private GameState _currentGameState = default;
    [SerializeField] [ReadOnly] private GameState _previousGameState = default;

    public void UpdateGameState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;

        _previousGameState = _currentGameState;
        _currentGameState = newGameState;
    }

    public void ResetToPreviousGameState()
    {
        if (_previousGameState == _currentGameState)
            return;

        GameState stateToReturnTo = _previousGameState;
        _previousGameState = _currentGameState;
        _currentGameState = stateToReturnTo;
    }
}

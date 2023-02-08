using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Gameplay,
    Pause, // ���� ���� ����
    LocationTransition, // �ó����� �̵� �� ȭ�� ��ȯ ����
    Combat
}

[CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState", order = 51)]
public class GameStateSO : DescriptionBaseSO
{
    public GameState CurrentGameState => _currentGameState;

    [SerializeField] private VoidEventChannelSO _sceneReadyEvent = default;

    [Header("Game states")]
    [SerializeField] [ReadOnly] private GameState _currentGameState = default;
    [SerializeField] [ReadOnly] private GameState _previousGameState = default;

    private List<Transform> _targetEnemies;

    public bool HasTarget => _targetEnemies.Count > 0;

    private List<Transform> _alertEnemies;

    private void Start()
    {
        _alertEnemies.Clear();
        _targetEnemies = new List<Transform>();
    }

    private void OnEnable()
    {
        _sceneReadyEvent.OnEventRaised += OnSceneReady;
    }

    private void OnDisable()
    {
        _sceneReadyEvent.OnEventRaised -= OnSceneReady;
    }

    private void OnSceneReady()
    {
        _alertEnemies.Clear();
    }

    public void AddAlertEnemy(Transform enemy)
    {
        if (!_alertEnemies.Contains(enemy))
        {
            _alertEnemies.Add(enemy);
        }

        UpdateGameState(GameState.Combat);
    }

    public void RemoveAlertEnemy(Transform enemy)
    {
        if (_alertEnemies.Contains(enemy))
        {
            _alertEnemies.Remove(enemy);

            if (_alertEnemies.Count == 0)
            {
                UpdateGameState(GameState.Gameplay);
            }
        }
    }

    // Ÿ���� �߰��Ǹ� ���� ����
    public void AddTargetEnemy(Transform enemy)
    {
        if(!_targetEnemies.Contains(enemy))
        {
            _targetEnemies.Add(enemy);
        }

        UpdateGameState(GameState.Combat);
    }

    public Transform GetFirstTargetEnemy()
    {
        if (_targetEnemies != null)
            return _targetEnemies[0];
        else return null;
    }

    // Ÿ���� �� �������� ����
    public void RemoveTargetEnemy(Transform enemy)
    {
        if(_targetEnemies.Contains(enemy))
        {
            _targetEnemies.Remove(enemy);

            if(_targetEnemies.Count == 0)
            {
                UpdateGameState(GameState.Gameplay);
            }
        }
    }

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

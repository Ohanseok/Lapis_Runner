using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageManagerSO _currentStageManager = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _startGameEvent = default;
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;

    private void OnEnable()
    {
        _startGameEvent.OnEventRaised += OnStartGame;
        _startStageEvent.OnEventRaised += OnStartStage;
    }

    private void OnDisable()
    {
        _startGameEvent.OnEventRaised -= OnStartGame;
        _startStageEvent.OnEventRaised -= OnStartStage;
    }

    private void OnStartGame()
    {
        _currentStageManager.StartGame();
    }

    private void OnStartStage()
    {
        StartCoroutine(_currentStageManager.SummonEnemy());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageManagerSO _currentStageManager = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _startGameEvent = default;
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += StartStage;
        _startGameEvent.OnEventRaised += OnStartGame;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= StartStage;
        _startGameEvent.OnEventRaised -= OnStartGame;
    }

    private void StartStage()
    {
        _currentStageManager.StartGame();
    }

    private void OnStartGame()
    {
        Debug.Log("StageManager OnStartGame");

        //_currentStageManager.StartGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageManagerSO _currentStageManager = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _startGameEvent = default;

    private void OnEnable()
    {
        _startGameEvent.OnEventRaised += OnStartGame;
    }

    private void OnDisable()
    {
        _startGameEvent.OnEventRaised -= OnStartGame;
    }

    private void OnStartGame()
    {
        _currentStageManager.StartGame();
    }
}

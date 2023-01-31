using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _startGameEvent = default;

    [Header("Test Call")]
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;

    private void Start()
    {
        if(_startGameEvent != null)
            _startGameEvent.RaiseEvent();

        _onSceneReady.RaiseEvent();
    }
}

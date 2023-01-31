using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;
    [SerializeField] private VoidEventChannelSO _stopStageEvent = default;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (_startStageEvent != null)
                _startStageEvent.RaiseEvent();
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            if (_stopStageEvent != null)
                _stopStageEvent.RaiseEvent();
        }
    }
}

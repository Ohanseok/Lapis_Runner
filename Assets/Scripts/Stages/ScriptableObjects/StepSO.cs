using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Step", menuName = "Stages/Step")]
public class StepSO : SerializableScriptableObject
{
    [Tooltip("The item to check")]
    [SerializeField] private bool _hasReward = default;

    [SerializeField] private bool _isDone = false;
    [SerializeField] private VoidEventChannelSO _endStepEvent = default;

    public bool HasReward => _hasReward;

    public VoidEventChannelSO EndStepEvent
    {
        set => _endStepEvent = value;
        get => _endStepEvent;
    }

    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }

    public void FinishStep()
    {
        if (_endStepEvent != null)
            _endStepEvent.RaiseEvent();
        _isDone = true;
    }
}

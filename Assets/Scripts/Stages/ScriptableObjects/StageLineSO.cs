using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageLine_", menuName = "Stages/StageLine")]
public class StageLineSO : SerializableScriptableObject
{
    [SerializeField] private int _idStageLine = 0;
    [SerializeField] private List<StageSO> _stages = new List<StageSO>();
    [SerializeField] private bool _isDone = false;
    [SerializeField] private VoidEventChannelSO _endStagelineEvent = default;

    public int IdStageLine => _idStageLine;
    public List<StageSO> Stages => _stages;
    public VoidEventChannelSO EndStagelineEvent => _endStagelineEvent;
    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }
    
    public void FinishStageline()
    {
        if (_endStagelineEvent != null)
            _endStagelineEvent.RaiseEvent();

        _isDone = true;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "Stage_", menuName = "Stages/Stage")]
public class StageSO : SerializableScriptableObject
{
    [SerializeField] private int _idStage = 0;
    [SerializeField] private bool _isDone = false;
    [SerializeField] VoidEventChannelSO _endStageEvent = default;

    [Header("SummonConfig")]
    [SerializeField] private EnemySummonSetSO _summonSet = default;

    public int IdStage => _idStage;
    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }
    public VoidEventChannelSO EndStageEvent => _endStageEvent;

    public EnemySummonSetSO SummonSet => _summonSet;

    public void FinishStage()
    {
        _isDone = true;
        if (_endStageEvent != null)
            _endStageEvent.RaiseEvent();
    }
}

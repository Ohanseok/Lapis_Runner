using UnityEngine;

[CreateAssetMenu(fileName = "Stage_", menuName = "Stages/Stage")]
public class StageSO : SerializableScriptableObject
{
    [SerializeField] private int _idStage = 0;
    [SerializeField] private bool _isDone = false;
    [SerializeField] VoidEventChannelSO _endStageEvent = default;

    [Header("SummonConfig")]
    [SerializeField] private EnemySummonSetSO _summonSet = default;
    [SerializeField] private int _maxSummonCount = 0;
    
    private int _summonCount = 0;

    public int IdStage => _idStage;
    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }

    public int MaxSummonCount => _maxSummonCount;

    public int SummonCount
    {
        get => _summonCount;
        set => _summonCount = value;
    }

    public VoidEventChannelSO EndStageEvent => _endStageEvent;

    public EnemySummonSetSO SummonSet => _summonSet;

    public void FinishStage()
    {
        _isDone = true;
        if (_endStageEvent != null)
            _endStageEvent.RaiseEvent();
    }

    public void ResetStage()
    {
        _isDone = false;
        _summonCount = 0;
    }
}

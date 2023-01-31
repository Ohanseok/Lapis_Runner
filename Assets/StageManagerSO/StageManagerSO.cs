using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageManager", menuName = "Stages/StageManager")]
public class StageManagerSO : ScriptableObject
{
    [Header("Data")]
    [SerializeField] private List<StageLineSO> _stagelines = default;

    [SerializeField] private TransformAnchor _enemyTransform = default;

    //[Header("Listening to channels")]

    [Header("Broadcasting on channels")]
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;
    [SerializeField] private VoidEventChannelSO _stopScrollingEvent = default;

    private StageSO _currentStage = null;
    private StageLineSO _currentStageline;

    private int _currentStageIndex = 0;
    private int _currentStagelineIndex = 0;
    
    public void StartGame()
    {
        StartStageLines();
    }

    private void StartStageLines()
    {
        if(_stagelines != null)
        {
            if(_stagelines.Exists(o => !o.IsDone))
            {
                _currentStagelineIndex = _stagelines.FindIndex(o => !o.IsDone);

                if (_currentStagelineIndex >= 0)
                    _currentStageline = _stagelines.Find(o => !o.IsDone);
            }
        }

        StartStage();
    }

    private void StartStage()
    {
        if(_currentStageline != null)
        {
            _currentStageIndex = _currentStageline.Stages.FindIndex(o => !o.IsDone);

            // 잘못되었을때 예외처리 추후 추가
            if((_currentStageline.Stages.Count > _currentStageIndex) && (_currentStageIndex >= 0))
            {
                _currentStage = _currentStageline.Stages[_currentStageIndex];
            }
        }

        if (_startStageEvent != null)
            _startStageEvent.RaiseEvent();
    }

    public IEnumerator SummonEnemy()
    {
        if(_currentStage != null)
        {
            // 걸어가는 시간. 나중에 늘려서 거리를 재자.
            yield return new WaitForSeconds(3.0f);

            if (_stopScrollingEvent != null)
                _stopScrollingEvent.RaiseEvent();

            Instantiate(_currentStage.SummonSet.Enemys[Random.Range(0, _currentStage.SummonSet.Enemys.Count)].Prefab, _enemyTransform.Value.position, _enemyTransform.Value.rotation);
        }
    }
}

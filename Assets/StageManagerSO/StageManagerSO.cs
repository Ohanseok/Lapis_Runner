using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageManager", menuName = "Stages/StageManager")]
public class StageManagerSO : ScriptableObject
{
    [Header("Data")]
    [SerializeField] private List<StageLineSO> _stagelines = default;

    [SerializeField] private TransformAnchor _enemyTransform = default;

    [Header("Listening to channels")]
    [SerializeField] private VoidEventChannelSO _continueWithStageEvent = default;
    [SerializeField] private VoidEventChannelSO _deathEnemyEvent = default;

    [Header("Broadcasting on channels")]
    [SerializeField] private StageEventChannelSO _startStageEvent = default;







    [SerializeField] private VoidEventChannelSO _stopScrollingEvent = default;
    [SerializeField] private VoidEventChannelSO _spawnEnemyEvent = default;

    private StageSO _currentStage = null;
    private StageLineSO _currentStageline;

    private int _currentStageIndex = 0;
    private int _currentStagelineIndex = 0;

    private void OnDisable()
    {
        _continueWithStageEvent.OnEventRaised -= CheckStageValidity;
        _deathEnemyEvent.OnEventRaised -= DeathEnemy;
    }

    public void StartGame()
    {
        _continueWithStageEvent.OnEventRaised += CheckStageValidity;
        _deathEnemyEvent.OnEventRaised += DeathEnemy;

        StartStageLines();
    }

    private void CheckStageValidity()
    {
        if(_currentStage != null)
        {
            if (!_currentStage.IsDone) // Done을 만들곳에서 SummonCount를 늘려주자.
            {
                _startStageEvent.RaiseEvent(_currentStage);
            }
            else
            {
                EndStage();
            }
        }
    }

    private void EndStage()
    {
        if(_currentStage != null)
        {
            _currentStage.FinishStage();
        }
        _currentStage = null;
        _currentStageIndex = -1;

        if(_currentStageline != null)
        {
            if(!_currentStageline.Stages.Exists(o => !o.IsDone))
            {
                EndStageline();
            }
            else
            {
                StartStage();
            }
        }

    }

    private void EndStageline()
    {
        if(_stagelines != null)
        {
            if(_currentStageline != null)
            {
                _currentStageline.FinishStageline();
            }

            if (!_stagelines.Exists(o => !o.IsDone))
            {
                ResetStagelines();   
            }
            else
            {
                StartStageLines();
            }
        }
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

        CheckStageValidity();
    }

    public void ResetStagelines()
    {
        foreach(var stageline in _stagelines)
        {
            stageline.IsDone = false;

            foreach(var stage in stageline.Stages)
            {
                stage.ResetStage();
            }
        }

        _currentStage = null;
        _currentStageline = null;
        _currentStageIndex = 0;
        _currentStagelineIndex = 0;

        StartStageLines();
    }

    private void DeathEnemy()
    {
        if(_currentStage != null)
        {
            if(!_currentStage.IsDone)
            {
                //_currentStage.SummonCount += 1;

                _currentStage.SummonMonsterCount += 1;

                if(_currentStage.SummonMonsterCount >= _currentStage.MaxSummonMonsterCount)
                {
                    _currentStage.SummonCount += 1;

                    // 바로 Done이 아니라 스테이지 보스전을 해야한다.
                    if (_currentStage.SummonCount >= _currentStage.MaxSummonCount)
                        _currentStage.IsDone = true;
                }
            }
        }

        CheckStageValidity();
    }
}

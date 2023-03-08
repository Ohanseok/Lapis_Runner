using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIIngameScreenController : MonoBehaviour
{
    [Header("Stage View")]
    [SerializeField] private TextMeshProUGUI _stage = default;
    [SerializeField] private TextMeshProUGUI _wave = default;

    [Header("Listening on")]
    [SerializeField] private StageEventChannelSO _stageEvent = default;

    private void OnEnable()
    {
        _stage.text = "";
        _wave.text = "";

        _stageEvent.OnEventRaised += OnStageEvent;
    }

    private void OnDisable()
    {
        _stageEvent.OnEventRaised -= OnStageEvent;
    }

    private void OnStageEvent(StageSO stage)
    {
        _stage.text = "STAGE " + stage.IdStage.ToString();
        _wave.text = (stage.SummonCount+1).ToString() + "/" + stage.MaxSummonCount.ToString();
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private MeshRenderer render;

    public float originalScrollSpeed;
    private float offset;

    private bool isScrolling = false;
    private float speed = 0.0f;

    [SerializeField] private bool _isNonStoppable = false;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _alertEnemyEvent = default;
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _startScrollingEvent = default;

    public bool IsNonStoppable => _isNonStoppable;

    private void Start()
    {
        speed = 0.0f;
        isScrolling = false;
        render = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _alertEnemyEvent.OnEventRaised += OnAlertEnemy;
        _startStageEvent.OnEventRaised += OnStartStage;
    }

    private void OnDisable()
    {
        _alertEnemyEvent.OnEventRaised -= OnAlertEnemy;
        _startStageEvent.OnEventRaised -= OnStartStage;
    }

    private void OnAlertEnemy()
    {
        OnStopScrolling();
    }

    private void OnStartStage()
    {
        OnStartScrolling();
    }

    private void Update()
    {
        if (isScrolling)
        {
            offset += Time.smoothDeltaTime * speed;
            render.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }

    private void OnStopScrolling()
    {
        if (!_isNonStoppable)
        {
            DOTween.To(() => speed, x => speed = x, 0, 1).OnComplete(() =>
            {
                isScrolling = false;
            });
        }
        else
        {
            DOTween.To(() => speed, x => speed = x, originalScrollSpeed / 2, 1).OnComplete(() =>
            {
                
            });
        }
    }

    private void OnStartScrolling()
    {
        isScrolling = true;

        if (_startScrollingEvent != null)
            _startScrollingEvent.RaiseEvent();

        DOTween.To(() => speed, x => speed = x, originalScrollSpeed, 1).OnComplete(() =>
        {

        });
    }
}

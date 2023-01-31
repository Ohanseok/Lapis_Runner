using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private MeshRenderer render;

    public float originalScrollSpeed;
    private float offset;

    public bool isScrolling = false;
    private float speed;

    [SerializeField] private bool _isNonStoppable = false;

    [SerializeField] private VoidEventChannelSO _startScrollingEvent = default;
    [SerializeField] private VoidEventChannelSO _stopScrollingEvent = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;

    public bool IsNonStoppable => _isNonStoppable;

    private void Start()
    {
        speed = 0;
        render = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _startStageEvent.OnEventRaised += OnStartStage;

        _startScrollingEvent.OnEventRaised += OnStartScrolling;
        //_stopScrollingEvent.OnEventRaised += OnStopScrolling;
    }

    private void OnDisable()
    {
        _startStageEvent.OnEventRaised -= OnStartStage;

        _startScrollingEvent.OnEventRaised -= OnStartScrolling;
        //_stopScrollingEvent.OnEventRaised -= OnStopScrolling;
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
            DOTween.To(() => speed, x => speed = x, 0, 3).OnComplete(() =>
            {
                isScrolling = false;
            });
        }
        else
        {
            DOTween.To(() => speed, x => speed = x, originalScrollSpeed / 2, 3).OnComplete(() =>
            {
                
            });
        }
    }

    private void OnStartScrolling()
    {
        isScrolling = true;

        DOTween.To(() => speed, x => speed = x, originalScrollSpeed, 3).OnComplete(() =>
        {

        });
    }
}

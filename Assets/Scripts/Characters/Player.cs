using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;
    [SerializeField] private VoidEventChannelSO _stopStageEvent = default;
    [SerializeField] private VoidEventChannelSO _hitEvent = default;

    [SerializeField] private EffectManager _effectManager;

    private Animator _controller;

    private void Awake()
    {
        _effectManager = GetComponentInChildren<EffectManager>();
        _controller = GetComponent<Animator>();
    }

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

        if(Input.GetKeyDown(KeyCode.A))
        {
            _controller.SetTrigger("IsAttack");
            _effectManager.StartEffect("Attack");

            if (_hitEvent != null)
                _hitEvent.RaiseEvent();
        }

        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                _controller.SetTrigger("IsAttack");
                _effectManager.StartEffect("Attack");

                if (_hitEvent != null)
                    _hitEvent.RaiseEvent();
            }
        }
    }

    public void StartEffect(string effectName)
    {
        _effectManager.StartEffect(effectName);
        _controller.SetTrigger("IsAttack");
    }
}

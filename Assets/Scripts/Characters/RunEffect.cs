using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEffect : MonoBehaviour
{
    private Animator _runAnimator;

    private bool _isRun = false;
    public bool IsRun => _isRun;

    [SerializeField] private VoidEventChannelSO _runEvent = default;
    [SerializeField] private VoidEventChannelSO _stopEvent = default;

    private void Awake()
    {
        _runAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _runAnimator.SetBool("isRun", _isRun);
    }

    private void OnEnable()
    {
        _runEvent.OnEventRaised += OnRun;
        _stopEvent.OnEventRaised += OnStop;
    }

    private void OnDisable()
    {
        _runEvent.OnEventRaised -= OnRun;
        _stopEvent.OnEventRaised -= OnStop;
    }

    private void OnRun()
    {
        _runAnimator.SetBool("isRun", true);
    }

    private void OnStop()
    {
        _runAnimator.SetBool("isRun", false);
    }

    public void SetRun(bool _run)
    {
        if (_run != _isRun)
        {
            _isRun = _run;
            _runAnimator.SetBool("isRun", _isRun);
        }
    }
}

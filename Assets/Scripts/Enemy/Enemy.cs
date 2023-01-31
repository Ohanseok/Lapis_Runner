using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _hitEvent = default;
    [SerializeField] private EffectManager _effectManager;

    private void Awake()
    {
        _effectManager = GetComponentInChildren<EffectManager>();
    }

    private void OnEnable()
    {
        _hitEvent.OnEventRaised += OnHit;
    }

    private void OnDisable()
    {
        _hitEvent.OnEventRaised -= OnHit;
    }

    private void OnHit()
    {
        _effectManager.StartEffect("Hit");
    }
}

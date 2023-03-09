using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarManager : MonoBehaviour
{
    [SerializeField] private Image _slidingImage = default;

    private HealthSO _healthSO = default;
    private Damageable _damageable = default;

    private void Awake()
    {
        _damageable = GetComponent<Damageable>();        
    }

    private void OnEnable()
    {
        _healthSO = GetComponent<Damageable>().CurrentHealthSO;

        _damageable.onHit += _damageable_onHit;

        InitializeHealthBar();
    }

    private void _damageable_onHit()
    {
        UpdateHeartImage();
    }

    private void OnDestroy()
    {
        _damageable.onHit -= _damageable_onHit;
    }

    private void InitializeHealthBar()
    {
        UpdateHeartImage();
    }

    private void UpdateHeartImage()
    {
        float heartPercent = 0;

        heartPercent = (float)_healthSO.CurrentHealth / (float)_healthSO.DynamicHealth;

        _slidingImage.fillAmount = heartPercent;
    }
}

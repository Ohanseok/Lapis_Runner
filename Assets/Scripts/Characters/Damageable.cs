using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private HealthSO _currentHealthSO;

    [Header("Listening on")]
    

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _updateHealthUI = default;
    [SerializeField] private VoidEventChannelSO _hitEvent = default;
    [SerializeField] private VoidEventChannelSO _deathEvent = default;

    public event UnityAction OnDie;

    public bool GetHit { get; set; }
    public bool IsDead { get; set; }

    private void Awake()
    {
        if(_currentHealthSO == null)
        {
            _currentHealthSO = ScriptableObject.CreateInstance<HealthSO>();
            _currentHealthSO.SetMaxHealth(50);
            _currentHealthSO.SetCurrentHealth(50);
        }

        if (_updateHealthUI != null)
            _updateHealthUI.RaiseEvent();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void ReceiveAnAttack(int damage)
    {
        if (IsDead)
            return;

        if (_hitEvent != null)
            _hitEvent.RaiseEvent();

        _currentHealthSO.InflictDamage(damage);

        if (_updateHealthUI != null)
            _updateHealthUI.RaiseEvent();

        GetHit = true;

        if(_currentHealthSO.CurrentHealth <= 0)
        {
            IsDead = true;

            if (OnDie != null)
                OnDie.Invoke();

            if (_deathEvent != null)
                _deathEvent.RaiseEvent();

            _currentHealthSO.SetCurrentHealth(50);
        }
    }
}
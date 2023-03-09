using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public delegate void HitEventHandler();
    public event HitEventHandler onHit;

    [Header("Health")]
    [SerializeField] private HealthSO _currentHealthSO;
    [SerializeField] private HealthConfigSO _configHealthSO;

    [Header("Listening on")]
    

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _updateHealthUI = default;
    [SerializeField] private VoidEventChannelSO _hitEvent = default;
    [SerializeField] private VoidEventChannelSO _deathEvent = default;

    public event UnityAction OnDie;

    public bool GetHit { get; set; }
    public bool IsDead { get; set; }

    public HealthSO CurrentHealthSO => _currentHealthSO;

    private void Awake()
    {
        if(_currentHealthSO == null)
        {
            _currentHealthSO = ScriptableObject.CreateInstance<HealthSO>();
            _currentHealthSO.Init(_configHealthSO);
            _currentHealthSO.SetMaxHealth();
            //_currentHealthSO.SetMaxHealth(1200);
            //_currentHealthSO.SetCurrentHealth(1200);
        }
        else
        {
            // Damageable에서 Health 설정하는 부분을 다른 곳으로 옮기던가
            // 여기서 아이템을 돌아서 설정하던가
            _currentHealthSO.Init(_configHealthSO);
            
            _currentHealthSO.SetMaxHealth();
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

        _currentHealthSO.InflictDamage(damage);

        onHit?.Invoke();

        if (_hitEvent != null)
            _hitEvent.RaiseEvent();

        if (_updateHealthUI != null)
            _updateHealthUI.RaiseEvent();

        //GetHit = true;

        if(_currentHealthSO.CurrentHealth <= 0)
        {
            IsDead = true;

            if (OnDie != null)
                OnDie.Invoke();

            if (_deathEvent != null)
                _deathEvent.RaiseEvent();

            _currentHealthSO.SetMaxHealth();
        }
    }
}

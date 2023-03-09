using UnityEngine;

[CreateAssetMenu(fileName = "PlayersHealth", menuName = "EntityConfig/Player's Health")]
public class HealthSO : ScriptableObject
{
	// 기본 설정 값 (몬스터의 경우 Instance를 생성하므로 설정된 configSO가 있을 수 없다.
	// Damageable에서 별도로 Config에 접근하도록 한다.
	[SerializeField] [ReadOnly] private HealthConfigSO configSO;

	// 버프 총량
	[SerializeField] [ReadOnly] private int _healthBuff;
	[SerializeField] [ReadOnly] private float _healthBuffRate;

	// 최종 최대 Health
	[SerializeField] [ReadOnly] private int _dynamicHealth;

	private int _currentHealth;

	public int HealthBuff => _healthBuff;

	public float HealthBuffRate => _healthBuffRate;

	public int DynamicHealth => _dynamicHealth;

	public int CurrentHealth => _currentHealth;

	public void Init(HealthConfigSO _configHealthSO)
    {
		configSO = _configHealthSO;

		_healthBuff = 0;
		_healthBuffRate = 0.0f;

		_currentHealth = _dynamicHealth = configSO.InitialHealth;
	}

	public void IncreaseBuff(int incValue)
    {
		_healthBuff += incValue;

		CalcDynamicHealth();
	}

	public void IncreaseBuffRate(float incValue)
    {
		_healthBuffRate += incValue;

		CalcDynamicHealth();
	}

	private void CalcDynamicHealth()
    {
		int platHealth = configSO.InitialHealth + _healthBuff;

		// HP 계산 공식 반영
		_dynamicHealth = (int)(platHealth + platHealth * (_healthBuffRate / 100));
	}

	public void SetMaxHealth()
    {
		_currentHealth = _dynamicHealth;
	}

	public void InflictDamage(int damage)
    {
		_currentHealth -= damage;
	}

	public void RestoreHealth(int healthValue)
    {
		_currentHealth += healthValue;
		if (_currentHealth > _dynamicHealth)
		{
			SetMaxHealth();
		}
	}
}

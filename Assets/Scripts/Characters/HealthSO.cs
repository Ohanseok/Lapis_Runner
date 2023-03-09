using UnityEngine;

[CreateAssetMenu(fileName = "PlayersHealth", menuName = "EntityConfig/Player's Health")]
public class HealthSO : ScriptableObject
{
	// �⺻ ���� �� (������ ��� Instance�� �����ϹǷ� ������ configSO�� ���� �� ����.
	// Damageable���� ������ Config�� �����ϵ��� �Ѵ�.
	[SerializeField] [ReadOnly] private HealthConfigSO configSO;

	// ���� �ѷ�
	[SerializeField] [ReadOnly] private int _healthBuff;
	[SerializeField] [ReadOnly] private float _healthBuffRate;

	// ���� �ִ� Health
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

		// HP ��� ���� �ݿ�
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

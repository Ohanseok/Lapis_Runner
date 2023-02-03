using UnityEngine;

[CreateAssetMenu(fileName = "PlayersHealth", menuName = "EntityConfig/Player's Health")]
public class HealthSO : ScriptableObject
{
    [Tooltip("The initial health")]
    [SerializeField] [ReadOnly] private int _maxHealth;
    [SerializeField] [ReadOnly] private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

	public void SetMaxHealth(int newValue)
	{
		_maxHealth = newValue;
	}

	public void SetCurrentHealth(int newValue)
	{
		_currentHealth = newValue;
	}

	public void InflictDamage(int DamageValue)
	{
		_currentHealth -= DamageValue;
	}

	public void RestoreHealth(int HealthValue)
	{
		_currentHealth += HealthValue;
		if (_currentHealth > _maxHealth)
			_currentHealth = _maxHealth;
	}
}
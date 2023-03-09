using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersVariable", menuName = "Variables/Player Variable")]
public class CharacterVariablesSO : ScriptableObject
{
    [Header("ClassType")]
    [SerializeField] private ClassTypeSO _type;
    [Header("체력")]
    [SerializeField] private HealthSO _health;
    [Header("공격력")]
    [SerializeField] private DamageSO _damage;
    [Header("공격 속도")]
    [SerializeField] private AttackSpeedSO _attackSpeed;
    [Header("크리티컬 대미지")]
    [SerializeField] private CriticalDamageSO _criticalDamage;
    [Header("스킬 쿨타임 감소")]
    [SerializeField] private SkillCooltimeDecreaseSO _skillCooldown;
    [Header("스킬 대미지")]
    [SerializeField] private SkillDamageSO _skillDamage;

    public HealthSO Health => _health;
}
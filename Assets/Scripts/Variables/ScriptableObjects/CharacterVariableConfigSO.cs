using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_VariableConfig", menuName = "Variables/Player Variable Config")]
public class CharacterVariableConfigSO : ScriptableObject
{
    [Header("ClassType")]
    [SerializeField] private ClassTypeSO _type;
    [Header("체력")]
    [SerializeField] private HealthConfigSO _health;
    [Header("공격력")]
    [SerializeField] private DamageConfigSO _damage;
    [Header("공격 속도")]
    [SerializeField] private AttackSpeedConfigSO _attackSpeed;
    [Header("크리티컬 대미지")]
    [SerializeField] private CriticalDamageConfigSO _criticalDamage;
    [Header("스킬 쿨타임 감소")]
    [SerializeField] private SkillCoolDownConfigSO _skillCooldown;
    [Header("스킬 대미지")]
    [SerializeField] private SkillDamageConfigSO _skillDamage;
}

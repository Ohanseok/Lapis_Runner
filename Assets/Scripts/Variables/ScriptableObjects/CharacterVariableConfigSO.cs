using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_VariableConfig", menuName = "Variables/Player Variable Config")]
public class CharacterVariableConfigSO : ScriptableObject
{
    [Header("ClassType")]
    [SerializeField] private ClassTypeSO _type;
    [Header("ü��")]
    [SerializeField] private HealthConfigSO _health;
    [Header("���ݷ�")]
    [SerializeField] private DamageConfigSO _damage;
    [Header("���� �ӵ�")]
    [SerializeField] private AttackSpeedConfigSO _attackSpeed;
    [Header("ũ��Ƽ�� �����")]
    [SerializeField] private CriticalDamageConfigSO _criticalDamage;
    [Header("��ų ��Ÿ�� ����")]
    [SerializeField] private SkillCoolDownConfigSO _skillCooldown;
    [Header("��ų �����")]
    [SerializeField] private SkillDamageConfigSO _skillDamage;
}

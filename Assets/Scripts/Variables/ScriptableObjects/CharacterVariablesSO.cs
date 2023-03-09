using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersVariable", menuName = "Variables/Player Variable")]
public class CharacterVariablesSO : ScriptableObject
{
    [Header("ClassType")]
    [SerializeField] private ClassTypeSO _type;
    [Header("ü��")]
    [SerializeField] private HealthSO _health;
    [Header("���ݷ�")]
    [SerializeField] private DamageSO _damage;
    [Header("���� �ӵ�")]
    [SerializeField] private AttackSpeedSO _attackSpeed;
    [Header("ũ��Ƽ�� �����")]
    [SerializeField] private CriticalDamageSO _criticalDamage;
    [Header("��ų ��Ÿ�� ����")]
    [SerializeField] private SkillCooltimeDecreaseSO _skillCooldown;
    [Header("��ų �����")]
    [SerializeField] private SkillDamageSO _skillDamage;

    public HealthSO Health => _health;
}
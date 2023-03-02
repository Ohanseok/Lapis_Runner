using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityValue
{
    [SerializeField] private AbilitySO _ability;

    public AbilitySO Ability => _ability;

    // �� �������� Ÿ�԰� Ƽ��, ������ ���� ��ġ�� �����Ѵ�.
    public float Value;

    public AbilityValue()
    {
        _ability = null;
        Value = 0.0f;
    }

    public AbilityValue(AbilitySO ability, float value)
    {
        _ability = ability;
        Value = value;
    }
}

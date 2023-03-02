using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityValue
{
    [SerializeField] private AbilitySO _ability;

    public AbilitySO Ability => _ability;

    // 이 아이템의 타입과 티어, 레벨을 보고 수치를 결정한다.
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

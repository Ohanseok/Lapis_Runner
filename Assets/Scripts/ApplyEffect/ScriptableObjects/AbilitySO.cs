using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Ability", menuName = "Talents/Ability")]
public class AbilitySO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString _name = default;

    [SerializeField] private string _description = default;

    [SerializeField] private bool _isRate = default;

    public string Description => _description;

    public bool IsRate => _isRate;

    public float Value(int level)
    {
        // 각각 계산 공식을 가지고 있자. SO로 가지고 있던지 해서 처리할 예정
        return level * 100;
    }
}

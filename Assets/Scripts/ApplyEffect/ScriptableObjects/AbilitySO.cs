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
        // ���� ��� ������ ������ ����. SO�� ������ �ִ��� �ؼ� ó���� ����
        return level * 100;
    }
}

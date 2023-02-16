using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public enum characterType
{
    Infantry,
    Archer,
    Darkmage,
    Monk
}

[CreateAssetMenu(fileName = "CharacterType", menuName = "Characters/CharacterType")]
public class CharacterTypeSO : ScriptableObject
{
    [SerializeField] private characterType _type = default;
    [SerializeField] private UICharactersTabSO _tabType = default;

    public characterType Type => _type;
    public UICharactersTabSO TabType => _tabType;
}

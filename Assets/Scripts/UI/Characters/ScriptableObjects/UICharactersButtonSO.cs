using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharactersButtonType
{
    TotalPromotion,
    TotalSkillLearning,
    Stats,
    GrowthSetEffect
}

[CreateAssetMenu(fileName = "CharactersButton", menuName = "UI/Characters/Button Type")]
public class UICharactersButtonSO : ScriptableObject
{
    [SerializeField] private CharactersButtonType _buttonType = default;

    public CharactersButtonType ButtonType => _buttonType;
}

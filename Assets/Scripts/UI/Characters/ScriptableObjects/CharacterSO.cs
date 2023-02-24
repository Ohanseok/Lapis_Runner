using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/Character")]
public class CharacterSO : ItemSO
{
    [SerializeField] private CharacterTierSO _tier = default;

    [SerializeField] private CharacterGradeSO _grade = default;

    [SerializeField] private ItemSO _skillBook = default;

    public CharacterTierSO Tier => _tier;
    public CharacterGradeSO Grade => _grade;
    public ItemSO SkillBook => _skillBook;
}

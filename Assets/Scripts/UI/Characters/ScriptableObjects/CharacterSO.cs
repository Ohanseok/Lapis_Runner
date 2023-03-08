using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/Character")]
public class CharacterSO : ItemSO
{
    [SerializeField] private CharacterTierSO _tier = default;

    [SerializeField] private CharacterGradeSO _grade = default;

    [SerializeField] private List<ItemSO> _skillBook = default;

    [SerializeField] private List<AbilitySO> _equireAbilitys = default;

    [SerializeField] private LocationTypeSO _locationType = default;

    public CharacterTierSO Tier => _tier;
    public CharacterGradeSO Grade => _grade;
    public List<ItemSO> SkillBook => _skillBook;

    public List<AbilitySO> EquireAbilitys => _equireAbilitys;

    public LocationTypeSO LocationType => _locationType;
}

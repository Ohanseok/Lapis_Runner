using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/Character")]
public class CharacterSO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString _name = default;

    [SerializeField] private Sprite _previewImage = default;

    [SerializeField] private CharacterTypeSO _characterType = default;

    [SerializeField] private GameObject _prefab = default;

    [SerializeField] private CharacterTierSO _tier = default;

    [SerializeField] private CharacterGradeSO _grade = default;

    [SerializeField] private int _needPieceCount = default;

    public LocalizedString Name => _name;
    public Sprite PreviewImage => _previewImage;
    public CharacterTypeSO CharacterType => _characterType;
    public GameObject Prefab => _prefab;
    public CharacterTierSO Tier => _tier;
    public CharacterGradeSO Grade => _grade;
}

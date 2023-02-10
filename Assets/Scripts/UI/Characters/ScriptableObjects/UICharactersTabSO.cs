using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharactersTabType
{
    Infantry,
    Archer,
    DarkMage,
    Monk
}

[CreateAssetMenu(fileName = "CharactersTabType", menuName = "UI/Characters/Tab Type")]
public class UICharactersTabSO : ScriptableObject
{
    [SerializeField] private Sprite _tabIcon = default;
    [SerializeField] private CharactersTabType _tabType = default;

    public Sprite TabIcon => _tabIcon;
    public CharactersTabType TabType => _tabType;
}

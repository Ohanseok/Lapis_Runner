using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum characterGrade
{
    One,
    Two,
    Three
}

[CreateAssetMenu(fileName = "CharacterGrade", menuName = "Characters/CharacterGrade")]
public class CharacterGradeSO : ScriptableObject
{
    [SerializeField] private characterGrade _grade = default;

    public characterGrade Grade => _grade;
}

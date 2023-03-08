using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum classType
{
    Infantry,
    Archer,
    DarkMage,
    Monk
}

[CreateAssetMenu(fileName = "ClassType", menuName = "Classes/ClassType")]
public class ClassTypeSO : ScriptableObject
{
    [SerializeField] private classType _type = default;

    public classType Type => _type;
}

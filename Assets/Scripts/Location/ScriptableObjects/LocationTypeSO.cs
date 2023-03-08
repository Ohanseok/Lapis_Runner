using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum locationType
{
    Summon_Infantry,
    Summon_Archer,
    Summon_DarkMage,
    Summon_Monk,
    Summon_Enemy_Random1,
    Summon_Enemy_Random2,
    Summon_Enemy_Random3,
    Summon_Enemy_Random4,
    Summon_Enemy_Random5
}

[CreateAssetMenu(fileName = "LocationType", menuName = "Locations/LocationType")]
public class LocationTypeSO : ScriptableObject
{
    [SerializeField] private locationType _type = default;

    public locationType Type => _type;
}

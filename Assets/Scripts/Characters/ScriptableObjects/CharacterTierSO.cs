using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum characterTier
{
    Normal,
    Beginner,
    Intermediate,
    Skilled,
    Hero,
    Master,
    Imperial,
    Legend,
    Myth,
    Transcendence,
    Arousal
}

[CreateAssetMenu(fileName = "CharacterTier", menuName = "Characters/CharacterTier")]
public class CharacterTierSO : ScriptableObject
{
    [SerializeField] private characterTier _tier = default;

    public characterTier Tier => _tier;
}

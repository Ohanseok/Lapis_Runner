using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Ability", menuName = "Talents/Ability")]
public class AbilitySO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString _name = default;

    [SerializeField] private string _description = default;

    public string Description => _description;
}

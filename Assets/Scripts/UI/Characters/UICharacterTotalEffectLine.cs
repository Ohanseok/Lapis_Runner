using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharacterTotalEffectLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _desc = default;
    [SerializeField] private TextMeshProUGUI _value = default;

    public void SetData(AbilitySO ability, string value)
    {
        _desc.text = ability.Description;
        _value.text = value;
        _value.text += (ability.IsRate == true) ? "%" : "";
    }
}

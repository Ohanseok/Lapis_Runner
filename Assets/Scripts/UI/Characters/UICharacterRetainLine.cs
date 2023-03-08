using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharacterRetainLine : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private TextMeshProUGUI _description = default;
    [SerializeField] private TextMeshProUGUI _value = default;
    [SerializeField] private TextMeshProUGUI _nextValue = default;

    private AbilitySO _currentAbility = default;

    public void SetData(AbilitySO _ability, string value, string nextValue)
    {
        Clear();

        _currentAbility = _ability;

        if(_currentAbility != null)
        {
            _description.text = _currentAbility.Description;
            _value.text = value;
            _value.text += (_ability.IsRate == true) ? "%" : "";
            _nextValue.text = nextValue;
            _nextValue.text += (_ability.IsRate == true) ? "%" : "";
        }
    }

    public void Clear()
    {
        _currentAbility = null;
        _description.text = "";
        _value.text = "";
        _nextValue.text = "";
    }
}

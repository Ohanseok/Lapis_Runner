using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharactersRetainEffect : MonoBehaviour
{
    private UICharacterLineGroup _group = default;

    private void Awake()
    {
        _group = GetComponentInChildren<UICharacterLineGroup>();
    }

    public void SetValue(List<AbilitySO> _ability, int level = 0)
    {
        _group.SetLine(_ability, level);        
    }
}

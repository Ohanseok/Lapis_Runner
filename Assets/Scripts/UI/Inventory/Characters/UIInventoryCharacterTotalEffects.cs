using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryCharacterTotalEffects : MonoBehaviour
{
    [SerializeField] private GameObject _line = default;
    [SerializeField] private GameObject _content = default;
    
    public void OpenTotalEffects(TalentsSO talents)
    {
        _content.transform.DetachChildren();

        foreach (var ability in talents.Abilitys)
        {
            UICharacterTotalEffectLine line = Instantiate(_line.GetComponent<UICharacterTotalEffectLine>(), _content.transform);
            line.SetData(ability.Ability, ability.Value.ToString());
        }

        gameObject.SetActive(true);
    }
}

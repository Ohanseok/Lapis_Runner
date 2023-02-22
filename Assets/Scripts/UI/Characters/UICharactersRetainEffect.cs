using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharactersRetainEffect : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _values;

    public void SetValue(List<StatSO> _stats, int level = 1)
    {
        for(int i = 0; i < _names.Count; i++)
        {
            _names[i].text = "";
            _values[i].text = "";
        }

        for(int i = 0; i < _stats.Count; i++)
        {
            _names[i].text = _stats[i].Name;
            _values[i].text = _stats[i].RunTimeValue(level);
        }
    }
}

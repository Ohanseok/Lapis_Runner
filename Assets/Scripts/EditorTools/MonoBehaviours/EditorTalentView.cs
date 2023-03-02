using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorTalentView : MonoBehaviour
{
    [SerializeField] private TalentsSO _talents;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _value;

    private void Start()
    {
        Refresh();
        
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (_talents.Abilitys.Count == 0)
        {
            _name.text = "¾øÀ½";
            _value.text = "";
        }
        else
        {
            _name.text = "";
            _value.text = "";

            for (int i = 0; i < _talents.Abilitys.Count; i++)
            {
                _name.text += _talents.Abilitys[i].Ability.Description + "\n";
                _value.text += _talents.Abilitys[i].Value.ToString() + "\n";
            }
        }
    }
}

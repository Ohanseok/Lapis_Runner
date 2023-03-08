using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterLineGroup : MonoBehaviour
{
    [SerializeField] private List<UICharacterRetainLine> _lines = new List<UICharacterRetainLine>();

    public void SetLine(List<AbilitySO> _ability, int level = 1)
    {
        bool isSet = false;

        foreach(var line in _lines)
        {
            isSet = false;

            for(int i = 0; i < _ability.Count; i++)
            {
                if(i == _lines.IndexOf(line))
                {
                    line.gameObject.SetActive(true);
                    line.SetData(_ability[i], _ability[i].Value(level).ToString(), _ability[i].Value(level+1).ToString());
                    isSet = true;
                    break;
                }
            }

            if (!isSet)
            {
                line.SetData(null, "", "");
                line.gameObject.SetActive(false);
            }
        }
    }
}

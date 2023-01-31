using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public List<Transform> _effects;

    public void StartEffect(string effectName)
    {
        for(int i = 0; i < _effects.Count; i++)
        {
            if(_effects[i].name.CompareTo(effectName) == 0)
            {
                _effects[i].gameObject.SetActive(false);
                _effects[i].gameObject.SetActive(true);
                break;
            }
        }
    }
}

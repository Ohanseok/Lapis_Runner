using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
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

    private int _damage = 0;

    public void StartEffect(string effectName, Transform trans, int damage)
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].name.CompareTo(effectName) == 0)
            {
                _effects[i].gameObject.SetActive(false);

                _damage = damage;
                _effects[i].localPosition = new Vector3(0, 0, 0);
                _effects[i].position = trans.position;
                _effects[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    public void OnTriggerDamage(bool entered, GameObject who)
    {
        if (entered && who.TryGetComponent(out Damageable d))
        {
            d.ReceiveAnAttack(_damage);
        }
    }

    public void StopEffect(string effectName)
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].name.CompareTo(effectName) == 0)
            {
                _effects[i].gameObject.SetActive(false);
                break;
            }
        }
    }
}

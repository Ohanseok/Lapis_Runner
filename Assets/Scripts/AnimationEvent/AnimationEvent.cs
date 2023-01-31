using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void StartEffect(string effectName)
    {
       GetComponentInParent<Player>().StartEffect(effectName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/UI/Item Event Channel")]
public class ItemEventChannelSO : DescriptionBaseSO
{
    public UnityAction<ItemSO> OnEventRaised;

    public void RaiseEvent(ItemSO item)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(item);
    }
}

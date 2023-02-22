using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ErrorType
{
    LackOfCondition_Archer,
    LackOfCondition_DarkMage,
    LackOfCondition_Monk
}

[CreateAssetMenu(menuName = "Events/Error Msg Event Channel")]
public class ErrorMsgEventChannelSO : DescriptionBaseSO
{
    public event UnityAction<ErrorType> OnEventRaised;

    public void RaiseEvent(ErrorType value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
    }
}

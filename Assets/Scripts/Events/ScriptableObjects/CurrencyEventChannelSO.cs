using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/UI/Currency Event Channel")]
public class CurrencyEventChannelSO : DescriptionBaseSO
{
	public UnityAction<CurrencySO> OnEventRaised;

	public void RaiseEvent(CurrencySO currency)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(currency);
	}
}

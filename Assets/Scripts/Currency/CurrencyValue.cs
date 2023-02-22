using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CurrencyValue
{
    [SerializeField] private CurrencySO _currency;

    public CurrencySO Currency => _currency;

    public int Value;

    public CurrencyValue()
    {
        _currency = null;
        Value = 0;
    }

    public CurrencyValue(CurrencyValue currencyValue)
    {
        _currency = currencyValue.Currency;
        Value = currencyValue.Value;
    }

    public CurrencyValue(CurrencySO currency, int value)
    {
        _currency = currency;
        Value = value;
    }
}

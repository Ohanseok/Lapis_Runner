using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EditorCurrencyView : MonoBehaviour
{
    [SerializeField] private WalletSO _wallet;
    [SerializeField] private CurrencySO _currency;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _value;

    [SerializeField] private CurrencyEventChannelSO _onChangeCurrency = default;

    private void Start()
    {
        _label.text = _currency.name;

        RefreshText();
    }

    private void OnEnable()
    {
        _onChangeCurrency.OnEventRaised += OnChange;
    }

    private void OnDisable()
    {
        _onChangeCurrency.OnEventRaised -= OnChange;
    }

    private void OnChange(CurrencySO arg0)
    {
        if(arg0 == _currency)
        {
            RefreshText();
        }
    }

    private void RefreshText()
    {
        var result = _wallet.Currencys.Where(o => o.Currency == _currency).FirstOrDefault();
        if (!result.Equals(default(CurrencySO)))
        {
            
            _value.text = result.Value.ToString();

            Debug.Log("RefreshText() : " + result.Currency.name + ", value : " + result.Value.ToString());
        }
    }

    public void IncreasedValue()
    {
        _wallet.Increased(_currency, 1000);
    }
}

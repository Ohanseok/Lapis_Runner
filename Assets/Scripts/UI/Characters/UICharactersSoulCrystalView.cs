using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharactersSoulCrystalView : MonoBehaviour
{
    [SerializeField] private WalletSO _wallet = default;
    [SerializeField] private CurrencySO _currency = default;
    [SerializeField] private TextMeshProUGUI _currencyValue = default;

    [Header("Listening to")]
    [SerializeField] private CurrencyEventChannelSO _changeCurrency = default;

    private void OnEnable()
    {
        RefreshCurrency();

        _changeCurrency.OnEventRaised += OnUse;
    }

    private void OnDisable()
    {
        _changeCurrency.OnEventRaised -= OnUse;
    }

    private void OnUse(CurrencySO currency)
    {
        if (currency != _currency) return;

        RefreshCurrency();
    }

    private void RefreshCurrency()
    {
        var result = _wallet.Currencys.Find(o => o.Currency == _currency);
        if (result != null)
        {
            _currencyValue.text = result.Value.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wallet", menuName = "Currencys/Wallet")]
public class WalletSO : ScriptableObject
{
    [SerializeField] private List<CurrencyValue> _currencys = new List<CurrencyValue>();

    public List<CurrencyValue> Currencys => _currencys;

    [Header("Broadcasting on")]
    [SerializeField] private CurrencyEventChannelSO _onChangeCurrency = default;

    public void Init()
    {
        if(_currencys == null)
        {
            _currencys = new List<CurrencyValue>();
        }

        foreach (CurrencyValue currency in _currencys)
        {
            currency.Value = 0;

            _onChangeCurrency.RaiseEvent(currency.Currency);
        }
    }

    public void Increased(CurrencySO currency, int value)
    {
        if (value <= 0)
            return;

        for (int i = 0; i < _currencys.Count; i++)
        {
            CurrencyValue currentCurrencyValue = _currencys[i];
            if (currency == currentCurrencyValue.Currency)
            {
                currentCurrencyValue.Value += value;

                _onChangeCurrency.RaiseEvent(currency);
                return;
            }
        }
    }

    public void Descreased(CurrencySO currency, int value)
    {
        if (value <= 0)
            return;

        for (int i = 0; i < _currencys.Count; i++)
        {
            CurrencyValue currentCurrencyValue = _currencys[i];
            if (currency == currentCurrencyValue.Currency)
            {
                currentCurrencyValue.Value -= value;

                if (currentCurrencyValue.Value < 0)
                    currentCurrencyValue.Value = 0;

                _onChangeCurrency.RaiseEvent(currency);
                return;
            }
        }
    }
}

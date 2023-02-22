using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Currency", menuName = "Currencys/Currency")]
public class CurrencySO : SerializableScriptableObject
{
    [SerializeField] private LocalizedString _name = default;

    [SerializeField] private Sprite _previewImage = default;

    [SerializeField] private string _description = default;

}

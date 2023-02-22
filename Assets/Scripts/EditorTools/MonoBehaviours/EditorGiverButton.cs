using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorGiverButton : MonoBehaviour
{
    [SerializeField] private CharacterSO _character;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private InventorySO _currentInventory;


    private void Start()
    {
        _label.text = _character.name + " giver";
    }

    public void GetCharacterPiece()
    {
        _currentInventory.Add(_character);
    }
}

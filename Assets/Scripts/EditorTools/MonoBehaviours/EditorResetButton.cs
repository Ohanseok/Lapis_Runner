using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorResetButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private InventorySO _currentInventory;

    // Start is called before the first frame update
    void Start()
    {
        _label.text = "Character Reset";
    }

    public void OnReset()
    {
        _currentInventory.Init();
    }
}

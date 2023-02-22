using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorResetButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private InventorySO _currentInventory;

    private EditorGiverItems _giver;

    [Header("Broadcasting to")]
    [SerializeField] private VoidEventChannelSO _onLoadSaveData = default;

    private void Awake()
    {
        _giver = GetComponentInParent<EditorGiverItems>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_currentInventory != null)
            _label.text = "Reset";
    }

    public void OnReset()
    {
        if (_giver != null)
            _giver.NewSaveSystem();
        //_currentInventory.Init();
    }

    public void SaveSystem()
    {
        if (_giver != null)
            _giver.SaveSystem();
    }

    public void LoadSaveData()
    {
        if (_onLoadSaveData != null)
            _onLoadSaveData.RaiseEvent();
    }
}

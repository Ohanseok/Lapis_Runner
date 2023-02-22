using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _LoadSaveDataEvent = default;

    private void OnEnable()
    {
        _LoadSaveDataEvent.OnEventRaised += OnLoadData;
    }

    private void OnDisable()
    {
        _LoadSaveDataEvent.OnEventRaised -= OnLoadData;
    }

    private void OnLoadData()
    {
        StartCoroutine(LoadSaveGame());
    }

    private IEnumerator LoadSaveGame()
    {
        yield return StartCoroutine(_saveSystem.LoadSavedWallet());

        yield return StartCoroutine(_saveSystem.LoadSavedInventory());
    }

    public void SaveSystem()
    {
        _saveSystem.SaveDataToDisk();
    }
}

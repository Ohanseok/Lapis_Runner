using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "SaveSystem", menuName = "SaveSystem/SaveSystem")]
public class SaveSystem : ScriptableObject
{
    [SerializeField] private InventorySO _playerInventory = default;
    [SerializeField] private WalletSO _playerWallet = default;
    [SerializeField] private TalentsTableSO _talentTable = default;

    public string saveFilename = "save.bono";
    public string backupSaveFilename = "save.bono.bak";
    public Save saveData = new Save();

    public void SetNewGameData()
    {
        FileManager.WriteToFile(saveFilename, "");

        _talentTable.Init();
        _playerInventory.Init();
        _playerWallet.Init();

        SaveDataToDisk();
    }

    public bool LoadSaveDataFromDisk()
    {
        if (FileManager.LoadFromFile(saveFilename, out var json))
        {
            saveData.LoadFromJson(json);

            Debug.Log("LoadFromJson SaveData");

            return true;
        }

        return false;
    }

    public void SaveDataToDisk()
    {
        saveData._itemStacks.Clear();
        saveData._currencyValues.Clear();
        saveData._equireItems.Clear();

        foreach (var currencyValue in _playerWallet.Currencys)
        {
            saveData._currencyValues.Add(new SerializedCurrencyValue(currencyValue.Currency.Guid, currencyValue.Value));
        }

        foreach(var itemStack in _playerInventory.Items)
        {
            saveData._itemStacks.Add(new SerializedItemStack(itemStack.Item.Guid, itemStack.Amount, itemStack.Level));
        }

        foreach(var equireItem in _playerInventory.EquireItems)
        {
            saveData._equireItems.Add(new string(equireItem == null ? null : equireItem.Guid));
        }

        if (FileManager.MoveFile(saveFilename, backupSaveFilename))
        {
            if (FileManager.WriteToFile(saveFilename, saveData.ToJson()))
            {
                Debug.Log("Save successful " + saveFilename);
            }
        }
    }

    public IEnumerator LoadSavedWallet()
    {
        _playerWallet.Init();

        foreach (var serializedCurrencyValue in saveData._currencyValues)
        {
            var loadItemOperationHandle = Addressables.LoadAssetAsync<CurrencySO>(serializedCurrencyValue.currencyGuid);
            yield return loadItemOperationHandle;
            if (loadItemOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                var currencySO = loadItemOperationHandle.Result;
                _playerWallet.Increased(currencySO, serializedCurrencyValue.value);
            }
        }
    }

    public IEnumerator LoadSavedInventory()
    {
        _playerInventory.Items.Clear();

        foreach(var serializedItemStack in saveData._itemStacks)
        {
            var loadItemOperationHandle = Addressables.LoadAssetAsync<ItemSO>(serializedItemStack.itemGuid);
            yield return loadItemOperationHandle;
            if(loadItemOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                var itemSO = loadItemOperationHandle.Result;
                _playerInventory.Add(itemSO, serializedItemStack.amount);
            }
        }

        foreach(var equireItem in saveData._equireItems)
        {
            if(equireItem == null || equireItem == "")
            {
                _playerInventory.ReplaceEquireItem((InventoryTabType)saveData._equireItems.FindIndex(o => o == equireItem), null);
            }
            else
            {
                var item = _playerInventory.Items.Find(o => o.Item.Guid == equireItem);
                _playerInventory.ReplaceEquireItem(item.Item.ItemType.TabType.TabType, item.Item);
            }
        }
    }
}

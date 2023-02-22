using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveSystem", menuName = "SaveSystem/SaveSystem")]
public class SaveSystem : ScriptableObject
{
    [SerializeField] private InventorySO _playerInventory = default;

    public string saveFilename = "save.bono";
    public string backupSaveFilename = "save.bono.bak";
    public Save saveData = new Save();

    public void SetNewGameData()
    {
        _playerInventory.Init();

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

        foreach(var itemStack in _playerInventory.Items)
        {
            saveData._itemStacks.Add(new SerializedItemStack(itemStack.Item.Guid, itemStack.Amount));
        }

        if (FileManager.MoveFile(saveFilename, backupSaveFilename))
        {
            if (FileManager.WriteToFile(saveFilename, saveData.ToJson()))
            {
                Debug.Log("Save successful " + saveFilename);
            }
        }
    }
}

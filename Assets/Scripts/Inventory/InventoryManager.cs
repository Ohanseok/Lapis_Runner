using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;

    public void SaveSystem()
    {
        _saveSystem.SaveDataToDisk();
    }
}

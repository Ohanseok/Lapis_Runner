using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGiverItems : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;

    public void NewSaveSystem()
    {
        _saveSystem.SetNewGameData();
    }

    public void SaveSystem()
    {
        _saveSystem.SaveDataToDisk();
    }
}

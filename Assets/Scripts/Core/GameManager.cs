using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StageManagerSO _stageManager = default;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        Debug.Log("GameManager StartGame");

        //_stageManager.StartGame();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] private Player _playerPrefab = default;

    [Header("Scene Ready Event")]
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;

    private LocationEntrance[] _spawnLocations;
    private Transform _defaultSpawnPoint;

    private void Awake()
    {
        _spawnLocations = GameObject.FindObjectsOfType<LocationEntrance>();
        _defaultSpawnPoint = transform.GetChild(0);
    }

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += SpawnPlayer;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= SpawnPlayer;
    }

    private Transform GetSpawnLocation()
    {
        int entranceIndex = Array.FindIndex(_spawnLocations, element =>
        element.LocationType == LocationEntrance.LOCATION_TYPE.PLAYER);

        if (entranceIndex == -1)
        {
            return _defaultSpawnPoint;
        }
        else
            return _spawnLocations[entranceIndex].transform;
    }

    private void SpawnPlayer()
    {
        Transform spawnLocation = GetSpawnLocation();
        Player playerInstance = Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);

        // Runtime Anchor를 하나 더 만들어서 캐릭터 위치를 추적하고 있어야 할 듯하다.
        // 그럼 리젠을 위한 위치 잡기용 Runtime Anchor는 필요없을 듯하다. 굳이 Runtime Anchor일 이유가???
    }
}

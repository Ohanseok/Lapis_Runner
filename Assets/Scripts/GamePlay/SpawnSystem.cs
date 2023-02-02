using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] private Player _playerPrefab = default;
    [SerializeField] private TransformAnchor _playerTransformAnchor = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private StageEventChannelSO _stageEvent = default;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;
    [SerializeField] private VoidEventChannelSO _alertEnemyEvent = default;
    [SerializeField] private VoidEventChannelSO _fightEnemyEvent = default;

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
        _stageEvent.OnEventRaised += StartStage;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= SpawnPlayer;
        _stageEvent.OnEventRaised -= StartStage;
    }

    private Transform GetSpawnLocation(LocationEntrance.LOCATION_TYPE type)
    {
        int entranceIndex = Array.FindIndex(_spawnLocations, element =>
        element.LocationType == type);

        if (entranceIndex == -1)
        {
            return _defaultSpawnPoint;
        }
        else
            return _spawnLocations[entranceIndex].transform;
    }

    private void SpawnPlayer()
    {
        Transform spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.PLAYER);
        Player playerInstance = Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);

        // Runtime Anchor를 하나 더 만들어서 캐릭터 위치를 추적하고 있어야 할 듯하다.
        _playerTransformAnchor.Provide(playerInstance.transform);

        // 그럼 리젠을 위한 위치 잡기용 Runtime Anchor는 필요없을 듯하다. 굳이 Runtime Anchor일 이유가???
    }

    private void StartStage(StageSO stage)
    {
        StartCoroutine(StageProcess(stage));
    }

    IEnumerator StageProcess(StageSO stage)
    {
        if(stage != null)
        {
            if (_startStageEvent != null)
                _startStageEvent.RaiseEvent();

            yield return new WaitForSeconds(3.0f); // 2초 달리다가 (사실 스테이지별로 세팅되거나 고정된 수치가 필요)

            // 여기서 발견 이벤트
            if (_alertEnemyEvent != null)
                _alertEnemyEvent.RaiseEvent();

            Transform spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.ENEMY);

            // Enemy의 타겟으로 Player를 설정
            GameObject enemy = Instantiate(stage.SummonSet.Enemys[Random.Range(0, stage.SummonSet.Enemys.Count)].Prefab, spawnLocation.position, spawnLocation.rotation);
            enemy.GetComponent<Enemy>().OnSetTarget(_playerTransformAnchor.Value.gameObject);

            // Enemy를 Player의 타겟으로 설정
            if (enemy.TryGetComponent(out Damageable damageableComp))
            {
                _playerTransformAnchor.Value.gameObject.GetComponent<Player>().OnSetTarget(enemy);
            }

            yield return new WaitForSeconds(1.0f); // 1초간 천천히 속도 줄이기 (가속도 시스템 들어가면 변경될 부분)

            if (_fightEnemyEvent != null)
                _fightEnemyEvent.RaiseEvent();
        }
    }
}

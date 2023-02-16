using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] private Player _playerPrefab = default;
    [SerializeField] private Player _player2Prefab = default;
    [SerializeField] private Player _player3Prefab = default;
    [SerializeField] private Player _player4Prefab = default;

    [Header("Transform References")]
    [SerializeField] private TransformAnchor _playerTransformAnchor = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private StageEventChannelSO _stageEvent = default;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _startGameEvent = default;
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
        var result = Array.FindAll(_spawnLocations, element => element.LocationType == type);

        if (result.Length < 1)
        {
            return _defaultSpawnPoint;
        }
        else if (result.Length == 1)
        {
            return result[0].transform;
        }
        else
        {
            return result[Random.Range(0, result.Length)].transform;
        }
    }

    private void SpawnPlayer()
    {
        Transform spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.COMMANDER);
        Player playerInstance = Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);

        // 부지휘관은 위치를 추적하지 않아도 어차피 고정이라서 괜찮을 듯.
        // 지휘관은 위,아래를 이동하므로 차후 이펙트 처리가 있을 경우 Runtime 위치를 알아야한다.
        _playerTransformAnchor.Provide(playerInstance.transform);

        Transform spawnLocation2 = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY01COMMANDER);
        Player playerInstance2 = Instantiate(_player2Prefab, spawnLocation2.position, spawnLocation2.rotation);

        Transform spawnLocation3 = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY02COMMANDER);
        Player playerInstance3 = Instantiate(_player3Prefab, spawnLocation3.position, spawnLocation3.rotation);

        Transform spawnLocation4 = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY03COMMANDER);
        Player playerInstance4 = Instantiate(_player4Prefab, spawnLocation4.position, spawnLocation4.rotation);

        /*
        spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY01COMMANDER);
        Instantiate(_player2Prefab, spawnLocation.position, spawnLocation.rotation);

        spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY02COMMANDER);
        Instantiate(_player2Prefab, spawnLocation.position, spawnLocation.rotation);

        spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY03COMMANDER);
        Instantiate(_player2Prefab, spawnLocation.position, spawnLocation.rotation);
        */

        // Runtime Anchor를 하나 더 만들어서 캐릭터 위치를 추적하고 있어야 할 듯하다.
        //_playerTransformAnchor.Provide(playerInstance.transform);

        // 그럼 리젠을 위한 위치 잡기용 Runtime Anchor는 필요없을 듯하다. 굳이 Runtime Anchor일 이유가???

        if (_startGameEvent != null)
            _startGameEvent.RaiseEvent();
    }

    private void StartStage(StageSO stage)
    {
        StartCoroutine(StageProcess(stage));
    }

    IEnumerator StageProcess(StageSO stage)
    {
        Debug.Log("SpawnSystem StageProcess");

        if(stage != null)
        {
            if (_startStageEvent != null)
                _startStageEvent.RaiseEvent();

            yield return new WaitForSeconds(1.0f); // 2초 달리다가 (사실 스테이지별로 세팅되거나 고정된 수치가 필요)

            // 여기서 발견 이벤트
            /*
            if (_alertEnemyEvent != null)
                _alertEnemyEvent.RaiseEvent();
            */

            

            for(int i = 0; i < stage.MaxSummonMonsterCount; i++)
            {
                Transform spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.ENEMY_SUMMON_LINE);

                Instantiate(stage.SummonSet.Enemys[Random.Range(0, stage.SummonSet.Enemys.Count)].Prefab, spawnLocation.position, spawnLocation.rotation);

                yield return new WaitForSeconds(0.25f);
            }

            // Enemy의 타겟으로 Player를 설정
            //GameObject enemy = Instantiate(stage.SummonSet.Enemys[Random.Range(0, stage.SummonSet.Enemys.Count)].Prefab, spawnLocation.position, spawnLocation.rotation);
            //enemy.GetComponent<Enemy>().OnSetTarget(_playerTransformAnchor.Value.gameObject);

            // Enemy를 Player의 타겟으로 설정
            /*
            if (enemy.TryGetComponent(out Damageable damageableComp))
            {
                _playerTransformAnchor.Value.gameObject.GetComponent<Player>().OnSetTarget(enemy);
            }
            */

            /*
            yield return new WaitForSeconds(1.0f); // 1초간 천천히 속도 줄이기 (가속도 시스템 들어가면 변경될 부분)

            if (_fightEnemyEvent != null)
                _fightEnemyEvent.RaiseEvent();
            */
        }
    }
}

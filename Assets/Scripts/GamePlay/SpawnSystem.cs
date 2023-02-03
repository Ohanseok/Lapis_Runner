using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] private Player _playerPrefab = default;
    [SerializeField] private Player _player2Prefab = default;

    [Header("Transform References")]
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

        // �����ְ��� ��ġ�� �������� �ʾƵ� ������ �����̶� ������ ��.
        // ���ְ��� ��,�Ʒ��� �̵��ϹǷ� ���� ����Ʈ ó���� ���� ��� Runtime ��ġ�� �˾ƾ��Ѵ�.
        _playerTransformAnchor.Provide(playerInstance.transform);

        /*
        spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY01COMMANDER);
        Instantiate(_player2Prefab, spawnLocation.position, spawnLocation.rotation);

        spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY02COMMANDER);
        Instantiate(_player2Prefab, spawnLocation.position, spawnLocation.rotation);

        spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.DEPUTY03COMMANDER);
        Instantiate(_player2Prefab, spawnLocation.position, spawnLocation.rotation);
        */

        // Runtime Anchor�� �ϳ� �� ���� ĳ���� ��ġ�� �����ϰ� �־�� �� ���ϴ�.
        //_playerTransformAnchor.Provide(playerInstance.transform);

        // �׷� ������ ���� ��ġ ���� Runtime Anchor�� �ʿ���� ���ϴ�. ���� Runtime Anchor�� ������???
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

            yield return new WaitForSeconds(3.0f); // 2�� �޸��ٰ� (��� ������������ ���õǰų� ������ ��ġ�� �ʿ�)

            // ���⼭ �߰� �̺�Ʈ
            /*
            if (_alertEnemyEvent != null)
                _alertEnemyEvent.RaiseEvent();
            */

            

            for(int i = 0; i < stage.MaxSummonMonsterCount; i++)
            {
                Transform spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.ENEMY_SUMMON_LINE);

                Instantiate(stage.SummonSet.Enemys[Random.Range(0, stage.SummonSet.Enemys.Count)].Prefab, spawnLocation.position, spawnLocation.rotation);

                yield return new WaitForSeconds(0.5f);
            }

            // Enemy�� Ÿ������ Player�� ����
            //GameObject enemy = Instantiate(stage.SummonSet.Enemys[Random.Range(0, stage.SummonSet.Enemys.Count)].Prefab, spawnLocation.position, spawnLocation.rotation);
            //enemy.GetComponent<Enemy>().OnSetTarget(_playerTransformAnchor.Value.gameObject);

            // Enemy�� Player�� Ÿ������ ����
            /*
            if (enemy.TryGetComponent(out Damageable damageableComp))
            {
                _playerTransformAnchor.Value.gameObject.GetComponent<Player>().OnSetTarget(enemy);
            }
            */

            /*
            yield return new WaitForSeconds(1.0f); // 1�ʰ� õõ�� �ӵ� ���̱� (���ӵ� �ý��� ���� ����� �κ�)

            if (_fightEnemyEvent != null)
                _fightEnemyEvent.RaiseEvent();
            */
        }
    }
}

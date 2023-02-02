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

        // Runtime Anchor�� �ϳ� �� ���� ĳ���� ��ġ�� �����ϰ� �־�� �� ���ϴ�.
        _playerTransformAnchor.Provide(playerInstance.transform);

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
            if (_alertEnemyEvent != null)
                _alertEnemyEvent.RaiseEvent();

            Transform spawnLocation = GetSpawnLocation(LocationEntrance.LOCATION_TYPE.ENEMY);

            // Enemy�� Ÿ������ Player�� ����
            GameObject enemy = Instantiate(stage.SummonSet.Enemys[Random.Range(0, stage.SummonSet.Enemys.Count)].Prefab, spawnLocation.position, spawnLocation.rotation);
            enemy.GetComponent<Enemy>().OnSetTarget(_playerTransformAnchor.Value.gameObject);

            // Enemy�� Player�� Ÿ������ ����
            if (enemy.TryGetComponent(out Damageable damageableComp))
            {
                _playerTransformAnchor.Value.gameObject.GetComponent<Player>().OnSetTarget(enemy);
            }

            yield return new WaitForSeconds(1.0f); // 1�ʰ� õõ�� �ӵ� ���̱� (���ӵ� �ý��� ���� ����� �κ�)

            if (_fightEnemyEvent != null)
                _fightEnemyEvent.RaiseEvent();
        }
    }
}

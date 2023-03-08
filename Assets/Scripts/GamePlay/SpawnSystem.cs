using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] private InventorySO _currentInventory = default;

    [Header("Transform References")]
    [SerializeField] private TransformAnchor _playerTransformAnchor = default;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private StageEventChannelSO _stageEvent = default;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _startGameEvent = default;
    [SerializeField] private VoidEventChannelSO _startStageEvent = default;

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

    private Transform GetSpawnLocation(LocationTypeSO type)
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
        // 내 인벤토리를 보고 장착 중인 캐릭터를 소환하자.
        for(int i = 0; i < _currentInventory.EquireItems.Count; i++)
        {
            if (_currentInventory.EquireItems[i] == null) continue;

            ItemSO item = _currentInventory.EquireItems[i];

            Transform spawnLocation = GetSpawnLocation(((CharacterSO)item).LocationType);
            Player playerInstance = Instantiate(item.Prefab.GetComponent<Player>(), spawnLocation.position, spawnLocation.rotation);

            // 부지휘관은 위치를 추적하지 않아도 어차피 고정이라서 괜찮을 듯.
            // 지휘관은 위,아래를 이동하므로 차후 이펙트 처리가 있을 경우 Runtime 위치를 알아야한다.
            switch (item.ItemType.TabType.TabType)
            {
                case InventoryTabType.Infantry:
                    _playerTransformAnchor.Provide(playerInstance.transform);
                    break;
            }
        }

        if (_startGameEvent != null)
            _startGameEvent.RaiseEvent();

        // Runtime Anchor를 하나 더 만들어서 캐릭터 위치를 추적하고 있어야 할 듯하다.
        //_playerTransformAnchor.Provide(playerInstance.transform);

        // 그럼 리젠을 위한 위치 잡기용 Runtime Anchor는 필요없을 듯하다. 굳이 Runtime Anchor일 이유가???
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

            for(int i = 0; i < stage.MaxSummonMonsterCount; i++)
            {
                EnemySO enemy = stage.SummonSet.Enemys[Random.Range(0, stage.SummonSet.Enemys.Count)];

                Transform spawnLocation = GetSpawnLocation(enemy.LocationType);

                Instantiate(enemy.Prefab, spawnLocation.position, spawnLocation.rotation);

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

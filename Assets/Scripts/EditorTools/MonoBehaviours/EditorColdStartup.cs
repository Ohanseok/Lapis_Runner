using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

// 초기화 씬을 거치지 않은 Cold Start를 에디터에서 허용함
public class EditorColdStartup : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private GameSceneSO _thisSceneSO = default;
    [SerializeField] private GameSceneSO _persistentManagersSO = default;
    [SerializeField] private AssetReference _notifyColdStartupChannel = default;
    [SerializeField] private VoidEventChannelSO _onSceneReadyChannel = default;
    [SerializeField] private SaveSystem _saveSystem = default;

    private bool isColdStart = false;

    private void Awake()
    {
        if (!SceneManager.GetSceneByName(_persistentManagersSO.sceneReference.editorAsset.name).isLoaded)
        {
            isColdStart = true;
        }
        CreateSaveFileIfNotPresent();
    }

    private void Start()
    {
        if (isColdStart)
        {
            _persistentManagersSO.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
        }
        CreateSaveFileIfNotPresent();
    }

    private void CreateSaveFileIfNotPresent()
    {
        if (_saveSystem != null && !_saveSystem.LoadSaveDataFromDisk())
        {
            _saveSystem.SetNewGameData();
        }
    }

    private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
    {
        _notifyColdStartupChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += OnNotifyChannelLoaded;
    }

    private void OnNotifyChannelLoaded(AsyncOperationHandle<LoadEventChannelSO> obj)
    {
        if (_thisSceneSO != null)
        {
            obj.Result.RaiseEvent(_thisSceneSO);
        }
        else
        {
            // 가짜 장면 이벤트를 호출
            _onSceneReadyChannel.RaiseEvent();
            // 여기에 오면 우리가 어떤 씬에 있는건지 정보를 알 방법이 없는 상태이다. 플레이어는 씬 전환이 불가하다.
        }
    }
#endif
}

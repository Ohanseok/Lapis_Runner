using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

// �ʱ�ȭ ���� ��ġ�� ���� Cold Start�� �����Ϳ��� �����
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
            // ��¥ ��� �̺�Ʈ�� ȣ��
            _onSceneReadyChannel.RaiseEvent();
            // ���⿡ ���� �츮�� � ���� �ִ°��� ������ �� ����� ���� �����̴�. �÷��̾�� �� ��ȯ�� �Ұ��ϴ�.
        }
    }
#endif
}

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Load Event Channel")]
public class LoadEventChannelSO : DescriptionBaseSO
{
    public UnityAction<GameSceneSO, bool, bool> OnLoadingRequested;

    public void RaiseEvent(GameSceneSO locationToLoad, bool showLoadingScreen = false, bool fadeScreen = false)
    {
        if (OnLoadingRequested != null)
        {
            OnLoadingRequested.Invoke(locationToLoad, showLoadingScreen, fadeScreen);
        }
        else
        {
            Debug.LogWarning("씬 로드가 요청되었지만 어느 곳에서도 LoadEventChannel을 " +
                "구독하지 않고 있습니다. 해당 이벤트를 수신 대기 중인지 확인하세요.");
        }
    }
}

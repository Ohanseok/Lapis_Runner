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
            Debug.LogWarning("�� �ε尡 ��û�Ǿ����� ��� �������� LoadEventChannel�� " +
                "�������� �ʰ� �ֽ��ϴ�. �ش� �̺�Ʈ�� ���� ��� ������ Ȯ���ϼ���.");
        }
    }
}

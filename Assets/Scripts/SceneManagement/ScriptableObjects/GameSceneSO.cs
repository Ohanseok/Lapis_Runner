using UnityEngine.AddressableAssets;

public class GameSceneSO : DescriptionBaseSO
{
    public GameSceneType sceneType;
    public AssetReference sceneReference;

    public enum GameSceneType
    {
        Location,           // ���� �÷��̰� ����
        Menu,               // ���� ȭ��

        PresistentManagers, // ���� �Ŵ���
        Gameplay            // ȭ�� UI
    }
}

using UnityEngine.AddressableAssets;

public class GameSceneSO : DescriptionBaseSO
{
    public GameSceneType sceneType;
    public AssetReference sceneReference;

    public enum GameSceneType
    {
        Location,           // 게임 플레이가 진행
        Menu,               // 메인 화면

        PresistentManagers, // 상주 매니저
        Gameplay            // 화면 UI
    }
}

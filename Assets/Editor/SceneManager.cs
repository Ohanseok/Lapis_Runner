using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class SceneManager
{
    private const string MenuInit = "Lapis_Knights/Scenes/Init";
    private const string MenuMain = "Lapis_Knights/Scenes/MainMenu";
    private const string MenuGame = "Lapis_Knights/Scenes/01_BurgosPlain";
    private const string MenuGameScreen = "Lapis_Knights/Scenes/Manager/GameScreen";
    private const string MenuPresistent = "Lapis_Knights/Scenes/Manager/Presistent";

    private const string MenuInitPath = "Assets/Scenes/Initialization.unity";
    private const string MenuMainPath = "Assets/Scenes/Menus/MainMenu.unity";
    private const string MenuGamePath = "Assets/Scenes/Locations/01_BurgosPlain.unity";
    private const string MenuGameScreenPath = "Assets/Scenes/Managers/Game.unity";
    private const string MenuPresistentPath = "Assets/Scenes/Managers/PresistentManagers.unity";

    [MenuItem(MenuInit)]
    public static void InitScene()
    {
        Menu.SetChecked(MenuInit, true);
        Menu.SetChecked(MenuMain, false);
        Menu.SetChecked(MenuGame, false);
        Menu.SetChecked(MenuGameScreen, false);
        Menu.SetChecked(MenuPresistent, false);
        OpenInitScene();
    }

    public static void OpenInitScene()
    {
        EditorSceneManager.OpenScene(MenuInitPath);
    }

    [MenuItem(MenuMain)]
    public static void MainScene()
    {
        Menu.SetChecked(MenuInit, false);
        Menu.SetChecked(MenuMain, true);
        Menu.SetChecked(MenuGame, false);
        Menu.SetChecked(MenuGameScreen, false);
        Menu.SetChecked(MenuPresistent, false);

        OpenMainScene();
    }

    public static void OpenMainScene()
    {
        EditorSceneManager.OpenScene(MenuMainPath);
    }

    [MenuItem(MenuGame)]
    public static void GameScene()
    {
        Menu.SetChecked(MenuInit, false);
        Menu.SetChecked(MenuMain, false);
        Menu.SetChecked(MenuGame, true);
        Menu.SetChecked(MenuGameScreen, false);
        Menu.SetChecked(MenuPresistent, false);

        OpenGameScene();
    }

    public static void OpenGameScene()
    {
        EditorSceneManager.OpenScene(MenuGamePath);
    }

    [MenuItem(MenuGameScreen)]
    public static void GameScreenScene()
    {
        Menu.SetChecked(MenuInit, false);
        Menu.SetChecked(MenuMain, false);
        Menu.SetChecked(MenuGame, false);
        Menu.SetChecked(MenuGameScreen, true);
        Menu.SetChecked(MenuPresistent, false);

        OpenGameScreenScene();
    }

    public static void OpenGameScreenScene()
    {
        EditorSceneManager.OpenScene(MenuGameScreenPath);
    }

    [MenuItem(MenuPresistent)]
    public static void PresistentScene()
    {
        Menu.SetChecked(MenuInit, false);
        Menu.SetChecked(MenuMain, false);
        Menu.SetChecked(MenuGame, false);
        Menu.SetChecked(MenuGameScreen, false);
        Menu.SetChecked(MenuPresistent, true);

        OpenPresistentScene();
    }

    public static void OpenPresistentScene()
    {
        EditorSceneManager.OpenScene(MenuPresistentPath);
    }
}

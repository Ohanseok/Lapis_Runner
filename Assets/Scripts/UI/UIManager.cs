using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private VoidEventChannelSO _openMailBoxScreen = default;
    [SerializeField] private VoidEventChannelSO _openSettingScreen = default;
    [SerializeField] private VoidEventChannelSO _openRankingScreen = default;
    [SerializeField] private VoidEventChannelSO _openCollectionsScreen = default;
    [SerializeField] private VoidEventChannelSO _openDungeonsScreen = default;
    [SerializeField] private VoidEventChannelSO _openShopScreen = default;
    [SerializeField] private VoidEventChannelSO _openGachaScreen = default;
    [SerializeField] private VoidEventChannelSO _openMissionsScreen = default;
    [SerializeField] private VoidEventChannelSO _openTrainingScreen = default;
    [SerializeField] private VoidEventChannelSO _openBuffScreen = default;
    [SerializeField] private VoidEventChannelSO _openCharactersScreen = default;
    [SerializeField] private VoidEventChannelSO _openCulsukScreen = default;

    [Header("Popups")]
    [SerializeField] private UIMailBoxController _mailboxScreen = default;
    [SerializeField] private UISettingsController _settingsScreen = default;
    [SerializeField] private UIRankingController _rankingScreen = default;
    [SerializeField] private UICollectionsController _collectionsScreen = default;
    [SerializeField] private UIDungeonsController _dungeonsScreen = default;
    [SerializeField] private UIShopController _shopScreen = default;
    [SerializeField] private UIGachaController _gachaScreen = default;
    [SerializeField] private UIMissionsController _missionsScreen = default;
    [SerializeField] private UITrainingController _trainingScreen = default;
    [SerializeField] private UIBuffController _buffScreen = default;
    [SerializeField] private UICharactersController _charactersScreen = default;
    [SerializeField] private UICulsukController _culsukScreen = default;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += ResetUI;
        _openMailBoxScreen.OnEventRaised += OpenMailBoxScreen;
        _openSettingScreen.OnEventRaised += OpenSettingScreen;
        _openRankingScreen.OnEventRaised += OpenRankingScreen;
        _openCollectionsScreen.OnEventRaised += OpenCollectionsScreen;
        _openDungeonsScreen.OnEventRaised += OpenDungeonsScreen;
        _openShopScreen.OnEventRaised += OpenShopScreen;
        _openGachaScreen.OnEventRaised += OpenGachaScreen;
        _openMissionsScreen.OnEventRaised += OpenMissionsScreen;
        _openTrainingScreen.OnEventRaised += OpenTrainingScreen;
        _openBuffScreen.OnEventRaised += OpenBuffScreen;
        _openCharactersScreen.OnEventRaised += OpenCharactersScreen;
        _openCulsukScreen.OnEventRaised += OpenCulsukScreen;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= ResetUI;
        _openMailBoxScreen.OnEventRaised -= OpenMailBoxScreen;
        _openSettingScreen.OnEventRaised -= OpenSettingScreen;
        _openRankingScreen.OnEventRaised -= OpenRankingScreen;
        _openCollectionsScreen.OnEventRaised -= OpenCollectionsScreen;
        _openDungeonsScreen.OnEventRaised -= OpenDungeonsScreen;
        _openShopScreen.OnEventRaised -= OpenShopScreen;
        _openGachaScreen.OnEventRaised -= OpenGachaScreen;
        _openMissionsScreen.OnEventRaised -= OpenMissionsScreen;
        _openTrainingScreen.OnEventRaised -= OpenTrainingScreen;
        _openBuffScreen.OnEventRaised -= OpenBuffScreen;
        _openCharactersScreen.OnEventRaised -= OpenCharactersScreen;
        _openCulsukScreen.OnEventRaised -= OpenCulsukScreen;
    }

    private void ResetUI()
    {
        _settingsScreen.gameObject.SetActive(false);
        _mailboxScreen.gameObject.SetActive(false);
    }

    #region MailBox
    public void OpenMailBoxScreen()
    {
        _mailboxScreen.Closed += CloseMailBoxScreen;

        _mailboxScreen.gameObject.SetActive(true);
    }

    private void CloseMailBoxScreen()
    {
        _mailboxScreen.Closed -= CloseMailBoxScreen;

        _mailboxScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Settings
    public void OpenSettingScreen()
    {
        _settingsScreen.Closed += CloseSettingScreen;

        _settingsScreen.gameObject.SetActive(true);
    }

    private void CloseSettingScreen()
    {
        _settingsScreen.Closed -= CloseSettingScreen;

        _settingsScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Ranking
    public void OpenRankingScreen()
    {
        _rankingScreen.Closed += CloseRankingScreen;

        _rankingScreen.gameObject.SetActive(true);
    }

    private void CloseRankingScreen()
    {
        _rankingScreen.Closed -= CloseRankingScreen;

        _rankingScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Collections
    public void OpenCollectionsScreen()
    {
        _collectionsScreen.Closed += CloseCollectionsScreen;

        _collectionsScreen.gameObject.SetActive(true);
    }

    private void CloseCollectionsScreen()
    {
        _collectionsScreen.Closed -= CloseCollectionsScreen;

        _collectionsScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Dungeons
    public void OpenDungeonsScreen()
    {
        _dungeonsScreen.Closed += CloseDungeonsScreen;

        _dungeonsScreen.gameObject.SetActive(true);
    }

    private void CloseDungeonsScreen()
    {
        _dungeonsScreen.Closed -= CloseDungeonsScreen;

        _dungeonsScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Shop
    public void OpenShopScreen()
    {
        _shopScreen.Closed += CloseShopScreen;

        _shopScreen.gameObject.SetActive(true);
    }

    private void CloseShopScreen()
    {
        _shopScreen.Closed -= CloseShopScreen;

        _shopScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Gacha
    public void OpenGachaScreen()
    {
        _gachaScreen.Closed += CloseGachaScreen;

        _gachaScreen.gameObject.SetActive(true);
    }

    private void CloseGachaScreen()
    {
        _gachaScreen.Closed -= CloseGachaScreen;

        _gachaScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Missions
    public void OpenMissionsScreen()
    {
        _missionsScreen.Closed += CloseMissionsScreen;

        _missionsScreen.gameObject.SetActive(true);
    }

    private void CloseMissionsScreen()
    {
        _missionsScreen.Closed -= CloseMissionsScreen;

        _missionsScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Training
    public void OpenTrainingScreen()
    {
        _trainingScreen.Closed += CloseTrainingScreen;

        _trainingScreen.gameObject.SetActive(true);
    }

    private void CloseTrainingScreen()
    {
        _trainingScreen.Closed -= CloseTrainingScreen;

        _trainingScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Buff
    public void OpenBuffScreen()
    {
        _buffScreen.Closed += CloseBuffScreen;

        _buffScreen.gameObject.SetActive(true);
    }

    private void CloseBuffScreen()
    {
        _buffScreen.Closed -= CloseBuffScreen;

        _buffScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Characters
    public void OpenCharactersScreen()
    {
        _charactersScreen.Closed += CloseCharactersScreen;

        _charactersScreen.gameObject.SetActive(true);
    }

    private void CloseCharactersScreen()
    {
        _charactersScreen.Closed -= CloseCharactersScreen;

        _charactersScreen.gameObject.SetActive(false);
    }
    #endregion

    #region Culsuk
    public void OpenCulsukScreen()
    {
        _culsukScreen.Closed += CloseCulsukScreen;

        _culsukScreen.gameObject.SetActive(true);
    }

    private void CloseCulsukScreen()
    {
        _culsukScreen.Closed -= CloseCulsukScreen;

        _culsukScreen.gameObject.SetActive(false);
    }
    #endregion
}

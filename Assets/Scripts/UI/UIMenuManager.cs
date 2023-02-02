using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private UIMainMenu _mainMenuPanel = default;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _googleLoginEvent = default;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.4f);
        SetMenuScreen();
    }

    private void SetMenuScreen()
    {
        _mainMenuPanel.GoogleLoginAction += ButtonGoogleLoginClicked;
    }

    private void ButtonGoogleLoginClicked()
    {
        Debug.Log("ButtonGoogleLoginClicked");
        _googleLoginEvent.RaiseEvent();
    }
}

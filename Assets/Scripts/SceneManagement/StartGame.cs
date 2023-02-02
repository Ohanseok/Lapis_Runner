using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameSceneSO _locationToLoad;
    [SerializeField] private bool _showLoadScreen = default;

    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _loadLocation = default;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _onGoogleLoginButton = default;

    private void Start()
    {
        _onGoogleLoginButton.OnEventRaised += GoogleLogin;
    }

    private void OnDisable()
    {
        _onGoogleLoginButton.OnEventRaised -= GoogleLogin;
    }

    private void GoogleLogin()
    {
        _loadLocation.RaiseEvent(_locationToLoad, _showLoadScreen);
    }
}

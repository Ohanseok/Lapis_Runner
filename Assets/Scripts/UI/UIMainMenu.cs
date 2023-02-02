using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button _GoogleLoginButton = default;

    public UnityAction GoogleLoginAction;

    private void OnEnable()
    {
        _GoogleLoginButton.onClick.AddListener(GoogleLoginButton);
    }

    private void OnDisable()
    {
        _GoogleLoginButton.onClick.RemoveListener(GoogleLoginButton);
    }

    private void GoogleLoginButton()
    {
        if(GoogleLoginAction != null)
            GoogleLoginAction.Invoke();
    }
}

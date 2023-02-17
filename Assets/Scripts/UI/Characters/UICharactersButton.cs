using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICharactersButton : MonoBehaviour
{
    public UnityAction<UICharactersButtonSO> ButtonClicked;

    [HideInInspector][ReadOnly] public UICharactersButtonSO _currentButtonType = default;

    public void SetButton(UICharactersButtonSO buttonType)
    {
        _currentButtonType = buttonType;
    }

    public void ClickButton()
    {
        ButtonClicked.Invoke(_currentButtonType);
    }
}

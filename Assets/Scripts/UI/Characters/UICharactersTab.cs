using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharactersTab : MonoBehaviour
{
    public UnityAction<UICharactersTabSO> TabClicked;

    [SerializeField] private Image _tabImage = default;
    [SerializeField] private Button _actionButton = default;
    [SerializeField] private Color _selectedIconColor = default;
    [SerializeField] private Color _deselectedIconColor = default;

    [ReadOnly] public UICharactersTabSO _currentTabType = default;

    public void SetTab(UICharactersTabSO tabType, bool isSelected)
    {
        _currentTabType = tabType;
        _tabImage.sprite = tabType.TabIcon;

        UpdateState(isSelected);
    }

    public void UpdateState(bool isSelected)
    {
        _actionButton.interactable = !isSelected;

        if(isSelected)
        {
            _tabImage.color = _selectedIconColor;
        }
        else
        {
            _tabImage.color = _deselectedIconColor;
        }
    }

    public void ClickButton()
    {
        TabClicked.Invoke(_currentTabType);
    }
}

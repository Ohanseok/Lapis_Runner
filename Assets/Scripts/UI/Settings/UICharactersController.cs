
using System.Collections.Generic;
using UnityEngine;

public class UICharactersController : UIController
{
    [SerializeField] private List<UICharactersTabSO> _tabTypesList = new List<UICharactersTabSO>();

    [SerializeField] private UICharactersTabs _tabsPanel = default;

    private UICharactersTabSO _selectedTab = default;

    private void OnEnable()
    {
        _tabsPanel.TabChanged += OnChangeTab;
    }

    private void OnDisable()
    {
        _tabsPanel.TabChanged -= OnChangeTab;
    }

    private void OnChangeTab(UICharactersTabSO tabType)
    {
        FillCharacter(tabType.TabType);
    }
    
    public void FillCharacter(CharactersTabType _selectedTabType = CharactersTabType.Infantry)
    {
        if((_tabTypesList.Exists(o => o.TabType == _selectedTabType)))
        {
            _selectedTab = _tabTypesList.Find(o => o.TabType == _selectedTabType);
        }
        else
        {
            if(_tabTypesList != null)
            {
                if(_tabTypesList.Count > 0)
                {
                    _selectedTab = _tabTypesList[0];
                }
            }
        }

        if(_selectedTab != null)
        {
            SetTabs(_tabTypesList, _selectedTab);

            // 내부 정보를 세팅
        }
        else
        {
            Debug.LogError("There's no selected tab");
        }
    }

    private void SetTabs(List<UICharactersTabSO> typesList, UICharactersTabSO selectedType)
    {
        _tabsPanel.SetTabs(typesList, selectedType);
    }
}

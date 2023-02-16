
using System.Collections.Generic;
using UnityEngine;

public class UICharactersController : UIController
{
    [SerializeField] private CharactersSO _currentCharacters = default;
    [SerializeField] private UIInventoryCharactersInspector _inspectorPanel = default;
    [SerializeField] private List<UICharactersTabSO> _tabTypesList = new List<UICharactersTabSO>();
    [SerializeField] private List<UICharacterSlot> _availableItemSlots = default;

    [Header("Listening to")]
    [SerializeField] private UICharactersTabs _tabsPanel = default;

    private UICharactersTabSO _selectedTab = default;

    private int selectedItemId = -1;

    private void OnEnable()
    {
        _tabsPanel.TabChanged += OnChangeTab;

        for(int i = 0; i < _availableItemSlots.Count; i++)
        {
            _availableItemSlots[i].ItemSelected += InspectItem;
        }
    }

    private void OnDisable()
    {
        _tabsPanel.TabChanged -= OnChangeTab;

        for(int i = 0; i < _availableItemSlots.Count; i++)
        {
            _availableItemSlots[i].ItemSelected -= InspectItem;
        }
    }

    public void InspectItem(CharacterSO itemToInspect)
    {
        if (itemToInspect == null)
        {
            HideItemInformation();
            return;
        }

        if (_availableItemSlots.Exists(o => o.currentItem != null && o.currentItem.Character == itemToInspect))
        {
            int itemIndex = _availableItemSlots.FindIndex(o => o.currentItem != null && o.currentItem.Character == itemToInspect);

            if (selectedItemId >= 0 && selectedItemId != itemIndex)
                UnselectItem(selectedItemId);

            selectedItemId = itemIndex;

            ShowItemInformation(itemToInspect, selectedItemId);
        }
        else
        {
            // currentItem이 없으므로 내부적으로 가지고 있는 _charactersSO를 찾아서 보여준다.
            ShowItemInformation(itemToInspect);
        }
    }

    private void ShowItemInformation(CharacterSO item, int selectedItemId = -1)
    {
        _inspectorPanel.FillInspector(item, selectedItemId);
        _inspectorPanel.gameObject.SetActive(true);
    }

    private void HideItemInformation()
    {
        _inspectorPanel.gameObject.SetActive(false);
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

            List<CharacterStack> listSlotsToShow = new List<CharacterStack>();
            listSlotsToShow = _currentCharacters.Characters.FindAll(o => o.Character.CharacterType.TabType == _selectedTab);
            // 내부 정보를 세팅
            FillCharacterSlots(listSlotsToShow, _selectedTab);
        }
        else
        {
            Debug.LogError("There's no selected tab");
        }
    }

    private void FillCharacterSlots(List<CharacterStack> listCharactersToShow, UICharactersTabSO _selectedTab)
    {
        if (_availableItemSlots == null)
            _availableItemSlots = new List<UICharacterSlot>();

        //int maxCount = Mathf.Max(listCharactersToShow.Count, _availableItemSlots.Count);

        // 이 부분에서 있는 갯수만큼 돌면서 SetItem을 하지말고, 전체 갯수만큼 돌면서
        // 슬롯이 활성화(1번이라도 먹어본 적이 있는) 여부를 보고, 활성화를 시키자.

        for (int i = 0; i < _availableItemSlots.Count; i++)
        {
            _availableItemSlots[i].SetInactiveItem(_selectedTab);
        }

        // CharacterSO에 티어와 별이 들어가 있고, 그걸 찾아와서 맞는 _availableItemSlots을 활성화
        for(int i = 0; i < listCharactersToShow.Count; i++)
        {
            int iIndex = (int)listCharactersToShow[i].Character.Tier.Tier * 3 + (int)listCharactersToShow[i].Character.Grade.Grade;

            if(iIndex < _availableItemSlots.Count)
            {
                bool isSelected = selectedItemId == iIndex;
                _availableItemSlots[iIndex].SetItem(listCharactersToShow[i], isSelected);
            }
        }

        /*
        for(int i = 0; i < maxCount; i++)
        {
            if(i < listCharactersToShow.Count)
            {
                bool isSelected = selectedItemId == i;
                _availableItemSlots[i].SetItem(listCharactersToShow[i], isSelected);
            }
            else if(i < _availableItemSlots.Count)
            {
                _availableItemSlots[i].SetInactiveItem();
            }
        }
        */

        HideItemInformation();

        if (selectedItemId >= 0)
        {
            UnselectItem(selectedItemId);
            selectedItemId = -1;
        }
        if(_availableItemSlots.Count > 0)
        {
            _availableItemSlots[0].SelectFirstElement();
        }
    }

    private void UnselectItem(int itemIndex)
    {
        if(_availableItemSlots.Count > itemIndex)
        {
            _availableItemSlots[itemIndex].UnselectItem();
        }
    }

    private void SetTabs(List<UICharactersTabSO> typesList, UICharactersTabSO selectedType)
    {
        _tabsPanel.SetTabs(typesList, selectedType);
    }
}

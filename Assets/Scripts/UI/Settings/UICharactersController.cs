
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UICharactersController : UIController
{
    [Header("SO Data")]
    [SerializeField] private TalentsTableSO _currentTalentsTable = default;
    [SerializeField] private WalletSO _currentWallet = default;
    [SerializeField] private CurrencySO _currencyEnhance = default;
    [SerializeField] private InventorySO _currentInventory = default;
    [SerializeField] private CharacterPieceTableSO _currentPiecesTable = default;
    [SerializeField] private List<ClassTypeSO> _classType = new List<ClassTypeSO>();
    [SerializeField] private List<InventoryTabSO> _tabTypesList = new List<InventoryTabSO>();
    [SerializeField] private List<UICharactersButtonSO> _buttonTypeList = new List<UICharactersButtonSO>();

    [Header("Inspector")]
    [SerializeField] private UIInventoryCharactersInspector _inspectorPanel = default;
    [SerializeField] private UIInventoryCharacterTotalEffects _inspector_class = default;

    [Header("Behaviour List")]
    [SerializeField] private List<UICharacterSlot> _availableItemSlots = default;

    [Header("Panels")]
    [SerializeField] private UICharactersTabs _tabsPanel = default;
    [SerializeField] private UICharactersButtons _buttonsPanel = default;

    [Header("Broadcasting to")]
    [SerializeField] private ErrorMsgEventChannelSO _errorMsgEvent = default;
    [SerializeField] private CurrencyEventChannelSO _changeCurrency = default;
    [SerializeField] private ItemEventChannelSO _changeItemLevel = default;

    private InventoryTabSO _selectedTab = default;

    private int equipmentItemId = -1;
    private int selectedItemId = -1;

    public UnityAction<ItemStack> OnDetailSkill;

    private void OnEnable()
    {
        // Tab
        _tabsPanel.TabChanged += OnChangeTab;

        // Function Buttons
        SetButtons(_buttonTypeList);
        _buttonsPanel.ButtonClicked += OnButtonClicked;

        // Total Slot Settings
        for (int i = 0; i < _availableItemSlots.Count; i++)
        {
            _availableItemSlots[i].Init(i);

            foreach (var cls in _classType)
            {
                _availableItemSlots[i].AddDefaultCharacter(_currentPiecesTable.GetPieces(cls).GetPiece(i));
            }
            
            _availableItemSlots[i].ItemSelected += InspectItem;
        }

        // InspectorPanel Buttons
        _inspectorPanel.OnEquip += OnEquipClicked;
        _inspectorPanel.OnPromotion += OnPromotionClicked;
        _inspectorPanel.OnEnhance += OnEnhanceClicked;
        _inspectorPanel.OnDetailSkill += OnDetailSkillButton;

        // Set Default Tab
        OnChangeTab(_tabTypesList[0]);
    }

    private void OnDisable()
    {
        _tabsPanel.TabChanged -= OnChangeTab;

        _buttonsPanel.ButtonClicked -= OnButtonClicked;

        _inspectorPanel.OnEquip -= OnEquipClicked;
        _inspectorPanel.OnPromotion -= OnPromotionClicked;
        _inspectorPanel.OnEnhance -= OnEnhanceClicked;

        for (int i = 0; i < _availableItemSlots.Count; i++)
        {
            _availableItemSlots[i].ItemSelected -= InspectItem;
        }

        _inspectorPanel.OnDetailSkill -= OnDetailSkillButton;
    }

    private void OnDetailSkillButton()
    {
        if (OnDetailSkill != null)
            OnDetailSkill.Invoke(_availableItemSlots[selectedItemId].currentItem);
    }

    private void OnEnhanceClicked()
    {
        if (selectedItemId == -1) return;

        ItemStack currentItem = _availableItemSlots[selectedItemId].currentItem;

        // 필요한 개수를 파악하는 부분도 별도로 클래스가 필요할지도...
        int needCurrency = (int)(((currentItem.Level + (currentItem.Level + 1)) * 2) + ((currentItem.Level + 1) * (currentItem.Level + 2) * 0.05f));

        var currentCurrency = _currentWallet.Currencys.Find(o => o.Currency == _currencyEnhance);

        if (currentCurrency == null) return;

        if (currentCurrency.Value < needCurrency) return;

        var targetItem = _currentInventory.Items.Find(o => o.Item == currentItem.Item);
        if (targetItem == null) return;

        // 지역변수인데 이것도 값이 바뀔까?
        targetItem.Level += 1;

        _currentWallet.Descreased(_currencyEnhance, needCurrency);
        _changeCurrency.RaiseEvent(_currencyEnhance);
        _changeItemLevel.RaiseEvent(targetItem.Item);
    }

    private void OnEquipClicked()
    {
        Debug.Log("장착 버튼 클릭 equipmentItemId(" + equipmentItemId + "), selectedItemId(" + selectedItemId + ")");
        if(selectedItemId != -1 && equipmentItemId != selectedItemId)
        {
            //_availableItemSlots[equipmentItemId].currentItem.isEquip = false;
            _availableItemSlots[equipmentItemId].UnEquipmentItem();
            //_availableItemSlots[selectedItemId].currentItem.isEquip = true;
            _availableItemSlots[selectedItemId].EquipmentItem();
            equipmentItemId = selectedItemId;
            _currentInventory.ReplaceEquireItem(_availableItemSlots[selectedItemId].currentItem.Item.ItemType.TabType.TabType, _availableItemSlots[selectedItemId].currentItem.Item);
            ShowItemInformation(_availableItemSlots[selectedItemId].currentItem);
        }
    }

    private void OnPromotionClicked()
    {
        if (selectedItemId == -1) return;

        // 현재 아이템
        ItemStack currentItem = _availableItemSlots[selectedItemId].currentItem;

        if (currentItem == null) return;

        // 아이템 기본 정보
        CharacterSO currentItemSO = (CharacterSO)currentItem.Item;

        if (currentItem.Amount < currentItemSO.Tier.NeedCount) return;

        _currentInventory.Remove(currentItemSO, currentItemSO.Tier.NeedCount);

        // 한 단계 윗등급 아이템
        if(_availableItemSlots[selectedItemId + 1].currentItem == null)
        {
            // 다음 슬롯에 저장되어 있는 기본 아이템 정보로 조각 추가
            var data = _availableItemSlots[selectedItemId + 1].CharacterSO.Find(o => o.ItemType.TabType == currentItemSO.ItemType.TabType);
            _currentInventory.Add(data);

            // 추가되어진 ItemStack으로 세팅
            _availableItemSlots[selectedItemId + 1].SetItem(_currentInventory.Items.Find(o => o.Item == data), false);
        }
        else
        {
            // 가지고 있는 아이템 갯수 추가
            _currentInventory.Add(_availableItemSlots[selectedItemId + 1].currentItem.Item);

            _availableItemSlots[selectedItemId + 1].SetItem(_availableItemSlots[selectedItemId + 1].currentItem, false);
        }

        _availableItemSlots[selectedItemId].SetItem(currentItem, true);

        ShowItemInformation(currentItem);
    }

    public void InspectItem(ItemSO itemToInspect)
    {
        if (itemToInspect == null)
        {
            HideItemInformation();
            return;
        }

        if (_availableItemSlots.Exists(o => o.currentItem != null && o.currentItem.Item == itemToInspect))
        {
            int itemIndex = _availableItemSlots.FindIndex(o => o.currentItem != null && o.currentItem.Item == itemToInspect);

            if (selectedItemId >= 0 && selectedItemId != itemIndex)
                UnselectItem(selectedItemId);

            selectedItemId = itemIndex;

            //ShowItemInformation(itemToInspect, selectedItemId);
            ShowItemInformation(_availableItemSlots[itemIndex].currentItem);
        }
        else
        {
            // currentItem이 없으므로 내부적으로 가지고 있는 _charactersSO를 찾아서 보여준다.
            ShowItemInformation(itemToInspect);
        }
    }

    private void ShowItemInformation(ItemStack item)
    {
        HideInformation();

        _inspectorPanel.FillInspectorItem(item);
        _inspectorPanel.gameObject.SetActive(true);
    }

    private void ShowItemInformation(ItemSO item, int selectedItemId = -1)
    {
        HideInformation();

        _inspectorPanel.FillInspector(item, selectedItemId);
        _inspectorPanel.gameObject.SetActive(true);
    }

    private void HideItemInformation()
    {
        _inspectorPanel.gameObject.SetActive(false);
    }

    private void ShowInformation()
    {
        HideItemInformation();

        var talents = _currentTalentsTable.TalentsTables.Find(o => o.ClassType == _selectedTab.ClassType);

        _inspector_class.OpenTotalEffects(talents);

        //_inspector_class.gameObject.SetActive(true);
    }

    private void HideInformation()
    {
        _inspector_class.gameObject.SetActive(false);
    }

    private void OnChangeTab(InventoryTabSO tabType)
    {
        var result = _currentInventory.Items.FindAll(o => o.Item.ItemType.TabType == tabType);
        if(result.Count != 0)
            FillCharacterPanel(tabType.TabType);
        else
        {
            switch(tabType.TabType)
            {
                case InventoryTabType.Archer:
                    _errorMsgEvent.RaiseEvent(ErrorType.LackOfCondition_Archer);
                    break;
                case InventoryTabType.DarkMage:
                    _errorMsgEvent.RaiseEvent(ErrorType.LackOfCondition_DarkMage);
                    break;
                case InventoryTabType.Monk:
                    _errorMsgEvent.RaiseEvent(ErrorType.LackOfCondition_Monk);
                    break;
            }
        }
    }
    
    private void OnButtonClicked(UICharactersButtonSO buttonType)
    {
        switch (buttonType.ButtonType)
        {
            case CharactersButtonType.TotalPromotion:
                Debug.Log("일괄 승급 버튼 클릭 처리");
                break;

            case CharactersButtonType.TotalSkillLearning:
                Debug.Log("일괄 스킬 습득 버튼 클릭 처리");
                break;

            case CharactersButtonType.Stats:
                ShowInformation();
                break;

            case CharactersButtonType.GrowthSetEffect:
                Debug.Log("성장 세트 효과 버튼 클릭 처리");
                break;
        }
    }

    // 탭이 선택될 때마다 호출 (기획상 상세 능력치를 보여주도록 설정)
    public void FillCharacterPanel(InventoryTabType _selectedTabType)
    {
        selectedItemId = -1;
        if ((_tabTypesList.Exists(o => o.TabType == _selectedTabType)))
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

            List<ItemStack> listSlotsToShow = new List<ItemStack>();
            listSlotsToShow = _currentInventory.Items.FindAll(o => o.Item.ItemType.Type == itemInventoryType.CharacterPiece && o.Item.ItemType.TabType == _selectedTab);

            // 내부 정보를 세팅
            FillInvetoryItems(listSlotsToShow);
        }
        else
        {
            Debug.LogError("There's no selected tab");
        }
    }

    private void FillInvetoryItems(List<ItemStack> listItemsToShow)
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
        /*
        for(int i = 0; i < listCharactersToShow.Count; i++)
        {
            int iIndex = (int)listCharactersToShow[i].Character.Tier.Tier * 3 + (int)listCharactersToShow[i].Character.Grade.Grade;

            if(iIndex < _availableItemSlots.Count)
            {
                bool isSelected = selectedItemId == iIndex;
                _availableItemSlots[iIndex].SetItem(listCharactersToShow[i], isSelected);
            }
        }

        equipmentItemId = listCharactersToShow.FindIndex(o => o.isEquip);
        */

        for (int i = 0; i < listItemsToShow.Count; i++)
        {
            CharacterSO charSO = (CharacterSO)listItemsToShow[i].Item;

            if(charSO == _currentInventory.EquireItem(_selectedTab.TabType))
            {
                selectedItemId = equipmentItemId = (int)charSO.Tier.Tier * 3 + (int)charSO.Grade.Grade;
            }

            int iIndex = (int)charSO.Tier.Tier * 3 + (int)charSO.Grade.Grade;

            if (iIndex < _availableItemSlots.Count)
            {
                bool isSelected = selectedItemId == iIndex;
                _availableItemSlots[iIndex].SetItem(listItemsToShow[i], isSelected);
            }
        }
    }

    private void UnselectItem(int itemIndex)
    {
        if(_availableItemSlots.Count > itemIndex)
        {
            _availableItemSlots[itemIndex].UnselectItem();
        }
    }

    private void SetTabs(List<InventoryTabSO> typesList, InventoryTabSO selectedType)
    {
        _tabsPanel.SetTabs(typesList, selectedType);
    }

    private void SetButtons(List<UICharactersButtonSO> buttonsList)
    {
        _buttonsPanel.SetButtons(buttonsList);
    }
}

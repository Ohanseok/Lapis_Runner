using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UICharacterSlot : MonoBehaviour
{
    [SerializeField] private Image _charImage = default;
    [SerializeField] private TextMeshProUGUI _itemCount = default;
    [SerializeField] private TextMeshProUGUI _needItemCount = default;
    [SerializeField] private TextMeshProUGUI _levelText = default;
    //[SerializeField] private Image _bgImage = default;
    //[SerializeField] private Image _imgSelected = default;
    [SerializeField] private Image _bgInactiveImage = default;
    [SerializeField] private Button _itemButton = default;
    //[SerializeField] private LocalizeSpriteEvent _bgLocalizedImage = default;
    [SerializeField] private List<GameObject> _stars = new List<GameObject>();

    private List<CharacterSO> _charactersSO = new List<CharacterSO>();

    public UnityAction<ItemSO> ItemSelected;

    [HideInInspector] public ItemStack currentItem;
    InventoryTabSO currentTab;

    public List<CharacterSO> CharacterSO => _charactersSO;

    //bool _isSelected = false;

    private void Awake()
    {
        _itemButton.onClick.AddListener(OnSelect);
    }

    public void Init(int index)
    {
        if (_charactersSO == null)
            _charactersSO = new List<CharacterSO>();

        _charactersSO.Clear();

        foreach(var obj in _stars)
        {
            obj.SetActive(false);
        }

        if (_stars[0] != null) _stars[0].SetActive(true);

        if (_stars[1] != null && index % 3 != 0) _stars[1].SetActive(true);

        if (_stars[2] != null && index % 3 == 2) _stars[2].SetActive(true);
    }

    public void AddDefaultCharacter(CharacterSO so)
    {
        _charactersSO.Add(so);
    }

    public void SetItem(ItemStack itemStack, bool isSelected)
    {
        //_isSelected = isSelected;

        //_bgImage.gameObject.SetActive(true);
        //_itemButton.gameObject.SetActive(true);

        currentItem = itemStack;

        //_imgSelected.gameObject.SetActive(_isSelected);

        //_bgLocalizedImage.enabled = false;

        /*
        CharacterSO _slotData = _charactersSO.Find(o => o.CharacterType.TabType == currentItem.Item.CharacterType.TabType);
        */

            
        /*
        _itemPreviewImage.sprite = itemStack.Character.PreviewImage;
        */

        _itemCount.text = itemStack.Amount.ToString();

        _needItemCount.text = ((CharacterSO)itemStack.Item).Tier.NeedCount.ToString();

        _levelText.text = itemStack.Level.ToString();

        _bgInactiveImage.gameObject.SetActive(false);
        //_bgImage.color = itemStack.Character.CharacterType.type;

        _charImage.sprite = itemStack.Item.PreviewImage;

        //if (_isSelected) SelectItem();
    }

    public void SetInactiveItem(InventoryTabSO _selectedTab)
    {
        currentItem = null;

        currentTab = _selectedTab;

        // 비활성화되어도 보여주는 데이터가 있다.
        CharacterSO _slotData = _charactersSO.Find(o => o.ItemType.TabType == _selectedTab);

        _itemCount.text = "0";
        _needItemCount.text = _slotData.Tier.NeedCount.ToString();

        _levelText.text = "0";

        /*
        if(_slotData != null)
            _itemPreviewImage.sprite = _slotData.PreviewImage;
        */

        //_imgSelected.gameObject.SetActive(false);

        /*
        _itemPreviewImage.gameObject.SetActive(false);
        _itemCount.gameObject.SetActive(false);
        _bgImage.gameObject.SetActive(false);
        _imgSelected.gameObject.SetActive(false);
        */

        //_itemButton.gameObject.SetActive(false);
        _bgInactiveImage.gameObject.SetActive(true);

        _charImage.sprite = _slotData.PreviewImage;
    }

    public void EquipmentItem()
    {
        //_imgSelected.gameObject.SetActive(true);
    }

    public void UnEquipmentItem()
    {
        //_imgSelected.gameObject.SetActive(false);
    }

    public void UnselectItem()
    {
        Debug.Log("UnselectItem");

        //_isSelected = false;
        //_imgSelected.gameObject.SetActive(false);
    }

    private void OnSelect()
    {
        SelectItem();
    }

    public void SelectItem()
    {

        Debug.Log("SelectItem");

        //_isSelected = true;

        // currentItem이 있으면 활성화 되있다는 것.
        if(ItemSelected != null)
        {
            if (currentItem != null && currentItem.Item != null)
            {
                //_imgSelected.gameObject.SetActive(true);
                ItemSelected.Invoke(currentItem.Item);
            }
            else // 활성화 안되어 있으면 기본 데이터만 출력
            {
                //_imgSelected.gameObject.SetActive(false);

                CharacterSO _slotData = _charactersSO.Find(o => o.ItemType.TabType == currentTab);

                if (_slotData != null)
                    ItemSelected.Invoke(_slotData);

                //ItemSelected.Invoke(null);
            }
        }
    } 
}

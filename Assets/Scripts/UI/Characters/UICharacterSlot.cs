using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UICharacterSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemCount = default;
    [SerializeField] private Image _itemPreviewImage = default;
    [SerializeField] private Image _bgImage = default;
    [SerializeField] private Image _imgSelected = default;
    [SerializeField] private Image _bgInactiveImage = default;
    [SerializeField] private Button _itemButton = default;
    [SerializeField] private LocalizeSpriteEvent _bgLocalizedImage = default;

    [Header("SO Data")]
    [SerializeField] private List<CharacterSO> _charactersSO = new List<CharacterSO>();

    public UnityAction<ItemSO> ItemSelected;

    [HideInInspector] public ItemStack currentItem;
    InventoryTabSO currentTab;

    public List<CharacterSO> CharacterSO => _charactersSO;

    bool _isSelected = false;

    private void OnEnable()
    {
        if(_isSelected)
        {
            SelectItem();
        }
    }

    public void SetItem(ItemStack itemStack, bool isSelected)
    {
        _isSelected = isSelected;

        _itemPreviewImage.gameObject.SetActive(true);
        _itemCount.gameObject.SetActive(true);
        _bgImage.gameObject.SetActive(true);
        //_itemButton.gameObject.SetActive(true);

        currentItem = itemStack;

        _imgSelected.gameObject.SetActive(currentItem.isEquip);

        _bgLocalizedImage.enabled = false;

        /*
        CharacterSO _slotData = _charactersSO.Find(o => o.CharacterType.TabType == currentItem.Item.CharacterType.TabType);
        */

        _itemPreviewImage.sprite = currentItem.Item.PreviewImage;
            
        /*
        _itemPreviewImage.sprite = itemStack.Character.PreviewImage;
        */
        _itemCount.text = itemStack.Amount.ToString();

        _bgInactiveImage.gameObject.SetActive(false);
        //_bgImage.color = itemStack.Character.CharacterType.type;
    }

    public void SetInactiveItem(InventoryTabSO _selectedTab)
    {
        currentItem = null;

        currentTab = _selectedTab;

        // 비활성화되어도 보여주는 데이터가 있다.
        CharacterSO _slotData = _charactersSO.Find(o => o.ItemType.TabType == _selectedTab);

        if(_slotData != null)
            _itemPreviewImage.sprite = _slotData.PreviewImage;

        /*
        _itemPreviewImage.gameObject.SetActive(false);
        _itemCount.gameObject.SetActive(false);
        _bgImage.gameObject.SetActive(false);
        _imgSelected.gameObject.SetActive(false);
        */

        //_itemButton.gameObject.SetActive(false);
        _bgInactiveImage.gameObject.SetActive(true);
    }

    public void EquipmentItem()
    {
        _imgSelected.gameObject.SetActive(true);
    }

    public void UnEquipmentItem()
    {
        _imgSelected.gameObject.SetActive(false);
    }

    public void UnselectItem()
    {
        _isSelected = false;
        //_imgSelected.gameObject.SetActive(false);
    }

    public void SelectFirstElement()
    {
        _isSelected = true;
        _itemButton.Select();
        SelectItem();
    }

    public void SelectItem()
    {
        _isSelected = true;

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

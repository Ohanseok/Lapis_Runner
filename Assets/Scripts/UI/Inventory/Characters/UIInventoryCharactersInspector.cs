using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;

public class UIInventoryCharactersInspector : MonoBehaviour
{
    [SerializeField] private InventorySO _currentInventory = default;
    [SerializeField] private Image _charImage;
    [SerializeField] private Image _skillImage;
    [SerializeField] private Button _EquipButton;
    [SerializeField] private Button _EnhanceButton;
    [SerializeField] private Button _PromotionButton;
    [SerializeField] private TextMeshProUGUI _pieceCountText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private List<GameObject> _stars;
    [SerializeField] private UICharactersRetainEffect _retainEffect;
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private TextMeshProUGUI _skillEffectText;

    public UnityAction OnEquip;
    public UnityAction OnPromotion;
    public UnityAction OnEnhance;

    [SerializeField] private ItemEventChannelSO _changeItemLevel;

    private void OnEnable()
    {
        _changeItemLevel.OnEventRaised += OnChangeLevel;
    }

    private void OnDisable()
    {
        _changeItemLevel.OnEventRaised -= OnChangeLevel;
    }

    private void OnChangeLevel(ItemSO item)
    {
        var data = _currentInventory.Items.Find(o => o.Item == item);
        if(data != null)
        {
            _levelText.text = data.Level.ToString();
        }
    }

    public void OnEquipButtonClick()
    {
        if(OnEquip != null)
            OnEquip.Invoke();
    }

    public void OnPromotionButtonClick()
    {
        if (OnPromotion != null)
            OnPromotion.Invoke();
    }

    public void OnOnEnhanceButtonClick()
    {
        if (OnEnhance != null)
            OnEnhance.Invoke();
    }

    private void SetUIEquipButton(bool isEquip = true)
    {
        _EquipButton.interactable = !isEquip;
    }

    private void SetUIPromotionButton(bool hasPiece = false)
    {
        _PromotionButton.interactable = hasPiece;
    }

    private void SetUIEnhanceButton(bool hasMoney = false, bool isMaxLevel = true)
    {
        if (hasMoney && !isMaxLevel)
            _EnhanceButton.interactable = true;
        else
            _EnhanceButton.interactable = false;
    }

    public void FillInspectorItem(ItemStack item)
    {
        CharacterSO charSO = (CharacterSO)item.Item;

        // Set UI
        if(_currentInventory.EquireItem(item.Item.ItemType.TabType.TabType) == charSO)
        {
            SetUIEquipButton(true);
        }
        else
        {
            SetUIEquipButton(false);
        }
        
        SetUIEnhanceButton(true, false); // 일단 돈도 있고, 최대 레벨 아닌 상태로 테스트
        SetUIPromotionButton(item.Amount >= charSO.Tier.NeedCount);
        
        if (charSO.Grade.Grade == characterGrade.One)
        {
            _stars[0].SetActive(true);
        }
        else if (charSO.Grade.Grade == characterGrade.Two)
        {
            _stars[0].SetActive(true);
            _stars[1].SetActive(true);
        }
        else if (charSO.Grade.Grade == characterGrade.Three)
        {
            _stars[0].SetActive(true);
            _stars[1].SetActive(true);
            _stars[2].SetActive(true);
        }

        // View Data
        _charImage.sprite = charSO.PreviewImage;

        _pieceCountText.text = item.Amount + "/" + charSO.Tier.NeedCount;
        _levelText.text = item.Level.ToString();
        _stars[0].SetActive(false);
        _stars[1].SetActive(false);
        _stars[2].SetActive(false);

        _retainEffect.SetValue(charSO.STATS, item.Level);

        if(charSO.SkillBook != null)
        {
            _skillImage.sprite = charSO.SkillBook.PreviewImage;

            _skillNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString(charSO.SkillBook.Name.TableReference, charSO.SkillBook.Name.TableEntryReference);

            //_skillNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString("New Table", "Berserker");

            if (charSO.SkillBook.STATS != null)
            {
                //_skillEffectText.text = charSO.SkillBook.STATS[0].RunTimeValue(item.Skill_Level);
            }
        }
    }

    public void FillInspector(ItemSO itemToInspect, int selectedItemId = -1)
    {
        CharacterSO charSO = (CharacterSO)itemToInspect;

        // Set UI
        SetUIEquipButton();
        SetUIEnhanceButton();
        SetUIPromotionButton();

        _charImage.sprite = itemToInspect.PreviewImage;

        _pieceCountText.text = "0/" + charSO.Tier.NeedCount;
        _levelText.text = "0";
        _stars[0].SetActive(false);
        _stars[1].SetActive(false);
        _stars[2].SetActive(false);

        if (charSO.Grade.Grade == characterGrade.One)
        {
            _stars[0].SetActive(true);
        }
        else if (charSO.Grade.Grade == characterGrade.Two)
        {
            _stars[0].SetActive(true);
            _stars[1].SetActive(true);
        }
        else if (charSO.Grade.Grade == characterGrade.Three)
        {
            _stars[0].SetActive(true);
            _stars[1].SetActive(true);
            _stars[2].SetActive(true);
        }

        _retainEffect.SetValue(itemToInspect.STATS, 1);

        if (charSO.SkillBook != null)
        {
            _skillImage.sprite = charSO.SkillBook.PreviewImage;

            _skillNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString(charSO.SkillBook.Name.TableReference, charSO.SkillBook.Name.TableEntryReference);

            //_skillNameText.text = LocalizationSettings.StringDatabase.GetLocalizedString("New Table", "Berserker");

            if (charSO.SkillBook.STATS != null)
            {
                _skillEffectText.text = charSO.SkillBook.STATS[0].Name + charSO.SkillBook.STATS[0].RunTimeValue(1);
            }
        }
    }
}

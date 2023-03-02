using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using System.Linq;

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
    [SerializeField] private TextMeshProUGUI _skillBookCountText;

    public UnityAction OnEquip;
    public UnityAction OnPromotion;
    public UnityAction OnEnhance;
    public UnityAction OnDetailSkill;

    [SerializeField] private ItemEventChannelSO _changeItemLevel;

    private void OnEnable()
    {
        _changeItemLevel.OnEventRaised += OnChangeLevel;
    }

    private void OnDisable()
    {
        _changeItemLevel.OnEventRaised -= OnChangeLevel;
    }

    public void OnDetailSkillButtonClick()
    {
        if (OnDetailSkill != null)
            OnDetailSkill.Invoke();
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

    private void SetStar(characterGrade grade)
    {
        foreach(var st in _stars)
        {
            if (st != null) st.SetActive(false);
        }

        switch (grade)
        {
            case characterGrade.One:
                if (_stars[(int)characterGrade.One] != null) _stars[(int)characterGrade.One].SetActive(true);
                break;

            case characterGrade.Two:
                if (_stars[(int)characterGrade.One] != null) _stars[(int)characterGrade.One].SetActive(true);
                if (_stars[(int)characterGrade.Two] != null) _stars[(int)characterGrade.Two].SetActive(true);
                break;

            case characterGrade.Three:
                if (_stars[(int)characterGrade.One] != null) _stars[(int)characterGrade.One].SetActive(true);
                if (_stars[(int)characterGrade.Two] != null) _stars[(int)characterGrade.Two].SetActive(true);
                if (_stars[(int)characterGrade.Three] != null) _stars[(int)characterGrade.Three].SetActive(true);
                break;
        }
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

        var itemStack = _currentInventory.SkillBooks.Where(o => o.Item == charSO.SkillBook).FirstOrDefault();
        if(itemStack != null)
        {
            _skillBookCountText.text =  itemStack.Amount.ToString() + "/5";
        }
        else
        {
            _skillBookCountText.text = "0/5";
        }
        

        SetStar(charSO.Grade.Grade);

        // View Data
        _charImage.sprite = charSO.PreviewImage;

        _pieceCountText.text = item.Amount + "/" + charSO.Tier.NeedCount;
        _levelText.text = item.Level.ToString();

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

        _skillBookCountText.text = "0/5";

        SetStar(charSO.Grade.Grade);

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

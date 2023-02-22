using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIInventoryCharactersInspector : MonoBehaviour
{
    [SerializeField] private Image _charImage;
    [SerializeField] private Button _EquipButton;
    [SerializeField] private Button _EnhanceButton;
    [SerializeField] private Button _PromotionButton;
    [SerializeField] private TextMeshProUGUI _pieceCountText;
    [SerializeField] private List<GameObject> _stars;
    [SerializeField] private UICharactersRetainEffect _retainEffect;

    public UnityAction OnEquip;
    public UnityAction OnPromotion;

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
        SetUIEquipButton(item.isEquip);
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

        _stars[0].SetActive(false);
        _stars[1].SetActive(false);
        _stars[2].SetActive(false);

        _retainEffect.SetValue(charSO.STATS, item.Level);
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
    }
}

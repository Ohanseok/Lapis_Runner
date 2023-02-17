using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryCharactersInspector : MonoBehaviour
{
    [SerializeField] private Image _charImage;
    [SerializeField] private Button _EupButton;
    [SerializeField] private Button _EnhanceButton;
    [SerializeField] private Button _PromotionButton;
    [SerializeField] private TextMeshProUGUI _pieceCountText;
    [SerializeField] private List<GameObject> _stars;

    public void FillInspectorItem(CharacterStack item)
    {
        _charImage.sprite = item.Character.PreviewImage;

        SetUI(true);

        _pieceCountText.text = item.Amount + "/" + item.Character.Tier.NeedCount;

        _stars[0].SetActive(false);
        _stars[1].SetActive(false);
        _stars[2].SetActive(false);

        if (item.Character.Grade.Grade == characterGrade.One)
        {
            _stars[0].SetActive(true);
        }
        else if(item.Character.Grade.Grade == characterGrade.Two)
        {
            _stars[0].SetActive(true);
            _stars[1].SetActive(true);
        }
        else if (item.Character.Grade.Grade == characterGrade.Three)
        {
            _stars[0].SetActive(true);
            _stars[1].SetActive(true);
            _stars[2].SetActive(true);
        }
    }

    public void FillInspector(CharacterSO itemToInspect, int selectedItemId = -1)
    {
        _charImage.sprite = itemToInspect.PreviewImage;

        // 활성화 되어 있다.
        SetUI(selectedItemId >= 0);

        _pieceCountText.text = "0/" + itemToInspect.Tier.NeedCount;

        _stars[0].SetActive(false);
        _stars[1].SetActive(false);
        _stars[2].SetActive(false);

        if (itemToInspect.Grade.Grade == characterGrade.One)
        {
            _stars[0].SetActive(true);
        }
        else if (itemToInspect.Grade.Grade == characterGrade.Two)
        {
            _stars[0].SetActive(true);
            _stars[1].SetActive(true);
        }
        else if (itemToInspect.Grade.Grade == characterGrade.Three)
        {
            _stars[0].SetActive(true);
            _stars[1].SetActive(true);
            _stars[2].SetActive(true);
        }
    }

    private void SetUI(bool IsSelected)
    {
        _EupButton.interactable = IsSelected;
        _EnhanceButton.interactable = IsSelected;
        _PromotionButton.interactable = IsSelected;
    }
}
